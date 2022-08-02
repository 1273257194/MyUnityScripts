using System;
using System.Collections.Generic;
using Hotfix;
using UnityEngine;

namespace UnityHotFix
{
    public class SceneLoadManager : HotFixSingleton<SceneLoadManager>
    {
        Dictionary<string, SceneLoad> m_sceneLoadDic=null;

        public void Init()
        {
            // m_sceneLoadDic = new Dictionary<string, SceneLoad>();
            // var sceneLoad = Activator.CreateInstance(typeof(GameSceneLoad), "A") as SceneLoad;
            // m_sceneLoadDic.Add("A", sceneLoad);
        }
        /// <summary>
        /// 加载本地场景，远程资源时调用
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadLocalScene(string sceneName)
        {
            if (Activator.CreateInstance(typeof(GameSceneLoad), sceneName) is SceneLoad sceneLoad) sceneLoad.Start();
        }

        public void LoadScene(string scene)
        {
            var sceneLoad = GetSceneLoad(scene);
            sceneLoad.Start();
        }

        SceneLoad GetSceneLoad(string scene)
        {
            if (!m_sceneLoadDic.TryGetValue(scene, out SceneLoad sceneLoad))
            {
                Debug.LogError($"[SceneLoadManager] Cannot found scene({scene}) loader");
            }

            return sceneLoad;
        }
    }
}