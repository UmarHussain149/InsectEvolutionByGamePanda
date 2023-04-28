using THEBADDEST;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace THEBADDEST.CharacterController3
{


    [CreateAssetMenu(menuName = "THEBADDEST/CharacterBehaviours/SwerveBehavior")]
    public class SwerveBehavior : CharacterBehaviour
    {

        //Serialize Fields

        [SerializeField] private float sensitivity;
        [SerializeField] private float animationMultiplier = 1;
        [SerializeField] private RangeBy movementRange;
        //Private Fields
        [SerializeField] public float moveSpeed;
        Transform transform => behaviour.transform;
        Rigidbody rigidbody => behaviour.Rigidbody;
        Animator animator => behaviour.Animator;
        Vector3 currentVelocity;

        DragInput dragInput;

        const int forwardInputValue = 1;
        const float m_interpolation = 0.01f;

        protected override void Init()
        {
            dragInput = new DragInput(sensitivity);
            base.Init();
        }

        protected override void DoUpdate()
        {
            //dragInput.Calculate();
            if (CanControl)
            {
                Movement();
            }
        }

        protected override void DoFixedUpdate()
        {


        }

        void Movement()
        {

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            transform.localPosition = new Vector3(transform.localPosition.x + h * moveSpeed * Time.deltaTime,
                                                 transform.localPosition.y, transform.localPosition.z);
            Vector3 pos = transform.localPosition;
            pos.x = Mathf.Clamp(pos.x, clampValueMin, clampValueMax);
            transform.localPosition = pos;
        }
        void UpdateAnimator(Vector3 velocity)
        {
            animator.SetFloat(AnimatorIDs.value, velocity.magnitude);
        }
    }
}