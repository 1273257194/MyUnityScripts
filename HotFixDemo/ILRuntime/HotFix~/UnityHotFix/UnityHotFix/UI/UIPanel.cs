namespace UnityHotFix.UI
{
    public class UIPanel : UIView
    {
        public object data;
        public UIPanel previousPanel;

        public UIPanel(string url) : base(url)
        { 
        }

        public override void Show()
        {
            base.Show();
            transform.SetAsLastSibling();
        }

        public override void Destroy()
        {
            base.Destroy();
            previousPanel = null;
        }
    }
}