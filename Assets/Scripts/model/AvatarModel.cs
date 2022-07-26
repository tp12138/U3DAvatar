using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;
[LuaCallCSharp ]
public class AvatarModel : MonoBehaviour
{

    [HideInInspector]
    public Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> skinnedSourceDict;//可替换资源的部位->编号->蒙皮字典
    [HideInInspector]
    public GameObject target;//目标骨架
    [HideInInspector]
    public Transform[] targetHips;//目标骨架的骨骼信息
    [HideInInspector]
    public string[,] targetDatas = new string[6, 2];
    [HideInInspector]
    public Dictionary<string, SkinnedMeshRenderer> targetSkinned;//目标骨架上部位->蒙皮的信息的映射字典
    [HideInInspector]
    public AssetBundle ab;
    private AssetBundle prefabAB;
    [HideInInspector]
    public Dictionary<string, Transform> targetHipsDict;//目标骨架身上 骨骼名字到骨骼的映射
    [HideInInspector]
    public List<SkinnedMeshRenderer> sourceSkinnedMesh;
    public Action<string> onInstancePart;
    public Action<GameObject> deleteSkinnedMesh;
    public void init()
    {
        skinnedSourceDict = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
        targetSkinned = new Dictionary<string, SkinnedMeshRenderer>();
    }
   
    // Start is called before the first frame update
    [LuaCallCSharp]
    public void save(string prefabName)
    {
        skinnedSourceDict.Clear();
        targetSkinned.Clear();
        SkinnedMeshRenderer[] res = this.loadAllSourcesFromAssetBundle(prefabName);
        foreach (var t in res)
        {
            string[] names = t.name.Split('-');
            if (skinnedSourceDict.ContainsKey(names[0]) == false)//初次在骨骼target下生成对应的身体组件
            {
                onInstancePart(names[0]);
                skinnedSourceDict.Add(names[0], new Dictionary<string, SkinnedMeshRenderer>());
            }
            skinnedSourceDict[names[0]].Add(names[1], t);
        }
        targetHipsDict = new Dictionary<string, Transform>();
        //保存目标骨架身上从名字到骨骼的映射,直接查找
        foreach (var t in targetHips)
            targetHipsDict.Add(t.name, t);

    }
    //根据蒙皮用到的骨骼得到骨架中对应骨骼的列表
    public List<Transform> getBonesBySmr(SkinnedMeshRenderer smr)
    {
        List<Transform> bones = new List<Transform>();
        foreach (var trans in smr.bones)
            if (targetHipsDict.ContainsKey(trans.name))
                bones.Add(targetHipsDict[trans.name]);
        return bones;
    }

    //保存模型现在身上的部位信息
    public void saveData(string part, string num)
    {
        int length = targetDatas.GetLength(0);//获得行数
        for (int i = 0; i < length; i++)
        {
            if (targetDatas[i, 0].Equals(part))
            {
                targetDatas[i, 1] = num;
                break;
            }
        }
    }

    public void configCharater(string[] part, string[] num)
    {
        int length = targetDatas.GetLength(0);

        for (int i = 0; i < length; i++)
        {
            targetDatas[i, 0] = part[i];
            targetDatas[i, 1] = num[i];
        }
    }

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

    //加载目标资源包
    public AssetBundle LoadAssetBundle(string bundleName)
    {
        return AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + bundleName);
    }
    //获取目标资源包中的所有skinnedMesh
    public SkinnedMeshRenderer[] loadAllSourcesFromAssetBundle(string AssetBundleName)
    {
        if (prefabAB == null)
            prefabAB = AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + AssetBundleName);

        UnityEngine.Object[] obj = prefabAB.LoadAllAssets();
        sourceSkinnedMesh = new List<SkinnedMeshRenderer>();
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
    //根据名字和编号从资源字典中获取对应的资源
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
    /// 将对象转变为GameObject类型
    /// </summary>
    /// <param name="_object">要改变的对象</param>
    /// <returns></returns>
    public GameObject TypeChange(object _object) { return (GameObject)_object; }

    public bool removeSkinnedMesh(GameObject go)
    {
        string[] name=go.name.Split('-');
        string part=name[0];
        string num=name[1];
        deleteSkinnedMesh(go);
        bool res = skinnedSourceDict[part].Remove(num);
        return res;
    }
}
