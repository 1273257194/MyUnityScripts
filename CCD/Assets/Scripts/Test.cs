using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Text showInfo;

    // Start is called before the first frame update
    async void Start()
    {
        ToJson();
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        showInfo.text = "等待完成";
    }
 
    public void ToJson()
    {
        var obj = new List<panelInfo>() { };
        obj.Add(new panelInfo("MainPanel"));
        obj.Add(new panelInfo("LoginPanel"));
        var str = JsonMapper.ToJson(obj);
        Debug.Log(str);
    }

    // Update is called once per frame
    void Update()
    {
    }
}