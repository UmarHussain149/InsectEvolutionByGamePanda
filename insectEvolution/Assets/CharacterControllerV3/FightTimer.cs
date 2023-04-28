using UnityEngine;
using UnityEngine.UI;
using AxisGames;
using System;
using DG.Tweening;

public class FightTimer : MonoBehaviour
{
    [Header("Canvas Ui")]

    [SerializeField] GameObject noThanks;
    [SerializeField] Text timerText;
    [SerializeField] DOTweenAnimation textAnimation;
    [Space(10)]

    [Header("Current Timing")]
    [SerializeField] float timePassed = 0;
    [SerializeField] float fillerTime = 0;

    [Header("Fighting Timer Settings")]

    [SerializeField] float maxTime = 10f;
    [SerializeField] float resetFactor = 1f;
    [SerializeField] float timer = 0;
    [Space(10)]
    [SerializeField] bool isFightStarted = false;
    [SerializeField] bool isTimeFinished = false;


    private void Awake()
    {

        InitializeTimer();

    }

    private void FixedUpdate()
    {
        if (isFightStarted)
        {
            timer += Time.deltaTime;
            //fillerTime -= Time.deltaTime;
            // fillerImage.fillAmount -= Time.deltaTime / fillerTime;
            //fillerImage.fillAmount = Mathf.MoveTowards(fillerImage.fillAmount,Time.deltaTime/fillerTime, Time.deltaTime);

            if (timer >= resetFactor)
            {
                timePassed--;
                timer = 0;

                timerText.text = timePassed.ToString();
                if (textAnimation) textAnimation.DORestart();
            }
            if (timePassed <= 0)
            {
                isFightStarted = false;
                noThanks.SetActive(true);
                timerText.gameObject.SetActive(false);
            }

        }
    }


    private void InitializeTimer()
    {
        timerText.text = maxTime.ToString();
        timePassed = maxTime;
        fillerTime = maxTime;
        isFightStarted = true;
        noThanks.SetActive(false);
    }

    #region ----- Game Events -----



    #endregion
}
