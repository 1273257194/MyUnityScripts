using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AnchorController 
{
    [MenuItem("Tools/自适应锚点")]
    private static void SelectionAnchor()
    {
        GameObject[] objs = Selection.gameObjects;
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].GetComponent<RectTransform>() == null)
                continue;
            AnchorCon(objs[i]);
        }
    }

    private static void AnchorCon(GameObject obj)
    {
        //位置信息
        Vector3 partentPos = obj.transform.parent.position;
        Vector3 localPos = obj.transform.position;
        //------获取rectTransform----
        RectTransform partentRect = obj.transform.parent.GetComponent<RectTransform>();
        RectTransform localRect = obj.GetComponent<RectTransform>();
        float partentWidth = partentRect.rect.width;
        float partentHeight = partentRect.rect.height;
        float localWidth = localRect.rect.width * 0.5f;
        float localHeight = localRect.rect.height * 0.5f;
        //---------位移差------
        float offX = localPos.x - partentPos.x;
        float offY = localPos.y - partentPos.y;

        float rateW = offX / partentWidth;
        float rateH = offY / partentHeight;
        localRect.anchorMax = localRect.anchorMin = new Vector2(0.5f + rateW, 0.5f + rateH);
        localRect.anchoredPosition = Vector2.zero;

        partentHeight = partentHeight * 0.5f;
        partentWidth = partentWidth * 0.5f;
        float rateX = (localWidth / partentWidth) * 0.5f;
        float rateY = (localHeight / partentHeight) * 0.5f;
        localRect.anchorMax = new Vector2(localRect.anchorMax.x + rateX, localRect.anchorMax.y + rateY);
        localRect.anchorMin = new Vector2(localRect.anchorMin.x - rateX, localRect.anchorMin.y - rateY);
        localRect.offsetMax = localRect.offsetMin = Vector2.zero;
    }

}