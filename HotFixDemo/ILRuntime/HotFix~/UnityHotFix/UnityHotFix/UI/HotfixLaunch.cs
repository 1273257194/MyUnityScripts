using System.Collections.Generic;
using UnityEngine;
using UnityHotFix;
using UnityHotFix.Tool;
using UnityHotFix.UI;
using UnityHotFix.UI.Limb;

namespace Hotfix
{
    public class HotfixLaunch
    {
        public static void Start(bool isHotfix)
        {
            Debug.Log("加载管理器-");
            UIPanelManager.instance.Init();
            UIViewManager.instance.Init(); 
            UIPanelManager.instance.ShowPanel<LimbLoginPanel>((x) => { Debug.Log($"HotfixLaunch:{x.transform.name}"); }); 
        }
    }
     
    
    
    
}