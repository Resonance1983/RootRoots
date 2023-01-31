using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//单例模式
public class Level<T> : MonoBehaviour where T: MonoBehaviour {
	static T instance;
	public static T Instance
    {
        get
        {
            if (Instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            return Instance;
        }
    }
}
