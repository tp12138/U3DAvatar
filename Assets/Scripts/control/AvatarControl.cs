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
    public AssetBundle ab;    //�����Ǽܵ�AB��
    [HideInInspector]
    private AssetBundle prefabAB; //�����滻��Դ��UI��ͼ��AB��
    [HideInInspector]
    public Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> skinnedSourceDict;//���滻��Դ�Ĳ�λ->���->��Ƥ�ֵ�'
    [CSharpCallLua]
    delegate void removeMesh(GameObject go);
    public Action<GameObject> remove;
    [CSharpCallLua]
    public Action<string, string> tryChangePeople;
    public void Awake()
    {

    }

    /// <summary>
    /// ��AB���е��滻��λ��Դ������ֵ���
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
            if (skinnedSourceDict.ContainsKey(names[0]) == false)//�����ڹ���target�����ɶ�Ӧ���������
            {
                skinnedSourceDict.Add(names[0], new Dictionary<string, SkinnedMeshRenderer>());
            }
            skinnedSourceDict[names[0]].Add(names[1], t);
        }
    }
  
    /// <summary>
    /// ��ȡ��Ӧ��λ���Ϊnum����Ƥ������Դ
    /// </summary>
    /// <param name="part">��λ</param>
    /// <param name="num">���</param>
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

    //תObject����ΪGameObject����
    public GameObject TypeChange(UnityEngine.Object go) { return (GameObject)go; }

    //��ȡ���п��滻��װ������
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
    /// ��ȡԤ������Դ�������еĿ��滻������Դ
    /// </summary>
    /// <param name="AssetBundleName">��Դ���İ���</param>
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

    //����Ŀ����Դ��
    public AssetBundle LoadAssetBundle(string bundleName)
    {
        return AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + bundleName);
    }

    /// <summary>
    /// �ӹ���Ԥ������Դ���л�ȡ���ֶ�Ӧ�ĹǼ�
    /// </summary>
    /// <param name="sourceName">�Ǽ�����</param>
    /// <returns></returns>
    [LuaCallCSharp]
    public System.Object loadSourcesFromAssetBundle(string sourceName)
    {
        var obj = ab.LoadAsset(sourceName);
        return obj;
    }
}
