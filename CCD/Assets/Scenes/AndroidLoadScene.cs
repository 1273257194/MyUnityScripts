using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Slotpart.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class SceneInfo
{
    public string code;
    public string sceneName;
}

public class AndroidLoadScene : MonoSingleton<AndroidLoadScene>
{
    public string sceneNameFilePath;
    public string loadSceneName;
    public List<SceneInfo> sceneInfos = new List<SceneInfo>();

    public Action<string> androidEvent;

    public override void Init()
    {
        base.Init();
        DontDestroyOnLoad(this);
        sceneNameFilePath = $"{Application.streamingAssetsPath}/{sceneNameFilePath}";
        Debug.Log($"场景配置文件地址：{sceneNameFilePath}");
        var sceneName = LoadDataTool.Instance.GetTextData(sceneNameFilePath);
        Debug.Log($"场景配置文件：{sceneName}");
        sceneInfos = string.IsNullOrEmpty(sceneName)
            ? new List<SceneInfo>() {new SceneInfo() {code = "error", sceneName = ""}}
            : JsonConvert.DeserializeObject<List<SceneInfo>>(sceneName);
    }

    [Button]
    public void SaveJsonInfo()
    {
        sceneNameFilePath = $"{Application.streamingAssetsPath}/{sceneNameFilePath}";
        var tempStr = JsonConvert.SerializeObject(sceneInfos);
        File.WriteAllText(sceneNameFilePath, tempStr);
        sceneNameFilePath = sceneNameFilePath.Replace($"{Application.streamingAssetsPath}/", "");
    }

    [Button]
    public void AddSceneInfo()
    {
        sceneInfos.Clear();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string name = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            sceneInfos.Add(new SceneInfo() {code = "", sceneName = name});
        }
    }

    [Button]
    public void LoadScene(string sceneCode)
    {
        Debug.Log($"LoadScene:{sceneCode}");
        var sceneName = sceneInfos.Find(x => x.code == sceneCode);
        loadSceneName = sceneName == null ? "" : sceneName.sceneName;
        Debug.Log($"Scene:{loadSceneName}");
        // SceneManager.LoadScene(loadSceneName);
        LoadSceneAsync(loadSceneName);
    }

    private async void LoadSceneAsync(string sceneName)
    {
        var tempImage = GameObject.Find("Canvas/Image").GetComponent<Image>();
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        var task = SceneManager.LoadSceneAsync(sceneName).ToUniTask(Progress.Create<float>(x =>
        {
            if (tempImage == null) return;
            var tempValue = tempImage.fillAmount;
            tempImage.fillAmount = Mathf.Lerp(tempValue, x, 0.2f);
        }));
        await task;
    }

    public void ReceiveAndroidMsg(string args)
    {
        //根据接收到的消息自定义处理
        Debug.Log($"来自Android的消息:{args}");
        androidEvent?.Invoke(args);
    }
}