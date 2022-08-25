namespace UnityHotFix.UI
{
    /// <summary>
    /// 默认层级页面使用UIPanel基类 重写三个Update可实现Mono内的Update
    /// </summary>
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