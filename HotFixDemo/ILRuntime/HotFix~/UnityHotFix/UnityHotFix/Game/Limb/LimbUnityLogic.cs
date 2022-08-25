using System;
using UnityEngine;
using UnityHotFix.UI;
using UnityHotFix.UI.Limb;

namespace UnityHotFix.Game.Limb
{
    /// <summary>
    /// 上下肢游戏热更入口 Mono生命周期事件
    /// </summary>
    public class LimbUnityLogic
    {
        public static Action awake = null;
        public static Action start = null;
        public static Action update = null;
        public static Action onDestroy = null;
        public static Action fixedUpdate = null;
        public static Action lateUpdate = null;


        public static void Awake()
        {
            Debug.Log("Net:Awake!!");
            awake?.Invoke();
        }

        public static void Start()
        {
            Debug.Log("Net:Start!!");
            start?.Invoke();
            //初始化控制器
            UIPanelManager.instance.Init();
            UIViewManager.instance.Init();
            LimbGameManager.Instance.Init(); 
            //打开第一个面板
            UIPanelManager.instance.ShowPanel<LimbLoginPanel>((x) => { Debug.Log($"HotfixLaunch:{x.transform.name}"); });
        }

        public static void Update()
        {
            // Debug.Log("Net:Update!!");
            update?.Invoke();
            UIViewManager.instance.Update(); 
        }

        public static void OnDestroy()
        {
            Debug.Log("Net:OnDestroy!!");
            onDestroy?.Invoke();
            awake = null;
            start = null;
            update = null;
            onDestroy = null;
            fixedUpdate = null;
            lateUpdate = null;
        }

        public static void FixedUpdate()
        {
            // Debug.Log("Net:FixedUpdate!!");
            fixedUpdate?.Invoke();
            UIViewManager.instance.FixedUpdate();
        }

        public static void LateUpdate()
        {
            // Debug.Log("Net:LateUpdate!!");
            lateUpdate?.Invoke();
            UIViewManager.instance.LateUpdate();
        }
    }
}