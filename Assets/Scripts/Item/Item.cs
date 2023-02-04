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

    private Animator characterAnimator;
    private int usedID = Animator.StringToHash("Used");

    private void Start()
    {
        characterAnimator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            result = collision.gameObject.GetComponent<Box>().number;
            switch (itemType)
            {
                case ItemType.SQR:
                    //result = ((int)Mathf.Sqrt(result) == Mathf.Sqrt(result)) ? (int)Mathf.Sqrt(result) : result ;
                    if ((int)Mathf.Sqrt(result) == Mathf.Sqrt(result) && result != 0)
                    {
                        result = (int)Mathf.Sqrt(result);
                        Debug.Log(result);
                        collision.gameObject.GetComponent<Box>().number = result;
                        //播放萝卜飞出箱子的动画
                        collision.transform.GetChild(1).gameObject.SetActive(true);
                        collision.gameObject.GetComponentInChildren<Carrot>().carrotAnim.SetBool("SubCarrot", true);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                        Debug.Log("无法开根 箱子与方块消失");
                    }
                    //TODO
                    break;

                //TODO
                case ItemType.ADD:
                    

                    //播放萝卜调入箱子的动画
                    collision.transform.GetChild(1).gameObject.SetActive(true);
                    collision.gameObject.GetComponentInChildren<Carrot>().carrotAnim.SetBool("AddCarrot", true);

                    Destroy(gameObject);

                    collision.gameObject.GetComponent<Box>().number += addNumber;
                    Debug.Log(collision.gameObject.GetComponent<Box>().number);
                    break;
                case ItemType.SUB:
                    collision.gameObject.GetComponent<Box>().number -= subNumber;
                    Debug.Log(collision.gameObject.GetComponent<Box>().number);

                    if (collision.gameObject.GetComponent<Box>().number < 0)
                        Debug.Log("负数  Loss");
                    characterAnimator.SetBool(usedID, true);
                    Move();
                    //Destroy(gameObject);
                    break;
                case ItemType.MUL:
                    break;
                case ItemType.DIV:
                    break;
                default: break;

            }
            collision.GetComponent<Box>().ReplaceBoxSprite(collision.gameObject.GetComponent<Box>().number);
        }
    }

    public void Move()
    {
        
        Vector3 targetPosition = transform.position;
        Debug.Log(Vector3.Distance(transform.position, targetPosition));
        if (Vector3.Distance(transform.position, targetPosition) < Mathf.Epsilon)
        {
            
            targetPosition = transform.position + 10 * Vector3.up;
            StartCoroutine(ItemMove(targetPosition));
        }
    }

    IEnumerator ItemMove(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2.5f);
            yield return null;
        }

    }
}
