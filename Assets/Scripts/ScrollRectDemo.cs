using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;
public class ScrollRectDemo : MonoBehaviour
{

    //生成物体的父物体即显示主体
    public Transform RankGrid;
    //拖动组件
    public ScrollRect scorllRect;

    //展示用实例
    public GameObject item;
    //可见行数
    public int row ;
    //可见列数
    public int col ;

    //单元格格子宽高
    public int itemWidth;

    //单元格间隙
    public int chink;

    //最终单元格高宽度
    public int cell;

    public string partName;
    // 所有数据的集合
    public List<string> datas = new List<string> { };

    //可见数据列表
    Dictionary<GameObject, int> datasAndIndex = new Dictionary<GameObject, int>();

    //拖拽状态
    public bool isDragIng = false;
    //加载实例状态
    public bool isLoadingRecord = false;

    //回收用的对象池
    List<GameObject> needDispose = new List<GameObject>();
    void Start()
    {
        var temp = new List<int>();
       /*for (int i = 0; i < 50; i++)
        {
            temp.Add(i);
        }*/
        loadAssets(6, 3, 80, 2, "item");
        //监听拖动
        scorllRect.onValueChanged.AddListener((value) => { OnRecordDrag(value.y); });
        //设置生成记录
        SetRecords();
    }

    /// <summary>
    /// 初始化配置列表中可能用到的资源与配置
    /// </summary>
    /// <param name="prefabName"></param>
    public void loadAssets(int row,int col,int itemWidth1,int chink,string prefabName)
    {
        RankGrid = this.transform.Find("Grid").GetComponent<Transform>();
        scorllRect = this.GetComponent<ScrollRect>();
        item = Resources.Load(prefabName) as GameObject;
        this.row = row;
        this.col = col;
        this.itemWidth = itemWidth1;
        this.chink = chink;
        int temp = (int)item.GetComponent<RectTransform>().rect.size.x;
        itemWidth = temp == itemWidth1 ? temp : itemWidth1;
        cell = itemWidth + chink;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="infos"></param>
    public void SetRecords()
    {
        ClearRecord();
        StartCoroutine(SetRecord());
    }

    /// <summary>
    /// 初始化可见列表协程
    /// </summary>
    /// <param name="infos">待展示数据集合</param>
    /// <returns></returns>
    public IEnumerator SetRecord()
    {
        while (isLoadingRecord)
        {
            yield return new WaitForFixedUpdate();
        }

        isLoadingRecord = true;
        datas.Clear();
        datas.AddRange(AvatarSystem._instance.getAllMeshNameOfPart(partName));

        int h = datas.Count/col * cell;
        if (datas.Count % col != 0) h += cell;
  
        //设置要拖动主体的宽高
        RankGrid.GetComponent<RectTransform>().sizeDelta = new Vector2(col*cell, h);
        RankGrid.GetComponent<RectTransform>().position = Vector3.zero;
        RankGrid.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        isDragIng = true;
        //第一次刷新生成物体
        for (int i = 0; i < (datas.Count > row*col? row *col : datas.Count); i++)
        {
            GameObject go = Instantiate(item) as GameObject;
      
            go.transform.SetParent(RankGrid);
            SetRecordItem(i, go);
           // yield return new WaitForSeconds(0.1f);
        }
        isDragIng = false;
        isLoadingRecord = false;
    }

    /// <summary>
    /// 设置并向可见列表中添加元素
    /// </summary>
    /// <param name="index">数据下标</param>
    /// <param name="go">显示用实例对象</param>
    void SetRecordItem(int index, GameObject go)
    {
        if (index >= datas.Count) return;
        datasAndIndex.Add(go, index);
        //设置位置
        go.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
        go.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3((index%col)*cell+(cell/2), GetPos_Y(index), 0);
        go.transform.GetComponent<RectTransform>().sizeDelta= new Vector2(itemWidth*2, itemWidth*2);
        go.SetActive(true);
        //设置信息显示
        go.name = datas[index];
        //go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "是" + datas[index];
        go.GetComponent<Image>().sprite =Resources.Load(datas[index], typeof(Sprite)) as Sprite;
        go.GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
       

    }

    /// <summary>
    /// 列表拖动监听方法
    /// </summary>
    /// <param name="y"></param>
    [LuaCallCSharp]
    void OnRecordDrag(float y)
    {
        if (datas.Count <= col*row) return;
        if (isDragIng) return;
        isDragIng = true;//禁止拖拽
        //获取可见的第一行元素的行号
        int indexNowRow = GetIndex(RankGrid.GetComponent<RectTransform>().anchoredPosition3D.y);
        //检测显示列表中元素是否超出范围
        foreach (var go in datasAndIndex.Keys)
        {
            if (datasAndIndex[go] >= indexNowRow*col && datasAndIndex[go] < (indexNowRow + row)*col)
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
        for (int i = indexNowRow*col; i < (indexNowRow + row) * col; i++)
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
                        SetRecordItem(i, needDispose[0]);
                        needDispose.Remove(needDispose[0]);
                    }
                }
            }
        }
        isDragIng = false;
    }

    /// <summary>
    /// 获取当前下标对应元素的y坐标位置
    /// </summary>
    /// <param name="index">元素下标</param>
    /// <returns>元素生成位置y坐标</returns>
    float GetPos_Y(int index)
    {
            
        float sizeY = index/col*cell;
       // if (index % 7 != 0) sizeY +=90;
        return -sizeY;
    }
    /// <summary>
    /// 获取顶部可显示的元素下标
    /// </summary>
    /// <param name="y">Grid的Y坐标</param>
    /// <returns>可见的第一个元素的下标</returns>
    int GetIndex(float y)
    {
        int index = 0;
        //float sizeY = 0;
        index = (int)(y / cell) ;
        if (index < 0) index = 0;
        return index;
    }

    
    /// <summary>
    /// 清理所有元素，回收到对象池
    /// </summary>
    public void ClearRecord()
    {
       // datas = new List<int>();
        foreach (var go in datasAndIndex.Keys)
        {
            //回收到对象池
            go.SetActive(false);
            needDispose.Add(go);
        }
        datasAndIndex.Clear();
    }
}
