using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;
[LuaCallCSharp ]
public class AvatarModel : MonoBehaviour
{

    [HideInInspector]
    public Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> skinnedSourceDict;//���滻��Դ�Ĳ�λ->���->��Ƥ�ֵ�
    [HideInInspector]
    public GameObject target;//Ŀ��Ǽ�
    [HideInInspector]
    public Transform[] targetHips;//Ŀ��ǼܵĹ�����Ϣ
    [HideInInspector]
    public string[,] targetDatas = new string[6, 2];
    [HideInInspector]
    public Dictionary<string, SkinnedMeshRenderer> targetSkinned;//Ŀ��Ǽ��ϲ�λ->��Ƥ����Ϣ��ӳ���ֵ�
    [HideInInspector]
    public AssetBundle ab;
    private AssetBundle prefabAB;
    [HideInInspector]
    public Dictionary<string, Transform> targetHipsDict;//Ŀ��Ǽ����� �������ֵ�������ӳ��
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
            if (skinnedSourceDict.ContainsKey(names[0]) == false)//�����ڹ���target�����ɶ�Ӧ���������
            {
                onInstancePart(names[0]);
                skinnedSourceDict.Add(names[0], new Dictionary<string, SkinnedMeshRenderer>());
            }
            skinnedSourceDict[names[0]].Add(names[1], t);
        }
        targetHipsDict = new Dictionary<string, Transform>();
        //����Ŀ��Ǽ����ϴ����ֵ�������ӳ��,ֱ�Ӳ���
        foreach (var t in targetHips)
            targetHipsDict.Add(t.name, t);

    }
    //������Ƥ�õ��Ĺ����õ��Ǽ��ж�Ӧ�������б�
    public List<Transform> getBonesBySmr(SkinnedMeshRenderer smr)
    {
        List<Transform> bones = new List<Transform>();
        foreach (var trans in smr.bones)
            if (targetHipsDict.ContainsKey(trans.name))
                bones.Add(targetHipsDict[trans.name]);
        return bones;
    }

    //����ģ���������ϵĲ�λ��Ϣ
    public void saveData(string part, string num)
    {
        int length = targetDatas.GetLength(0);//�������
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

    //��ȡ���п��滻��װ������
    public List<string> getAllMeshNameOfPart(string partName)
    {
        List<string> temp = new List<string>();
        foreach (KeyValuePair<string, SkinnedMeshRenderer> t in skinnedSourceDict[partName])
        {
            temp.Add(t.Value.name);
        }
        return temp;
    }

    //����Ŀ����Դ��
    public AssetBundle LoadAssetBundle(string bundleName)
    {
        return AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + bundleName);
    }
    //��ȡĿ����Դ���е�����skinnedMesh
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
    //�������ֺͱ�Ŵ���Դ�ֵ��л�ȡ��Ӧ����Դ
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
    /// ������Դ������,��assetbundle���м�����Դ
    /// </summary>
    /// <param name="sourceName">��Դ����</param>
    /// <returns></returns>
    [LuaCallCSharp]
    public System.Object loadSourcesFromAssetBundle(string sourceName)
    {
        var obj = ab.LoadAsset(sourceName);
        return obj;
    }
    /// <summary>
    /// ��ȡĳ�������µ�ȫ��Transform���
    /// </summary>
    /// <param name="tar"></param>
    /// <returns></returns>
    [LuaCallCSharp]
    public Transform[] getBonesFromObj(GameObject tar)
    {
        return tar.GetComponentsInChildren<Transform>();
    }
    /// <summary>
    /// ������ת��ΪGameObject����
    /// </summary>
    /// <param name="_object">Ҫ�ı�Ķ���</param>
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
