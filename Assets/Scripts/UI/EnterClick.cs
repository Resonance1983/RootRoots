using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterClick : MonoBehaviour
{
    bool isPause = true;

    private void Start() {
        Pause();
        isPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            if (isPause){
                unPause();
                this.gameObject.SetActive(false);
            }  
            else
                Pause();
        }    
    }

    private void Pause() {
        Time.timeScale = 0f;
    }
    private void unPause() {
        Time.timeScale = 1f;
    }
}
