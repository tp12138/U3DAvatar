using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleUIViev : MonoBehaviour
{
    [HideInInspector]
    public AvatarModel avatarModel;
    [HideInInspector]
    public AvatarControl avatarControl;
    // Start is called before the first frame update
    void Awake()
    {
        avatarModel = gameObject.GetComponent<AvatarModel>();
        avatarControl = gameObject.GetComponent<AvatarControl>();
        avatarModel.onInstancePart += setNewPart;
        avatarModel.deleteSkinnedMesh += deleteGameObject;
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeMesh(string part, string num)
    {
         SkinnedMeshRenderer smrTemp = avatarModel.getSkinnedMeshByPartAndNum(part, num);
         if (smrTemp == null)
            {
                Debug.LogError("not config skinnedMesh");
                return;
            }
            List<Transform> bones=avatarModel.getBonesBySmr(smrTemp);
            //Ìæ»»SkinnedMesh
            avatarModel.targetSkinned[part].material = smrTemp.sharedMaterial;
            avatarModel.targetSkinned[part].bones = bones.ToArray();
            avatarModel.targetSkinned[part].sharedMesh = smrTemp.sharedMesh;
            avatarModel.targetSkinned[part].rootBone = smrTemp.rootBone;
            avatarModel.saveData(part, num);
    }


    public void setNewPart(string partName)
    {
        GameObject go = new GameObject();
        go.name = partName;
        go.transform.parent = avatarModel.target.GetComponent<Transform>();
        avatarModel.targetSkinned.Add(partName,go.AddComponent<SkinnedMeshRenderer>());
    }

    public void deleteGameObject(GameObject go)
    {
        Destroy(go);
    }
}
