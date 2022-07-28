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
    /// ��ʶ��ǰ�ذ��е�Ԫ��,�����ղ�����ʾ��Ԫ��
    /// </summary>
    /// <param name="indexNowRow"></param>
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
        foreach (var go in needDispose)
        {
            datasAndIndex.Remove(go);
            //����
            removeItem(go);
        }
    }

    /// <summary>
    /// ˢ�µذ��ϵ�Ԫ��,����������Ԫ��
    /// </summary>
    /// <param name="indexNowRow"></param>
    public void generaNewItem(int startNum, int endNum)
    {
        for (int i = startNum; i <= endNum; i++)
        {
            if (datasAndIndex.ContainsValue(i))
            {
                //��λ���Ѿ���item�� ��������
                continue;
            }
            else//��λ��û��item ��Ҫ����һ��
            {
                if (i < datas.Count)
                {
                    //�����������п��е�ʵ��
                    if (needDispose.Count > 0)
                    {
                        //����������item
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
