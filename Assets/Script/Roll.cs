using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Roll 
{
    public int value;

    public bool rollStopped = true;

    public float delay;
    public Text valueDisplay;
    public IEnumerator Rotate()
    {
        rollStopped = false;
        yield return new WaitForSeconds(delay);
        value = Random.Range(1, 4);
        rollStopped = true;
    }
}
