using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;
[Hotfix]
public class ScrollRectModel : MonoBehaviour
{
    [HideInInspector]
    public List<string> datas = new List<string>();
    [HideInInspector]
    public Dictionary<GameObject, int> datasAndIndex;
    [HideInInspector]
    public List<GameObject> needDispose;
    [HideInInspector]
    public GameObject item;
    [HideInInspector]
    public int itemWidth;
    [HideInInspector]
    public Action<int, GameObject,ScrollRectModel> setRecordItem;
    [HideInInspector]
    public Action<GameObject> removeItem;
    [HideInInspector]
    public LuaTable scriptEnv;
    public TextAsset luaScript;
    [HideInInspector]
    internal static LuaEnv luaEnv = new LuaEnv();
    // Start is called before the first frame update
    void Awake()
    {
        datasAndIndex = new Dictionary<GameObject, int>();
        needDispose = new List<GameObject>();
        scriptEnv = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        scriptEnv.Set("self", this);
        luaEnv.DoString(luaScript.text, "ScrollRectModel.Lua", scriptEnv);
    }
    /// <summary>
    /// 标识当前地板中的元素,并回收不该显示的元素
    /// </summary>
    /// <param name="indexNowRow"></param>
   [Hotfix]
    public void removeUnUseItem(int startNum,int endNum)
    {
        foreach (var go in datasAndIndex.Keys)
        {
            if (datasAndIndex[go] >= startNum  && datasAndIndex[go] <=endNum)
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
       foreach(var t in needDispose)
       {
           deleteItem(t);
       }
    }

    /// <summary>
    /// 刷新地板上的元素,重新生成新元素
    /// </summary>
    /// <param name="indexNowRow"></param>
    [Hotfix]
    public void generaNewItem(int startNum, int endNum)
    {
        for (int i = startNum; i <=endNum; i++)
        {
            if (i < datas.Count)
            {
                if (datasAndIndex.ContainsValue(i))
                {
                    //此位置已经有item了 不做处理
                    continue;
                }
                else//此位置没有item 需要加载一个
                {
                    addNewItem(i);
                }
            }
           
        }
    }

    [Hotfix]
    public void addNewItem(int index)
    {
        Debug.Log("is c#");
    }

    [Hotfix]
    public void deleteItem(GameObject go)
    {
      
    }

    public void clearRecord()
    {
        datasAndIndex.Clear();
        needDispose.Clear();
    }
}
