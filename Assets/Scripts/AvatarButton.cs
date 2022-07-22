using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class AvatarButton : MonoBehaviour, IPointerClickHandler
{
    string[] names;
    public UnityEvent leftClick;
    public UnityEvent rightClick;
    /// <summary>
    /// ���õ���¼�
    /// </summary>
    private void Start()
    {
        leftClick.AddListener(new UnityAction(ButtonLeftClick));
        rightClick.AddListener(new UnityAction(ButtonRightClick));
        names = gameObject.name.Split('-');
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
    }
    //�������¼�,��װ
    private void ButtonLeftClick()
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
                PlayAnimation("walk");
                break;
        }
    }

    //�Ҽ�����¼�,ɾ��װ��
    private void ButtonRightClick()
    {
        AvatarSystem._instance.removeMesh(names[0], names[1]);
        Destroy(gameObject);
    }
    //���Ŷ���
    public void PlayAnimation(string animName)
    { //��װ��������

        Animation anim = GameObject.FindWithTag("Player").GetComponent<Animation>();
        if (!anim.IsPlaying(animName))
        {
            anim.Play(animName);
            anim.PlayQueued("idle1");
        }

    }

}
