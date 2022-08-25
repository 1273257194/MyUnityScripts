using System;
using UnityEngine;
using UnityHotFix.Game.PaperAirplane;
using UnityHotFix.UI;

namespace UnityHotFix.Game.HandTraining
{
    public class HandTrainingUnityLogic
    {
        public static Action awake = null;
        public static Action start = null;
        public static Action update = null;
        public static Action onDestroy = null;
        public static Action fixedUpdate = null;
        public static Action lateUpdate = null;


        public static void Awake()
        {
            Debug.Log($"{typeof(HandTrainingUnityLogic)}:Awake!!");
            awake?.Invoke();
            Application.targetFrameRate = 30;
        }

        public static void Start()
        {
            Debug.Log($"{typeof(HandTrainingUnityLogic)}:Start!!");
            start?.Invoke();
            //初始化控制器
            UIPanelManager.instance.Init();
            UIViewManager.instance.Init();
             HandTrainingGameManager.Instance.Start();
        }

        public static void Update()
        {
            // Debug.Log($"{typeof(PaperAirplaneUnityLogic)}:Update!!");
            update?.Invoke();
            UIViewManager.instance.Update();
        }

        public static void OnDestroy()
        {
            Debug.Log($"{typeof(HandTrainingUnityLogic)}:OnDestroy!!");
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
            // Debug.Log($"{typeof(PaperAirplaneUnityLogic)}:FixedUpdate!!");
            fixedUpdate?.Invoke();
            UIViewManager.instance.FixedUpdate();
        }

        public static void LateUpdate()
        {
            // Debug.Log($"{typeof(PaperAirplaneUnityLogic)}:LateUpdate!!");
            lateUpdate?.Invoke();
            UIViewManager.instance.LateUpdate();
        }
    }
}