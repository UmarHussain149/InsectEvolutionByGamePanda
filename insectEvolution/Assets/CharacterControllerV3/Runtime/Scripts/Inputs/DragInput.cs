using UnityEngine;


namespace THEBADDEST.CharacterController3
{


    public class DragInput : PointerInput
    {

        public float Vertical { get; protected set; }
        public float Horizontal { get; protected set; }
        private Vector3 prevPosition;
        public float sensitivity;

        public DragInput(float sensitivity)
        {
            this.sensitivity = sensitivity;
            OnPointerDown += PointerDown;
            OnPointerUp += PointerUp;
            OnPointerUpdate += PointerUpdate;
        }
        void PointerDown(Vector3 mousePosition)
        {
            prevPosition = Input.mousePosition;
        }

        void PointerUp(Vector3 mousePosition)
        {
            Horizontal = 0;
            Vertical = 0;
        }

        void PointerUpdate(Vector3 mousePosition)
        {
            Vector3 touchDelta = mousePosition - prevPosition; // screen touch delta
            var positionDelta = touchDelta * sensitivity;
            positionDelta.x /= Screen.width / 2f;
            positionDelta.y /= Screen.height / 2f;
            Horizontal = positionDelta.x;
            Vertical = positionDelta.y;
            prevPosition = mousePosition;
        }



    }


}