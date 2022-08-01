using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AddCom : MonoBehaviour
{
    public InputField inputField;
    public Button btn;

    async void OnEnable()
    {
        inputField = transform.Find("InputField").GetComponent<InputField>();
        btn = transform.Find("Button").GetComponent<Button>();
        inputField.text = "A";
        btn.GetComponentInChildren<Text>().text = "已绑定";
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        btn.GetComponentInChildren<Text>().text = "button";
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        btn.onClick.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
    }
}