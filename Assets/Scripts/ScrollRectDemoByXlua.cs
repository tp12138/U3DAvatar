
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
                //û������Χ,������
                continue;
            }
            else
            {
                //������Χ,�ջص��������
                needDispose.Add(go);
            }
        }
        //��������Χ��Ԫ���Ƴ��ɼ��б�����
        foreach (var go in needDispose)
        {
            datasAndIndex.Remove(go);
            //����
            go.SetActive(false);
        }
        //int cur = 0;
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
