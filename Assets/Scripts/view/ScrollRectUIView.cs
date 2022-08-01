using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using UnityEngine.UI;
public class ScrollRectUIView : MonoBehaviour
{
   // public GameObject listContent;
   // public GameObject scrollRect;
   // public string partName;
   // [HideInInspector]
   // public ScrollRectModel SRM;
    [HideInInspector]
    public ScrollRectControl SRC;
    [HideInInspector]
    public AvatarModel avatarModel;
    [HideInInspector]
    public RoleUIViev roleUIViev;
    [HideInInspector]
    public static AvatarControl _instance;
    [HideInInspector]
    public AssetBundle ab;    //包含骨架的AB包
    [HideInInspector]
    private AssetBundle prefabAB; //包含替换资源和UI贴图的AB包
    [HideInInspector]
    public Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> skinnedSourceDict;//可替换资源的部位->编号->蒙皮字典
    public Action<string, string, string> onRoleChange;
    public Action<GameObject> onInitNewRole;
    public Action<string> onAddNewPart;
    public TextAsset luaScript;//Lua文件资源
    void Awake()
    {
     
        SRC=transform.gameObject.GetComponent<ScrollRectControl>();
        SRC.setNewItemInView += setRecordItem;
        SRC.deletaItemInView += disableItem;
        SRC.setContent += setContent;
       
    }
    [LuaCallCSharp]
    public void setRecordItem(int index,GameObject go,ScrollRectModel SRM)
    {
        go.transform.SetParent(SRC.listContent.GetComponent<Transform>());
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 1f);
        rt.anchoredPosition3D =SRC.getItemPosition(index);
        rt.sizeDelta = new Vector2(SRM.itemWidth * 2, SRM.itemWidth * 2);
        go.SetActive(true);
        go.name = SRM.datas[index];
        AvatarButton ab = go.GetComponent<AvatarButton>();
        ab.onButtonLeftClick += chengeMesh;
        ab.onButtonRightClick += AvatarControl._instance.remove;
        ab.onButtonRightClick += SRM.deleteItem;
        Sprite sprit = AvatarControl._instance.loadSpriteFromAssetBundle(go.name);
        go.GetComponent<Image>().sprite = sprit;
        go.GetComponent<Toggle>().group = SRC.scrollRect.GetComponent<ToggleGroup>();
        
    }
    public void chengeMesh(string state,string a ,string b){
        //Debug.Log("in ScrollUIView be left click");
        AvatarControl._instance.tryChangePeople(a,b);
    }
    public void setContent(int width, int height)
    {
        RectTransform rtf = SRC.listContent.GetComponent<RectTransform>();
        rtf.sizeDelta = new Vector2(width, height);
        rtf.localPosition = Vector3.zero;
   }

    public void disableItem(GameObject go)
    {
        go.SetActive(false);
    }

}
