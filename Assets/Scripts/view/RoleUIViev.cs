using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class RoleUIViev : MonoBehaviour
{
    [HideInInspector]
    public AvatarModel avatarModel;
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
   
    // Start is called before the first frame update
    void Awake()
    {
        targetSkinned = new Dictionary<string, SkinnedMeshRenderer>();
        avatarModel = gameObject.GetComponent<AvatarModel>();
        avatarControl = gameObject.GetComponent<AvatarControl>();
        avatarModel.onUpdatePart += changeMesh;
        avatarModel.onAddNewPart += setNewPart;
        
    }


    /// <summary>
    /// �޸ĳ�����ģ�͵Ķ�Ӧ��λ����Ƥ������Ⱦ��������
    /// </summary>
    /// <param name="part">��λ</param>
    /// <param name="num">��λ������Ƥ�ı��</param>
    public void changeMesh(string part, string num)
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
    }

    /// <summary>
    /// �ڹǼ������ɲ�λ
    /// </summary>
    /// <param name="partName">��λ����</param>
    public void setNewPart(string partName)
    {
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
    public void initTargetBones()
    {
        targetHipsDict = new Dictionary<string, Transform>();
        targetHips = target.GetComponentsInChildren<Transform>();
        //����Ŀ��Ǽ����ϴ����ֵ�������ӳ��,ֱ�Ӳ���
        foreach (var t in targetHips)
            targetHipsDict.Add(t.name, t);
    }

}
