using UnityEngine;
using DG.Tweening;

namespace THEBADDEST.InteractSyetem
{


    public class Hurdle : TriggerEffector
    {
        public LevelType level;
        THEBADDEST.CharacterController3.CharacterController characterController;

        protected override void OnEffect(Collider other, IEffectContainer container)
        {
            triggered = true;
            base.OnEffect(other, container);
            characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();
            characterController.hitOnHigerlevelParticle.Play();
            characterController.CurrentPorgression(level, -0.25f);
            characterController.currentInsect.GetComponent<DOTweenAnimation>().DORestartById("Hit");
        }

    }


}