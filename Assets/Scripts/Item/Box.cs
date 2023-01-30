using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Color finishColor;
    Color originColor;

    private void Start()
    {
        originColor = GetComponent<SpriteRenderer>().color;
        FindObjectOfType<GameManager>().totalBoxs++;
    }

    //箱子同样进行射线检测
    public bool CanMoveToDir(Vector2 dir)
    {
        //发射射线偏离一点（具体偏离数值、长度调试确定）
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.5f, dir, 0.5f);
        //如果没打到东西
        if (!hit)
        {
            transform.Translate(dir);//dir表示要移动的距离，根据实际情况调整
            return true;
        }

        return false;
    }

    //如果到达目的地（通过触发器判断是否终点）
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Target"))
        {
            Debug.Log("6666");
            FindObjectOfType<GameManager>().finishedBoxs++;
            FindObjectOfType<GameManager>().CheckFinish();
            GetComponent<SpriteRenderer>().color = finishColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            FindObjectOfType<GameManager>().finishedBoxs--;
            GetComponent<SpriteRenderer>().color = originColor;
        }
    }
}
