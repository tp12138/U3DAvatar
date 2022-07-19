using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class dealWithFBX:MonoBehaviour
{
    //public 
    public GameObject fbx;
    // Start is called before the first frame update
   public static void deal(GameObject source)
    {
        Debug.Log("?");
        SkinnedMeshRenderer[] smrs = source.GetComponentsInChildren<SkinnedMeshRenderer>();
       Dictionary<string, SkinnedMeshRenderer> sourceSmrs = new Dictionary<string, SkinnedMeshRenderer>();
       for (int i = 0; i < smrs.Length; i++)
       {
           sourceSmrs.Add(smrs[i].name, smrs[i]);
       }
      // Material[] materials = Resources.LoadAll<Material>(Application.dataPath+"/Sources/Character/characters/Female/Per Texture Materials");
       var materials = Resources.LoadAll<Material>("");
       //Debug.Log(materials.Length);
       int qian = 0;
       string tn = materials[0].name.Split('_')[0].Split('-')[0];
       //string now = "";
        for (int i = 0; i < materials.Length; i++)
       {
           string[] names = materials[i].name.Split('_');
           string[] tnames = names[0].Split('-');
           if (tn.Equals(tnames[0]))
           {
               qian++;
           }
           else
           {
               qian = 1;
               tn = tnames[0];
           }
           GameObject go = new GameObject();
           SkinnedMeshRenderer newSmr = go.AddComponent<SkinnedMeshRenderer>();
           SkinnedMeshRenderer newS = new SkinnedMeshRenderer();
           newSmr.bones = sourceSmrs[names[0]].bones;
           newSmr.sharedMesh = sourceSmrs[names[0]].sharedMesh;
           newSmr.material = materials[i];
           newSmr.rootBone = sourceSmrs[names[0]].rootBone;
            string[] temp=names[0].Split('-');
            go.name = temp[0] + "-" + (qian).ToString();
           var t= PrefabUtility.CreatePrefab(Application.dataPath + "/Prefabs/" + go.name + ".prefab", go);
           Debug.Log(t == null);
       }

    }
   public void Awake()
   {
       deal(fbx);
   }
}
