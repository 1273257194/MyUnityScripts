using UnityEngine;
using UnityEngine.UI;

namespace UnityHotFix.UI
{
    public class LoadingView : UIView
    {
        public Image sliderFill;
        public Text sliderText;

        public LoadingView(string url) : base(url)
        {
            viewLayer = ViewLayer.Popup;
            isDontDestroyOnLoad = false;
        }

        public override void Init()
        {
            base.Init();
            sliderFill = transform.Find("Slider/SliderFill").GetComponent<Image>();
            sliderText = transform.Find("Slider/Text").GetComponent<Text>();
            sliderFill.fillAmount = 0;
            sliderText.text = "0%";
        }

        public override void Show()
        {
            base.Show();
            SetProgress(0);
        }

        public override void Hide()
        {
            base.Hide();
            Debug.Log("loadingView被关闭");
        }

        public void SetProgress(float value)
        {
            if (sliderFill != null)
            { 
                sliderFill.fillAmount = value;
            }

            //Debug.Log(value);
            if (sliderText != null) sliderText.text = $"{(int) (value * 100)}%";
        }
    }
}