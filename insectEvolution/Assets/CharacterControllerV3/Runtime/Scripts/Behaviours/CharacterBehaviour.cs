using GameDevUtils;
using System.Collections;
using System.Collections.Generic;
using THEBADDEST;
using UnityEngine;


namespace THEBADDEST.CharacterController3
{


    public abstract class CharacterBehaviour : ScriptableObject
    {

        protected GameDevBehaviour behaviour;
        public virtual bool CanControl { get; set; }
        public float clampValueMax;
        public float clampValueMin;
        public Transform targetBoss;

        public virtual void Init(GameDevBehaviour behaviour)
        {
            this.behaviour = behaviour;
            this.behaviour.OnInit += Init;
            this.behaviour.OnFixedUpdate += DoFixedUpdate;
            this.behaviour.OnUpdate += DoUpdate;
            clampValueMin = -2.5f;
            clampValueMax = 1.8f;
        }

        protected virtual void Init()
        {
            CanControl = false;
        }


        protected abstract void DoUpdate();


        protected abstract void DoFixedUpdate();

    }


}