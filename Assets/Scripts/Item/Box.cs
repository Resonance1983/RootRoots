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
    [Header("失败ui")]
    public GameObject loseUI;


    //[SerializeField]
    //Text boxNum;
    SpriteRenderer boxSR;
    SpriteRenderer boxCover;
    private void Start()
    {
        targetPosition = transform.position;

        originColor = GetComponentInChildren<SpriteRenderer>().color;
        

        //boxNum.text = number.ToString();
        boxSR = transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
        boxCover = transform.GetChild(2).GetComponentInChildren<SpriteRenderer>();
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
            Debug.Log("box not hit");
            ReplaceBoxSprite(number);
            
            Move(dir);//dir表示要移动的距离，根据实际情况调整
            return true;
        }
        else
        {
            //判断后方是否有箱子
            if (hit.collider.GetComponent<Box>() != null)
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

            //若成功进入A  则  终点播放动画
            if ((int)Mathf.Sqrt(number) == Mathf.Sqrt(number))
            {
                collision.GetComponentInChildren<Animator>().enabled = true;
            }
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Target"))
    //    {
    //        FindObjectOfType<GameManager>().finishedBoxs--;
    //        GetComponentInChildren<SpriteRenderer>().color = originColor;
    //    }
    //}


    //判断胜利失败
    public void WinOrLose()
    {
        
        if ((int)Mathf.Sqrt(number) == Mathf.Sqrt(number))
        {
            //过关
            FindObjectOfType<GameManager>().finishedBoxs++;
            FindObjectOfType<GameManager>().CheckFinish();

            //箱子消失
            Destroy(gameObject);

            GetComponentInChildren<SpriteRenderer>().color = finishColor;
            Debug.Log("胜利");
        }
        else
        {
            loseUI.SetActive(true);
        }

    }

    
    //切换箱子图片
    public void ReplaceBoxSprite(int boxcarrotNum)
    {
        string BoxSpritePath = "Item/Box/box_";
        switch (boxcarrotNum)
        {
            case -1:
            case -2:
            case -3:
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

        if (boxcarrotNum > 5)
        {
            
            ReplaceCoverSprite(boxcarrotNum);
        }
        else
        {
            boxCover.sprite = null;
        }
    }

    public void ReplaceCoverSprite(int num)
    {
        string CoverSpritePath = "Item/Box/box_";
        switch (num)
        {
            case 6:
                CoverSpritePath += "6";
                break;
            case 7:
                CoverSpritePath += "7";
                break;
            case 9:
                CoverSpritePath += "9";
                break;
            case 10:
                CoverSpritePath += "10";
                break;
            case 15:
                CoverSpritePath += "15";
                break;
            case 16:
                CoverSpritePath += "16";
                break;
            case 17:
                CoverSpritePath += "17";
                break;
        }
        boxCover.sprite = Resources.Load<Sprite>(CoverSpritePath);
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
