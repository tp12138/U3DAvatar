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
    /// ��ʶ��ǰ�ذ��е�Ԫ��,�����ղ�����ʾ��Ԫ��
    /// </summary>
    /// <param name="indexNowRow"></param>
   [Hotfix]
    public void removeUnUseItem(int startNum,int endNum)
    {
        foreach (var go in datasAndIndex.Keys)
        {
            if (datasAndIndex[go] >= startNum  && datasAndIndex[go] <=endNum)
            {
                //û������Χ,������
                continue;
            }
            else
            {
                //������Χ,�ջص��������
                needDispose.Add(go);
                
            }
        }
       foreach(var t in needDispose)
       {
           deleteItem(t);
       }
    }

    /// <summary>
    /// ˢ�µذ��ϵ�Ԫ��,����������Ԫ��
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
                    //��λ���Ѿ���item�� ��������
                    continue;
                }
                else//��λ��û��item ��Ҫ����һ��
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
