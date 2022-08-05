using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using UnityEngine;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntimeTest.TestFramework;
using Slotpart.Tools;
using UnityEngine.ResourceManagement.AsyncOperations;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

public class ILRuntimeWrapper : MonoSingleton<ILRuntimeWrapper>
{
    public Action<string, object> TestActionDelegate;

    public AppDomain appDomain;

    public string bindClass;
    private IType classType;
    private ILTypeInstance instance;
    private IMethod updateMethod, fixedUpdateMethod, lateUpdateMethod, awakeMethod, startMethod, onDestroyMethod;

    private System.IO.MemoryStream m_fs, m_p;
    private bool m_isGameStart, m_startUpdate;

    public bool IsGameStart
    {
        get => m_isGameStart;
        set => m_isGameStart = value;
    }

    public void Awake()
    {
        base.Awake();
        IsGameStart = false;
        m_startUpdate = false;
        appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();
    }

    private void FixedUpdate()
    {
        if (fixedUpdateMethod != null) appDomain.Invoke(fixedUpdateMethod, instance);
    }

    private void Update()
    {
        if (!IsGameStart) return;
        if (updateMethod != null) appDomain.Invoke(updateMethod, instance);
    }

    private void LateUpdate()
    {
        if (!IsGameStart) return;
        if (lateUpdateMethod != null) appDomain.Invoke(lateUpdateMethod, instance);
    }

    private void OnDestroy()
    {
        if (!IsGameStart) return;
        if (onDestroyMethod != null) appDomain.Invoke(onDestroyMethod, instance);
    }

    /// <summary>
    /// 加载dll，pdb
    /// </summary>
    /// <param name="dll"></param>
    /// <param name="pdb"></param>
    public void LoadHotFixAssembly(byte[] dll, byte[] pdb)
    {
        m_fs = new MemoryStream(dll);
        m_p = new MemoryStream(pdb);
        try
        {
            appDomain.LoadAssembly(m_fs, m_p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }
        catch
        {
            Debug.LogError("加载热更DLL失败，请确保已经编译过热更DLL");
            return;
        }

        appDomain.DebugService.StartDebugService(56000);
        InitializeILRuntime();
    }

    private void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册，HelloWorld示例暂时没有需要注册的
        //Action<string> 的参数为一个string
        Debug.Log("主工程里注册委托");
        appDomain.DelegateManager.RegisterMethodDelegate<string, object>();

        //unityAction的委托转换器
        appDomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
        {
            return new UnityEngine.Events.UnityAction(() => { ((Action) act).Invoke(); });
        });
    }

    /// <summary>
    /// 进入游戏
    /// </summary>
    public void EnterGame()
    {
        //HelloWorld，第一次方法调用
        //appDomain.Invoke("HotFix_Project.InstanceClass", "StaticFunTest", null, null);
        appDomain.DelegateManager.RegisterMethodDelegate<string>();
        appDomain.DelegateManager.RegisterMethodDelegate<float>();
        appDomain.DelegateManager.RegisterMethodDelegate<AsyncOperationHandle<GameObject>>();
        appDomain.DelegateManager.RegisterFunctionDelegate<bool>();
        appDomain.DelegateManager.RegisterFunctionDelegate<System.Collections.Generic.KeyValuePair<System.String, ILRuntime.Runtime.Intepreter.ILTypeInstance>, System.Boolean>();
        appDomain.DelegateManager
            .RegisterFunctionDelegate<System.Collections.Generic.KeyValuePair<System.String, ILRuntime.Runtime.Intepreter.ILTypeInstance>,
                ILRuntime.Runtime.Intepreter.ILTypeInstance>();
        appDomain.DelegateManager.RegisterFunctionDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Boolean>();
        appDomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>>((act) =>
        {
            return new System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>((obj) =>
            {
                return ((Func<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Boolean>) act)(obj);
            });
        });
        appDomain.RegisterCrossBindingAdaptor(new IAsyncStateMachineClassInheritanceAdaptor());
        ILRuntime.Runtime.Generated.CLRBindings.Initialize(appDomain);
        IsGameStart = true;
        //开始调用热更工程
        InitHotFixMethod();

        //开始执行热更工程
        //appDomain.Invoke("HotFix_Project.MainBehaviour","Awake",null,null);
    }

    public void InitHotFixMethod()
    {
        if (string.IsNullOrEmpty(bindClass))
        {
            bindClass = "UnityHotFix.Properties.UnityLogic";
        }

        Debug.Log(bindClass);
        if (IsGameStart)
        {
            classType = appDomain.LoadedTypes[bindClass];
            instance = (classType as ILType)?.Instantiate();

            awakeMethod = classType.GetMethod("Awake", 0);
            startMethod = classType.GetMethod("Start", 0);
            updateMethod = classType.GetMethod("Update", 0);
            onDestroyMethod = classType.GetMethod("OnDestroy", 0);
            fixedUpdateMethod = classType.GetMethod("FixedUpdate", 0);
            lateUpdateMethod = classType.GetMethod("LateUpdate", 0);

            if (awakeMethod != null)
            {
                appDomain.Invoke(awakeMethod, instance);
            }
        }

        //开始调用热更工程的start
        appDomain.Invoke(startMethod, instance);
    }
}