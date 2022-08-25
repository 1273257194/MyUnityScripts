using System;
using BestHTTP.WebSocket;
using Cysharp.Threading.Tasks;
using Slotpart.Training.Limb.Scripts;
using UnityEngine;
using UnityHotFix.Game.Limb;
using UnityHotFix.Game.Tool;

namespace UnityHotFix.Game.HandTraining
{
    public class HandObj
    {
        public Transform transform;
        public GameObject gameObj;
        public Animator animator;

        public HandObj()
        {
        }

        public HandObj(GameObject obj)
        {
            gameObj = obj;
            transform = obj.transform;
            animator = obj.GetComponent<Animator>();
        }
    }

    public class HandTrainingGameManager : Properties.BaseSingleton<HandTrainingGameManager>
    {
        private HandObj handObj;
        private WebSocket webSocket;
        public bool isPlaying;
        private bool isPause;
 

        public void Start()
        {handObj = new HandObj(GameSceneObjController.Instance.sceneObjs["HandObj"]);
            Debug.Log("Init1");
            webSocket = StartSocket();
        }

    private WebSocket StartSocket()
        {
            Debug.Log("InitSocket");
            Debug.Log(UpLoadGameInfo.Instance.DeviceUrl);
            WebSocketController.Instance.isReconnection = true;
            WebSocketController.Instance.Init(UpLoadGameInfo.Instance.DeviceUrl);
            WebSocketController.Instance.StartSever();
            WebSocketController.Instance.OnMessageEvent += GetInfo;
            return WebSocketController.Instance.WebSocket;
        }

        /// <summary>
        /// 得到服务器返回的数据
        /// </summary>
        public void GetInfo(string info)
        {
            Debug.Log(info);
        }

        /// <summary>
        /// 控制模型动画
        /// </summary>
        public void ModelControl()
        {
        }

        public async void GameLogic()
        {
            while (isPlaying)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                if (isPause)
                {
                    await UniTask.NextFrame();
                    continue;
                }
            }
        }

        public void DownTime()
        {
            DownTimeController.Instance.gameTime = UpLoadGameInfo.gameStartInfo.gameTime;
            DownTimeController.Instance.Start();
            DownTimeController.Instance.endDownTimeEvent += () =>
            {
                //放置第二次打开结束页面
                if (isPlaying)
                {
                    GameOver();
                }
            };
        }

        public void UpLoadData()
        {
        }

        public void StartGame()
        {
            isPause = false;
            isPlaying = true;
            GameLogic();
            DownTime();
        }

        public void GameOver()
        {
            isPlaying = false;
        }
    }
}