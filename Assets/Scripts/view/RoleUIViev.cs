using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;
public class RoleUIViev : MonoBehaviour
{

    [HideInInspector]
    public AvatarControl avatarControl;
    [HideInInspector]
    public GameObject target;//Ŀ��Ǽ�
    [HideInInspector]
    public Transform[] targetHips;//Ŀ��ǼܵĹ�����Ϣ
    [HideInInspector]
    public Dictionary<string, SkinnedMeshRenderer> targetSkinned;//Ŀ��Ǽ��ϲ�λ->��Ƥ����Ϣ��ӳ���ֵ�
    [HideInInspector]
    public Dictionary<string, Transform> targetHipsDict;//Ŀ��Ǽ����� �������ֵ�������ӳ��
    private LuaEnv luaEnv;
    public TextAsset luaScript;
    [CSharpCallLua]
    delegate void RemoveMesh(GameObject go);
    RemoveMesh a;
    //delegate void removeMesh(GameObject go)
    // Start is called before the first frame update
    void Start()
    {
        targetSkinned = new Dictionary<string, SkinnedMeshRenderer>();
        avatarControl = gameObject.GetComponent<AvatarControl>();
        luaEnv = new LuaEnv();
        luaEnv.DoString(luaScript.text);
        AvatarControl._instance.remove = luaEnv.Global.Get<Action<GameObject>>("OnremoveMesh");
        AvatarControl._instance.tryChangePeople = luaEnv.Global.Get<Action<string, string>>("tryToChangePeople");
      
    }

    public void removeMesh(GameObject go)
    {
        a(go);
    } 

    public byte[] LoadLuaScript(ref string filename)
    {

        string path = Application.dataPath + "/Scripts/control/" + filename + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(System.IO.File.ReadAllText(path));
    }

    public void onInitNewRole(GameObject target)
    {
        this.target = target;// Instantiate(target);
        initTargetBones(this.target);
    }
    /// <summary>
    /// �޸ĳ�����ģ�͵Ķ�Ӧ��λ����Ƥ������Ⱦ��������
    /// </summary>
    /// <param name="part">��λ</param>
    /// <param name="num">��λ������Ƥ�ı��</param>
    public void changeMesh(string state,string part, string num)
    {
         SkinnedMeshRenderer smrTemp = avatarControl.getSkinnedMeshByPartAndNum(part, num);
         if (smrTemp == null)
            {
                Debug.LogError("not config skinnedMesh");
                return;
            }
         List<Transform> bones =getBonesBySmr(smrTemp);
         //�滻SkinnedMesh
         targetSkinned[part].material = smrTemp.sharedMaterial;
         targetSkinned[part].bones = bones.ToArray();
         targetSkinned[part].sharedMesh = smrTemp.sharedMesh;
         targetSkinned[part].rootBone = smrTemp.rootBone;
         switch (state)
         {
             case "pants":
                 PlayAnimation("item_pants");
                 break;
             case "shoes":
                 PlayAnimation("item_boots");
                 break;
             case "top":
                 PlayAnimation("item_shirt");
                 break;
             default:
                 PlayAnimation("walk");
                 break;
         }
    }

    /// <summary>
    /// �ڹǼ������ɲ�λ
    /// </summary>
    /// <param name="partName">��λ����</param>
    public void setNewPart(string partName)
    {
        //Debug.Log("set a new part :" + partName);
        GameObject go = new GameObject();
        go.name = partName;
        go.transform.parent =target.GetComponent<Transform>();
        targetSkinned.Add(partName,go.AddComponent<SkinnedMeshRenderer>());
    }

    /// <summary>
    /// ��ȡ��Ƥ��Ⱦ���й�����Ӧ�Ǽ��еĹ�������
    /// </summary>
    /// <param name="smr"></param>
    /// <returns>��Ƥ������Ⱦ����Ӧ�Ĺ�������</returns>
    public List<Transform> getBonesBySmr(SkinnedMeshRenderer smr)
    {
        List<Transform> bones = new List<Transform>();
        foreach (var trans in smr.bones)
            if (targetHipsDict.ContainsKey(trans.name))
                bones.Add(targetHipsDict[trans.name]);
        return bones;
    }

    /// <summary>
    /// ��ȡĿ��ǼܵĹ�����Ϣ,������������->������ӳ��
    /// </summary>
    private void initTargetBones(GameObject tObj)
    {
        this.target = tObj;
        targetHipsDict = new Dictionary<string, Transform>();
        targetHips = target.GetComponentsInChildren<Transform>();
        //����Ŀ��Ǽ����ϴ����ֵ�������ӳ��,ֱ�Ӳ���
        foreach (var t in targetHips)
            targetHipsDict.Add(t.name, t);
    }
    private void PlayAnimation(string animName)
    { //��װ��������

        Animation anim = GameObject.FindWithTag("Player").GetComponent<Animation>();
        if (!anim.IsPlaying(animName))
        {
            anim.Play(animName);
            anim.PlayQueued("idle1");
        }

    }


}
