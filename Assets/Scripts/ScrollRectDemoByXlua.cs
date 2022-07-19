
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using UnityEngine.UI;
[Hotfix]
[CSharpCallLua]
[LuaCallCSharp]
public class ScrollRectDemoByXlua : MonoBehaviour
{
    public GameObject listContent;
    public GameObject scrollRect;
    [HideInInspector]
    public List<string> datas =new List<string>();
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
    private Action<int, GameObject> setRecordItem;
    [HideInInspector]
    public List<GameObject> needDispose;
    public string partName;
    [HideInInspector]
    public LuaTable scriptEnv;
    public TextAsset luaScript;
    [HideInInspector]
    internal static LuaEnv luaEnv = new LuaEnv();
    void Awake()
    {
    }
  
    // Start is called before the first frame update
    [LuaCallCSharp]
    void Start()
    {
        //Image a = new Image();
       
        datasAndIndex = new Dictionary<GameObject, int>();
        needDispose = new List<GameObject>();
        scriptEnv = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        scriptEnv.Set("self", this);
        scriptEnv.Set("listContent", listContent);
        scriptEnv.Set("scrollRect", scrollRect);
        luaEnv.DoString(luaScript.text, "LuaTestScript", scriptEnv);
        setRecordItem = luaEnv.Global.Get<Action<int, GameObject>>("setRecordItem");

    }
    public byte[] LoadLuaScript(ref string filename)
    {

        string path = Application.dataPath + "/Scripts/" + filename + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(System.IO.File.ReadAllText(path));
    }
    // Update is called once per frame
    void Update()
    {

    }
    [LuaCallCSharp]
    public void onRecordDrag(float y)
    {
        //Debug.Log(datas.Count);
        if (datas.Count <= col * row) return;
        if (isDragIng) return;
        isDragIng = true;
        RectTransform rectTransform = listContent.GetComponent<RectTransform>();
        int indexNowRow = getIndex(rectTransform.anchoredPosition3D.y, cell);

        foreach (var go in datasAndIndex.Keys)
        {
            if (datasAndIndex[go] >= indexNowRow * col && datasAndIndex[go] < (indexNowRow + row) * col)
            {
                //没超出范围,不回收
                continue;
            }
            else
            {
                //超出范围,收回到对象池内
                needDispose.Add(go);
            }
        }
        //将超出范围的元素移除可见列表并隐藏
        foreach (var go in needDispose)
        {
            datasAndIndex.Remove(go);
            //隐藏
            go.SetActive(false);
        }
        //int cur = 0;
        for (int i = indexNowRow * col; i < (indexNowRow + row) * col; i++)
        {
            if (datasAndIndex.ContainsValue(i))
            {
                //此位置已经有item了 不做处理
                continue;
            }
            else//此位置没有item 需要加载一个
            {
                if (i < datas.Count)
                {
                    //如果对象池中有空闲的实例
                    if (needDispose.Count > 0)
                    {
                        //设置生成新item
                        setRecordItem(i, needDispose[0]);
                        needDispose.Remove(needDispose[0]);
                    }
                }
            }
        }
        isDragIng = false;
    }
    [Hotfix]
    public int getPos_Y(int index, int col, int cell)
    {
        int sizeY = index / col * cell;
        return -sizeY;
    }


    [Hotfix]
    public int getIndex(float y, int cell)
    {
        int index = 0;
        index = (int)(y / cell);
        if (y < 0) index = 0;
        return index;
    }
}
