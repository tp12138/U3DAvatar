using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using UnityEngine.UI;
public class ScrollRectControl : MonoBehaviour
{
   [HideInInspector]
   public int col;
   [HideInInspector]
   public int row;
   [HideInInspector]
   public int cell;
   [HideInInspector]
   public bool isDragIng;
   [HideInInspector]
   public int dataCount;
   [HideInInspector]
   public Vector2 itemSize;
    [HideInInspector]
    public LuaTable scriptEnv;
    public TextAsset luaScript;
    [HideInInspector]
    internal static LuaEnv luaEnv = new LuaEnv();
   
    public Dictionary<GameObject, int> datasAndIndex;
    public List<GameObject> needDispose;
    public Action<GameObject> recycleItem;
    public Action<int> setNewItem;
    public Action<GameObject> tryToDeleteItem;
    void Start()
    {
        datasAndIndex = new Dictionary<GameObject, int>();
        scriptEnv = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        scriptEnv.Set("self", this);
        luaEnv.DoString(luaScript.text, "ScrollControl.Lua", scriptEnv);
        recycleItem = scriptEnv.Get<Action<GameObject>>("RecycleItem");
        setNewItem = scriptEnv.Get<Action<int>>("SetNewItem");
        tryToDeleteItem = scriptEnv.Get<Action<GameObject>>("deleteItem");
        this.isDragIng = false;
    }
    void OnEnable()
    {
        if (scriptEnv != null)
        {
            var temp = transform.GetComponent<ScrollRectUIView>().listContent.GetComponent<Transform>();
            if (temp.childCount > 0)
            {
                for (int i = 0; i < temp.childCount; i++)
                {
                    Destroy(temp.GetChild(i).gameObject);
                }
            }
            luaEnv.DoString(luaScript.text, "ScrollControl.Lua", scriptEnv);
        }

    }
    public void onRecordDrag(float y)
    {
       
        if (dataCount <=col * row) return;
        float posY = transform.GetComponent<ScrollRectUIView>().listContent.GetComponent<RectTransform>().anchoredPosition3D.y;
        if (isDragIng) return;
        if (Math.Abs(posY) < cell / 2) return;
        isDragIng = true;
        int indexNowRow = getIndex(posY);
        int startNum = indexNowRow * col;
        int endNum = (indexNowRow + row) * col;
        List<GameObject> unUseItem = new List<GameObject>();
        foreach (var go in datasAndIndex.Keys)
        {
            if (datasAndIndex[go] >= startNum && datasAndIndex[go] < endNum)
                continue;//û������Χ,������
            else
                unUseItem.Add(go); //������Χ,�ջص��������
        }
        
        if (unUseItem.Count > 0)
        {
            //��������Χ��Ԫ���Ƴ��ɼ��б�����
            foreach (var go in unUseItem)
            {
                datasAndIndex.Remove(go);
                recycleItem(go);
            }
            for (int i = startNum; i < endNum; i++)
            {
                if (datasAndIndex.ContainsValue(i))
                {
                    //��λ���Ѿ���item�� ��������
                    continue;
                }
                else//��λ��û��item ��Ҫ����һ��
                {
                    if (i < dataCount && i>=0)
                    {
                        //�����������п��е�ʵ��
                        setNewItem(i);
                    }
                }
            }
        }
        
        isDragIng = false;
    }

    public int getIndex(float y)
    {
        int index = 0;
        index = (int)(y / cell);
        if (y < 0) index = 0;
        return index;
     }
 

    public void deleteItem(GameObject go)
    {
        tryToDeleteItem(go);
    }


}
