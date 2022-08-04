 
using UnityEngine.UI;  

namespace UnityHotFix.UI
{
    public abstract class LoginPanel : UIPanel
    {
        private Button mLoginBtn;
        private InputField mUserNameInput;
        private Text buttonText;

        protected LoginPanel(string url) : base(url)
        {
            viewLayer = ViewLayer.Default;
        }

        public override void Update()
        {
            base.Update();
            if (mUserNameInput.text != null)
            {
                buttonText.text = mUserNameInput.text;
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
             
            mLoginBtn = transform.Find("LoginButton").GetComponent<Button>();
            mUserNameInput = transform.Find("UserNameInputField").GetComponent<InputField>();
            buttonText = mLoginBtn.GetComponentInChildren<Text>();
            mLoginBtn.onClick.AddListener(OnClick);
        }

        private static void OnClick()
        {
            UIPanelManager.instance.ShowPanel<MainPanel>("MainPanel");
            UIViewManager.instance.ShowView<MenuPanel>();
            UIViewManager.instance.ShowView<MenuPanel>();
        }
    }
}