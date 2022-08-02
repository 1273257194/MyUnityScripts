using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityHotFix.Tool;

namespace UnityHotFix.UI
{
    public class UIViewManager : ManagerBase<UIViewManager>
    {
        private List<UIView> m_UIViewList = new List<UIView>();

        public List<string> panelNames = new List<string>();
        public UIView currentView; //当前显示的页面 
        Dictionary<string, UIView> m_UIViewDic; //存放所有存在在场景中的UIPanel  

        Transform m_UICanvas;
        public Transform m_PopupUIView;
        public Transform m_LongShowPanel;

        public override void Init()
        {
            base.Init();
            m_UICanvas = GameObject.Find("Canvas").transform;
            m_PopupUIView = m_UICanvas.transform.Find("PopupPanel");
            m_LongShowPanel = m_UICanvas.transform.Find("LongShowPanel");
            LoadInit();
        }


        /// <summary>
        /// 获取需要的所有面板名字并对字典赋值
        /// </summary>
        public void LoadInit()
        {
            //对字典的key赋值
            m_UIViewDic = new Dictionary<string, UIView>();
            panelNames = ResMgr.Instance.loadPool;
            for (int i = 0; i < panelNames.Count; i++)
            {
                if (!m_UIViewDic.ContainsKey(panelNames[i]))
                {
                    m_UIViewDic.Add(panelNames[i], null);
                }
            }
        }

        public void ShowView<T>() where T : UIView
        {
            ShowView<T>(null, null);
        }

        public void ShowView<T>(Action<T> callback) where T : UIView
        {
            ShowView(callback, null);
        }

        public void ShowView<T>(object data) where T : UIView
        {
            ShowView<T>(null, data);
        }

        //显示一个UIPanel，参数为回调和自定义传递数据
        public void ShowView<T>(Action<T> callback, object data) where T : UIView
        {
            string url = GetUrl(typeof(T));
            if (!string.IsNullOrEmpty(url))
            {
                LoadView<T>(url, data, () =>
                {
                    var panel = ShowView(url);
                    callback?.Invoke(panel as T);
                });
            }
        }

        //显示UIPanel
        UIView ShowView(string url)
        {
            if (m_UIViewDic.TryGetValue(url, out UIView view))
            {
                view = m_UIViewDic[url];
                if (!view.IsVisible)
                {
                    //currentView?.Hide();  
                    view.Show();
                    currentView = view;
                }
                else
                    Debug.Log("UIPanel is visible:" + url);
            }
            else
                Debug.LogError("UIPanel not loaded:" + url);

            return view;
        }

        //加载UIPanel对象
        public void LoadView<T>(string url, object data, Action callback)
        {
            if (m_UIViewDic.TryGetValue(url, out UIView uiView) && uiView != null)
            {
                if (uiView.IsLoaded)
                    callback?.Invoke();
            }
            else
            {
                uiView = CreateView(url, typeof(T));
                if (uiView == null)
                    Debug.LogError("UIPanel not exist: " + url);
                else
                {
                    m_UIViewDic[url] = uiView; 
                    Debug.Log($"LoadView URL: {uiView.URL}");
                    uiView.Load(() =>
                    {
                        if (uiView.IsLoaded)
                        {
                            var rectTransform = uiView.rectTransform;
                            switch (uiView.viewLayer)
                            {
                                case ViewLayer.LongShow:
                                    rectTransform.SetParent(m_LongShowPanel);
                                    break;
                                case ViewLayer.Popup:
                                    rectTransform.SetParent(m_PopupUIView);
                                    break;
                            }

                            rectTransform.pivot = new Vector2(0.5f, 0.5f);
                            rectTransform.anchorMax = new Vector2(1f, 1f);
                            rectTransform.anchorMin = new Vector2(0, 0);
                            rectTransform.anchoredPosition = new Vector2(0, 0);
                            rectTransform.offsetMax = new Vector2(0, 0);
                            rectTransform.offsetMin = new Vector2(0, 0);

                            callback?.Invoke();
                        }
                        else
                            m_UIViewDic.Remove(url);
                    });
                }
            }
        }

        //实例化UIPanel对象
        UIView CreateView(string url, Type t)
        {
            if (!m_UIViewDic.ContainsKey(url)) return null;
            var view = CreateView(t, url) as UIView;
            return view;

            ////var panel = CreateInstance<UIPanel>(url);
            ////UIViewManager.Instance.AddUIView(panel as UIView);
            //return panel;
        }

        //隐藏当前显示的UIPanel
        public void HideView()
        {
            currentView.Hide();
        }

        //隐藏找到的的UIPanel
        public void HideView<T>() where T : UIView
        {
            var type = GetUrl(typeof(T));

            if (m_UIViewDic.ContainsKey(type))
            {
                if (m_UIViewDic[type] != null && m_UIViewDic[type].IsLoaded)
                {
                    m_UIViewDic[type].Hide();
                }
            }
        }

        public void DestroyView<T>()
        {
            UnLoadView(GetUrl(typeof(T)));
        }

        void UnLoadView(string url)
        {
            if (m_UIViewDic.TryGetValue(url, out UIView view))
            {
                view.Destroy();
                m_UIViewDic.Remove(url);
            }
            else
                Debug.LogError("UIPanel not exist: " + url);
        }

        void UnLoadAllView()
        {
            foreach (var view in m_UIViewDic.Values)
                view.Destroy();
            m_UIViewDic.Clear();
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
            UnLoadAllView();
        }


        public override void Update()
        {
            base.Update();
            // Debug.Log("UIView -> Update");
            for (int i = 0; i < m_UIViewList.Count; i++)
            {
                var mUIView = m_UIViewList[i];
                if (mUIView.isWillDestroy)
                {
                    mUIView.DestroyImmediately();
                    m_UIViewList.RemoveAt(i);
                    i--;
                    continue;
                }

                if (mUIView.IsVisible && mUIView.IsLoaded)
                {
                    mUIView.Update();
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            // Debug.Log("UIView -> FixedUpdate");
            for (int i = 0; i < m_UIViewList.Count; i++)
            {
                if (m_UIViewList[i].IsVisible)
                {
                    m_UIViewList[i].FixedUpdate();
                }
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            // Debug.Log("UIView -> LateUpdate");
            for (int i = 0; i < m_UIViewList.Count; i++)
            {
                if (m_UIViewList[i].IsVisible)
                {
                    m_UIViewList[i].LateUpdate();
                }
            }
        }

        public UIView CreateView(Type type, params object[] args)
        {
            var view = Activator.CreateInstance(type, args) as UIView;

            if (view != null)
            {
                m_UIViewList.Add(view);
            }

            return view;
        }

        public int SceneNameCount => m_UIViewDic?.Count ?? 0;

        public void DestroyAll()
        {
            for (var i = m_UIViewList.Count - 1; i >= 0; i--)
            {
                m_UIViewList[i].Destroy();
            }
        }

        //隐藏找到的的UIPanel
        public void HideAllView(bool isHideDontDestroy = false)
        {
            foreach (KeyValuePair<string, UIView> panel in m_UIViewDic)
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

        public void DestroyViewOnLoadScene()
        { 
            HideAllView(true);


            // var destroyObjs = m_UIViewDic.Where(x =>
            // {
            //     if (x.Value != null)
            //     {
            //         return !x.Value.isDontDestroyOnLoad;
            //     }
            //
            //     return false;
            // }).Select(x => x.Value).ToList();
            // for (var i = 0; i < destroyObjs.Count; i++)
            // {
            //     Debug.Log("11111111111111111111111111");
            //     destroyObjs[i].DestroyImmediately();
            //     destroyObjs[i] = null;
            //     Debug.Log("11111111111111111111111111111");
            // }
            //
            // Debug.Log(33333);
            // destroyObjs.Clear();
            //
            //
            // Debug.Log(11111111);
            // var destroyObj = m_UIViewList.FindAll(x => !x.isDontDestroyOnLoad).ToList();
            // for (var i = 0; i < destroyObj.Count; i++)
            // {
            //     destroyObj[i].Destroy();
            //     destroyObj[i] = null;
            // }
            //
            // m_UIViewList.RemoveAll(x => !x.isDontDestroyOnLoad);
        }
    }
}