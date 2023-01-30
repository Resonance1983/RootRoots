using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移动方向")]
    Vector2 moveDir;
    [Header("层级检测")]
    public LayerMask detectLayer;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D))
            moveDir = Vector2.right;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            moveDir = Vector2.left;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            moveDir = Vector2.up;

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            moveDir = Vector2.down;

        if(moveDir != Vector2.zero)
        {
            //如果该方向可以移动
            if(CanMoveToDir(moveDir))
            {
                Move(moveDir);
            }
        }

        moveDir = Vector2.zero;
    }


    //射线检测判断能否移动
    bool CanMoveToDir(Vector2 dir)
    {
        //detectLayer:避免射线打到本身
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.5f, detectLayer);

        if (!hit)
            return true;
        else
        {
            if (hit.collider.GetComponent<Box>() != null)
                //如果检测到的是箱子，则告诉箱子可以移动
                return hit.collider.GetComponent<Box>().CanMoveToDir(dir);
        }

        return false;
    }

    void Move(Vector2 dir)
    {
        transform.Translate(dir);//dir表示要移动的距离，根据实际情况调整
    }
}
