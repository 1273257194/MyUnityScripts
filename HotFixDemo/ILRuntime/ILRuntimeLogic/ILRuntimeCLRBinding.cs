#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;
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

    static void InitILRuntime(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
    {
        // //这里需要注册所有热更DLL中用到的跨域继承Adapter，否则无法正确抓取引用
         appdomain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
        //appdomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        //  appdomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
        //  appdomain.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());
        //  appdomain.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
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
                ((Action<BestHTTP.WebSocket.WebSocket, System.String>) act)(webSocket, message);
            });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.WebSocket.OnWebSocketErrorDelegate>((act) =>
        {
            return new BestHTTP.WebSocket.OnWebSocketErrorDelegate((webSocket, reason) => { ((Action<BestHTTP.WebSocket.WebSocket, System.String>) act)(webSocket, reason); });
        });
        appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.WebSocket.OnWebSocketClosedDelegate>((act) =>
        {
            return new BestHTTP.WebSocket.OnWebSocketClosedDelegate((webSocket, code, message) =>
            {
                ((Action<BestHTTP.WebSocket.WebSocket, System.UInt16, System.String>) act)(webSocket, code, message);
            });
        });

        appdomain.DelegateManager.RegisterMethodDelegate<BestHTTP.WebSocket.WebSocket, System.UInt16, System.String>();


        appdomain.DelegateManager.RegisterDelegateConvertor<BestHTTP.WebSocket.OnWebSocketOpenDelegate>((act) =>
        {
            return new BestHTTP.WebSocket.OnWebSocketOpenDelegate((webSocket) => { ((Action<BestHTTP.WebSocket.WebSocket>) act)(webSocket); });
        });
    }
}
#endif