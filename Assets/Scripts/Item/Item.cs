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
    /// 用于加减、根号等操作
    /// </summary>
    [Header("物品事件种类")]
    public ItemType itemType;
    int result;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            result = collision.gameObject.GetComponent<Box>().number;
            switch (itemType)
            {
                case ItemType.SQR:
                    result = ((int)Mathf.Sqrt(result) == Mathf.Sqrt(result)) ? (int)Mathf.Sqrt(result) : result ;
                    Debug.Log(result);
                    collision.gameObject.GetComponent<Box>().number = result;
                    //TODO
                    break;

                //TODO
                case ItemType.ADD:
                    break;
                case ItemType.SUB:
                    break;
                case ItemType.MUL:
                    break;
                case ItemType.DIV:
                    break;
                default: break;

            }

        }
    }
}
