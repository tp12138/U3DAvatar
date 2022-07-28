using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ScrollRectModel : MonoBehaviour
{
    [HideInInspector]
    public List<string> datas = new List<string>();
    
    [HideInInspector]
    public Dictionary<GameObject, int> datasAndIndex;
    [HideInInspector]
    public Action<int, GameObject> setRecordItem;
    [HideInInspector]
    public Action<GameObject> removeItem;
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
        foreach (var go in needDispose)
        {
            datasAndIndex.Remove(go);
            //隐藏
            removeItem(go);
        }
    }

    /// <summary>
    /// 刷新地板上的元素,重新生成新元素
    /// </summary>
    /// <param name="indexNowRow"></param>
    public void generaNewItem(int startNum, int endNum)
    {
        for (int i = startNum; i <= endNum; i++)
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

    public void deleteItem(GameObject go)
    {
        removeItem(go);
    }
}
