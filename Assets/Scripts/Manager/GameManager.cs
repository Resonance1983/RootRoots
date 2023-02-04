using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("要达成的箱子数量")]
    public int totalBoxs;
    public int finishedBoxs;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            //重新加载当前关卡
            ResetStage();
    }

    public void CheckFinish()
    {
        if (finishedBoxs == totalBoxs)
        {
            print("YOU WIN!");
            //显示结束UI
            GameObject Dialog = GameObject.Find("Canvas").transform.Find("Dialog").gameObject;
            Dialog.SetActive(true);
            //StartCoroutine(LoadNextStage());
        }
    }


    public void ResetStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNext(){
        StartCoroutine(LoadNextStage());
    }

    IEnumerator LoadNextStage()
    {
        yield return new WaitForSeconds(1);
        //当前关卡编号+1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
