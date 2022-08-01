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

    public class UIView : IView
    {
        public UIView(string url)
        {
            this.URL = url;
        }

        public string URL { private set; get; }
        public GameObject gameObject { set; get; }
        public Transform transform { set; get; }
        public RectTransform rectTransform { set; get; }
        public bool IsLoaded => gameObject != null;

        public ViewLayer viewLayer = ViewLayer.Default;
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