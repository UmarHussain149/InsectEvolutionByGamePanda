using System;
using UnityEngine;

public class FreezeBone : MonoBehaviour
{
   public bool  freezeRotationAndPosition;
   public bool  clampRotation;
   // public float minRotationLimit;
   // public float maxRotationLimit;

   // void Update()
   // {
   //    if (clampRotation)
   //    {
   //       var clampedRotation = Mathf.Clamp(transform.eulerAngles.y, minRotationLimit, maxRotationLimit);
   //       transform.rotation = new Quaternion(transform.eulerAngles.x, clampedRotation, transform.eulerAngles.z, transform.rotation.w);
   //    }
   // }

}
