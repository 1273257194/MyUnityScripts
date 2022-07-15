using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slotpart.Tools
{
    // 不继承MonoBehaviour 
    public class BaseSingleton<T>  where T : BaseSingleton<T>, new()
    {
        private static T instance;
 
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    if (instance == null)
                    {
                        instance = new T();
                        instance.Init();
                    }
                }
                return instance;
            }
        }
 
        virtual public void Init()
        {
        }
    }
 
// 继承自MonoBehaviour 
    public class MonoSingleton<T> : MonoBehaviour where T:MonoSingleton<T> 
    {
        private static T instance;
 
        public static T Instance {
            get
            {
                if(instance == null)
                {
                    // 在场景中根据类型查找引用
                    instance = FindObjectOfType<T>();
                    if(instance == null)
                    {
                        // 创建脚本对象
                        new GameObject("Singleton of "+typeof(T)).AddComponent<T>();
                    }
                    else
                    {
                        instance.Init();
                    }
                
                }
                return instance;
            }
        }
        public void Awake()
        {
            if(instance == null)
            {
                instance = this as T;
                Init();
            }
        }
        public virtual void Init()
        {
 
        }
    }
}