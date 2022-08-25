using UnityEngine;
using UnityEngine.UI;
using UnityHotFix.Game.Limb;
using UnityHotFix.Game.PaperAirplane;

namespace UnityHotFix.UI.PaperAirplane
{
    public class PAOverPanel : UIPanel
    {
        private Text timeText;
        private Text scoreText;
        private Text huxiText;
        private Text huxiText1;
        private Text huxiText2;

        private Button okBtn;

        public PAOverPanel(string url) : base(url)
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
            timeText.text = PaperAirplaneGameManager.ToTimeStr(DownTimeController.Instance.UseTime());
            scoreText.text = PaperAirplaneGameManager.collisionCount <= 0 ? "0" : (PaperAirplaneGameManager.collisionCount * 100).ToString("###,###");
            huxiText.text = $"{PaperAirplaneGameManager.Instance.breathingRate:f0}次/分钟";
            huxiText1.text = $"{PaperAirplaneGameManager.Instance.hBreathing}次";
            huxiText2.text = $"{PaperAirplaneGameManager.Instance.xBreathing}次";
            PaperAirplaneGameManager.Instance.GameOver();
        }
        public override void Update()
        {
            base.Update();
            if (transform != null)
            {
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
                transform.localScale = Vector3.one;
            }
        }
        public override void Init()
        {
            base.Init();
            timeText = transform.Find("Image/TimeText").GetComponent<Text>();
            scoreText = transform.Find("Image/ScoreText").GetComponent<Text>();
            huxiText = transform.Find("Image/huxiText").GetComponent<Text>();
            huxiText1 = transform.Find("Image/huxiText1").GetComponent<Text>();
            huxiText2 = transform.Find("Image/huxiText2").GetComponent<Text>();
            okBtn = transform.Find("Image/OkBtn").GetComponent<Button>();
            var exitData = "";
            if (UpLoadGameInfo.gameStartInfo != null && string.IsNullOrEmpty(UpLoadGameInfo.gameStartInfo.gameNameAbb))
            {
                exitData = UpLoadGameInfo.gameStartInfo.gameNameAbb;
            }

            okBtn.onClick.AddListener(() => { SendMsg.Instance.SendMsgToAndroid("exit", exitData); });
        }
    }
}