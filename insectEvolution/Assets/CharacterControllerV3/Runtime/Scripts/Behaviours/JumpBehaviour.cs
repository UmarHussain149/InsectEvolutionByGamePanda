using System;
using THEBADDEST.InteractSyetem;
using UnityEngine;


namespace THEBADDEST.CharacterController3
{


    [CreateAssetMenu(menuName = "THEBADDEST/CharacterBehaviours/JumpBehaviour", fileName = "JumpBehaviour", order = 0)]
    public class JumpBehaviour : CharacterBehaviour
    {

        [SerializeField] AnimationCurve animationCurve;

        [SerializeField] float jumpPower;
        [SerializeField] float jumpDelay;

        Rigidbody rigidbody => behaviour.Rigidbody;
        Transform transform => animator.transform;
        Animator animator => behaviour.Animator;
        JumpInput jumpInput;
        Vector3 currentPosition;
        float intercept;
        bool isPerformingJump = false;

        protected override void Init()
        {
            base.Init();
            jumpInput = new JumpInput(jumpDelay);
            JumpTrigger.onJumpTrigger += jumpInput.DoIJum;
        }

        protected override void DoUpdate()
        {
            if (!CanControl) return;
            if (!jumpInput.CanJmp()) return;
            intercept = 0;
            isPerformingJump = true;
            //UpdateAnimator(true);
        }


        protected override void DoFixedUpdate()
        {
            if (!CanControl) return;
            if (!isPerformingJump)
            {
                return;
            }

            currentPosition = transform.position;
            intercept = Mathf.MoveTowards(intercept, jumpDelay, Time.deltaTime);
            currentPosition.y = jumpPower * animationCurve.Evaluate(intercept / jumpDelay);
            transform.position = currentPosition;
            if (Math.Abs(intercept - jumpDelay) < 0.5f * jumpDelay)
            {
                //UpdateAnimator(false);
            }

            if (intercept >= jumpDelay)
            {
                isPerformingJump = false;
            }
        }

        void UpdateAnimator(bool jumpValue)
        {
            animator.SetBool(AnimatorIDs.jump, jumpValue);
        }

    }


    public class JumpInput
    {

        bool isPerformingJump;
        bool DoJump;
        float jumpInputDelay;
        float currentTime;

        public JumpInput(float jumpInputDelay)
        {
            this.jumpInputDelay = jumpInputDelay;
        }

        public bool CanJmp()
        {
            if (isPerformingJump)
            {
                currentTime -= Time.deltaTime;
                if (currentTime < 0) isPerformingJump = false;
                return false;
            }

            if (DoJump)
            {
                DoJump = false;
                isPerformingJump = true;
                currentTime = jumpInputDelay;
                return true;
            }

            return false;
        }
        public void DoIJum()
        {
            DoJump = true;

        }

    }


}