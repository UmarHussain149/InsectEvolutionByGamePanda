using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{

    public float waitTime = 1;


    private void OnEnable()
    {
        StartCoroutine(itemDisable());

    }
    IEnumerator itemDisable()
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }

}

