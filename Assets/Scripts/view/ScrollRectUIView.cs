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
   // [HideInInspector]
   // public ScrollRectModel SRM;
    [HideInInspector]
    public ScrollRectControl SRC;
  /*  [HideInInspector]
    public LuaTable scriptEnv;
    public TextAsset luaScript;
    [HideInInspector]
    internal static LuaEnv luaEnv = new LuaEnv();*/
    void Awake()
    {
     
        SRC=transform.gameObject.GetComponent<ScrollRectControl>();
      //  SRC.setNewItemInView += setRecordItem;
       // SRC.deletaItemInView += disableItem;
       // SRC.setContent += setContent;

      /*  scriptEnv = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        scriptEnv.Set("self", this);
        scriptEnv.Set("listContent", listContent);
        scriptEnv.Set("scrollRect", scrollRect);
        luaEnv.DoString(luaScript.text, "ScrollControl.Lua", scriptEnv);*/
        scrollRect.GetComponent<ScrollRect>().onValueChanged.AddListener((value) => { onDrag(value.y); });
    }


    public void onDrag(float y)
    {
        float posY = listContent.GetComponent<RectTransform>().anchoredPosition3D.y;
        SRC.onRecordDrag(posY);
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
      //  ab.onButtonRightClick += SRM.deleteItem;
        Sprite sprit = AvatarControl._instance.loadSpriteFromAssetBundle(go.name);
        go.GetComponent<Image>().sprite = sprit;
        go.GetComponent<Toggle>().group = scrollRect.GetComponent<ToggleGroup>();
        
    }
    public void chengeMesh(string state,string a ,string b){
        //Debug.Log("in ScrollUIView be left click");
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
       // Debug.Log(go==null);
        go.SetActive(false);
    }

  //  public GameObject getListContent() { return listContent == null ? null : listContent; }

 //   public GameObject getScrollRect() { return this.scrollRect; }
}
