using System;
using System.IO;
using UnityEngine;
using Cysharp.Threading.Tasks;
using ILRuntimeTest.TestFramework;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations; 
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

public class ILRuntimeInit : MonoBehaviour
{
    public AppDomain appdomain;


    System.IO.MemoryStream fs;
    System.IO.MemoryStream p;

    async void OnEnable()
    {
        appdomain = new AppDomain();
        appdomain.RegisterCrossBindingAdaptor(new IAsyncStateMachineClassInheritanceAdaptor());
        appdomain.DelegateManager.RegisterMethodDelegate<AsyncOperationHandle<GameObject>>();
        appdomain.DelegateManager.RegisterFunctionDelegate<int, string>();
        appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((action) =>
        {
            return new UnityEngine.Events.UnityAction(() => { ((Action) action).Invoke(); });
        });
        // UnityWebRequest webRequest = UnityWebRequest.Get(Application.streamingAssetsPath + "/UnityHotFix.dll");
        // UnityWebRequest webRequestPdb = UnityWebRequest.Get(Application.streamingAssetsPath + "/UnityHotFix.pdb");

        var dllRes=await Addressables.LoadAssetAsync<TextAsset>("UnityHotFix_dll_res").ToUniTask();
        var pdbRes=await Addressables.LoadAssetAsync<TextAsset>("UnityHotFix_pdb_res").ToUniTask();
        // UnityWebRequest webRequest = UnityWebRequest.Get(Application.dataPath + "/Model/UnityHotFix_dll_res.bytes");
        // UnityWebRequest webRequestPdb = UnityWebRequest.Get(Application.dataPath + "/Model/UnityHotFix_pdb_res.bytes");
        //
        // await webRequest.SendWebRequest();
        // await webRequestPdb.SendWebRequest();
        // Debug.Log(1);
        // if (webRequest.result == UnityWebRequest.Result.Success)
        // {
        //     byte[] dll = webRequest.downloadHandler.data;
        //
        //     fs = new MemoryStream(dll);
        //     Debug.Log(2);
        // }
        //
        // if (webRequestPdb.result == UnityWebRequest.Result.Success)
        // {
        //     byte[] pdb = webRequestPdb.downloadHandler.data;
        //
        //     p = new MemoryStream(pdb);
        //     Debug.Log(3);
        // }
        fs = new MemoryStream(dllRes.bytes); 
        p = new MemoryStream(pdbRes.bytes);
        try
        {
            appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            Debug.Log(4);
        }
        catch
        {
            Debug.LogError("加载热更DLL失败，请确保已经通过VS打开Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln编译过热更DLL");
        }

        //appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;

        appdomain.Invoke("UnityHotFix.Properties.InstanceClass", "StaticFunTest", null, null);
        appdomain.Invoke("UnityHotFix.Class1", "StaticFunTest", null, null);
        appdomain.Invoke("UnityHotFix.Properties.InstanceClass", "Start", null, null);
        Debug.Log(5);
    }

    private void OnDestroy()
    {
        if (fs != null)
            fs.Close();
        if (p != null)
            p.Close();
        fs = null;
        p = null;
    }

    void Update()
    {
    }
}