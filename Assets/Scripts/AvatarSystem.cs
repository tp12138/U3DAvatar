using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
[LuaCallCSharp]
public class AvatarSystem : MonoBehaviour
{
    [HideInInspector]
    public static AvatarSystem _instance;
    [HideInInspector]
    public Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> skinnedSources;//所有可替换资源的字典信息
    [HideInInspector]
    public GameObject target;//资源实例目标
    [HideInInspector]
    public Transform[] targetHips;//目标骨骼信息
    [HideInInspector]
    public string[,] targetDatas = new string[6, 2]; //{ {"eyes","1"},{"hair","1"},{"top","1"},{"pants","1"},{"shoes","1"},{"face","1"}};//目标各部位对应的是几号材质
    [HideInInspector]
    public Dictionary<string, SkinnedMeshRenderer> targetMesh;//目标身上,不同部位对应的mesh
    [HideInInspector]
    public AssetBundle ab;
    [HideInInspector]
    public AssetBundle prefabsAb;
    [HideInInspector]
    Dictionary<String,Transform> targetHipsDict;
    [HideInInspector]
    public List<SkinnedMeshRenderer> sourceSKinnedMesh;

    /// <summary>
    /// 生成部位->编号->具体SkinnedMesh的字典
    /// 生成身体部位与其对应的SkinnedMesh的字典
    /// 加载lua文件并且运行
    /// 初始化目标角色的Mesh
    /// </summary>
    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this); //不删除游戏物体
        skinnedSources = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
        targetMesh = new Dictionary<string, SkinnedMeshRenderer>();
        LuaEnv luaEnv = new LuaEnv();
        luaEnv.AddLoader(LoadLuaScript);
        luaEnv.DoString("require'AvatarSystem'");
        initCharacter();
        

    }


    /// <summary>
    /// lua文件转载器
    /// </summary>
    /// <param name="filename">要装载的lua模块名</param>
    /// <returns></returns>
    public byte[] LoadLuaScript(ref string filename)
    {
        string path = Application.dataPath + "/Scripts/" + filename + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(System.IO.File.ReadAllText(path));
   
    }

    /// <summary>
    /// 初始化角色的各个部件mesh
    /// </summary>
    public void initCharacter()
    {
        for (int i = 0; i < targetDatas.GetLength(0);i++ )
            OnChangePeople(targetDatas[i,0],targetDatas[i,1]);
    }

    /// <summary>
    /// 从资源文件中将所有资源保存到对应的字典中
    /// 根据资源中的部位,再场景的目标骨架中生成对应的部位
    /// 并且保存骨架中,骨骼名字到骨骼的映射
    /// </summary>
    /// <param name="prefabName">assetBundle包名</param>
    [LuaCallCSharp]
    public void save(string prefabName)
    {
        skinnedSources.Clear();
        targetMesh.Clear();
        SkinnedMeshRenderer[] res = this.loadAllSourcesFromAssetBundle(prefabName);
        foreach (var t in res)
        {
            string[] names = t.name.Split('-');
            if (skinnedSources.ContainsKey(names[0]) == false)//初次在骨骼target下生成对应的身体组件
            {
                GameObject go = new GameObject();
                go.name = names[0];
                go.transform.parent = target.GetComponent<Transform>();
                targetMesh.Add(names[0], go.AddComponent<SkinnedMeshRenderer>());
                skinnedSources.Add(names[0], new Dictionary<string, SkinnedMeshRenderer>());
            }
            skinnedSources[names[0]].Add(names[1], t);
        }
        targetHipsDict = new Dictionary<string, Transform>();
        //保存目标骨架身上从名字到骨骼的映射,直接查找
        foreach (var t in targetHips)
            targetHipsDict.Add(t.name, t);
            
    }

    /// <summary>
    /// 根据对应的部位和要更新的模型编号,替换人物对应部位的Mesh
    /// </summary>
    /// <param name="part">部位</param>
    /// <param name="num">部位编号</param>
     void changeMesh(string part, string num)
    {
        if (skinnedSources.ContainsKey(part) == false)
        {
            Debug.LogError("part name error " + part);
            return;
        }
        else
        {
            if (skinnedSources[part].ContainsKey(num) == false)
            {
                Debug.LogError("the part :" + part + ",do not have num:" + num);
                return;
            }
        }
        SkinnedMeshRenderer smrTemp = skinnedSources[part][num];
        if (smrTemp == null)
        {
            Debug.LogError("not config skinnedMesh");
            return;
        }
        List<Transform> bones = new List<Transform>();
        foreach (var trans in smrTemp.bones)
           if (targetHipsDict.ContainsKey(trans.name))
               bones.Add(targetHipsDict[trans.name]);
         //替换SkinnedMesh
        targetMesh[part].material = smrTemp.sharedMaterial;
        targetMesh[part].bones = bones.ToArray();
        targetMesh[part].sharedMesh = smrTemp.sharedMesh;
        targetMesh[part].rootBone = smrTemp.rootBone;
        saveData(part, num, targetDatas);
    }
    

    /// <summary>
    /// 再目标信息保存的二维数组上保存,对应部位的编号
    /// </summary>
    /// <param name="part">部位</param>
    /// <param name="num">编号</param>
    /// <param name="targetStr">目标部位信息数组</param>
    void saveData(string part, string num, string[,] targetStr)
    {
        int length = targetStr.GetLength(0);//获得行数
        for (int i = 0; i < length; i++)
        {
            if(targetDatas[i,0].Equals(part))
            {
                targetDatas[i, 1] = num;
                break;
            }
        }
    }

    /// <summary>
    /// 根据配置来的部位数组和对应部位的编号数组配置初始的角色部位编号
    /// </summary>
    /// <param name="part">部位</param>
    /// <param name="num">编号</param>
    [LuaCallCSharp]
    public void configCharater(string[] part, string[] num)
    {
        int length=targetDatas.GetLength(0);

        for (int i = 0; i < length; i++)
            {
                targetDatas[i, 0] = part[i];
                targetDatas[i, 1] = num[i];
            }
    }

    //外部调用,更改目标服装
    public void OnChangePeople(string part, string num)
    {
        changeMesh(part, num);
    }

    //获取所有可替换服装的名字
    public List<string> getAllMeshNameOfPart(string partName)
    {
        List<string> temp = new List<string>();
        foreach (KeyValuePair<string, SkinnedMeshRenderer> t in skinnedSources[partName])
        {
            temp.Add(t.Value.name);
        }
        return temp;
    }
    /// <summary>
    /// 根据assetbundle包的名字加载AssetBundle包
    /// </summary>
    /// <param name="bundleName">包名字</param>
    /// <returns></returns>
    [LuaCallCSharp]
    public AssetBundle LoadAssetBundle(string bundleName)
    {
        return AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + bundleName);
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
        Texture2D ass = ab.LoadAsset<Texture2D>(sourceName+".jpg");
        Sprite mySprite = Sprite.Create(ass, new Rect(0.0f, 0.0f, ass.width,ass.height), new Vector2(0.5f, 0.5f), 100.0f);
        return mySprite;
    }
    /// <summary>
    /// 根据资源的名字,从assetbundle包中加载资源
    /// </summary>
    /// <param name="sourceName">资源名字</param>
    /// <returns></returns>
    [LuaCallCSharp]
    public System.Object loadSourcesFromAssetBundle(string sourceName)
    {
        var obj = ab.LoadAsset(sourceName);
        return obj;
    }
   /// <summary>
   /// 从资源包中获取其中所有的SkinnedMeshRenderer组件
   /// </summary>
   /// <param name="AssetBundleName">资源包名字</param>
   /// <returns></returns>
    public SkinnedMeshRenderer[] loadAllSourcesFromAssetBundle(string AssetBundleName)
    {
        if(prefabsAb==null)
            prefabsAb = AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + AssetBundleName);
       
        UnityEngine.Object[] obj = prefabsAb.LoadAllAssets();
        sourceSKinnedMesh = new List<SkinnedMeshRenderer>();
       for (int i = 0; i < obj.Length; i++)
        {
            GameObject t=obj[i] as GameObject;
            if (t.GetComponent<SkinnedMeshRenderer>() != null)
            {
                sourceSKinnedMesh.Add(t.GetComponent<SkinnedMeshRenderer>());
            }
        }
        return sourceSKinnedMesh.ToArray();
    }
    /// <summary>
    /// 将对象转变为GameObject类型
    /// </summary>
    /// <param name="_object">要改变的对象</param>
    /// <returns></returns>
    public GameObject TypeChange(object _object) { return (GameObject)_object; }
    /// <summary>
    /// 生成字符串到SkinnedMeshRenderer的字典
    /// </summary>
    /// <returns></returns>
    [LuaCallCSharp]
    public Dictionary<string, SkinnedMeshRenderer> getDict()
    {
        return new Dictionary<string, SkinnedMeshRenderer>();
    }
   /// <summary>
   /// 获取某个物体下的全部Transform组件
   /// </summary>
   /// <param name="tar"></param>
   /// <returns></returns>
    [LuaCallCSharp]
    public Transform[] getBonesFromObj(GameObject tar)
    {
        return tar.GetComponentsInChildren<Transform>();
    }
    /// <summary>
    /// 从保存资源的字典中,移除某个部位某个编号的部件
    /// </summary>
    /// <param name="part">部位</param>
    /// <param name="num">编号</param>
    /// <returns></returns>
    public bool removeMesh(string part, string num)
    {
        bool res=skinnedSources[part].Remove(num);
        return res;
    }
}
