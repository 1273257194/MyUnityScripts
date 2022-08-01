using System;
using System.Diagnostics;
using Cysharp.Threading.Tasks;
using Slotpart.Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Debug = UnityEngine.Debug;

namespace UnityHotFix.Properties
{
    public class InstanceClass
    {
        private static AddCom addCom;

        public static void StaticFunTest()
        {
            UnityEngine.Debug.Log("热更新代码");
            var gameObj = GameObject.Find("Canvas");
            addCom = gameObj.AddComponent<AddCom>();

            var obj = GameObject.FindObjectOfType<Loader>();
            obj.asset.LoadAssetAsync<GameObject>().Completed += OnLoadPlayer;
            Wait();
        }

        public async static void Wait()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            var str = LoadDataTool.Instance.GetTextData(Application.streamingAssetsPath + "/AText.txt");
            Debug.Log(str);
        }

        public static void OnLoadPlayer(AsyncOperationHandle<GameObject> handle)
        {
            switch (handle.Status)
            {
                case AsyncOperationStatus.None:
                    break;
                case AsyncOperationStatus.Succeeded:
                    var insObj = UnityEngine.Object.Instantiate(handle.Result);
                    insObj.transform.position = Vector3.zero;
                    break;
                case AsyncOperationStatus.Failed:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static GameObject obj1;

        public static void Start()
        {
            Debug.Log("Start");
            obj1 = GameObject.CreatePrimitive(PrimitiveType.Sphere); //球体
            obj1.transform.position = Vector3.zero;
            var obj = GameObject.FindObjectOfType<Loader>();
            var value = obj.TestFunctionDelegate.Invoke(100);
            Debug.Log($".NetLog:{value}");
            addCom.btn.onClick.AddListener(() => { Debug.Log("addComBtn被点击了"); });
        }

        public static void Update()
        {
            Debug.Log("Update");
            obj1.transform.Translate(Vector3.up * Time.deltaTime);
        }

        public static void FixedUpdate()
        {
            Debug.Log("FixedUpdate");
        }
    }
}