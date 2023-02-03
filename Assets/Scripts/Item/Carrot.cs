using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

public class Carrot : MonoBehaviour
{
    public Animator carrotAnim;
    void Awake()
    {
        carrotAnim= GetComponent<Animator>();
    }

    //关闭所有动画 (当前有bug)
    public void StopAllAnimation()
    {
        carrotAnim.SetBool("AddCarrot", false);
        carrotAnim.SetBool("SubCarrot" , false);
    }
}
