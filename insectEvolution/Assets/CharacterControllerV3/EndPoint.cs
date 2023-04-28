
using UnityEngine;
using DG.Tweening;
using Cinemachine;


namespace THEBADDEST.InteractSyetem
{


    public class EndPoint : TriggerEffector
    {

        THEBADDEST.CharacterController3.CharacterController characterController;

        protected override void OnEffect(Collider other, IEffectContainer container)
        {
            //triggered = true;
            base.OnEffect(other, container);
            if (other.CompareTag("Player"))
            {

                characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();
                characterController.behaviours[0].clampValueMax = 0;
                characterController.behaviours[0].clampValueMin = 0;
                characterController.splineMove.ChangeSpeed(15);
                characterController.progressionbar.SetActive(false);
                //characterController.camera.GetComponent<CinemachineBrain>().enabled = true;
                // characterController.camera.GetComponent<SmoothFollow>().enabled = false;
                characterController.cinemachine.SetTrigger("End");
            }


        }

    }


}