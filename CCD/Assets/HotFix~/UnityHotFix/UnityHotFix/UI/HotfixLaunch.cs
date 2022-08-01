using System.Collections.Generic;
using UnityEngine;
using UnityHotFix;
using UnityHotFix.Tool;
using UnityHotFix.UI;

namespace Hotfix
{
    public class HotfixLaunch
    {
        public static void Start(bool isHotfix)
        {
            Debug.Log("加载管理器-");
            UIPanelManager.instance.Init();
            UIViewManager.instance.Init();
            SceneLoadManager.Instance.Init();
            UIPanelManager.instance.ShowPanel<LoginPanel>((x) => { Debug.Log($"HotfixLaunch:{x.transform.name}"); }); 
        }
    }
}