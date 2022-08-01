using System;
using System.Collections.Generic;
using Hotfix;
using UnityEngine;
using UnityHotFix.UI;

namespace UnityHotFix.Properties
{
    public class UnityLogic
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
            HotfixLaunch.Start(true);
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