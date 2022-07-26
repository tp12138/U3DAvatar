using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using UnityEngine.UI;
public class ScrollRectControl : MonoBehaviour
{
   public ScrollRectUIView scrollView;
   public ScrollRectModel scrollModel;
   // private Action<int, GameObject> setRecordItem;
    [HideInInspector]
    public bool isDragIng;
    [HideInInspector]
    public bool isLoadingRecord;
    [HideInInspector]
    public LuaTable scriptEnv;
    public TextAsset luaScript;
    [HideInInspector]
    internal static LuaEnv luaEnv = new LuaEnv();
    // Start is called before the first frame update
    void Start()
    {
        scrollView = this.gameObject.GetComponent<ScrollRectUIView>();
        scrollModel =this.gameObject.GetComponent<ScrollRectModel>();
        scriptEnv = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        scriptEnv.Set("self", this);
        scriptEnv.Set("listContent", scrollView.listContent);
        scriptEnv.Set("scrollRect", scrollView.scrollRect);
        luaEnv.DoString(luaScript.text, "LuaTestScript", scriptEnv);
        //setRecordItem = luaEnv.Global.Get<Action<int, GameObject>>("setRecordItem");
        scrollView.scrollRect.GetComponent<ScrollRect>().onValueChanged.AddListener((value) => { onRecordDrag(value.y); });
    }
    void OnEnable()
    {
        if (scriptEnv != null)
        {
            var temp = scrollView.listContent.GetComponentsInChildren<Transform>();
            for (int i = 1; i < temp.Length; i++)
                Destroy(temp[i].gameObject);
            luaEnv.DoString(luaScript.text, "LuaTestScript", scriptEnv);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onRecordDrag(float y)
    {
        //Debug.Log(scrollModel.datas == null);
        if (scrollModel.datas.Count < scrollModel.col * scrollModel.row) return;
        if (isDragIng) return;
        isDragIng= true;
        int indexNowRow=scrollModel.getIndex(y,scrollModel.cell);
        scrollModel.removeUnUseItem(indexNowRow);
        scrollModel.generaNewItem(indexNowRow);
        isDragIng = false;
    }

}
