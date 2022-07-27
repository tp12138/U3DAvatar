using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using UnityEngine.UI;
public class ScrollRectControl : MonoBehaviour
{
   [HideInInspector]
   public ScrollRectUIView scrollView;
    [HideInInspector]
   public ScrollRectModel scrollModel;
   [HideInInspector]
   public List<string> datas = new List<string>();
   [HideInInspector]
   public int col;
   [HideInInspector]
   public int row;
   [HideInInspector]
   public int cell;
   [HideInInspector]
   public int chink;
    [HideInInspector]
    public bool isDragIng;
    [HideInInspector]
    public bool isLoadingRecord;
    [HideInInspector]
    public Dictionary<GameObject, int> datasAndIndex;
    [HideInInspector]
    public List<GameObject> needDispose;
    [HideInInspector]
    public GameObject item;
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
    public void onRecordDrag(float y)
    {
        //Debug.Log(scrollModel.datas == null);
        if (scrollModel.datas.Count <col * row) return;
        if (isDragIng) return;
        isDragIng= true;
        int indexNowRow=getIndex(y,cell);
        int startNum = indexNowRow * col;
        int endNum = (indexNowRow + row) * col;
        scrollModel.removeUnUseItem(startNum,endNum);
        scrollModel.generaNewItem(startNum, endNum);
        isDragIng = false;
    }

    public int getIndex(float y, int cell)
    {
        int index = 0;
        index = (int)(y / cell);
        if (y < 0) index = 0;
        return index;
    
     }
    public int getPos_Y(int index, int col, int cell)
    {
        int sizeY = index / col * cell;
        return -sizeY;
    }

    public Vector3 getItemPosition(int index)
    {
        int x = (int)(index % col * cell + 0.5 * cell);
        int y = getPos_Y(index, col, cell);
        Vector3 vt3 = new Vector3(x, y, 0);
        return vt3;
    }
}
