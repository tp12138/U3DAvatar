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
    /// 配置点击事件
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
    //左键点击事件,换装
    private void ButtonLeftClick()
    {
        AvatarControl._instance.OnChangePeople(names[0], names[1]);
       
    }

    //右键点击事件,删除装备
    private void ButtonRightClick()
    {
        AvatarControl._instance.removeMesh(gameObject);
        //Destroy(gameObject);
    }
}
