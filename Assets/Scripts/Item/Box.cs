using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
public class Box : MonoBehaviour
{
    public Color finishColor;
    Color originColor;
    [Header("对应数值")]
    public int number;
    [Header("层级检测")]
    public LayerMask detectLayer;
    [Header("移动方向")]
    Vector3 targetPosition;
    [Header("箱子移动速度")]
    public float moveSpeed = 1f;


    //[SerializeField]
    //Text boxNum;
    SpriteRenderer boxSR;
    private void Start()
    {
        targetPosition = transform.position;

        originColor = GetComponentInChildren<SpriteRenderer>().color;
        FindObjectOfType<GameManager>().totalBoxs++;

        //boxNum.text = number.ToString();
        boxSR = GetComponentInChildren<SpriteRenderer>();
        ReplaceBoxSprite(number);
    }

    //箱子同样进行射线检测
    public bool CanMoveToDir(Vector2 dir)
    {
        //发射射线偏离一点（具体偏离数值、长度调试确定）
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir *0.5f, dir, 0.4f, detectLayer);
        //如果没打到东西
        if (!hit)
        {
            ReplaceBoxSprite(number);
            Move(dir);//dir表示要移动的距离，根据实际情况调整
            return true;
        }
        else
        {
            //判断后方是否有箱子
            if(hit.collider.GetComponent<Box>() != null)
            {
                number += hit.collider.GetComponent<Box>().number;
                Debug.Log("推动箱子数量"+number);
                ReplaceBoxSprite(number);
                hit.collider.GetComponent<Box>().number = 0;
                hit.collider.GetComponent<Box>().ReplaceBoxSprite(0);
            }
        }
        Debug.Log("走不动啦！");
        return false;
    }

    //如果到达目的地（通过触发器判断是否终点）
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Target"))
        {
            WinOrLose();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            FindObjectOfType<GameManager>().finishedBoxs--;
            GetComponentInChildren<SpriteRenderer>().color = originColor;
        }
    }


    //判断胜利失败
    public void WinOrLose()
    {
        if ((int)Mathf.Sqrt(number) == Mathf.Sqrt(number))
        {
            //过关
            FindObjectOfType<GameManager>().finishedBoxs++;
            FindObjectOfType<GameManager>().CheckFinish();
            GetComponentInChildren<SpriteRenderer>().color = finishColor;
            Debug.Log("胜利");
        }
        else
        {
            
            Debug.Log("失败");
        }
    }

    
    //切换箱子图片
    public void ReplaceBoxSprite(int boxcarrotNum)
    {
        string BoxSpritePath = "Item/Box/box_";
        switch (boxcarrotNum)
        {
            case 0:
                BoxSpritePath += "0";
                break;
            case 1:
                BoxSpritePath += "1";
                break;
            case 2:
                BoxSpritePath += "2";
                break;
            case 3:
                BoxSpritePath += "3";
                break;
            case 4:
                BoxSpritePath += "4";
                break;
            case 5:
                BoxSpritePath += "5";
                break;
            default:
                BoxSpritePath += "fill";
                break;
        }

        boxSR.sprite = Resources.Load<Sprite>(BoxSpritePath);
    }

    public void Move(Vector3 moveDir)
    {
        if (Vector3.Distance(transform.position, targetPosition) < Mathf.Epsilon)
        {
            targetPosition = transform.position + moveDir;
            StartCoroutine(BoxMove(targetPosition));
        }
    }
    IEnumerator BoxMove(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }

    }
}
