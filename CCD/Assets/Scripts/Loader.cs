using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public AssetReference asset;
    public AssetReference sceneAsset;

    public InputField sceneNameInputField;

    public delegate string TestDelegateFunction(int a);

    public   TestDelegateFunction TestFunctionDelegate;

    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerPrefs.DeleteKey(Addressables.kAddressablesRuntimeDataPath);
        Addressables.InitializeAsync();
        sceneLoadBtn.onClick.AddListener(LoadScene);
        TestFunctionDelegate += i =>
        {
            Debug.Log(i);
            return i.ToString();
        };
    }

    public Button sceneLoadBtn;

    public void LoadScene()
    {
        Addressables.LoadSceneAsync(sceneNameInputField.text);
    }
    // public async void LoadScene()
    // {
    //     var task = Addressables.LoadSceneAsync(sceneNameInputField.text).ToUniTask(Progress.Create<float>((x) =>
    //     {
    //         sceneNameInputField.interactable = false;
    //         sceneNameInputField.text = $"加载进度{x * 100:f0}%";
    //         Debug.Log($"加载进度{x * 100:f0}%");
    //     }));
    //     await task;
    // }

    public void OnLoadPlayer(AsyncOperationHandle<GameObject> handle)
    {
        switch (handle.Status)
        {
            case AsyncOperationStatus.None:
                break;
            case AsyncOperationStatus.Succeeded:
                var insObj = Instantiate(handle.Result);
                insObj.transform.position = Vector3.zero;
                break;
            case AsyncOperationStatus.Failed:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}