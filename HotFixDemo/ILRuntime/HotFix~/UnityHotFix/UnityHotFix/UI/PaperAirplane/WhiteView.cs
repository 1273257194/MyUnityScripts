using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Slotpart.Training.Limb.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UnityHotFix.UI.PaperAirplane
{
    public class WhiteView : UIView
    {
        public Image white;
        public Text subtitles;
        public Text subtitles1;
        public Text subtitles2;
        public Text subtitles3;
        public Text subtitles4;

        public WhiteView(string url) : base(url)
        {
            viewLayer = ViewLayer.Popup;
        }

        public override void Init()
        {
            base.Init();
            white = gameObject.GetComponent<Image>();
            subtitles = transform.Find("Subtitles").GetComponent<Text>();
            subtitles1 = transform.Find("Subtitles1").GetComponent<Text>();
            subtitles2 = transform.Find("Subtitles2").GetComponent<Text>();
            subtitles3 = transform.Find("Subtitles3").GetComponent<Text>();
            subtitles4 = transform.Find("Subtitles4").GetComponent<Text>();
            white.color = new Color(1, 1, 1, 0);
            subtitles.color = new Color(1, 1, 1, 0);
            subtitles1.color = new Color(1, 1, 1, 0);
            subtitles2.color = new Color(1, 1, 1, 0);
            subtitles3.color = new Color(1, 1, 1, 0);
            subtitles4.color = new Color(1, 1, 1, 0);
        }

        public override void Show()
        {
            base.Show();
            transform.SetAsLastSibling();
            ShowSubtitles();
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

        public async void ShowSubtitles()
        {
            subtitles.color = new Color(1, 1, 1, 1);
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            await GameSceneObjController.Instance.gameObject.GetComponent<DotweenTool>().TextDoColor(subtitles, Color.clear, 0.5f, null);
            subtitles1.color = new Color(1, 1, 1, 1);
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            await GameSceneObjController.Instance.gameObject.GetComponent<DotweenTool>().TextDoColor(subtitles1, Color.clear, 0.5f, null);
        }

        public void ShowColor()
        {
            white.color = new Color(1, 1, 1, 1);
        }

        public Tweener HideColor()
        {
            return GameSceneObjController.Instance.gameObject.GetComponent<DotweenTool>().ImageDoColor(white, Color.clear, 0.5f, null);
        }

        public async void ShowSubtitles1(Action callBack)
        {
            subtitles2.color = new Color(1, 1, 1, 1);
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            await GameSceneObjController.Instance.gameObject.GetComponent<DotweenTool>().TextDoColor(subtitles2, Color.clear, 0.5f, null);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            callBack?.Invoke();
        }

        public async void ShowSubtitles2(Action callBack)
        {
            subtitles3.color = new Color(1, 1, 1, 1);
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            await GameSceneObjController.Instance.gameObject.GetComponent<DotweenTool>().TextDoColor(subtitles3, Color.clear, 0.5f, null);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            callBack?.Invoke();
        }
        // public void ShowEvent()
        // { 
        //     white.SetActive(false);
        //     GameSceneObjController.Instance.gameObject.GetComponent<DotweenTool>().ImageDoColor(white, Color.clear, 2, callBack);
        //     // DotweenTool.Instance.ImageDoColor(white, Color.clear, 2, callBack);
        // }
    }
}