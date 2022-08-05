 
using UnityEngine;
using UnityEngine.UI;

namespace UnityHotFix.UI.Limb
{
    public abstract class LimbLoginPanel : UIPanel
    {
        private Button mBtn; 

        protected LimbLoginPanel(string url) : base(url)
        {
            viewLayer = ViewLayer.Default;
        }

        public override void Update()
        {
            base.Update(); 
        }

        public override void Show()
        {
            base.Show();
            transform.SetAsLastSibling(); 
            
        }

        public override void Init()
        {
            base.Init(); 
            mBtn = transform.Find("mBtn").GetComponent<Button>(); 
            mBtn.onClick.AddListener(OnClick);
        }

        private static void OnClick()
        {
            Debug.Log("Update !! mBtnClick");
        }
    }
}