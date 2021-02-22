using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _ins;
    public static T Ins
    {
        get 
        {
            GameObject obj = GameObject.Find("GameManager") ? GameObject.Find("GameManager") : new GameObject("GameManager");
            if (obj.GetComponent<T>())
                _ins = obj.GetComponent<T>();
            else
                _ins = obj.AddComponent<T>();

            return _ins;
        }
    }
    public virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
