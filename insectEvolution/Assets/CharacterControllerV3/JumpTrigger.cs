
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;
using MergeSystem;

namespace THEBADDEST.InteractSyetem
{


    public class JumpTrigger : CollideEffector
    {

        THEBADDEST.CharacterController3.CharacterController characterController;
        public static event Action onJumpTrigger;
        [SerializeField] Collider hurdlCollider;
        protected override void OnEffect(Collider other, IEffectContainer container)
        {
            triggered = true;
            base.OnEffect(other, container);
            if (other.CompareTag("Player"))
            {
                characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();
                //characterController.splineMove.speed += 5;
                //characterController.progressionbar.SetActive(false);
                //characterController.behaviours[0].CanControl = false;
                //characterController.behaviours[1].targetBoss = target;
                characterController.behaviours[2].CanControl = true;
                hurdlCollider.enabled = false;
                onJumpTrigger?.Invoke();
                FunctionTimer.Create(() =>
                {
                    if (characterController.transform.GetChild(0).TryGetComponent<DOTweenAnimation>(out DOTweenAnimation animation))
                    {
                        animation.DORestartById("Rotate");
                    }
                    //characterController.splineMove.speed -= 5;
                }, 0.4f);
            }

            //characterController.currentBehaviour++;


        }

        private void OnDestroy()
        {
            onJumpTrigger = null;
        }
    }


}