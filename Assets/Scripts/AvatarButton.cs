using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AvatarButton : MonoBehaviour
{
    string[] names;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Toggle>().onValueChanged.AddListener(onButtonClicked);
        names = gameObject.name.Split('-');

    }
    public void onButtonClicked(bool value)
    {
           AvatarSystem._instance.OnChangePeople(names[0], names[1]);
           switch (names[0])
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
                   break;
           }
    }
    public void PlayAnimation(string animName)
    { //»»×°¶¯»­Ãû³Æ

        Animation anim = GameObject.FindWithTag("Player").GetComponent<Animation>();
        if (!anim.IsPlaying(animName))
        {
            anim.Play(animName);
            anim.PlayQueued("idle1");
        }

    }

}
