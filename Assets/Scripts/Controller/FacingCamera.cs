using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    //获取所有子物体
    Transform[] childs;
    void Start()
    {
        //循环获取要朝向摄像机的子物体
        childs = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        //场景中的sprite始终朝向摄像机
        for(int i = 0; i < childs.Length; i++)
        {
            childs[i].rotation = Camera.main.transform.rotation;
        }
    }
}
