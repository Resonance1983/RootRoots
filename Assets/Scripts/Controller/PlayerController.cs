using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移动方向")]
    Vector3 moveDir;
    Vector3 targetPosition;
    [Header("层级检测")]
    public LayerMask detectLayer;

    [SerializeField]
    float detactDistance = 1f;//检测距离
    [SerializeField]
    float moveSpeed = 4f;

    //移动状态 false=正常移动，true=推箱子移动
    private bool pushing;

    //Animator相关
    private Animator characterAnimator;
    private int isMovingID = Animator.StringToHash("IsMoving");
    private int isPushingID = Animator.StringToHash("IsPushing");
    private int verticalID = Animator.StringToHash("Vertical");
    private int horizontalID = Animator.StringToHash("Horizontal");

    private void Start()
    {
        targetPosition = transform.position;
        characterAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D))
        {
            moveDir = Vector3.right;
            characterAnimator.SetFloat(verticalID, 1f);
            characterAnimator.SetFloat(horizontalID, 0f);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            moveDir = Vector3.left;
            characterAnimator.SetFloat(verticalID, -1f);
            characterAnimator.SetFloat(horizontalID, 0f);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            moveDir = Vector3.up;
            characterAnimator.SetFloat(verticalID, 0f);
            characterAnimator.SetFloat(horizontalID, 1f);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            moveDir = Vector3.down;
            characterAnimator.SetFloat(verticalID, 0f);
            characterAnimator.SetFloat(horizontalID, -1f);
        }
        if (moveDir != Vector3.zero)
        {
            if (CanMoveToDir(moveDir))
            {
                Move(moveDir);
            }
        }
        else
        {
            characterAnimator.SetBool(isPushingID, false);
            characterAnimator.SetBool(isMovingID, false);
        }
        moveDir = Vector3.zero;
    }


    //射线检测判断能否移动
    bool CanMoveToDir(Vector3 dir)
    {
        //detectLayer:避免射线打到本身
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, detactDistance, detectLayer);

        if (!hit)
        {
            Debug.Log("unhit");
            pushing = false;
            return true;
        }   
        else
        {
            if (hit.collider.GetComponent<Box>() != null)
            {
                Debug.Log("hitbox");
                //如果检测到的是箱子 并且箱子后没有箱子 则告诉箱子可以移动
                pushing = true;
                return hit.collider.GetComponent<Box>().CanMoveToDir(dir);
            }
        }
        return false;
    }
    void Move(Vector3 dir)
    {
        //transform.Translate(dir , Space.World);//dir表示要移动的距离，根据实际情况调整

        //求出将要移动目标位置

        if (Vector3.Distance(transform.position , targetPosition ) < Mathf.Epsilon)
        {
            if (pushing)
            {
                characterAnimator.SetBool(isPushingID, true);
            }
            else
            {
                characterAnimator.SetBool(isMovingID, true);
            }

            targetPosition = transform.position + dir;
            StartCoroutine(PlayerMove(targetPosition));
        }
    }

    //玩家移动协程
    IEnumerator PlayerMove(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position , targetPosition) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition,  Time.deltaTime * moveSpeed);
            yield return null;
        }

    }
}
