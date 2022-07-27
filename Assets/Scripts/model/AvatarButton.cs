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
        AvatarControl._instance.OnChangePeople(names[0], names[1]);
       
    }

    //�Ҽ�����¼�,ɾ��װ��
    private void ButtonRightClick()
    {
        AvatarControl._instance.removeMesh(gameObject);
        //Destroy(gameObject);
    }
}
