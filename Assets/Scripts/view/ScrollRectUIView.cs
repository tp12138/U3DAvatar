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
    void Awake()
    {
        SRM = transform.gameObject.GetComponent<ScrollRectModel>();
        SRM.setNewItem += setRecordItem;
    }

  
    [LuaCallCSharp]
    public int getPos_Y(int index, int col, int cell)
    {
        int sizeY = index / col * cell;
        return -sizeY;
    }
    [LuaCallCSharp]
    public void setRecordItem(int index,GameObject go,ScrollRectModel srm)
    {
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 1f);
        int x =(int)(index % srm.col*srm.cell + 0.5 * srm.cell);
        int y = getPos_Y(index,srm.col,srm.cell);
        rt.anchoredPosition3D = new Vector3(x, y, 0);
        rt.sizeDelta = new Vector2(srm.itemWidth * 2, srm.itemWidth * 2);
        go.SetActive(true);
        go.name = srm.datas[index];
        Sprite sprit = AvatarControl._instance.loadSpriteFromAssetBundle(srm.datas[index]);
        go.GetComponent<Image>().sprite = sprit;
        go.GetComponent<Toggle>().group = scrollRect.GetComponent<ToggleGroup>();
    }
}
