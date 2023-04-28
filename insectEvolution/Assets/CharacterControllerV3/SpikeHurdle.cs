using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;
using GameAssets.GameSet.GameDevUtils.Managers;
using UnityEngine.UI;

namespace THEBADDEST.InteractSyetem
{
    public class SpikeHurdle : TriggerEffector
    {

        public LevelType level;
        [SerializeField] int levelCount;
        THEBADDEST.CharacterController3.CharacterController characterController;

        protected override void OnEffect(Collider other, IEffectContainer container)
        {
            Vibration.VibrateNope();
            triggered = true;
            base.OnEffect(other, container);
            characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();

            GroundAction();
            this.GetComponent<Collider>().enabled = false;

        }


        void GroundAction()
        {


            characterController.CurrentPorgression(level, -0.25f);
            characterController.currentInsect.GetComponent<DOTweenAnimation>().DORestartById("Hit");

        }
    }
}