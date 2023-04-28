using PathCreation;
using UnityEngine;


namespace THEBADDEST.CharacterController3
{


    [CreateAssetMenu(menuName = "THEBADDEST/CharacterBehaviours/Path Movement", fileName = "PathMovement", order = 0)]
    public class PathMovementBehaviour : CharacterBehaviour
    {

        [SerializeField] PathCreator pathCreator;
        //Serialize Fields
        [SerializeField] private float moveSpeed = 2;
        [SerializeField] private float sensitivity = 1;
        [SerializeField] private float animationMultiplier = 1;
        [SerializeField] RangeBy xRange;
        [SerializeField] Vector3 pathRotationOffset;
        //Private Fields
        Transform transform => behaviour.transform;
        Rigidbody rigidbody => behaviour.Rigidbody;
        Animator animator => behaviour.Animator;
        Vector3 currentVelocity;

        DragInput dragInput;

        const int forwardInputValue = 1;
        const float m_interpolation = 0.01f;
        PathCreator runtimePathCreator;
        float coveredDistance = 0;
        float xOffset = 0;

        protected override void Init()
        {
            dragInput = new DragInput(sensitivity);
            coveredDistance = 0;
            xOffset = 0;
            runtimePathCreator = Instantiate(pathCreator);
            base.Init();
        }

        protected override void DoUpdate()
        {
            animator.speed = animationMultiplier;
            dragInput.Calculate();
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

            UpdateAnimator(currentVelocity);
        }

        void Movement()
        {
            float v = forwardInputValue;
            float h = dragInput.Horizontal;
            currentVelocity = transform.forward * v;
            coveredDistance += m_interpolation * moveSpeed;
            var desirePosition = runtimePathCreator.path.GetPointAtDistance(coveredDistance, EndOfPathInstruction.Stop);
            desirePosition.y = transform.position.y;
            xOffset += h;
            xOffset = xRange.Clamp(xOffset);
            desirePosition += transform.right * xOffset;
            rigidbody.MovePosition(desirePosition);
        }

        void Rotation()
        {
            transform.rotation = runtimePathCreator.path.GetRotationAtDistance(coveredDistance, EndOfPathInstruction.Stop) * Quaternion.Euler(pathRotationOffset);
        }

        void UpdateAnimator(Vector3 velocity)
        {
            animator.SetFloat(AnimatorIDs.value, velocity.magnitude);
        }

    }


}