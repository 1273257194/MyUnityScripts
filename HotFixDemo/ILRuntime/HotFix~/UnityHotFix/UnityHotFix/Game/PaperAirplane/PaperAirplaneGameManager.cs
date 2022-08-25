using DG.Tweening;
using System;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using LitJson;
using Slotpart.Training.Limb.Scripts;
using UnityEngine;
using UnityHotFix.Game.Limb;
using UnityHotFix.Game.Tool;
using UnityHotFix.Properties;
using UnityHotFix.UI;
using UnityHotFix.UI.PaperAirplane;
using Slotpart.Training.PaperAirplane.Scripts;
using Object = UnityEngine.Object;

namespace UnityHotFix.Game.PaperAirplane
{
    public class PaperAirplaneGameManager : BaseSingleton<PaperAirplaneGameManager>
    {
        private Transform fly;
        private Transform flyObj;
        private GameObject sceneObj;
        private string DeviceInfoUrl;
        private DeviceInfo deviceInfo;

        private bool IsPlaying { get; set; }
        private bool IsGameLead { get; set; }

        public int gameTime;

        private float currentHighly;

        //呼气时间
        public float hTiming;

        //吸气时间
        public float xTiming;

        // private float gameSelectXTime = 3;
        // private float gameSelectHTime = 3;

        private Camera mainCamera;

        public int xBreathing;
        public int hBreathing;

        public float breathingRate;
        private float breathingTime;

        private const float MHigh = 0;
        private const float MLow = -1f;
        public static int collisionCount;

        private Animator flyAni;

        private CinemachinePathBase mPath;
        private Transform holeObj;
        public static bool isHoleCollision = true;
        private CinemachineDollyCart flyPath;
        private WhiteView whiteView;
        private DateTime startTime;
        private DateTime endTime;

        private BreathingLogic breathingLogic = new BreathingLogic();
        private ParticleSystemsController particleSystemsController = new ParticleSystemsController();

        public override void Init()
        {
            Application.targetFrameRate = 30;
            DeviceInfoUrl = UpLoadGameInfo.Instance.DeviceUrl;
            Debug.Log(DeviceInfoUrl);
            var paths = Object.FindObjectsOfType<CinemachineSmoothPath>(true);
            var path = paths[UnityEngine.Random.Range(0, paths.Length)];
            mainCamera = Camera.main;
            breathingLogic.Init();
            particleSystemsController.Init();
            fly = GameSceneObjController.Instance.sceneObjs["Fly"].transform;
            flyObj = GameSceneObjController.Instance.sceneObjs["FlyObj"].transform;
            holeObj = GameSceneObjController.Instance.sceneObjs["HoleObj"].transform;
            sceneObj = GameSceneObjController.Instance.sceneObjs["地形"];
            flyAni = flyObj.GetComponent<Animator>();
            flyPath = fly.GetComponent<CinemachineDollyCart>();
            flyPath.m_Path = path;
            mPath = path;
            //注册mono生命周期事件
            PaperAirplaneUnityLogic.update += Update;
            PaperAirplaneUnityLogic.onDestroy += OnDestroy;
            //初始化webSocket
            WebSocketController.Instance.isReconnection = true;
            WebSocketController.Instance.Init(DeviceInfoUrl);
            //开始Socket
            WebSocketController.Instance.StartSever();
            //注册事件
            WebSocketController.Instance.OnMessageEvent += GetDeviceInfo;
            // WebSocketController.Instance.OnOpenEvent += () =>
            // { 
            //     UnityToAndroidSetDevice.Instance.SetDevice(WebSocketController.Instance.WebSocket,"begin",UpLoadGameInfo.gameStartInfo.gameNameAbb);
            //     Debug.Log("向安卓发送开始设备消息");
            // };
            sceneObj.SetActive(false);
            startTime = DateTime.Now;
            StartGame();
            Debug.Log("开始游戏");
        }


        /// <summary>
        /// 解析从网络获取到呼吸带的数据
        /// </summary>
        /// <param name="str"></param>
        private void GetDeviceInfo(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            try
            {
                //Debug.Log(str);
                var obj = JsonMapper.ToObject<DeviceInfo>(str);
                if (obj == null)
                {
                    Debug.Log("Json反序列化失败");
                    return;
                }

                deviceInfo = obj;
            }
            catch (Exception e)
            {
                Debug.Log($"GetDeviceInfo ERROR!! : {e}");
            }
        }

        /// <summary>
        /// 呼气和吸气的计时
        /// </summary>
        private async void Timing()
        {
            float tempHTime = 0;
            float tempXTime = 0;
            hBreathing++;
            //游戏正在进行才计算吸气呼气时间
            while (IsPlaying)
            {
                //等待1秒
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                //正在呼吸才记录时间否则时间为0
                if (deviceInfo != null && deviceInfo.blhuxiState == 1)
                {
                    //呼气开始计时
                    if (deviceInfo.blhuqi == 1)
                    {
                        //完成一次吸记一次 因为在吸的时候呼气的时间为0
                        if (hTiming == 0)
                        {
                            hBreathing++;
                        }

                        xTiming = 0;
                        hTiming += 0.1f;
                        //Debug.Log($"呼气时间：{ToTimeStr(hTiming, 0.1f)}");
                        tempHTime = hTiming;
                    }


                    //吸气开始计时
                    if (deviceInfo.blxiqi == 1)
                    {
                        if (xTiming == 0)
                        {
                            xBreathing++;
                        }

                        hTiming = 0;
                        xTiming += 0.1f;
                        //Debug.Log($"吸气时间：{ToTimeStr(xTiming, 0.1f)}");
                        tempXTime = xTiming;
                    }

                    //呼吸次数对2求余等于0表示一次呼吸
                    if ((hBreathing + xBreathing) % 2 != 0) continue;
                    breathingTime = tempHTime + tempXTime;
                    //Debug.Log($"{breathingTime}={tempHTime}+{tempXTime}");

                    breathingRate = 60 / breathingTime;
                    //Debug.Log($"{breathingRate}={60}/{breathingTime}");
                }
                else
                {
                    hTiming = 0;
                    xTiming = 0;
                    //return;
                }
            }
        }

        public static string ToTimeStr(float s, float more = 3)
        {
            var timeSpan = TimeSpan.FromSeconds(s);
            switch (more)
            {
                case 0.1f:
                    return $"{timeSpan.Seconds:0}.{timeSpan.Milliseconds:f0}";
                case 1:
                    return $"{timeSpan.Seconds:00}";
                case 2:
                    return $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
                case 3:
                    return $"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
            }

            return "";
        }

        private bool isGameLeadPuase;
        private int promptXTimes;
        private int promptHTimes;
        private int maxPercentage = 80;
        private int minPercentage = 25;

        private async UniTask GameLead()
        {
            Debug.Log("测试测试");
            breathingLogic.SetLocatingPos(minPercentage);
            await UniTask.Delay(TimeSpan.FromSeconds(6f));
            var speed = 0.1f;
            //局部方法只在此调用
            void SetLocatingPosHigh()
            {
                breathingLogic.SetLocatingPos(maxPercentage);
                breathingLogic.checkCallBack = null;
                breathingLogic.checkCallBack = SetLocatingPosLow;
                //不同完成次数执行不同的逻辑
                switch (promptXTimes)
                {
                    //前几次跳过教学提示
                    case 0:
                    case 1:
                        break;
                    //熟悉两次之后开始提示
                    case 2:
                        isGameLeadPuase = true;
                        speed = 0;
                        //显示提示UI 在UI展示完成后 继续逻辑
                        whiteView.ShowSubtitles1(() =>
                        {
                            isGameLeadPuase = false;
                            speed = 0.1f;
                        });
                        break;
                    case 3:

                        IsGameLead = false;

                        break;
                } 
                promptXTimes++;
            }

            //局部方法只在此调用
            void SetLocatingPosLow()
            {
                breathingLogic.SetLocatingPos(minPercentage);
                breathingLogic.checkCallBack = null;
                breathingLogic.checkCallBack = SetLocatingPosHigh;

                //不同完成次数执行不同的逻辑
                switch (promptHTimes)
                {
                    //前几次跳过教学提示
                    case 0:
                    case 1:
                        break;
                    //熟悉两次之后开始提示
                    case 2:
                        isGameLeadPuase = true;
                        speed = 0;
                        whiteView.ShowSubtitles2(() =>
                        {
                            isGameLeadPuase = false;
                            speed = 0.1f;
                        });
                        break;
                    case 3:

                        IsGameLead = false;

                        break;
                }
 
                promptHTimes++;
            }

            breathingLogic.checkCallBack = SetLocatingPosLow;
            Debug.Log("引导关卡");
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            
            while (IsPlaying && IsGameLead /*&& collisionCount < 3*/)
            {
                var position = fly.position;
                fly.Translate(Vector3.forward * speed);
                if (deviceInfo != null && deviceInfo.blhuxiState != 1)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(0.002f));
                    continue;
                }

                if (position.z != 0 && position.z % 100 <= 1)
                {
                    GameSceneObjController.Instance.sceneObjs["Outer"].gameObject.SetActive(true);
                    GameSceneObjController.Instance.sceneObjs["Outer"].transform.position = new Vector3(0, 0, ((int) (position.z / 100) + 1) * 100);
                }

                // if ((int) (position.z % 100) == 0)
                // {
                //     SetHolePos(new Vector3(0, 0, position.z + 20));
                // }

                await UniTask.Delay(TimeSpan.FromSeconds(0.002f));
            }

            Debug.Log("引导关卡结束");
            IsGameLead = false;
        }

        // private bool isClearGameLeadObj = true;
        private static readonly int high = Animator.StringToHash("High");
        private static readonly int low = Animator.StringToHash("Low");
        private static readonly int middle = Animator.StringToHash("Middle");

        private async UniTask ClearGameLeadObj()
        {
            collisionCount = 0;
            // isClearGameLeadObj = true;
            Debug.Log("清除引导关卡对象");
            GameSceneObjController.Instance.sceneObjs["Outer"].gameObject.SetActive(false);
            //摄像机背景颜色调整
            await mainCamera.DOColor(Color.white, 2f).ToUniTask();
            //显示白色UI 
            whiteView.ShowColor();
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            //显示场景对象
            sceneObj.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            //打开轨道移动脚本
            StartPath();
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            //调整摄像机clearFlags
            mainCamera.clearFlags = CameraClearFlags.Skybox;
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            await whiteView.HideColor();
            UIPanelManager.instance.ShowPanel<PAGamePanel>();
            // isClearGameLeadObj = false;
        }

        private async void BreathingLogic()
        {
            float percentage = 0;
            while (IsPlaying)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                if (isGameLeadPuase) continue;
                if (deviceInfo?.blhuxiState != 1)
                {
                    breathingLogic.HideUI();
                    continue;
                }

                breathingLogic.ShowUI();
                if (deviceInfo.blhuqi == 1)
                {
                    percentage += 10f;
                    percentage = percentage >= 100 ? 100 : percentage;
                    Debug.Log(percentage);
                    breathingLogic.SetValueTheProgressBar(percentage);
                    breathingLogic.Check(percentage);
                }
                else
                {
                    percentage -= 15f;
                    percentage = percentage <= 0 ? 0 : percentage;
                    Debug.Log(percentage);
                    breathingLogic.SetValueTheProgressBar(percentage);
                    breathingLogic.Check(percentage);
                }
            }
        }

        private async void GameLogic()
        {
            //Debug.Log("主逻辑");
            while (IsPlaying)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
                if (isGameLeadPuase) continue;
                if (deviceInfo?.blhuxiState != 1) continue;
                //Debug.Log("主逻辑循环");
                if (deviceInfo.blhuqi == 1)
                {
                    if (Mathf.Abs(flyObj.localPosition.y - MHigh) > 0.5f)
                    {
                        flyAni.SetBool(high, true);
                        flyAni.SetBool(low, false);
                        flyAni.SetBool(middle, false);
                    }
                    else
                    {
                        flyAni.SetBool(high, false);
                        flyAni.SetBool(low, false);
                        flyAni.SetBool(middle, true);
                    }

                    currentHighly += 0.9f;
                    // flyObj.localPosition = new Vector3(pos.x, Mathf.Lerp(pos.y, currentHighly, Time.deltaTime * 0.2f), pos.z); 
                    if (currentHighly >= MHigh)
                    {
                        currentHighly = MHigh;
                    }
                }

                //吸气时的逻辑
                if (deviceInfo.blxiqi == 1)
                {
                    if (Mathf.Abs(flyObj.localPosition.y - MLow) > 0.5f)
                    {
                        flyAni.SetBool(high, false);
                        flyAni.SetBool(low, true);
                        flyAni.SetBool(middle, false);
                    }
                    else
                    {
                        //播放平飞动画
                        flyAni.SetBool(high, false);
                        flyAni.SetBool(low, false);
                        flyAni.SetBool(middle, true);
                    }

                    //吸气时一直减少当前的高度
                    currentHighly -= 0.9f;
                    //高度小于等于最低值时
                    if (currentHighly <= MLow)
                    {
                        //当前值设置为最低值
                        currentHighly = MLow;
                        //设置当前高度 
                        //设置呼气时需要穿过的圆环
                        // if (!IsGameLead)
                        // {
                        //     SetHolePos(20);
                        // } 
                    }
                }
            }
        }

        private void SetHolePos(float distance)
        {
            if (mPath == null || !isHoleCollision) return;
            if (flyPath == null) return;
            isHoleCollision = false;
            var mPosition = flyPath.m_Position + distance;

            holeObj = particleSystemsController.GetObj().transform;
            holeObj.position = mPath.EvaluatePositionAtUnit(mPosition, CinemachinePathBase.PositionUnits.Distance);
            holeObj.rotation = mPath.EvaluateOrientationAtUnit(mPosition, CinemachinePathBase.PositionUnits.Distance);
        }

        private void SetHolePos(Vector3 pos)
        {
            //isHoleCollision = false;
            //holeObj.position = pos;
        }

        //碰撞到光环事件 主工程Momo脚本调用
        public void Collision()
        {
            AudioManager.Instance.PlayOne(AudioManager.Instance.AudioClips["星光"]);
            isHoleCollision = true;
            particleSystemsController.SetEffect();
            if (!IsGameLead)
            {
                collisionCount++;
            }

            Debug.Log($"得分:{collisionCount} ");
        }

        public void UnCollision()
        {
            AudioManager.Instance.PlayOne(AudioManager.Instance.AudioClips["水晶吊坠"]);
            isHoleCollision = true;
            particleSystemsController.SetEffect();
        }

        private void StartPath()
        {
            flyPath.enabled = true;
        }

        private void Update()
        {
            // var random = Random.Range(-1, 2);
            // var pos = fly.position;
            // fly.position = new Vector3(pos.x, Mathf.Lerp(pos.y, random, Time.deltaTime * 0.2f), pos.z);
            var worldCanvas = GameSceneObjController.Instance.sceneObjs["WorldCanvas"];
            worldCanvas.transform.LookAt(mainCamera.transform);
            if (isGameLeadPuase)
            {
                return;
            }

            var pos = flyObj.localPosition;
            flyObj.localPosition = new Vector3(pos.x, Mathf.Lerp(pos.y, currentHighly, Time.deltaTime * 0.7f), pos.z);
        }

        /// <summary>
        /// 对象销毁时需要取消注册等操作
        /// </summary>
        private void OnDestroy()
        {
            IsGameLead = IsPlaying = false;
            collisionCount = 0;
            WebSocketController.Instance.isReconnection = false;
            WebSocketController.Instance.Close();
            WebSocketController.Instance.Destroy();

            PaperAirplaneUnityLogic.update -= Update;
            PaperAirplaneUnityLogic.onDestroy -= OnDestroy;
        }

        private async void StartGame()
        {
            if (IsPlaying)
            {
                return;
            }

            UIViewManager.instance.ShowView<WhiteView>();
            whiteView = UIViewManager.instance.GetView<WhiteView>();

            collisionCount = 0;
            IsGameLead = true;
            IsPlaying = true;
            Timing();
            BreathingLogic();
            GameLogic();
            await GameLead();
            await ClearGameLeadObj();
            if (UpLoadGameInfo.gameStartInfo != null && UpLoadGameInfo.gameStartInfo.gameTime != 0)
            {
                DownTimeController.Instance.gameTime = gameTime = UpLoadGameInfo.gameStartInfo.gameTime;
            }
            else
            {
                DownTimeController.Instance.gameTime = 60;
            }


            DownTimeController.Instance.endDownTimeEvent = () => { UIPanelManager.instance.ShowPanel<PAOverPanel>(); };
            DownTimeController.Instance.Start();


            breathingLogic.SetLocatingPos(minPercentage);

            //局部方法只在此调用
            void SetLocatingPosHigh()
            {
                breathingLogic.SetLocatingPos(maxPercentage);
                breathingLogic.checkCallBack = null;
                breathingLogic.checkCallBack = SetLocatingPosLow;
                Debug.Log("GameLogic :设置光环位置");
                SetHolePos(10);
            }

            //局部方法只在此调用
            void SetLocatingPosLow()
            {
                breathingLogic.SetLocatingPos(minPercentage);
                breathingLogic.checkCallBack = null;
                breathingLogic.checkCallBack = SetLocatingPosHigh;
                // Debug.Log("GameLogic :设置光环位置");
                // SetHolePos(10);
            }

            breathingLogic.checkCallBack = SetLocatingPosHigh;
        }

        /// <summary>
        /// 远程上传训练数据地址
        /// </summary>
        private string ip = "http://139.129.230.203:8221";

        private string upLoadUrl = "/app/patient/game/unity/uploadTrain";

        public void GameOver()
        { 
            // UnityToAndroidSetDevice.Instance.SetDevice(WebSocketController.Instance.WebSocket,"end",UpLoadGameInfo.gameStartInfo.gameNameAbb);
            // Debug.Log("向安卓发送停止设备消息");
            IsPlaying = false;
            WebSocketController.Instance.Close();
            //得出总用时分钟数
            var useMinutes = DownTimeController.Instance.UseTime() / 60;
            useMinutes = useMinutes == 0 ? 1 : useMinutes;
            //停止计时
            DownTimeController.Instance.endDownTimeEvent = () => { };
            DownTimeController.Instance.End();
            //呼吸频率 = (（呼吸次数+吸气次数）/ 2)/用时多少分钟；
            var temp = (xBreathing + hBreathing) / 2 / useMinutes;
            breathingRate = temp;
            endTime = DateTime.Now;
            try
            {
                var upLoadData = new UpLoadData
                {
                    breathing = breathingRate.ToString("f0"),
                    score = (collisionCount * 100).ToString(),
                    xBreathing = xBreathing.ToString(),
                    hBreathing = hBreathing.ToString()
                };
                var upLoadDataStr = JsonMapper.ToJson(upLoadData);
                Debug.Log(upLoadDataStr);
                // Dictionary<string, string> data = new Dictionary<string, string>();


                // //训练设备类型编号
                // data.Add("devAbb", UpLoadGameInfo.gameStartInfo.devAbb);
                // //训练设备蓝牙编号
                // data.Add("devCode", UpLoadGameInfo.gameStartInfo.devCode);
                // //	患者ID
                // data.Add("ptId", UpLoadGameInfo.gameStartInfo.userId);

                //TODO 正式打包时解开注释
                //UpLoadDeviceData upLoadDeviceData = new UpLoadDeviceData();
                // upLoadDeviceData.devAbb =UpLoadGameInfo.gameStartInfo.devAbb;
                // upLoadDeviceData.devCode = UpLoadGameInfo.gameStartInfo.devCode;
                // upLoadDeviceData.ptId = UpLoadGameInfo.gameStartInfo.userId;
                string devAdd="";
                string devCode="";
                if (UpLoadGameInfo.gameStartInfo.deviceInitInfo != null)
                {
                    devAdd= UpLoadGameInfo.gameStartInfo.deviceInitInfo.devAdd;
                    devCode=UpLoadGameInfo.gameStartInfo.deviceInitInfo.devCode;
                }

                var upLoadDeviceData = new UpLoadDeviceData
                {
                    devAbb =devAdd,
                    devCode =devCode ,
                    ptId = UpLoadGameInfo.gameStartInfo.userId,
                    trainRecordEndTime = endTime.ToString("u"),
                    trainRecordResult = upLoadDataStr,
                    trainRecordStartTime = startTime.ToString("u")
                };
                var upLoadDeviceDataStr = JsonMapper.ToJson(upLoadDeviceData);
                Debug.Log(upLoadDeviceDataStr);
                ip = UpLoadGameInfo.Instance.ip;
                upLoadUrl = UpLoadGameInfo.Instance.TrainOverDataPostUrl;
                DataPostTool.Instance.PostText(ip + upLoadUrl, upLoadDeviceDataStr);
            }
            catch (Exception e)
            {
                Debug.Log("游戏结束上传数据报错：" + e);
            }
        }

        public class UpLoadDeviceData
        {
            public string devAbb;
            public string devCode;
            public string ptId;
            public string trainRecordEndTime;
            public string trainRecordResult;
            public string trainRecordStartTime;
        }

        public class UpLoadData
        {
            public string score;
            public string breathing;
            public string xBreathing;
            public string hBreathing;
        }
    }
}