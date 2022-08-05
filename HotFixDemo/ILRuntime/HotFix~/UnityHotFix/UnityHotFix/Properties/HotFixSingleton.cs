using UnityEngine;

public class HotFixSingleton<T> where T : class, new()
{
    private static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                T t = new T();
                if (t is MonoBehaviour)
                {
                    //Debug.LogError("该类不是普通类，需要继承MonoSingleton");
                    return null;
                }
                m_instance = t;
            }
            return m_instance;
        }
    }
}