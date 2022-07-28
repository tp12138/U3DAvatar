using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using UnityEngine.UI;
public class ScrollRectUIView : MonoBehaviour
{
    public GameObject listContent;
    public GameObject scrollRect;
    public string partName;
    [HideInInspector]
    public ScrollRectModel SRM;
    [HideInInspector]
    public ScrollRectControl SRC;
    
    void Awake()
    {
        SRM = transform.gameObject.GetComponent<ScrollRectModel>();
        SRC=transform.gameObject.GetComponent<ScrollRectControl>();
        SRM.setRecordItem += setRecordItem;
        SRM.removeItem += disableItem;
    }
    [LuaCallCSharp]
    public void setRecordItem(int index,GameObject go)
    {
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 1f);
        rt.anchoredPosition3D =SRC.getItemPosition(index);
        rt.sizeDelta = new Vector2(SRM.itemWidth * 2, SRM.itemWidth * 2);
        go.SetActive(true);
        go.name = SRM.datas[index];
        AvatarButton ab = go.GetComponent<AvatarButton>();
        ab.onButtonLeftClick += AvatarControl._instance.OnChangePeople;
        ab.onButtonRightClick += AvatarControl._instance.removeMesh;
        ab.onButtonRightClick += SRM.deleteItem;
        Sprite sprit = AvatarControl._instance.loadSpriteFromAssetBundle(go.name);
        go.GetComponent<Image>().sprite = sprit;
        go.GetComponent<Toggle>().group = scrollRect.GetComponent<ToggleGroup>();
    }

    public void setContent(int width, int height)
    {
        RectTransform rtf = listContent.GetComponent<RectTransform>();
        rtf.sizeDelta = new Vector2(width, height);
        rtf.localPosition = Vector3.zero;
    }

    public void disableItem(GameObject go)
    {
        go.SetActive(false);
    }
    
}
