using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityHotFix.UI
{
    public abstract class PopupView : UIView
    {
        protected PopupView(string url) : base(url)
        {
            viewLayer = ViewLayer.Popup;
        }

        private Button quitBtn;
        private Button backBtn;
        private Button okBtn;
        private Text popupText;


        public override void Init()
        {
            base.Init();

            popupText = transform.Find("TopUI/PopupText").GetComponent<Text>();
            backBtn = transform.Find("Image/Back").GetComponent<Button>();
            backBtn.onClick.AddListener(BackClick);
            quitBtn = transform.Find("Image/Quit").GetComponent<Button>();
            okBtn = transform.Find("OKbtn").GetComponent<Button>();
            quitBtn.onClick.AddListener(QuitClick);
            okBtn.onClick.AddListener(OkClick);
            popupText.text = "弹窗";
        }

        public override void Show()
        {
            base.Show();
            transform.SetAsLastSibling();
        }


        private void QuitClick()
        {
            UIPanelManager.instance.ShowPanel<LoginPanel>();
            UIPanelManager.instance.HidePanel<MainPanel>();
            UIViewManager.instance.HideView<MenuPanel>();
            UIViewManager.instance.HideView<PopupView>();
        }

        private void BackClick()
        {
            UIViewManager.instance.HideView<PopupView>();
        }

        private void OkClick()
        { 
            SceneLoadManager.Instance.LoadAddressScene("A");
            // UIViewManager.instance.ShowView<LoadingView>();
        }
    }
}