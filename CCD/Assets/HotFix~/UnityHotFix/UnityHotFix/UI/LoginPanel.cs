using System;
using UnityEngine;
using UnityEngine.UI; 
using UnityHotFix.Properties;

namespace UnityHotFix.UI
{
    public class LoginPanel : UIPanel
    {
        Button m_loginBtn;
        InputField m_userNameInput;
        private Text buttonText;

        public LoginPanel(string url) : base(url)
        {
            viewLayer = ViewLayer.Default;
        }

        public override void Update()
        {
            base.Update();
            if (m_userNameInput.text != null)
            {
                buttonText.text = m_userNameInput.text;
            }
        }

        public override void Show()
        {
            base.Show();
            transform.SetAsLastSibling(); 
        }

        public override void Init()
        {
            base.Init();
            m_loginBtn = transform.Find("LoginButton").GetComponent<Button>();
            m_userNameInput = transform.Find("UserNameInputField").GetComponent<InputField>();
            buttonText = m_loginBtn.GetComponentInChildren<Text>();
            m_loginBtn.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            UIPanelManager.instance.ShowPanel<MainPanel>("MainPanel");
            UIViewManager.instance.ShowView<MenuPanel>();
        }
    }
}