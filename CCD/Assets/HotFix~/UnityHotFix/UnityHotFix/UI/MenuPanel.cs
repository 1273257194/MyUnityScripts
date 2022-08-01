using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityHotFix.UI
{
    public abstract class MenuPanel : UIView
    {
        private Button quitBtn;
        private Button backBtn;
        private Button openPopupBtn;
        private Text timeText;

        protected MenuPanel(string url) : base(url)
        {
            viewLayer = ViewLayer.LongShow;
            isDontDestroyOnLoad = true;
        }
        
        public override void Init()
        {
            base.Init(); 
            
            timeText = transform.Find("TopUI/TimeText").GetComponent<Text>();
            backBtn = transform.Find("Image/Back").GetComponent<Button>();
            openPopupBtn = transform.Find("Image/OpenPopup").GetComponent<Button>();
            backBtn.onClick.AddListener(BackClick);
            quitBtn = transform.Find("Image/Quit").GetComponent<Button>();
            quitBtn.onClick.AddListener(QuitClick);
            openPopupBtn.onClick.AddListener(OpenPopup);
            quitBtn.interactable = false;
            backBtn.interactable = false;
        }

        public override void Show()
        {
            base.Show(); 
            transform.SetAsLastSibling();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (timeText != null) timeText.text = $"{DateTime.Now.Hour:00}:{DateTime.Now.Minute:00}:{DateTime.Now.Second:00}";
        }

        private void QuitClick()
        {
            UIViewManager.instance.ShowView<LoginPanel>();
            UIPanelManager.instance.HidePanel<MainPanel>(); 
        }

        private void BackClick()
        {
            UIPanelManager.instance.HidePanel();
        }

        private void OpenPopup()
        {
            UIViewManager.instance.ShowView<PopupView>();
        }
    }
}