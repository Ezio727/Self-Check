using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : Singleton<UImanager>
{
    public Text totalC;
    public Text WinText;
    public void OnStart()
    {
        if (GameManager.Ins.check == false)
        {
            GameManager.Ins.cash.TotalCash -= 1;
            GameManager.Ins.onLeverPulled.Invoke();
            GameManager.Ins.check = true;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
