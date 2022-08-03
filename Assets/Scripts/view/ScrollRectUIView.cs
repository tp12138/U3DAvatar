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
     public ScrollRectControl SRC;
    void Awake()
    {
        SRC = transform.gameObject.GetComponent<ScrollRectControl>();
    }
    void Start()
    {
        scrollRect.GetComponent<ScrollRect>().onValueChanged.AddListener((value) => { SRC.onRecordDrag(value.y); });
    }

    [LuaCallCSharp]
    public void setRecordItem(int index,GameObject go,string itemName,Vector2 itemSize,Vector3 itemPos)
    {
        go.transform.SetParent(listContent.GetComponent<Transform>());
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 1f);
        rt.anchoredPosition3D = itemPos;
        rt.sizeDelta = itemSize;
        go.SetActive(true);
        go.name = itemName;
        AvatarButton ab = go.GetComponent<AvatarButton>();
        ab.onButtonLeftClick += chengeMesh;
        ab.onButtonRightClick += AvatarControl._instance.remove;
        ab.onButtonRightClick += SRC.deleteItem;
        Sprite sprit = AvatarControl._instance.loadSpriteFromAssetBundle(go.name);
        go.GetComponent<Image>().sprite = sprit;
        go.GetComponent<Toggle>().group = scrollRect.GetComponent<ToggleGroup>();
 
    }
    public void chengeMesh(string state,string a ,string b){
        AvatarControl._instance.tryChangePeople(a,b);
    }
    public void setContent(int width, int height)
    {
        RectTransform rtf = listContent.GetComponent<RectTransform>();
        rtf.sizeDelta = new Vector2(width, height);
        rtf.localPosition = Vector3.zero;
   }
    [LuaCallCSharp]
    public void disableItem(GameObject go)
    {
        go.SetActive(false);
    }

    [LuaCallCSharp]
    public void destroyItem(GameObject go)
    {
        Destroy(go);
    }
}
