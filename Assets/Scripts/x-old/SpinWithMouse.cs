using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWithMouse : MonoBehaviour
{
    private bool isClick = false;
    private Vector3 nowPos;
    private Vector3 oldPos;
    public float length = 5;

    void OnMouseUp()
    { //���̧��
        isClick = false;
    }

    void OnMouseDown()
    { //��갴��

        isClick = true;
    }

    void Update()
    {
        nowPos = Input.mousePosition;
        if (isClick)
        { //��갴�²�����
            Vector3 offset = nowPos - oldPos;
            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y) && Mathf.Abs(offset.x) > length)
            { //������ת
                transform.Rotate(Vector3.up, -offset.x);
            }
        }
        oldPos = Input.mousePosition;
    }
}
