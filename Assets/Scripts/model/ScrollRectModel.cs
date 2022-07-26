using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ScrollRectModel : MonoBehaviour
{
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
    public Dictionary<GameObject, int> datasAndIndex;
    [HideInInspector]
    private Action<int, GameObject> setRecordItem;
    [HideInInspector]
    public List<GameObject> needDispose;
    [HideInInspector]
    public GameObject item;
    [HideInInspector]
    public int itemWidth;

    [HideInInspector]
    public Action<int, GameObject, ScrollRectModel> setNewItem;
    // Start is called before the first frame update
    void Awake()
    {
        datasAndIndex = new Dictionary<GameObject, int>();
        needDispose = new List<GameObject>();
    }
    /// <summary>
    /// 标识当前地板中的元素,并回收不该显示的元素
    /// </summary>
    /// <param name="indexNowRow"></param>
    public void removeUnUseItem(int indexNowRow)
    {
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
        foreach (var go in needDispose)
        {
            datasAndIndex.Remove(go);
            //隐藏
            go.SetActive(false);
        }
    }

    /// <summary>
    /// 刷新地板上的元素,重新生成新元素
    /// </summary>
    /// <param name="indexNowRow"></param>
    public void generaNewItem(int indexNowRow)
    {
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
                        datasAndIndex[needDispose[0]] = i;
                        //setRecordItem(i, needDispose[0]);
                        setNewItem(i, needDispose[0], this);
                        needDispose.Remove(needDispose[0]);
                    }
                }
            }
        }
    }

    // Update is called once per frame

    public int getIndex(float y, int cell)
    {
        int index = 0;
        index = (int)(y / cell);
        if (y < 0) index = 0;
        return index;
    }
}
