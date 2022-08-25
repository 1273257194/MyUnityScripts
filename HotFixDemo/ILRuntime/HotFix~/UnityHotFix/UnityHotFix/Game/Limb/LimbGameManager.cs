using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Slotpart.Training.Limb.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityHotFix.Game.Tool;
using UnityHotFix.Properties;
using UnityHotFix.UI.Limb;

namespace UnityHotFix.Game.Limb
{
    public class LimbGameManager : BaseSingleton<LimbGameManager>
    {
        public int speed;
        public Dictionary<string, List<Transform>> backGround = new Dictionary<string, List<Transform>>();
        private List<MoveGroup> moveGroups = new List<MoveGroup>();
        
        public override void Init()
        {
            //注册fixedUpdate事件
            LimbUnityLogic.fixedUpdate = FixedUpdate;
            WebSocketController.Instance.Init("ws://127.0.0.1:8081");
            WebSocketController.Instance.StartSever();
            WebSocketController.Instance.OnMessageEvent += s =>
            {
                //接收到消息
                if (int.TryParse(s, out speed))
                {
                    SetSpeed(speed);
                }
            };
            WebSocketController.Instance.OnErrorEvent += () => { SetSpeed(1); };
            Move2DGroupInit();
        }

        public void Move2DGroupInit()
        {
            Move2DGroup ground = new Move2DGroup();
            ground.Init(GameSceneObjController.Instance.sceneObjs["BackGround_far"].transform);
            ground.isMove = true;
            ground.percentage = 1;
            Move2DGroup ground1 = new Move2DGroup();
            ground1.Init(GameSceneObjController.Instance.sceneObjs["BackGround_middle"].transform);
            ground1.isMove = true;
            ground1.percentage = 0.7f;
            Move2DGroup ground2 = new Move2DGroup();
            ground2.Init(GameSceneObjController.Instance.sceneObjs["BackGround_nearly"].transform);
            ground2.isMove = true;
            ground2.percentage = 0.2f;
            moveGroups.Add(ground);
            moveGroups.Add(ground1);
            moveGroups.Add(ground2);
        }

        public async void Move3DGroupInit()
        {
            Move3DGroup ground = new Move3DGroup();
            ground.Init(GameSceneObjController.Instance.sceneObjs["BackGround_far"].transform);
            var obj = await Addressables.LoadAssetAsync<GameObject>("BackGround").ToUniTask();
            ground.Created3DBackGround(obj);
            ground.isMove = true;
            ground.percentage = 1;
            moveGroups.Add(ground);
        }

        private void SetSpeed(int value)
        {
            var tempValue = value;
            if (value < 0)
            {
                Debug.Log("speed值小于0");
            } 
            speed = tempValue;
            for (int i = 0; i < moveGroups.Count; i++)
            {
                moveGroups[i].SetValue(speed);
            }
        }


        public void Update()
        {
            Debug.Log("Update !! GameManager!!");
        }

        public void FixedUpdate()
        {
            Debug.Log("FixedUpdate !! GameManager!!");
        }
    }
}