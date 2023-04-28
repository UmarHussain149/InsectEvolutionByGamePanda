using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Loading
{


    public class LoadingScreen : MonoBehaviour
    {
        //SoundManager soundManager;
        [SerializeField] float delay = 0.07f;
        [SerializeField] Slider slider;
        [Space]
        [Space]
        [SerializeField] float timer;
        [Space]
        [Header("ANIMATION")]
        [Space]
        //[SerializeField] DOTweenAnimation animation;
        bool isComplete;
        private void Start()
        {

            //animation.DORestart();
            isComplete = false;
            StartCoroutine(StartLoading());
        }
        public void StartScene()
        {
            Invoke("DelayStart", 1.8f);
            //soundManager.BackGroundSound(true);
        }

        IEnumerator StartLoading()
        {

            while (!isComplete)
            {
                yield return new WaitForSeconds(delay);
                timer += 0.001f;
                if (slider.value != 1)
                {
                    slider.value += timer;
                }
                else
                {
                    this.gameObject.SetActive(false);

                    isComplete = true;
                }
            }
        }

        private void DelayStart()
        {
            this.gameObject.SetActive(false);
            CancelInvoke();
        }
    }
}