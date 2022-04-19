using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializableDic : MonoBehaviour
{
    private static SerializableDic _instance;
    public static SerializableDic Instance => _instance ?? null;
    void Start()
    {
        SerializableDictionary<string, string> dictionary1 = new SerializableDictionary<string, string>();
        dictionary1["1"] = "2";
        dictionary1["2"] = "3";//设置key和value
        dictionary1[0] = "4";//根据index更改数据
        for (int i = 0; i < 2; i++)
        {
            Debug.Log(dictionary1[i]);
        }
        string json = JsonUtility.ToJson(dictionary1);//转换json
        Debug.Log(json);
        dictionary1 = null;
        dictionary1 = JsonUtility.FromJson<SerializableDictionary<string, string>>(json);//json转换字典
        Debug.Log(dictionary1["1"]);
    }
    

}
public class SerializableDictionary<K, V> : ISerializationCallbackReceiver, IEnumerable
{
    [SerializeField]
    private List<K> m_keys;
    [SerializeField]
    private List<V> m_values;
    private Dictionary<K, V> m_Dictionary = new Dictionary<K, V>();

    public V this[K key]//根据key值获得value
    {
        get
        {
            if (!m_Dictionary.ContainsKey(key))//如果没有这个key值
                return default(V);//
            return m_Dictionary[key];//返回该key的value
        }
        set
        {
            m_Dictionary[key] = value;//设置这个key的value
        }
    }
    public V this[int index]//根据key坐标获得value
    {
        get
        {
            m_keys = new List<K>();
            foreach (var item in m_Dictionary)
            {
                m_keys.Add(item.Key);
            }
            if (m_Dictionary.Count > index && index > -1)
            {
                return m_Dictionary[m_keys[index]];
            }
            else return default(V);

        }
        set
        {
            m_keys = new List<K>();
            foreach (var item in m_Dictionary)
            {
                m_keys.Add(item.Key);
            }
            if (m_Dictionary.ContainsKey(m_keys[index]))
            {
                m_Dictionary[m_keys[index]] = value;
            }
        }
    }
    public int Count//字典数量
    {
        get
        {
            return m_Dictionary.Count;
        }
    }

    public IEnumerator GetEnumerator()//迭代器实现
    {
        return ((IEnumerable)m_Dictionary).GetEnumerator();
    }
    public void OnAfterDeserialize()//自定义类序列化
    {
        int length = m_keys.Count;
        m_Dictionary = new Dictionary<K, V>();
        for (int i = 0; i < length; i++)
        {
            m_Dictionary[m_keys[i]] = m_values[i];
        }
        m_keys = null;
        m_values = null;
    }

    public void OnBeforeSerialize()//自定义类反序列化
    {
        m_keys = new List<K>();
        m_values = new List<V>();
        foreach (var item in m_Dictionary)
        {
            m_keys.Add(item.Key);
            m_values.Add(item.Value);
        }
    }
}
