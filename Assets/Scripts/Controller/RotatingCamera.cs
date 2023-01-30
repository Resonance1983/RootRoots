using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    public float rotateTime = 0.2f;
    private Transform player;
    //防止多次旋转冲突
    private bool isRotating = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        //摄像机跟随玩家
        transform.position = player.position;

        Rotate();
    }

    void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {
            StartCoroutine(RotateAround(-45,rotateTime));
        }
        if (Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            StartCoroutine(RotateAround(45, rotateTime));
        }
    }

    //旋转角度 旋转时间
    IEnumerator RotateAround(float angle,float time)
    {
        //计算需要旋转多少次（FixedUpdate一秒执行60次）
        float number = 60 * time;
        float nextAngle = angle / number;
        isRotating = true;

        for(int i = 0; i < number; i++)
        {
            transform.Rotate(new Vector3(0, 0, nextAngle));
            yield return new WaitForFixedUpdate();
        }
        isRotating = false;
    }
}
