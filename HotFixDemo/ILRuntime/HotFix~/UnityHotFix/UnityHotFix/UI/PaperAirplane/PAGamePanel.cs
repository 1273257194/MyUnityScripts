using UnityEngine;
using UnityEngine.UI;
using UnityHotFix.Game.Limb;
using UnityHotFix.Game.PaperAirplane;

namespace UnityHotFix.UI.PaperAirplane
{
    public class PAGamePanel : UIPanel
    {
        private Text timeText;

        private Text scoreText;
        //private Text huxiText;
        //private Text huxiText1;

        private Button backBtn;

        public PAGamePanel(string url) : base(url)
        {
            viewLayer = ViewLayer.Default;
        }

        public override void Show()
        {
            base.Show();
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.SetAsLastSibling();
        }

        public override void Init()
        {
            base.Init();
            timeText = transform.Find("TopBar/Image/TimeText").GetComponent<Text>();
            scoreText = transform.Find("TopBar/Image/ScoreText").GetComponent<Text>();
            //huxiText = transform.Find("TopBar/Image/huxiText").GetComponent<Text>();
            //huxiText1 = transform.Find("TopBar/Image/huxiText_1").GetComponent<Text>(); 
            backBtn = transform.Find("TopBar/BackBtn").GetComponent<Button>();
            backBtn.onClick.AddListener(() =>
            {
                // UIPanelManager.instance.HidePanel<PAGamePanel>();
                UIPanelManager.instance.ShowPanel<PAOverPanel>();
            });
        }

        public override void Update()
        {
            base.Update();
            var scoreStr = PaperAirplaneGameManager.collisionCount <= 0 ? "0" : (PaperAirplaneGameManager.collisionCount * 100).ToString("###,###");
            timeText.text = $"时间：{PaperAirplaneGameManager.ToTimeStr(DownTimeController.Instance.currentTime, 2)}";
            scoreText.text = $"得分：{scoreStr}";
            //huxiText.text = $"{PaperAirplaneGameManager.Instance.breathingRate:0}次/分"; 
            //huxiText1.text = PaperAirplaneGameManager.Instance.hTiming > 0
            //     ? $"呼气：{PaperAirplaneGameManager.ToTimeStr(PaperAirplaneGameManager.Instance.hTiming, 1f)}"
            //     : $"吸气：{PaperAirplaneGameManager.ToTimeStr(PaperAirplaneGameManager.Instance.xTiming, 1f)}";
            DownTimeController.Instance.showTimeEvent = (x) => { timeText.text = PaperAirplaneGameManager.ToTimeStr(x); };
            if (transform != null)
            {
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
                transform.localScale = Vector3.one;
            }
        }
    }
}