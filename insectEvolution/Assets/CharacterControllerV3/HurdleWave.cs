using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HurdleWave : MonoBehaviour
{
    [SerializeField] List<DOTweenAnimation> animatedHurdles = new List<DOTweenAnimation>();
    [SerializeField] float speed;
    void Start()
    {
        StartCoroutine(PlayAnimation());
    }
    IEnumerator PlayAnimation()
    {
        for (int i = 0; i < animatedHurdles.Count; i++)
        {
            animatedHurdles[i].DORestart();
            yield return new WaitForSeconds(speed);
        }
    }

    void StartAnimation()
    {
        StartCoroutine(PlayAnimation());
    }
}
