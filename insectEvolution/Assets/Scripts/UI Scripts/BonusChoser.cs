using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BonusChoser : MonoBehaviour
{
    int value;
    int CollectedCoin;
    int curvalue;
    [SerializeField] Text bonusText; //this used to show how many you got from the chest it located in complete panel

    private void Start()
    {
        CoinsManager.Instance.AddCoinOnGamePlay();
        CollectedCoin = UpgardeManager.instace.characterController.totallCollection;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("bonus"))
        {
            //ReferenceManager.instance.gameManager.PopVibrate();
            //ReferenceManager.instance.soundManager.SelectSound();
            collision.GetComponent<DOTweenAnimation>().DORestart();
            value = int.Parse(collision.transform.GetComponentInChildren<Text>().text);
            curvalue = value * CollectedCoin;
            bonusText.text = curvalue.ToString();

            //SwitchChests(value);
            //CoinsManager.Instance.AddCoins(value);
            //CoinsManager.Instance.AddCoinOnGamePlay();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<DOTweenAnimation>().DORewind();
    }

    public void StopAnimation()
    {

        CoinsManager.Instance.AdCase = true;
        CoinsManager.Instance.CollectionValue = curvalue;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<DOTweenAnimation>().DOPause();
    }


}
