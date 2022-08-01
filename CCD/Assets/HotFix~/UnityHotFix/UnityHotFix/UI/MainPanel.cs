using UnityEngine.UI;

namespace UnityHotFix.UI
{
    public class MainPanel : UIPanel
    {
        private Button openBtn;
        private Button backBtn;

        public MainPanel(string url) : base(url)
        {
            viewLayer = ViewLayer.Default;
        }

        public override void Init()
        {
            base.Init();
            openBtn = transform.Find("OpenBtn").GetComponent<Button>();
            openBtn.onClick.AddListener(Click);
            backBtn = transform.Find("Back").GetComponent<Button>();
            backBtn.onClick.AddListener(BackClick);
        }

        public override void Show()
        {
            base.Show();
            transform.SetAsLastSibling();
        }

        public void Click()
        {
            UIPanelManager.instance.ShowPanel<LoginPanel>();
        }

        private void BackClick()
        {
            UIPanelManager.instance.ShowPanel<LoginPanel>();
            UIViewManager.instance.HideView<MenuPanel>(); 
        }
    }
}