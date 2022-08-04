#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;
using ILRuntimeDemo;
using ILRuntimeTest.TestFramework;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Reflection.Obfuscation(Exclude = true)]
public class ILRuntimeCLRBinding
{
    [MenuItem("MyMenu/ILRuntime/通过自动分析热更DLL生成CLR绑定")]
    static void GenerateCLRBindingByAnalysis()
    {
        //用新的分析热更dll调用引用来生成绑定代码
        ILRuntime.Runtime.Enviorment.AppDomain domain = new ILRuntime.Runtime.Enviorment.AppDomain();
        using (System.IO.FileStream fs = new System.IO.FileStream("Assets/Model/UnityHotFix_dll_res.bytes", System.IO.FileMode.Open, System.IO.FileAccess.Read))
        {
            domain.LoadAssembly(fs);

            //Crossbind Adapter is needed to generate the correct binding code
            InitILRuntime(domain);
            ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(domain, "Assets/Model/ILRuntime/Generated");
        }

        AssetDatabase.Refresh();
    }

    static void InitILRuntime(ILRuntime.Runtime.Enviorment.AppDomain domain)
    {
        //这里需要注册所有热更DLL中用到的跨域继承Adapter，否则无法正确抓取引用
        domain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
        domain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        domain.RegisterCrossBindingAdaptor(new TestClassBaseAdapter());
        domain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
        domain.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());
        domain.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
        
        domain.DelegateManager.RegisterMethodDelegate<string>();
        domain.DelegateManager.RegisterMethodDelegate<float>();
        domain.DelegateManager.RegisterFunctionDelegate<bool>();
        domain.DelegateManager.RegisterFunctionDelegate<System.Collections.Generic.KeyValuePair<System.String, ILRuntime.Runtime.Intepreter.ILTypeInstance>, System.Boolean>();

        domain.DelegateManager.RegisterFunctionDelegate<System.Collections.Generic.KeyValuePair<System.String, ILRuntime.Runtime.Intepreter.ILTypeInstance>, ILRuntime.Runtime.Intepreter.ILTypeInstance>();

       domain.DelegateManager.RegisterFunctionDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Boolean>();
       domain.DelegateManager.RegisterDelegateConvertor<System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>>((act) =>
        {
            return new System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>((obj) =>
            {
                return ((Func<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Boolean>)act)(obj);
            });
        });


        domain.DelegateManager.RegisterMethodDelegate<AsyncOperationHandle<GameObject>>();
        domain.RegisterCrossBindingAdaptor(new IAsyncStateMachineClassInheritanceAdaptor()); 
    }
}
#endif