using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;
public class AvatarControl : MonoBehaviour
{

    [HideInInspector]
    public static AvatarControl _instance;
    [HideInInspector]
    public AssetBundle ab;    //包含骨架的AB包
    [HideInInspector]
    private AssetBundle prefabAB; //包含替换资源和UI贴图的AB包
    [HideInInspector]
    public Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> skinnedSourceDict;//可替换资源的部位->编号->蒙皮字典'
    [CSharpCallLua]
    delegate void removeMesh(GameObject go);
    public Action<GameObject> remove;
    [CSharpCallLua]
    public Action<string, string> tryChangePeople;
    public void Awake()
    {

    }

    /// <summary>
    /// 将AB包中的替换部位资源保存进字典中
    /// </summary>
    /// <param name="prefabName"></param>
    [LuaCallCSharp]
    public void saveSources(string prefabName)
    {
        if (skinnedSourceDict == null)
        {
            skinnedSourceDict = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
        }
        
        skinnedSourceDict.Clear();
        RoleUIViev re = GetComponent<RoleUIViev>();
        SkinnedMeshRenderer[] res =loadAllSourcesFromAssetBundle(prefabName);
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

    //转Object类型为GameObject类型
    public GameObject TypeChange(UnityEngine.Object go) { return (GameObject)go; }

    //获取所有可替换服装的名字
    public List<string> getAllMeshNameOfPart(string partName)
    {
        List<string> temp = new List<string>();
        foreach (KeyValuePair<string, SkinnedMeshRenderer> t in skinnedSourceDict[partName])
            temp.Add(t.Value.name);
        return temp;
    }

    public Sprite loadSpriteFromAssetBundle(string sourceName)
    {
        Texture2D ass = ab.LoadAsset<Texture2D>(sourceName + ".jpg");
        Sprite mySprite = Sprite.Create(ass, new Rect(0.0f, 0.0f, ass.width, ass.height), new Vector2(0.5f, 0.5f), 100.0f);
        return mySprite;
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
}
