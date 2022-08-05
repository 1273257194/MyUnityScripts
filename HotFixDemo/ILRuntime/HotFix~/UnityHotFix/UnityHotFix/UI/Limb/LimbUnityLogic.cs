﻿using System;
using Hotfix;
using UnityEngine;

namespace UnityHotFix.UI.Limb
{
    public class LimbUnityLogic
    {
        static Action awake = null;
        static Action start = null;
        static Action update = null;
        static Action onDestroy = null;
        static Action fixedUpdate = null;
        static Action lateUpdate = null;


        public static void Awake()
        {
            Debug.Log("Net:Awake!!");
            awake?.Invoke();
        }

        public static void Start()
        {
            Debug.Log("Net:Start!!");
            start?.Invoke(); 
            UIPanelManager.instance.Init();
            UIViewManager.instance.Init(); 
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