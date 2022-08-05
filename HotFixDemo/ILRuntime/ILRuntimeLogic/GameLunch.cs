/*----------------游戏启动入口脚本-------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Slotpart.Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

/// <summary>
/// 加载方式
/// </summary>
public enum LoadingMode
{
    ByLocalDll,
    ByLocalAddressable
}

public class GameLunch : MonoSingleton<GameLunch>
{
    //public Transform father;
    [Tooltip("dll文件的加载方式")] public LoadingMode loadingMode = LoadingMode.ByLocalAddressable;
    public string bindClass;
    public void Awake()
    {
        base.Awake();
        Caching.ClearCache();
        //初始化游戏框架
        //资源管理
        gameObject.AddComponent<ResMgr>();
        gameObject.AddComponent<ILRuntimeWrapper>();
        //gameObject.AddComponent<MediaPlayer>();
        StartHotFix();
        //LoadAddressables();
    }

    /// <summary>
    /// 测试加载AA
    /// </summary>
    /// <returns></returns>
    public async Task LoadAddressables()
    {
        //GameObject target= await Addressables.LoadAssetAsync<GameObject>("Canvas").Task;
        // var target = await ResMgr.Instance.GetAssetCache<GameObject>("Canvas");
        // GameObject.Instantiate(target);
        Caching.ClearCache();
        // var canvas= Addressables.InstantiateAsync("Canvas");
        // await canvas.Task;
        // await Addressables.InstantiateAsync("MainUIPanel",canvas.Result.transform).Task;
    }

    /// <summary>
    /// 功能： 加载热更文件dll
    /// <code>Addressables系统加载资源 -> 热更新代码 -> 代码逻辑</code>     
    /// </summary>
    /// <returns></returns>
    public async UniTaskVoid StartHotFix()
    {
        //去服务器上下载最新的aa包资源 
        PlayerPrefs.DeleteKey(Addressables.kAddressablesRuntimeDataPath);
        Addressables.InitializeAsync(); 
        //下载热更代码
        //string m_url=null;
        byte[] dll = { };
        byte[] pdb = { };
        if (loadingMode == LoadingMode.ByLocalDll)
        {
            //StartCoroutine(CheckHotUpdate(dll,pdb));
        }
        else if (loadingMode == LoadingMode.ByLocalAddressable)
        {
            TextAsset assetDll = await Addressables.LoadAssetAsync<TextAsset>("UnityHotFix_dll_res").ToUniTask();
            dll = assetDll.bytes;
            TextAsset assetPdb = await Addressables.LoadAssetAsync<TextAsset>("UnityHotFix_pdb_res").ToUniTask();
            pdb = assetPdb.bytes;
            ILRuntimeWrapper.Instance.LoadHotFixAssembly(dll, pdb);
            if (bindClass != null) ILRuntimeWrapper.Instance.bindClass = bindClass;
            ILRuntimeWrapper.Instance.EnterGame();
        }
        // ILRuntimeWrapper.Instance.LoadHotFixAssembly(dll,pdb);
        // ILRuntimeWrapper.Instance.EnterGame();
    }
}