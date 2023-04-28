using UnityEngine;


namespace THEBADDEST.CharacterController3
{


    [CreateAssetMenu(menuName = "THEBADDEST/CharacterBehaviours/Free Movement", fileName = "FreeMovementBehaviour", order = 0)]
    public class FreeMovementBehaviour : CharacterBehaviour
    {

        [SerializeField] float forwardSpeed;
        [SerializeField] float rotationSpeed;
        [SerializeField] float animatorSpeedFactor;

        //Private Fields
        Transform transform => behaviour.transform;
        Rigidbody rigidbody => behaviour.Rigidbody;
        Animator animator => behaviour.Animator;
        JoyStickInput joyStickInput;
        float currentRotation;
        Vector3 currentVelocity;

        protected override void Init()
        {
            base.Init();
            joyStickInput = new JoyStickInput(120f, false, true);
        }

        protected override void DoUpdate()
        {
            joyStickInput.Calculate();
        }

        protected override void DoFixedUpdate()
        {
            if (CanControl)
            {
                Movement();
                Rotation();
            }
            else
            {
                currentVelocity = Vector3.zero;
            }

            UpdateAnimator();
        }

        void Movement()
        {
            currentVelocity = (joyStickInput.JoystickDirection.magnitude > 0) ? behaviour.transform.forward * forwardSpeed : Vector3.zero;
            currentVelocity.y = rigidbody.velocity.y;
            rigidbody.velocity = currentVelocity;
        }

        void Rotation()
        {
            if (joyStickInput.JoystickDirection.magnitude > 0)
            {
                currentRotation = Mathf.Atan2(joyStickInput.JoystickDirection.x, joyStickInput.JoystickDirection.y);
                currentRotation = Mathf.Rad2Deg * currentRotation;
                var targetRotation = Quaternion.Euler(Vector3.up * (currentRotation));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        void UpdateAnimator()
        {
            animator.SetFloat(AnimatorIDs.value, currentVelocity.magnitude > 0 ? 1 : 0);
            animator.speed = animatorSpeedFactor;
        }

    }


}