using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityHotFix.Properties;
using UnityHotFix.UI.Limb;

namespace UnityHotFix.Game.Limb
{
    public class DownTimeController : BaseSingleton<DownTimeController>
    {
        public int gameTime;
        public Action initEvent, startDownTimeEvent, pauseDownTimeEvent, endDownTimeEvent;
        public Action<int> showTimeEvent;
        public bool isPause;
        private bool isStart;
        public bool isPlaying;
        public bool isInit;
        public int currentTime;

        public override void Init()
        {
            if (isInit)
            {
                return;
            }

            initEvent?.Invoke();
            isInit = true;
        }

        public int UseTime()
        {
            return gameTime - currentTime;
        }

        public async void Start()
        {
            if (isStart)
            {
                Debug.Log("重复开启，已经开始计时");
                return;
            }

            isPlaying = true;
            var tempTime = gameTime;
            startDownTimeEvent?.Invoke();
            isStart = true;
            while (tempTime > 0 && isPlaying)
            {
                await UniTask.WaitUntil(() => !isPause);
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                tempTime--;
                currentTime = tempTime;
                showTimeEvent?.Invoke(currentTime);
            }

            isStart = false;
            endDownTimeEvent?.Invoke();
        }

        public void Pause()
        {
            isPause = !isPause;
            pauseDownTimeEvent?.Invoke();
        }

        public void End()
        {
            isPlaying = false;
            isPause = false;
            isStart = false;
            currentTime = 0;
            isInit = false;
        }

        /// <summary>
        /// 返回格式化的当前剩余时间
        /// </summary>
        /// <returns>00:00:00</returns>
        public string ToTimeStr()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);

            return $"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
        }

        public void OnDestroy()
        {
            isPlaying = false;
        }
    }
}