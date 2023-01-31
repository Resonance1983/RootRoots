using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        NULL,
        ADD,
        SUB,
        MUL,
        DIV,
        SQR
    }
    /// <summary>
    /// ���ڼӼ������ŵȲ���
    /// </summary>
    [Header("��Ʒ�¼�����")]
    public ItemType itemType;
    int result;

    [SerializeField]
    int addNumber = 2;
    [SerializeField]
    int subNumber = -3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            result = collision.gameObject.GetComponent<Box>().number;
            switch (itemType)
            {
                case ItemType.SQR:
                    //result = ((int)Mathf.Sqrt(result) == Mathf.Sqrt(result)) ? (int)Mathf.Sqrt(result) : result ;
                    if ((int)Mathf.Sqrt(result) == Mathf.Sqrt(result))
                    {
                        result = (int)Mathf.Sqrt(result);
                        Debug.Log(result);
                        collision.gameObject.GetComponent<Box>().number = result;
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        Destroy(gameObject);
                        Debug.Log("无法开根 箱子与方块消失");
                    }
                    //TODO
                    break;

                //TODO
                case ItemType.ADD:
                    collision.gameObject.GetComponent<Box>().number += addNumber;
                    Debug.Log(collision.gameObject.GetComponent<Box>().number);
                    break;
                case ItemType.SUB:
                    collision.gameObject.GetComponent<Box>().number -= subNumber;
                    Debug.Log(collision.gameObject.GetComponent<Box>().number);
                    break;
                case ItemType.MUL:
                    break;
                case ItemType.DIV:
                    break;
                default: break;

            }
            //关闭当前脚本  防止重复运算
            this.GetComponent<Item>().enabled= false;
        }
    }
}
