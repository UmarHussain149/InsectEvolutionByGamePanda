using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;
using System.Collections.Generic;

namespace THEBADDEST.InteractSyetem
{


    public class Fight : TriggerEffector
    {

        THEBADDEST.CharacterController3.CharacterController characterController;
        [SerializeField] Transform target;
        [SerializeField] Animator venom;
        [SerializeField] Transform targetLarwa;
        [SerializeField] GameObject venomPrefeb;
        [SerializeField] Transform venomInstantiatePos;
        [SerializeField] Transform parent;
        public GameObject endPointCharacter;
        public GameObject RemainendPointCharacter;
        private void Start()
        {
            UpdateEndPointCharacter();
        }
        protected override void OnEffect(Collider other, IEffectContainer container)
        {
            //triggered = true;
            base.OnEffect(other, container);
            if (other.CompareTag("Player"))
            {
                //venomDami.SetActive(false);
                UpdateEndPoint = 1;
                var venomClone = Instantiate(venomPrefeb, venomInstantiatePos.position, venomInstantiatePos.rotation);
                venomClone.transform.parent = parent;
                venom = venomClone.GetComponentInChildren<Animator>();
                characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();
                characterController.canvasPlayer.SetActive(false);
                Destroy(characterController.m_Rigidbody);
                characterController.GenrateLarwa(22, targetLarwa);
                characterController.upGradelevelParticle.Play();
                characterController.splineMove.ChangeSpeed(0);
                characterController.progressionbar.SetActive(false);
                characterController.behaviours[0].CanControl = false;
                characterController.behaviours[1].targetBoss = target;
                characterController.behaviours[1].CanControl = true;
                characterController.currentBehaviour++;
                characterController.currentInsectAnimator.SetTrigger("Idle");
                characterController.hand.SetActive(true);
                characterController.tap.SetActive(true);
                StartCoroutine(HideTutorial());
                venom.SetTrigger("Walk");

            }

        }
        IEnumerator HideTutorial()
        {
            yield return new WaitForSeconds(1);
            venom.SetTrigger("Walk");
            yield return new WaitForSeconds(2);
            characterController.hand.SetActive(false);
            characterController.tap.SetActive(false);
        }

        int UpdateEndPoint
        {
            set { PlayerPrefs.SetInt("UpdateEndPoint", value); }
            get { return PlayerPrefs.GetInt("UpdateEndPoint"); }
        }
        void UpdateEndPointCharacter()
        {
            if (UpdateEndPoint != 1)
            {
                endPointCharacter.SetActive(true);
                RemainendPointCharacter.SetActive(false);

            }
            else
            {
                RemainendPointCharacter.SetActive(true);
                endPointCharacter.SetActive(false);
            }
        }
    }

}