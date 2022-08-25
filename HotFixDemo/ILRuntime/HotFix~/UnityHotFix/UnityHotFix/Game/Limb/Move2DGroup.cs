using UnityEngine;

namespace UnityHotFix.Game.Limb
{
    /// <summary>
    /// 2d场景的移动
    /// </summary>
    public class Move2DGroup : MoveGroup
    {
        private MeshRenderer meshRenderer;
        private static readonly int scrollX = Shader.PropertyToID("_ScrollX");

        public override void Init(Transform tra)
        {
            base.Init(tra);
            meshRenderer = parent.GetComponentInChildren<MeshRenderer>();
        }

        public override void SetValue(int _speed, bool _isMove = true)
        {
            base.SetValue(_speed, _isMove);
            if (isMove)
            {
                if (meshRenderer == null) return;
                if (meshRenderer.material != null)
                    meshRenderer.material.SetFloat(scrollX, speed * percentage);
            }
            else
            {
                if (meshRenderer == null) return;
                if (meshRenderer != null)
                    meshRenderer.material.SetFloat(scrollX, 0);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}