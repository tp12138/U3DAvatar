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
    /// ��ʶ��ǰ�ذ��е�Ԫ��,�����ղ�����ʾ��Ԫ��
    /// </summary>
    /// <param name="indexNowRow"></param>
    public void removeUnUseItem(int indexNowRow)
    {
        foreach (var go in datasAndIndex.Keys)
        {
            if (datasAndIndex[go] >= indexNowRow * col && datasAndIndex[go] < (indexNowRow + row) * col)
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
            go.SetActive(false);
        }
    }

    /// <summary>
    /// ˢ�µذ��ϵ�Ԫ��,����������Ԫ��
    /// </summary>
    /// <param name="indexNowRow"></param>
    public void generaNewItem(int indexNowRow)
    {
        for (int i = indexNowRow * col; i < (indexNowRow + row) * col; i++)
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

    // Update is called once per frame

    public int getIndex(float y, int cell)
    {
        int index = 0;
        index = (int)(y / cell);
        if (y < 0) index = 0;
        return index;
    }
}
