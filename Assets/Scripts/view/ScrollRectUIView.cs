using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using UnityEngine.UI;
public class ScrollRectUIView : MonoBehaviour
{
   // public GameObject listContent;
   // public GameObject scrollRect;
   // public string partName;
   // [HideInInspector]
   // public ScrollRectModel SRM;
    [HideInInspector]
    public ScrollRectControl SRC;
    [HideInInspector]
    public AvatarModel avatarModel;
    [HideInInspector]
    public RoleUIViev roleUIViev;
    [HideInInspector]
    public static AvatarControl _instance;
    [HideInInspector]
    public AssetBundle ab;    //包含骨架的AB包
    [HideInInspector]
    private AssetBundle prefabAB; //包含替换资源和UI贴图的AB包
    [HideInInspector]
    public Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> skinnedSourceDict;//可替换资源的部位->编号->蒙皮字典
    public Action<string, string, string> onRoleChange;
    public Action<GameObject> onInitNewRole;
    public Action<string> onAddNewPart;
    public TextAsset luaScript;//Lua文件资源
    void Awake()
    {
     
        SRC=transform.gameObject.GetComponent<ScrollRectControl>();
        SRC.setNewItemInView += setRecordItem;
        SRC.deletaItemInView += disableItem;
        SRC.setContent += setContent;
       
    }
    [LuaCallCSharp]
    public void setRecordItem(int index,GameObject go,ScrollRectModel SRM)
    {
        go.transform.SetParent(SRC.listContent.GetComponent<Transform>());
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 1f);
        rt.anchoredPosition3D =SRC.getItemPosition(index);
        rt.sizeDelta = new Vector2(SRM.itemWidth * 2, SRM.itemWidth * 2);
        go.SetActive(true);
        go.name = SRM.datas[index];
        AvatarButton ab = go.GetComponent<AvatarButton>();
        ab.onButtonLeftClick += AvatarControl._instance.OnChangePeople;
        ab.onButtonRightClick += AvatarControl._instance.removeMesh;
        ab.onButtonRightClick += SRM.deleteItem;
        Sprite sprit = AvatarControl._instance.loadSpriteFromAssetBundle(go.name);
        go.GetComponent<Image>().sprite = sprit;
        go.GetComponent<Toggle>().group = SRC.scrollRect.GetComponent<ToggleGroup>();
        
    }

    public void setContent(int width, int height)
    {
        RectTransform rtf = SRC.listContent.GetComponent<RectTransform>();
        rtf.sizeDelta = new Vector2(width, height);
        rtf.localPosition = Vector3.zero;
   }

    public void disableItem(GameObject go)
    {
        go.SetActive(false);
    }

    
    public void Awake()
    {
        //_instance = this;
        DontDestroyOnLoad(this); //不删除游戏物体
        avatarModel = gameObject.GetComponent<AvatarModel>();
        avatarModel.onUpdatePart += OnChangePeople;
        avatarModel.onAddNewPart += OnAddNewPart;
        roleUIViev = gameObject.GetComponent<RoleUIViev>();
        skinnedSourceDict = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
        LuaEnv luaEnv = new LuaEnv();
        luaEnv.DoString(luaScript.text, "AvatarControl.Lua");
        initCharacter();

    }

    /// <summary>
    /// 将AB包中的替换部位资源保存进字典中
    /// </summary>
    /// <param name="prefabName"></param>
    [LuaCallCSharp]
    public void save(string prefabName)
    {
        skinnedSourceDict.Clear();

        SkinnedMeshRenderer[] res = loadAllSourcesFromAssetBundle(prefabName);
        foreach (var t in res)
        {
            string[] names = t.name.Split('-');
            if (skinnedSourceDict.ContainsKey(names[0]) == false)//初次在骨骼target下生成对应的身体组件
            {
                skinnedSourceDict.Add(names[0], new Dictionary<string, SkinnedMeshRenderer>());
            }
            skinnedSourceDict[names[0]].Add(names[1], t);
        }
    }
    /// <summary>
    /// 初始化角色mesh
    /// </summary>
    public void initCharacter()
    {
        string[,] targetDatas = avatarModel.targetDatas;
        for (int i = 0; i < targetDatas.GetLength(0); i++)
            OnChangePeople(targetDatas[i, 0], targetDatas[i, 0], targetDatas[i, 1]);
    }


    //外部调用,更改目标服装
    [Hotfix]
    public void tryToChangePeople(string part, string num)
    {

    }

    [Hotfix]
    public void OnAddNewPart(string part)
    {
        onAddNewPart(part);
    }


    /// <summary>
    /// 数据处理完毕后的回调,通知view调整显示效果
    /// </summary>
    /// <param name="part"></param>
    /// <param name="num"></param>
    public void OnChangePeople(string state, string part, string num)
    {
        onRoleChange(state, part, num);
    }
    /// <summary>
    /// 从保存资源的字典中,移除某个部位某个编号的部件
    /// </summary>
    /// <param name="part">部位</param>
    /// <param name="num">编号</param>
    /// <returns></returns>
    public void removeMesh(GameObject go)
    {
        //avatarModel.removeSkinnedMesh(go);
        string[] names = go.name.Split('-');
        skinnedSourceDict[names[0]].Remove(names[1]);
    }



    /// <summary>
    /// 获取对应部位编号为num的蒙皮网格资源
    /// </summary>
    /// <param name="part">部位</param>
    /// <param name="num">编号</param>
    /// <returns></returns>
    public SkinnedMeshRenderer getSkinnedMeshByPartAndNum(string part, string num)
    {
        if (skinnedSourceDict.ContainsKey(part) == false)
        {
            Debug.LogError("part name error " + part);
            return null;
        }
        else
        {
            if (skinnedSourceDict[part].ContainsKey(num) == false)
            {
                Debug.LogError("the part :" + part + ",do not have num:" + num);
                return null;
            }
        }
        return skinnedSourceDict[part][num];
    }

    /// <summary>
    /// 根据资源名字和类型,加载资源
    /// </summary>
    /// <param name="sourceName">资源名字</param>
    /// <param name="type">资源类型</param>
    /// <returns></returns>
    [LuaCallCSharp]
    public Sprite loadSpriteFromAssetBundle(string sourceName)
    {
        Texture2D ass = ab.LoadAsset<Texture2D>(sourceName + ".jpg");
        Sprite mySprite = Sprite.Create(ass, new Rect(0.0f, 0.0f, ass.width, ass.height), new Vector2(0.5f, 0.5f), 100.0f);
        return mySprite;
    }


    /// <summary>
    /// 从骨骼预制体资源包中获取名字对应的骨架
    /// </summary>
    /// <param name="sourceName">骨架名字</param>
    /// <returns></returns>
    [LuaCallCSharp]
    public System.Object loadSourcesFromAssetBundle(string sourceName)
    {
        var obj = ab.LoadAsset(sourceName);
        return obj;
    }

    /// <summary>
    /// 获取预制体资源包中所有的可替换部件资源
    /// </summary>
    /// <param name="AssetBundleName">资源包的包名</param>
    /// <returns></returns>
    public SkinnedMeshRenderer[] loadAllSourcesFromAssetBundle(string AssetBundleName)
    {
        if (prefabAB == null)
            prefabAB = AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + AssetBundleName);

        UnityEngine.Object[] obj = prefabAB.LoadAllAssets();
        List<SkinnedMeshRenderer> sourceSkinnedMesh = new List<SkinnedMeshRenderer>();
        for (int i = 0; i < obj.Length; i++)
        {
            GameObject t = obj[i] as GameObject;
            if (t.GetComponent<SkinnedMeshRenderer>() != null)
            {
                sourceSkinnedMesh.Add(t.GetComponent<SkinnedMeshRenderer>());
            }
        }
        return sourceSkinnedMesh.ToArray();
    }

    //加载目标资源包
    public AssetBundle LoadAssetBundle(string bundleName)
    {
        return AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + bundleName);
    }

    //转Object类型为GameObject类型
    public GameObject TypeChange(UnityEngine.Object go) { return (GameObject)go; }

    //获取所有可替换服装的名字
    public List<string> getAllMeshNameOfPart(string partName)
    {
        List<string> temp = new List<string>();

        foreach (KeyValuePair<string, SkinnedMeshRenderer> t in skinnedSourceDict[partName])
        {
            temp.Add(t.Value.name);
        }

        return temp;
    }
    
}
