using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
#pragma warning disable 649
namespace UnityStandardAssets.Utility
{
    public class SmoothFollow : MonoBehaviour
    {

        // The target we are following
        [SerializeField]
        public Transform target;
        // The distance in the x-z plane to the target
        [SerializeField]
        internal float distance = 10.0f;
        // the height we want the camera to be above the target
        [SerializeField]
        internal float height = 5.0f;

        [SerializeField]
        internal float rotationDamping;
        [SerializeField]
        internal float heightDamping;

        // Use this for initialization
        void Start() { }

        // Update is called once per frame
        void LateUpdate()
        {
            // Early out if we don't have a target
            if (!target)
                return;

            // Calculate the current rotation angles
            var wantedRotationAngle = target.eulerAngles.y;
            var wantedHeight = target.position.y + height;

            var currentRotationAngle = transform.eulerAngles.y;
            var currentHeight = transform.position.y;

            // Damp the rotation around the y-axis
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;

            // Set the height of the camera
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            // Always look at the target
            transform.LookAt(target);
        }

        public IEnumerator MoveCamToNewTarget(Transform Newtarget, float _time)
        {
            float T = 0;
            Vector3 StatPOs = target.transform.position;
            Quaternion StartRot = target.transform.rotation;


            while (T <= 1)
            {
                T += Time.deltaTime / _time;
                target.transform.position = Vector3.Lerp(StatPOs, Newtarget.position, T);
                target.transform.rotation = Quaternion.Lerp(StartRot, Newtarget.rotation, T);
                //Debug.Log("kjdksjd");
                yield return null;

            }

        }
    }
}