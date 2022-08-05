namespace UnityHotFix.UI
{
    public interface IView
    {
        void Init();
        void Show();
        void Update();
        void LateUpdate();
        void FixedUpdate();
        void Hide();
        void Destroy();
    }

    // public enum UIPanelLayer
    // {
    //     Default,
    //     Top,
    //     Popup
    // }
}