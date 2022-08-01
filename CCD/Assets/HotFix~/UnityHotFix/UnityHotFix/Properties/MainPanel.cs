using UnityEngine;
using UnityEngine.UI;

namespace UnityHotFix.Properties
{
    public class MainPanel
    {
        public static Button openBtn;
        public static string name = "MainPanel";
        public static GameObject m_selfGameObject;

        public  static void OnInit()
        {
            m_selfGameObject = ResMgr.Instance.GetTarget<Transform>("MainPanel").gameObject; 

            openBtn = ResMgr.Instance.GetTarget<Button>("MainPanel/OpenBtn");
            openBtn.onClick.AddListener(() => { Debug.Log("OpenBtn"); });
            Debug.Log(m_selfGameObject != null); 
            if (m_selfGameObject != null)
            {
                Debug.Log($"获得{name}的GameObject");
            }
        }

        public static void OnOpen()
        {
        }

        public static void OnClose()
        {
        }
    }
}