using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishClick : MonoBehaviour
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene(0);
        } else if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
