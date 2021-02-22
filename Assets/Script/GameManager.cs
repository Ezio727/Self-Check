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
    public Transform spaw1;
    public int winPrize()
    {
        int prize;
        if(rolls[0].value == rolls[1].value && rolls[1].value == rolls[2].value)
        {
            if (rolls[0].value == 1)
            {
                prize = 2;
            }
            else if (rolls[1].value == 2)
            {
                prize = 18;

            }
            else if (rolls[2].value == 3)
            {
                prize = 4;
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
        Instantiate(prefab, new Vector3(Random.Range(-3, 3),spaw1.position.y,0), Quaternion.identity);
        Instantiate(prefab, new Vector3(Random.Range(-3, 3)-1f, spaw1.position.y, 0), Quaternion.identity);
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
    IEnumerator st(Roll rl, int value)
    {
        rl.rollStopped = false;
        yield return new WaitForSeconds(1);
        rl.value = value;
        rl.rollStopped = true;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(st(rolls[0], 1));
            StartCoroutine(st(rolls[1], 1));
            StartCoroutine(st(rolls[2], 1));
            check = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(st(rolls[0], 2));
            StartCoroutine(st(rolls[1], 2));
            StartCoroutine(st(rolls[2], 2));
            check = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(st(rolls[0], 3));
            StartCoroutine(st(rolls[1], 3));
            StartCoroutine(st(rolls[2], 3));
            check = true;
        }

        if (rolls[0].rollStopped && rolls[1].rollStopped && rolls[2].rollStopped)
        {
            if (check)
            {
                UImanager.Ins.WinText.gameObject.SetActive(true);
                cash.TotalCash += winPrize();
                if (winPrize() != 0)
                {
                    if(rolls[0].value == 1)
                    {
                        Instantiate(prefab, new Vector3(Random.Range(-3, 3), spaw1.position.y, 0), Quaternion.identity);
                    }
                    else if(rolls[0].value == 2)
                    {
                        StartCoroutine(inst(1));
                        StartCoroutine(inst(2));
                        StartCoroutine(inst(3));
                        StartCoroutine(inst(4));
                        StartCoroutine(inst(5));
                    }
                    else if(rolls[0].value == 3)
                    {
                        StartCoroutine(inst(1));
                    }
                    UImanager.Ins.WinText.text = "You won " + winPrize() + " coin";
                }
                else
                    UImanager.Ins.WinText.text = "You lose";
                check = false;
            }
            

        }
        else
        {
            
            UImanager.Ins.WinText.gameObject.SetActive(false);

        }
        foreach (Roll roll in rolls)
        {
            if (!roll.rollStopped)
                roll.valueDisplay.text = Random.Range(1, 4).ToString();
            else
                roll.valueDisplay.text = roll.value.ToString();
        }

        UImanager.Ins.totalC.text = "Total cash:" + cash.TotalCash.ToString();
    }
}
