using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class dealWithFBX:MonoBehaviour
{

   public GameObject[] fbx;
   public GameObject target;
   public static void dealMesh(GameObject source,GameObject target)
    {
       SkinnedMeshRenderer[] smrs = source.GetComponentsInChildren<SkinnedMeshRenderer>();
       Dictionary<string, SkinnedMeshRenderer> sourceSmrs = new Dictionary<string, SkinnedMeshRenderer>();
       //����ԭģ�͵����ֵ�skinnedMesh�ֵ�
       foreach (var r in smrs)
         sourceSmrs.Add(r.name, r);
       Dictionary<string, Transform> nameToBones = new Dictionary<string, Transform>();//�Ǽ���,���ֵ������Ķ�Ӧ
       Transform[] trans = target.GetComponentsInChildren<Transform>();//��ȡ�Ǽ��еĹ�����Դ
       Dictionary<string, Mesh> nameToMesh = new Dictionary<string, Mesh>();//���������Mesh,���ֵ�Mesh���ֵ�
       //����Ǽ������ֵ�������ӳ��
       for (int i = 0; i < trans.Length; i++)
           nameToBones.Add(trans[i].name, trans[i]);
       //��ԭģ���а����mesh���������ֵ���Mesh��ӳ��
       for (int i = 0; i < smrs.Length; i++)
       {
           Mesh tempMesh = smrs[i].sharedMesh;
           Mesh newMesh = UnityEngine.Object.Instantiate<Mesh>(tempMesh);
           newMesh.name = string.Format(tempMesh.name);
           string newMeshPath = Path.Combine("Assets/Asset/", string.Format("{0}.{1}", newMesh.name, "asset"));
           AssetDatabase.CreateAsset(newMesh, newMeshPath);
           AssetDatabase.SaveAssets();
           AssetDatabase.Refresh();
           Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(newMeshPath);
           if (mesh != null) nameToMesh.Add(mesh.name, mesh);
           else Debug.Log("can`t load mesh:" + newMeshPath);
       }

       var materials = Resources.LoadAll<Material>("");
       int num = 0;
       //���ݲ�����,���������µ�skinnedMesh ���ұ���Ԥ����
       if(materials!=null&&materials.Length!=0){
           string nameOld = materials[0].name.Split('_')[0].Split('-')[0];
           for (int i = 0; i < materials.Length; i++)
           {
               string[] names = materials[i].name.Split('_');
               string[] tnames = names[0].Split('-');
               if (nameOld.Equals(tnames[0]))
               {
                   num++;
               }
               else
               {
                   num = 1;
                   nameOld = tnames[0];
               }
               GameObject go = new GameObject();
               SkinnedMeshRenderer newSmr = go.AddComponent<SkinnedMeshRenderer>();
               SkinnedMeshRenderer oldSmr=sourceSmrs[names[0]];
               List<Transform> bones = new List<Transform>();
               foreach (var t in oldSmr.bones)
               {
                   if(nameToBones.ContainsKey(t.name))
                       bones.Add(nameToBones[t.name]);
               }
               newSmr.bones = bones.ToArray();//�������ù���
               newSmr.sharedMesh = nameToMesh[names[0]];//��������mesh
               newSmr.material = materials[i];
               newSmr.rootBone = nameToBones[sourceSmrs[names[0]].rootBone.name];
               go.name = tnames[0] + "-" + num.ToString();
               newSmr.localBounds = sourceSmrs[names[0]].bounds;
               Debug.Log(PrefabUtility.CreatePrefab(Application.dataPath + "/Prefabs/" + go.name + ".prefab", go)==null);
           }
       }
       
    }

   public void dealAnimationClip(GameObject[] fbx,GameObject target)
   {
       Dictionary<string,AnimationClip> list = new Dictionary<string,AnimationClip>();
       for (int i = 1; i < fbx.Length;i++ )
       {
           var t = fbx[i];
           var anim = t.GetComponent<Animation>().clip;
           AnimationClip newClip = new AnimationClip();
           EditorUtility.CopySerialized(anim, newClip);
           newClip.name = string.Format(anim.name);
           string newAnimClpiPath = Path.Combine("Assets/Asset/", string.Format("{0}.{1}", newClip.name, "anim"));
           AssetDatabase.CreateAsset(newClip, newAnimClpiPath);
           AssetDatabase.SaveAssets();
           list.Add(anim.name, AssetDatabase.LoadAssetAtPath<AnimationClip>(newAnimClpiPath));
       }
       Animation animation = target.AddComponent<Animation>();
       foreach(KeyValuePair<string,AnimationClip> t in list){
           animation.AddClip(t.Value,t.Key);
       }
       animation.clip = list["idle1"]; 
   }
   public void Awake()
   {
       //dealAnimationClip(fbx, target);
       //dealMesh(fbx[0],target);
       
   }
}
