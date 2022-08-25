using System;
using DG.Tweening;
using Slotpart.Training.Limb.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UnityHotFix.Game.PaperAirplane
{
    public class BreathingLogic
    {
        public Image pictureProgressBar;

        public Text pctText;

        // public Transform locatingSign;
        // private float highestPoint;

        // private float lowestPoint;
        public Action checkCallBack;

        private float targetPercentage;
        // private Image progressBarLight;

        public void Init()
        {
            pictureProgressBar = GameSceneObjController.Instance.sceneObjs["pictureProgressBar"].GetComponent<Image>();
            // progressBarLight = GameSceneObjController.Instance.sceneObjs["ProgressBarLight"].GetComponent<Image>();
            pctText = GameSceneObjController.Instance.sceneObjs["PctText"].GetComponent<Text>();
            // locatingSign = GameSceneObjController.Instance.sceneObjs["locatingSign"].transform;

            // highestPoint = ((RectTransform) pictureProgressBar.transform).rect.height;
            // lowestPoint = 0f;
            SetLocatingPos(80);
        }

        public void HideUI()
        {
            if (!pictureProgressBar.gameObject.activeSelf) return;
            pictureProgressBar.gameObject.SetActive(false);
            pctText.gameObject.SetActive(false);
        }

        public void ShowUI()
        {
            if (pictureProgressBar.gameObject.activeSelf) return;
            pictureProgressBar.gameObject.SetActive(true);
            pctText.gameObject.SetActive(true);
        }

        /// <summary>
        /// 设置标志位的高度
        /// </summary>
        /// <param name="percentage">所在位置的百分比</param>
        public void SetLocatingPos(float percentage)
        {
            //数值设置
            targetPercentage = percentage;
            //效果显示
            // var average = highestPoint / 100;
            // var pos = ((RectTransform) locatingSign).anchoredPosition;
            // var currentHeight = average * percentage;
            // Debug.Log("currentHeight:" + currentHeight);
            // if (currentHeight <= 0)
            // {
            //     currentHeight = lowestPoint;
            // } 
            // ((RectTransform) locatingSign).anchoredPosition = new Vector2(pos.x, currentHeight);
        }

        /// <summary>
        /// 设置当前进度条的位置
        /// </summary>
        /// <param name="percentage"></param>
        public void SetValueTheProgressBar(float percentage)
        {
            var progressBarPercentage = percentage / 100;
            // pictureProgressBar.fillAmount = progressBarPercentage;
            DotweenTool.Instance.ImageDoFill(pictureProgressBar, progressBarPercentage, 0.1f, null);
            pctText.text = $"{percentage}%";
            // if (progressBarLight.gameObject.activeSelf)
            // {
            //     DotweenTool.Instance.ImageDoFill(progressBarLight, progressBarPercentage, 0.1f, null);
            // } 
            // Check(percentage);
        }

        public void Check(float percentage)
        {
            if (percentage >= 80)
            {
                pctText.color = Color.green;
            }
            else if (percentage <= 25)
            {
                pctText.color = Color.green;
            }
            else
                pctText.color = Color.white;

            if (Mathf.Abs(percentage - targetPercentage) <= 0f)
            {
                Debug.Log("达到目标值");
                checkCallBack?.Invoke();
            }
        }
    }
}