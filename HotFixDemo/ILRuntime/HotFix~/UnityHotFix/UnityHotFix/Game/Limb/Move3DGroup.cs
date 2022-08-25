using System.Collections.Generic;
using UnityEngine;
using UnityHotFix.UI.Limb;

namespace UnityHotFix.Game.Limb
{
    /// <summary>
    /// 3d场景的移动
    /// </summary>
    public class Move3DGroup : MoveGroup
    {
        public Vector3 oldPos;
        public float offsetValue;
        public List<Transform> traGroup = new List<Transform>();


        public override void Init(Transform tra)
        {
            base.Init(tra);
            for (int i = 0; i < parent.childCount; i++)
            {
                Object.Destroy(parent.GetChild(i).gameObject);
            }  
            LimbUnityLogic.fixedUpdate += Move;
        }

        public void Created3DBackGround(GameObject prefab)
        {
            traGroup.Clear();
            for (int i = 0; i < 3; i++)
            {
                var obj = GameObject.Instantiate(prefab, parent);
                obj.transform.localPosition = new Vector3(i * 5, 0);
                traGroup.Add(obj.transform);
                oldPos = obj.transform.localPosition;
            }
        }

        public override void SetValue(int _speed, bool _isMove = true)
        {
            base.SetValue(_speed, _isMove);
        }

        public void Move()
        {
            if (!isMove)
            {
                return;
            }

            for (int i = 0; i < traGroup.Count; i++)
            {
                if (offsetValue > traGroup[i].localPosition.x)
                {
                    traGroup[i].localPosition += speed * percentage * Vector3.left;
                }
                else
                {
                    traGroup[i].localPosition = oldPos;
                }
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            LimbUnityLogic.fixedUpdate -= Move;
        }
    }
}