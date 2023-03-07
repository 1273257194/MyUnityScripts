using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
/// <summary>
/// 测试toggle点击后不能通过 EventSystem.current.currentSelectedGameObject获取到对象，通过RaycastAll获取其中的第一个对象来获取到点击的对象
/// </summary>
public class ClickListener : MonoBehaviour
{
    List<RaycastResult> list = new List<RaycastResult>();
    /// <summary>
    /// 点中ui
    /// </summary>
    private GameObject ClickUI()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            return null;
        }

        var currentObj = EventSystem.current.currentSelectedGameObject;

        if (currentObj!=null)
        {
            return currentObj;
        } 
        //场景中的EventSystem

        PointerEventData eventData = new PointerEventData(EventSystem.current);

        //鼠标位置
        eventData.position = Input.mousePosition;

        //调用所有GraphicsRacaster里面的Raycast，然后内部会进行排序，
        //直接拿出来，取第一个就可以用了
        EventSystem.current.RaycastAll(eventData, list);

        //这个函数抄的unity源码的，就是取第一个值
        var raycast = FindFirstRaycast(list);

        //获取父类中事件注册接口
        //如Button，Toggle之类的，毕竟我们想知道哪个Button被点击了，而不是哪张Image被点击了
        //当然可以细分为IPointerClickHandler, IBeginDragHandler之类细节一点的，各位可以自己取尝试
        var go = ExecuteEvents.GetEventHandler<IEventSystemHandler>(raycast.gameObject);
		
        //既然没拿到button之类的，说明只有Image挡住了，取点中结果即可
        if (go == null)
        {
            go = raycast.gameObject;
        }
        return go;

		
    }

    /// <summary>
    /// Return the first valid RaycastResult.
    /// </summary>
    private RaycastResult FindFirstRaycast(List<RaycastResult> candidates)
    {
        for (var i = 0; i < candidates.Count; ++i)
        {
            if (candidates[i].gameObject == null)
                continue;

            return candidates[i];
        }
        return new RaycastResult();
    }
}