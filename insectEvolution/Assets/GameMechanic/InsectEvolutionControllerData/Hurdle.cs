using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THEBADDEST.InteractSyetem;
using GameAssets.GameSet.GameDevUtils.Managers;

namespace InsectEvolution
{


    public class Hurdle : TriggerEffector
    {
        THEBADDEST.CharacterController3.CharacterController characterController;
        bool isDestroy;
        protected override void OnEffect(Collider other, IEffectContainer container)
        {
            base.OnEffect(other, container);
            if (other.CompareTag("Player"))
            {

                Vibration.VibratePeek();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.bombHitClip, 1);
                characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();

                if (!isDestroy)
                {

                    GameController.changeGameState(GameState.Fail);
                    characterController.currentInsect.SetActive(false);
                    Instantiate(characterController.splashPrefeb, characterController.transform.position, characterController.splashPrefeb.transform.rotation);
                    isDestroy = true;
                }
                characterController.GetComponent<Rigidbody>().isKinematic = true;


                characterController.GetComponent<Collider>().enabled = false;
            }
        }
    }
}