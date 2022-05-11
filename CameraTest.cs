using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.Serialization;

public class CameraTest : MonoBehaviour
{
    public RawImage showImage;
    private WebCamTexture webCamTexture;
    public string deviceName;
    public Button btn;
    public int count;
    public bool isShooting;
    public bool isNotInBack;
    private List<Texture2D> textures = new List<Texture2D>();

    public string savePath;

    IEnumerator Start()
    {
        btn.onClick.AddListener(Call);
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            deviceName = devices[0].name;
            Debug.Log(deviceName);
            webCamTexture = new WebCamTexture(deviceName, Screen.height, Screen.width, 30);
            showImage.texture = webCamTexture;
            webCamTexture.Play();
            showImage.rectTransform.localEulerAngles = new Vector3(0, 0, -webCamTexture.videoRotationAngle);
            // showImage.rectTransform.sizeDelta = new Vector2(1920 * 1.9f, 1080 * 1.9f);
            // showImage.rectTransform.anchorMax= Vector2.one;
            // showImage.rectTransform.anchorMin= Vector2.zero;
        }
    }

    private void OnApplicationPause(bool focus)
    {
        isNotInBack = focus;
        if (focus) //进入程序状态更改为前台
        {
            if (!webCamTexture.isPlaying)
            {
                webCamTexture.Play();
            }
        }
        else
        {
            //离开程序进入到后台状态

            webCamTexture.Pause();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        isNotInBack = focus;
        if (focus) //进入程序状态更改为前台
        {
            if (!webCamTexture.isPlaying)
            {
                webCamTexture.Play();
            }
        }
        else
        {
            //离开程序进入到后台状态

            webCamTexture.Pause();
        }
    }

    private async void Call()
    {
        isShooting = !isShooting;
        while (isShooting)
        {
            if (!isNotInBack)
            {
                await UniTask.WaitForEndOfFrame(this);
                continue;
            }

            await GetTexture2d();
            count++;
        }

        OnDestroy();
    }

    async UniTask GetTexture2d()
    {
        await UniTask.WaitForEndOfFrame(this);
        Texture2D t = new Texture2D(webCamTexture.width, webCamTexture.height);
        t.SetPixels(webCamTexture.GetPixels());
        t.Apply();
        byte[] byt = t.EncodeToJPG();
        textures.Add(t);
        // await UniTask.Delay(100);
        // File.WriteAllBytes(Application.streamingAssetsPath + "/" + DateTime.Now.ToString("t") + ".png", byt);
        // 判断图片 bytes 是否为空 
        if (byt.Length > 0)
        {
            // 判断Android 平台，进行对应路径设置  
            string platformPath = Application.streamingAssetsPath + "/MyTempPhotos";
#if UNITY_ANDROID && !UNITY_EDITOR
            platformPath = "/sdcard/DCIM";
            if (!Directory.Exists(platformPath))
            {
                Directory.CreateDirectory(platformPath);
            }
         platformPath += "/MyTempPhotos";
#endif
            // 如果文件夹不存在，就创建文件夹 
            if (!Directory.Exists(platformPath))
            {
                Directory.CreateDirectory(platformPath);
            }

            // 保存图片 
            savePath = platformPath + "/image.jpg";
            FileStream fileStream = new FileStream(savePath, FileMode.OpenOrCreate);
            fileStream.Write(byt, 0, byt.Length);
            //File.WriteAllBytes(savePath, byt);
            fileStream.Flush();   
            fileStream.Close();
            fileStream.Dispose(); 
            Destroy(t);
            // await UniTask.Delay(100);
        }
    }

    public void OnDestroy()
    {
        for (var i = 0; i < textures.Count; i++)
        {
            Destroy(textures[i]);
        }

        textures.Clear();
    }
}