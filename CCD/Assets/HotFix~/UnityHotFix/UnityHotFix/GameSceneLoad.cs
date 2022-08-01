using System;
using Cysharp.Threading.Tasks;
using Hotfix;
using UnityHotFix.UI;

namespace UnityHotFix
{
    public class GameSceneLoad : SceneLoad
    {
        
        public GameSceneLoad(string sceneName) : base(sceneName)
        {
            
        }
        protected override void RegisterAllLoadTask()
        {
            base.RegisterAllLoadTask();
            RegisterLoadTask(LoadTask1);
            RegisterLoadTask(LoadTask2);
        }
        void LoadTask1(Action<float> callback)
        {
            Task1(callback);
        }
        async void Task1(Action<float> callback)
        { 
            float pValue = 0;
            while (pValue<1)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                pValue += 0.01f;
                callback(pValue);
            }
        }
		
        void LoadTask2(Action<float> callback)
        {
            Task2(callback);
        }
		
        async void Task2(Action<float> callback)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            callback(0.3f);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            callback(0.5f);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            callback(0.8f);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            callback(1);
        }
		
        protected override void OnLoadFinish()
        {
            base.OnLoadFinish(); 
        }
         
    }
}