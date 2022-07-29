using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using UnityEngine.UI;
public class ScrollRectControl : MonoBehaviour
{
   [HideInInspector]
   public ScrollRectModel scrollModel;
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
    public LuaTable scriptEnv;
    public TextAsset luaScript;
    public string partName;
    [HideInInspector]
    internal static LuaEnv luaEnv = new LuaEnv();
    [HideInInspector]
    public Action<int, GameObject,ScrollRectModel> setNewItemInView;
    [HideInInspector]
    public Action<int, int> setContent;
    public GameObject listContent;
    public GameObject scrollRect;
    [HideInInspector]
    public Action<GameObject> deletaItemInView;
    // Start is called before the first frame update
    void Start()
    {
        //scrollView = this.gameObject.GetComponent<ScrollRectUIView>();
        scrollModel =this.gameObject.GetComponent<ScrollRectModel>();
        scrollModel.setRecordItem += setNewItem;
        scrollModel.removeItem += deleteItem;
        scriptEnv = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        scriptEnv.Set("self", this);
        scriptEnv.Set("listContent", listContent);
        scriptEnv.Set("scrollRect", scrollRect);
        luaEnv.DoString(luaScript.text, "ScrollControl.Lua", scriptEnv);
        scrollRect.GetComponent<ScrollRect>().onValueChanged.AddListener((value) => { onRecordDrag(value.y); });
    }
    void OnEnable()
    {
        if (scriptEnv != null)
        {
            
            var temp = listContent.GetComponentsInChildren<Transform>();
            for (int i = 1; i < temp.Length; i++)
                Destroy(temp[i].gameObject);
            luaEnv.DoString(luaScript.text, "LuaTestScript", scriptEnv);
        }
    }
    public void onRecordDrag(float y)
    {
        if (scrollModel.datas.Count <=col * row) return;
        if (isDragIng) return;
        float posY = listContent.GetComponent<RectTransform>().anchoredPosition3D.y;
        if (Math.Abs(posY) < cell / 2) return;
        isDragIng= true;
        int indexNowRow = getIndex(posY, cell);
        int startNum = indexNowRow * col;
        int endNum = (indexNowRow + row) * col;
        scrollModel.removeUnUseItem(startNum,endNum);
        scrollModel.generaNewItem(startNum, endNum);
        isDragIng = false;
    }

    private void setNewItem(int index, GameObject go,ScrollRectModel srm)
    {
        setNewItemInView(index,go,srm);
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

    private void deleteItem(GameObject go)
    {
       
        scrollModel.datasAndIndex.Remove(go);
        deletaItemInView(go);
    }
    public void onchan(Vector2 t)
    {
        Debug.Log(t.y);
    }
}
