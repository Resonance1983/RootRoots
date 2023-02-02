using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    //��ȡ����������
    Transform[] childs;
    void Start()
    {
        //ѭ����ȡҪ�����������������
        childs = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        //�����е�spriteʼ�ճ��������
        for(int i = 0; i < childs.Length; i++)
        {
            childs[i].rotation = Camera.main.transform.rotation;
        }
    }
}
