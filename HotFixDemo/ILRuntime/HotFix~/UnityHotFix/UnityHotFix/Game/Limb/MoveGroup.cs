using System.Collections.Generic;
using UnityEngine;

namespace UnityHotFix.Game.Limb
{
    public class MoveGroup
    {
        public Transform parent;

        public int speed;

        public bool isMove;
        public float percentage;

        public virtual void Init(Transform tra)
        {
            parent = tra;
            SetValue(0, false);
        }

        public virtual void SetValue(int _speed, bool _isMove = true)
        {
            speed = _speed;
            isMove = _isMove;
            if (speed < 0)
            {
                speed = 0;
            }
        }

        public virtual void OnDestroy()
        {
        }
    }
}