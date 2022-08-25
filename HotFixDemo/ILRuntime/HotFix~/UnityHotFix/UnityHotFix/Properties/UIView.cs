using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace UnityHotFix.UI
{
    public enum ViewLayer
    {
        Default,
        LongShow,
        Popup
    }
    /// <summary>
    /// 长时间显示和弹窗使用UIView基类 重写三个Update可实现Mono内的Update
    /// </summary>
    public class UIView : IView
    {
        /// <summary>
        /// 初始化属性
        /// </summary>
        /// <param name="url">当前面板的名字 需要和Addressable内的名字相同</param>
        public UIView(string url)
        {
            this.URL = url;
        }

        public string URL { private set; get; }
        /// <summary>
        /// 当前面板的对象
        /// </summary>
        public GameObject gameObject { set; get; }
        public Transform transform { set; get; }
        public RectTransform rectTransform { set; get; }
        /// <summary>
        /// 是否已经被加载过
        /// </summary>
        public bool IsLoaded => gameObject != null;
        /// <summary>
        /// 需要显示在什么层级
        /// </summary>
        public ViewLayer viewLayer = ViewLayer.Default;
        /// <summary>
        /// 在加载场景后是否需要关闭
        /// </summary>
        public bool isDontDestroyOnLoad = false;

        public bool IsVisible
        {
            get => IsLoaded && gameObject.activeSelf;
            set
            {
                if (!IsLoaded) return;
                if (gameObject)
                {
                    gameObject.SetActive(value);
                }
            }
        }

        internal bool isWillDestroy;

        public virtual void Init()
        {
            IsVisible = false;
        }

        public virtual void Show()
        {
            IsVisible = true;
        }

        public virtual void Update()
        {
        }

        public virtual void LateUpdate()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Hide()
        {
            IsVisible = false;
        }

        public virtual void Destroy()
        { 
            isWillDestroy = true; 
            if (IsVisible)
            { 
                Hide();
            }
        }

        public void DestroyImmediately()
        {
            if (!isWillDestroy)
            {
                Destroy();
            }

            if (gameObject != null) Object.Destroy(gameObject);
            gameObject = null;
            transform = null;
            rectTransform = null;
        }

        public virtual void Load(Action callBack = null)
        {
             Addressables.LoadAssetAsync<GameObject>(URL).Completed += (prefab) =>
             {
                 gameObject = Object.Instantiate(prefab.Result) as GameObject;
                 if (gameObject)
                 {
                     transform = gameObject.transform;
                     rectTransform = gameObject.GetComponent<RectTransform>();
                     Init();
                     callBack?.Invoke();
                 }
             };
        }
    }
}