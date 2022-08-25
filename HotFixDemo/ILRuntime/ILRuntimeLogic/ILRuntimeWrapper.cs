using System; 
using System.IO;
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.ILRuntimeLogic;
using UnityEngine;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntimeTest.TestFramework;
using Slotpart.Tools;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

public class ILRuntimeWrapper : MonoSingleton<ILRuntimeWrapper>
{
    public Action<string, object> TestActionDelegate;

    public AppDomain appdomain;

    // public string bindClass;
    private IType classType;
    private ILTypeInstance instance;
    public IMethod updateMethod, fixedUpdateMethod, lateUpdateMethod, awakeMethod, startMethod, onDestroyMethod;

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
        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
    }

    private void FixedUpdate()
    {
        if (fixedUpdateMethod != null) appdomain.Invoke(fixedUpdateMethod, instance);
    }

    private void Update()
    {
        if (!IsGameStart) return;
        if (updateMethod != null) appdomain.Invoke(updateMethod, instance);
    }

    private void LateUpdate()
    {
        if (!IsGameStart) return;
        if (lateUpdateMethod != null) appdomain.Invoke(lateUpdateMethod, instance);
    }

    private void OnDestroy()
    {
        if (!IsGameStart) return;
        if (onDestroyMethod != null) appdomain.Invoke(onDestroyMethod, instance);
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
            appdomain.LoadAssembly(m_fs, m_p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }
        catch
        {
            Debug.LogError("加载热更DLL失败，请确保已经编译过热更DLL");
            return;
        }

        appdomain.DebugService.StartDebugService(56000);
        InitializeILRuntime();
    }

    private void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册，HelloWorld示例暂时没有需要注册的
        //Action<string> 的参数为一个string
        Debug.Log("主工程里注册委托");
        appdomain.DelegateManager.RegisterMethodDelegate<string, object>();

        //unityAction的委托转换器
        appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
        {
            return new UnityEngine.Events.UnityAction(() => { ((Action) act).Invoke(); });
        });
          LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);
        appdomain.DelegateManager.RegisterMethodDelegate<string>(); 
        appdomain.DelegateManager.RegisterMethodDelegate<float>();
        appdomain.DelegateManager.RegisterMethodDelegate<BestHTTP.WebSocket.WebSocket>();
        appdomain.DelegateManager.RegisterMethodDelegate<AsyncOperationHandle<GameObject>>();
        appdomain.DelegateManager.RegisterFunctionDelegate<bool>();
        appdomain.DelegateManager.RegisterMethodDelegate<BestHTTP.WebSocket.WebSocket, System.String>();
        appdomain.RegisterCrossBindingAdaptor(new IAsyncStateMachineClassInheritanceAdaptor());
        appdomain.DelegateManager.RegisterFunctionDelegate<System.Collections.Generic.KeyValuePair<System.String, ILRuntime.Runtime.Intepreter.ILTypeInstance>, System.Boolean>();
        appdomain.DelegateManager
            .RegisterFunctionDelegate<System.Collections.Generic.KeyValuePair<System.String, ILRuntime.Runtime.Intepreter.ILTypeInstance>,
                ILRuntime.Runtime.Intepreter.ILTypeInstance>();
        appdomain.DelegateManager.RegisterFunctionDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Boolean>();
        appdomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>>((act) =>
        {
            return new System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>((obj) =>
            {
                return ((Func<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Boolean>) act)(obj);
            });
        });  
        appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.WebSocket.OnWebSocketMessageDelegate>((act) =>
        {
            return new BestHTTP.WebSocket.OnWebSocketMessageDelegate((webSocket, message) =>
            {
                ((Action<BestHTTP.WebSocket.WebSocket, System.String>)act)(webSocket, message);
            });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.WebSocket.OnWebSocketErrorDelegate>((act) =>
        {
            return new BestHTTP.WebSocket.OnWebSocketErrorDelegate((webSocket, reason) =>
            {
                ((Action<BestHTTP.WebSocket.WebSocket, System.String>)act)(webSocket, reason);
            });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.WebSocket.OnWebSocketClosedDelegate>((act) =>
        {
            return new BestHTTP.WebSocket.OnWebSocketClosedDelegate((webSocket, code, message) =>
            {
                ((Action<BestHTTP.WebSocket.WebSocket, System.UInt16, System.String>)act)(webSocket, code, message);
            });
        });

        appdomain.DelegateManager.RegisterMethodDelegate<BestHTTP.WebSocket.WebSocket, System.UInt16, System.String>();

        
        appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.WebSocket.OnWebSocketOpenDelegate>((act) =>
        {
            return new BestHTTP.WebSocket.OnWebSocketOpenDelegate((webSocket) =>
            {
                ((Action<BestHTTP.WebSocket.WebSocket>)act)(webSocket);
            });
        }); 
        appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.TweenCallback>((act) =>
        {
            return new DG.Tweening.TweenCallback(() =>
            {
                ((Action)act)();
            });
        }); 
         ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);
    }

    /// <summary>
    /// 进入游戏
    /// </summary>
    public void EnterGame()
    {
        //HelloWorld，第一次方法调用
        //appDomain.Invoke("HotFix_Project.InstanceClass", "StaticFunTest", null, null);
      
        IsGameStart = true;
        //开始调用热更工程
        //InitHotFixMethod();
        FindObjectOfType<HotFixBase>().InitHotFixMethod();
        //开始执行热更工程
        //appDomain.Invoke("HotFix_Project.MainBehaviour","Awake",null,null);
    }

    // public void InitHotFixMethod()
    // {
    //     if (string.IsNullOrEmpty(bindClass))
    //     {
    //         bindClass = "UnityHotFix.Properties.UnityLogic";
    //     }
    //
    //     Debug.Log(bindClass);
    //     if (IsGameStart)
    //     {
    //         classType = appDomain.LoadedTypes[bindClass];
    //         instance = (classType as ILType)?.Instantiate();
    //
    //         awakeMethod = classType.GetMethod("Awake", 0);
    //         startMethod = classType.GetMethod("Start", 0);
    //         updateMethod = classType.GetMethod("Update", 0);
    //         onDestroyMethod = classType.GetMethod("OnDestroy", 0);
    //         fixedUpdateMethod = classType.GetMethod("FixedUpdate", 0);
    //         lateUpdateMethod = classType.GetMethod("LateUpdate", 0);
    //
    //         if (awakeMethod != null)
    //         {
    //             appDomain.Invoke(awakeMethod, instance);
    //         }
    //     }
    //
    //     //开始调用热更工程的start
    //     appDomain.Invoke(startMethod, instance);
    // }
}