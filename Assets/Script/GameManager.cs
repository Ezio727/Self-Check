using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /*1 1 1
    2 2 2
    3 3 3

    Total: ($20)
    Button: Start ($-1)

Win - 111 - ($2x1) = $2 -Coin 1
Win - 222 - ($2x9) ->= $18 JACKPOT - Coin 10
Win - 333 - ($2x2) = $4 - Coin 2
    */
    public delegate void OnLeverPulled();
    public OnLeverPulled onLeverPulled;

    public Cash cash;
    public Roll[] rolls;
    public bool check;
    public GameObject prefab;
    public Transform spaw1, spaw2;
    public int winPrize()
    {
        int prize;
        if(rolls[0].value == rolls[1].value && rolls[1].value == rolls[2].value)
        {
            if (rolls[0].value == 1)
            {
                prize = 2;

                Instantiate(prefab, new Vector3(Random.Range(-250, 250), spaw2.position.y, 0), Quaternion.identity);
            }
            else if (rolls[1].value == 2)
            {
                prize = 18;
                for(int i = 0;i <5; i++)
                {
                    StartCoroutine(inst((float)i));
                }
            }
            else if (rolls[2].value == 3)
            {
                prize = 4;
                StartCoroutine(inst(1));
            }
            else
                prize = 0;

        }
        else
        {
            prize = 0;
        }
        return prize;
    }
    public IEnumerator inst(float delay)
    {
        yield return new WaitForSeconds(delay/2);
        GameObject rb1 = Instantiate(prefab, new Vector3(Random.Range(-250, 250),spaw1.position.y,0), Quaternion.identity);
        GameObject rb2 = Instantiate(prefab, new Vector3(Random.Range(-250, 250), spaw2.position.y, 0), Quaternion.identity);
    }
    void Start()
    {
        rolls[0].delay = 1;
        rolls[1].delay = 2;
        rolls[2].delay = 3;
        rolls[0].rollStopped = true;
        rolls[1].rollStopped = true;
        rolls[2].rollStopped = true;
        onLeverPulled += LeverPull;
    }
    void LeverPull()
    {
        StartCoroutine(rolls[0].Rotate());
        StartCoroutine(rolls[1].Rotate());
        StartCoroutine(rolls[2].Rotate());
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            for (int i = 0; i < 5; i++)
            {
                StartCoroutine(inst((float)i));
            }
        if (rolls[0].rollStopped && rolls[1].rollStopped && rolls[2].rollStopped)
        {
            if (check)
            {
                UImanager.Ins.WinText.gameObject.SetActive(true);
                cash.TotalCash += winPrize();
                check = false;
            }
            if (winPrize() != 0)
            {
                UImanager.Ins.WinText.text = "You won " + winPrize() + " coin";
            }
            else
                UImanager.Ins.WinText.text = "You lose";

        }
        else
        {
            
            UImanager.Ins.WinText.gameObject.SetActive(false);

        }
        foreach (Roll roll in rolls)
        {
            if (!roll.rollStopped)
                roll.valueDisplay.text = Random.Range(1, 3).ToString();
            else
                roll.valueDisplay.text = roll.value.ToString();
        }

        UImanager.Ins.totalC.text = "Total cash:" + cash.TotalCash.ToString();
    }
}
