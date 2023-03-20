using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HotUpdate.HandTraining1
{
    public class AnimatorSlider : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public bool drag = false;
        public Animator animator;
        AnimatorStateInfo animatorStateInfo;
        public Slider component;
        public Text startTimeText, endTimeText;
        public Action dragUpEvent;
        public Action<bool> dragEvent;

        public void Awake()
        {
            startTimeText = transform.Find("StartTimeText").GetComponent<Text>();
            endTimeText = transform.Find("EndTimeText").GetComponent<Text>();
        }

        private void OnEnable()
        {
            animator = AnimatorController.Instance.currentAnimator;
            component.value = 0;
            drag = false;
        }

        private void Start()
        {
            // SetTimeText();
        }

        /// <summary>
        /// 设置进度条上的动画时间
        /// </summary>
        public async void SetTimeText()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            startTimeText.text = $"00:00";
            Debug.Log(animator.GetCurrentAnimatorClipInfo(0).Length);
            TimeSpan span = TimeSpan.FromSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            endTimeText.text = $"{span.Minutes:00}:{span.Seconds:00}";
        }

        private void Update()
        {
            // if (Input.GetMouseButtonUp(0))
            // {
            //     drag = false;
            // } 
            if (!drag && !AnimatorController.Instance.isPause)
            {
                if (animator)
                {
                    animator.speed = 1;
                    //获取当前动画片段信息
                    animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                    //normalizedTime 是个0-1的值 代表进度
                    //去除loop模式下超出整数的部分
                    var normalizedTime = animatorStateInfo.normalizedTime;
                    var value = normalizedTime - (int) normalizedTime;
                    component.value = value;

                    TimeSpan span = TimeSpan.FromSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * value);
                    startTimeText.text =  $"{span.Minutes:00}:{span.Seconds:00}"; 
                }
            }
            else
                animator.speed = 0;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            drag = true;
            dragEvent?.Invoke(!drag);
        }


        public void UpdateAuto_Anim(float normalizedTime)
        {
            if (animator != null)
            {
                animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                //从指定进度开始播放
                animator.Play(animatorStateInfo.fullPathHash, 0, normalizedTime);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            drag = true;
            UpdateAuto_Anim(component.value);
            dragEvent?.Invoke(!drag);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            drag = true;
            UpdateAuto_Anim(component.value);
            dragEvent?.Invoke(!drag);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            drag = false;
            AnimatorController.Instance.CheckSection();
            dragEvent?.Invoke(!drag);
            dragUpEvent?.Invoke();
        }
    }
}