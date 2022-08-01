using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using LitJson;
using Slotpart.Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public class panelInfo
{
    public string panelName;

    public panelInfo()
    {
    }

    public panelInfo(string name)
    {
        panelName = name;
    }
}

public class ResMgr : MonoSingleton<ResMgr>
{
    [SerializeField] public Transform parents;
    public List<string> loadPool;
    public Dictionary<string, IType> panels = new Dictionary<string, IType>();
    private string m_names;

    public void Awake()
    {
        base.Awake();
        loadPool = new List<string>();
        parents = GameObject.Find("Canvas").transform;
        InstantiateOfAddressables();
    }

    /// <summary>
    /// 获取组件
    /// </summary>
    /// <param name="names">名字</param>
    /// <typeparam name="T">组件</typeparam>
    /// <returns></returns>
    public T GetTarget<T>(string names) where T : UnityEngine.Object
    {
        var target = GameObject.Find(names).GetComponent<T>();
        return target;
    }

 

    /// <summary>
    /// 通过addressable实例化ui
    /// </summary>
    /// <param name="names">ui预制体的名字</param>
    public async void InstantiateOfAddressables()
    {
        //var target = Addressables.InstantiateAsync(names,parents);
        //await target.Task;
        //loadPool.Add(names);
        //m_names = names;
        //ILRuntimeWrapper.Instance.IsGameStart = false;
        // Addressables.InstantiateAsync(names, parents).Completed += (handle) => OnCompleted(handle, names);
        //ILRuntimeWrapper.Instance.finishLoadAction("主工程里面的委托@@@@");

        // var textAsset = await Addressables.LoadAssetAsync<TextAsset>("PanelName").ToUniTask();
        // var obj = JsonMapper.ToObject<List<panelInfo>>(textAsset.text);
        // obj.ForEach(x=> loadPool.Add(x.panelName));
        // for (int i = 0; i < loadPool.Count; i++)
        // {
        //     var type = ILRuntimeWrapper.Instance.appDomain.GetType("UnityHotFix.Properties."+loadPool[i]);
        //     if (panels.ContainsKey(loadPool[i]))
        //     {
        //         panels[loadPool[i]] = type;
        //     }
        //     else
        //     {
        //         panels.Add(loadPool[i], type);
        //     } 
        //     var instance = (type as ILType)?.Instantiate();
        //     var initMethod = type.GetMethod("OnInit", 0); 
        //     ILRuntimeWrapper.Instance.appDomain.Invoke(initMethod, instance );
        // }

        var uiPanelNameStr = await Addressables.LoadAssetAsync<TextAsset>("PanelName").ToUniTask();
        var obj = JsonMapper.ToObject<List<panelInfo>>(uiPanelNameStr.text);
        loadPool = obj.Select(x => x.panelName).ToList();
    }

    private void OnCompleted(AsyncOperationHandle<GameObject> obj, string names)
    {
        Debug.Log($"{names}加载完了");
        obj.Result.name = names;
        //loadPool.Remove(names);
        //ILRuntimeWrapper.Instance.IsGameStart = true;
        //ILRuntimeWrapper.Instance.appDomain.Invoke("HotFix_Project.Main", "FixedUpdate", null, obj.Result.name);
        ILRuntimeWrapper.Instance.TestActionDelegate(names, obj.Result);
    }

    private void OnCompleted<T>(AsyncOperationHandle<IList<T>> obj, string names) where T : UnityEngine.Object
    {
        Debug.Log($"{names}加载完了");
        //obj.Result[0].name = names;
        //loadPool.Remove(names);
        //ILRuntimeWrapper.Instance.IsGameStart = true;
        //ILRuntimeWrapper.Instance.appDomain.Invoke("HotFix_Project.Main", "FixedUpdate", null, obj.Result.name);
        ILRuntimeWrapper.Instance.TestActionDelegate(names, obj.Result[0]);
    }

    /// <summary>
    /// 加载资源到内存
    /// </summary>
    /// <param name="names"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async void LoadByAddressable<T>(string names) where T : UnityEngine.Object
    {
        //var target = await Addressables.LoadAssetsAsync<T>(names, null, Addressables.MergeMode.None).Task;
        Addressables.LoadAssetsAsync<T>(names, null).Completed += (handle) => OnCompleted(handle, names);
    }
}