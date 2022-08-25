using UnityEngine;

namespace UnityHotFix.Properties
{
    // 不继承MonoBehaviour 
    public class BaseSingleton<T> where T : BaseSingleton<T>, new()
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

 
}