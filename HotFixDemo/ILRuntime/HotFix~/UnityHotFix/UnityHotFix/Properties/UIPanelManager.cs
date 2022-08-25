using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityHotFix.Tool;

namespace UnityHotFix.UI
{
    public class UIPanelInfo
    {
        public string panelName;
    }

    public class UIPanelManager : ManagerBase<UIPanelManager>
    {
        public List<string> panelNames = new List<string>();
        public UIPanel currentPanel; //当前显示的页面

        public Dictionary<string, UIPanel> m_UIPanelDic; //存放所有存在在场景中的UIPanel  

        Transform m_UICanvas;
        public Transform m_DefaultPanel;

        public override void Init()
        {
            base.Init();
            m_UICanvas = ResMgr.Instance.parents;

            m_DefaultPanel = m_UICanvas.transform.Find("DefaultPanel");
            LoadInit();
        }

        /// <summary>
        /// 获取需要的所有面板名字并对字典赋值
        /// </summary>
        public void LoadInit()
        {
            //对字典的key赋值
            m_UIPanelDic = new Dictionary<string, UIPanel>();
            panelNames = ResMgr.Instance.loadPool;
            for (int i = 0; i < panelNames.Count; i++)
            {
                if (!m_UIPanelDic.ContainsKey(panelNames[i]))
                {
                    m_UIPanelDic.Add(panelNames[i], null);
                }
            }
        }

        public void ShowPanel<T>() where T : UIPanel
        {
            ShowPanel<T>(null, null);
        }

        public void ShowPanel<T>(Action<T> callback) where T : UIPanel
        {
            ShowPanel(callback, null);
        }

        public void ShowPanel<T>(object data) where T : UIPanel
        {
            ShowPanel<T>(null, data);
        }

        //显示一个UIPanel，参数为回调和自定义传递数据
        public void ShowPanel<T>(Action<T> callback, object data) where T : UIPanel
        {
            string url = GetUrl(typeof(T));
            if (!string.IsNullOrEmpty(url))
            {
                LoadPanel<T>(url, data, () =>
                {
                    var panel = ShowPanel(url);
                    callback?.Invoke(panel as T);
                });
            }
        }

        //显示UIPanel
        UIPanel ShowPanel(string url)
        {
            if (m_UIPanelDic.TryGetValue(url, out UIPanel panel))
            {
                panel = m_UIPanelDic[url];
                if (!panel.IsVisible)
                {
                    currentPanel?.Hide();
                    panel.previousPanel = currentPanel;

                    panel.Show();
                    currentPanel = panel;
                }
                else
                    Debug.Log("UIPanel is visible:" + url);
            }
            else
                Debug.LogError("UIPanel not loaded:" + url);

            return panel;
        }

        //加载UIPanel对象
        public void LoadPanel<T>(string url, object data, Action callback)
        {
            if (m_UIPanelDic.TryGetValue(url, out UIPanel panel) && panel != null)
            {
                if (panel.IsLoaded)
                    callback?.Invoke();
            }
            else
            {
                panel = CreatePanel(url, typeof(T));
                if (panel == null)
                    Debug.LogError("UIPanel not exist: " + url);
                else
                {
                    panel.data = data;
                    m_UIPanelDic[url] = panel;
                    Debug.Log($"LoadView URL: {panel.URL}");
                    panel.Load(() =>
                    {
                        if (panel.IsLoaded)
                        {
                            var rectTransform = panel.rectTransform;
                            rectTransform.SetParent(m_DefaultPanel);

                            rectTransform.pivot = new Vector2(0.5f, 0.5f);
                            rectTransform.anchorMax = new Vector2(1f, 1f);
                            rectTransform.anchorMin = new Vector2(0, 0);
                            rectTransform.anchoredPosition = new Vector2(0, 0);
                            rectTransform.offsetMax = new Vector2(0, 0);
                            rectTransform.offsetMin = new Vector2(0, 0);

                            callback?.Invoke();
                        }
                        else
                            m_UIPanelDic.Remove(url);
                    });
                }
            }
        }

        //实例化UIPanel对象
        UIPanel CreatePanel(string url, Type t)
        {
            if (!m_UIPanelDic.ContainsKey(url)) return null;
            var panel = UIViewManager.instance.CreateView(t, url) as UIPanel;
            return panel;
        }

        //隐藏当前显示的UIPanel
        public void HidePanel()
        {
            currentPanel.Hide();
            //显示上一层页面
            if (currentPanel.previousPanel != null && currentPanel.previousPanel.IsLoaded)
            {
                currentPanel.previousPanel.Show();
                currentPanel = currentPanel.previousPanel;
            }
        }

        //隐藏找到的的UIPanel
        public void HidePanel<T>() where T : UIPanel
        {
            var type = GetUrl(typeof(T));

            if (m_UIPanelDic.ContainsKey(type))
            {
                if (m_UIPanelDic[type] != null && m_UIPanelDic[type].IsLoaded)
                {
                    m_UIPanelDic[type].Hide();
                }
            }
        }

        //隐藏 UIPanel
        public void HideAllPanel(bool isHideDontDestroy = false)
        {
            foreach (KeyValuePair<string, UIPanel> panel in m_UIPanelDic)
            {
                if (panel.Value != null && panel.Value.IsLoaded)
                {
                    if (isHideDontDestroy)
                    {
                        if (!panel.Value.isDontDestroyOnLoad)
                        {
                            panel.Value.Hide();
                        }
                    }
                    else
                        panel.Value.Hide();
                }
            }
        }

        public void DestroyPanel<T>()
        {
            UnLoadPanel(GetUrl(typeof(T)));
        }

        void UnLoadPanel(string url)
        {
            Debug.Log(99);
            if (m_UIPanelDic.TryGetValue(url, out UIPanel panel))
            {
                panel.Destroy();
                m_UIPanelDic.Remove(url);
            }
            else
                Debug.LogError("UIPanel not exist: " + url);
        }

        void UnLoadAllPanel()
        {
            foreach (var panel in m_UIPanelDic.Values)
                panel.Destroy();
            m_UIPanelDic.Clear();
        }

        //根据UIPanel的Type获取其对应的url
        string GetUrl(Type t)
        {
            foreach (var pName in panelNames)
                if (pName == t.Name)
                    return pName;
            Debug.LogError($"Cannot found type({t.Name})");
            return null;
        }

        public override void OnApplicationQuit()
        {
            UnLoadAllPanel();
        }

        public int SceneNameCount => m_UIPanelDic?.Count ?? 0;

        public void UnLoadPanelOnLoadScene()
        {
            HideAllPanel(true);
        }
    }
}