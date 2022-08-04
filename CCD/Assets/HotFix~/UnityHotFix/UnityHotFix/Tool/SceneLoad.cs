using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityHotFix.UI;

namespace Hotfix
{
    public class SceneLoad
    {
        //加载场景时，其他需要执行的任务。每个任务的进度为0-1
        protected delegate void LoadTaskDelegate(Action<float> callback);

        protected class LoadTask
        {
            public float progress;
            LoadTaskDelegate m_loadTask;
            Action m_progressAction;

            //加载任务和进度更新
            public LoadTask(LoadTaskDelegate task, Action action)
            {
                m_loadTask = task;
                m_progressAction = action;
            }

            public void Start()
            {
                progress = 0;
                //执行任务
                m_loadTask.Invoke((p) =>
                {
                    //更新进度
                    progress = Mathf.Clamp01(p);
                    m_progressAction?.Invoke();
                });
            }
        }

        string m_sceneName;
        LoadingView m_loadingView;
        List<LoadTask> m_loadTaskList; //任务列表
        int m_totalSceneLoadProgress; //加载场景所占的任务数
        int m_totalProgress; //总任务数（加载场景所占的任务数+其他任务的数量，用于计算loading百分比）
        bool m_isLoadFinish;
        private float taskProgress;

        protected SceneLoad(string sceneName)
        {
            m_sceneName = sceneName;
            m_loadTaskList = new List<LoadTask>();
            m_totalSceneLoadProgress = 2;
            m_totalProgress = m_loadTaskList.Count + m_totalSceneLoadProgress;
        }

        public virtual void Start()
        {
            RegisterAllLoadTask();
            m_isLoadFinish = false;
            m_loadingView = null;
            taskProgress = 0;
            UIViewManager.instance.ShowView<LoadingView>(OnLoadingPanelLoaded);
        }

        protected virtual void OnLoadingPanelLoaded(LoadingView panel)
        {
            m_loadingView = panel;
            LoadScene();
        }

        //注册所有需要执行的其他任务
        protected virtual void RegisterAllLoadTask()
        {
        }

        //注册一个新任务
        protected virtual void RegisterLoadTask(LoadTaskDelegate task)
        {
            m_loadTaskList.Add(new LoadTask(task, UpdateLoadTaskProgress));
        }

        //更新任务进度
        protected virtual void UpdateLoadTaskProgress()
        {
            // taskProgress = m_totalSceneLoadProgress;
            foreach (var task in m_loadTaskList)
                taskProgress += task.progress;
            // UpdateProgress(taskProgress);
        }

        //加载场景前执行，主要做一些内存清理的工作
        protected virtual void OnPreLoadScene()
        {
            UIPanelManager.instance.UnLoadPanelOnLoadScene();
            UIViewManager.instance.DestroyViewOnLoadScene();
        }

        //更新总进度
        protected virtual void UpdateProgress(float progress)
        {
            float progressPercent = Mathf.Clamp01(progress / m_totalProgress);
            //Debug.Log($"TotalProgress:{progressPercent}");
            m_loadingView.SetProgress(progressPercent);

            //所有任务进度为1时，即加载完成
            if (progressPercent >= 1 && !m_isLoadFinish)
            {
                LoadFinish();
                m_isLoadFinish = true;
            }
        }

        //所有任务加载完成
        async void LoadFinish()
        {
            if (m_isLoadFinish)
            {
                return;
            }

            Debug.Log($"Loads scene '{m_sceneName}' completed.");

            if (sceneOperation != null) sceneOperation.allowSceneActivation = true;
            OnLoadFinish();
            //等待0.5s，这样不会进度显示100%的时候瞬间界面消失。
            await UniTask.Delay(TimeSpan.FromSeconds(1f)); 
            m_loadingView.Hide();
            OnPreLoadScene();
        }

        //加载完成时执行
        protected virtual void OnLoadFinish()
        {
        }

        private AsyncOperation sceneOperation;

        //加载场景
        private async void LoadScene()
        {
            var clearSceneProgress = Progress.Create<float>((x) =>
            {
                // Debug.Log($"ClearSceneLoadProgress:{x}");
                UpdateProgress(taskProgress + x);
            });
            // //先跳转空场景，进行内存的清理 
            Debug.Log(1);
            var clearSceneOperation = SceneManager.LoadSceneAsync("ClearScene", LoadSceneMode.Single);
            Debug.Log(2);
            var clearTask = clearSceneOperation.ToUniTask(clearSceneProgress);
            Debug.Log(3);
            await clearTask;
            Debug.Log(4);
            GC.Collect();

            Debug.Log("start load scene: " + m_sceneName);
            var sceneProgress = Progress.Create<float>((x) =>
            {
                // Debug.Log($"SceneLoadProgress:{x}");
                UpdateProgress(1 + taskProgress + x);
            });
            sceneOperation = SceneManager.LoadSceneAsync(m_sceneName, LoadSceneMode.Additive);
            sceneOperation.allowSceneActivation = false;
            var task = sceneOperation.ToUniTask(sceneProgress);
            StartLoadTask();
            await task;
            // await UniTask.WaitUntil(() => sceneOperation.progress >= 0.9f);
            Debug.Log("加载完成");
            // When allowSceneActivation is set to false then progress is stopped at 0.9. The isDone is then maintained at false.
            // When allowSceneActivation is set to true isDone can complete.  
            //为true时，场景切换
        }

        //执行其他加载任务
        protected virtual void StartLoadTask()
        {
            //Debug.Log($"任务数量：{m_loadTaskList.Count}");
            if (m_loadTaskList.Count == 0)
                return;
            foreach (var task in m_loadTaskList)
                task.Start();
        }
    }
}