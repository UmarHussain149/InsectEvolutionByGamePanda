using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControler : MonoBehaviour
{



    public float clampValueMax;
    public float clampValueMin;
    Rigidbody m_Rigidbody;
    public Transform Child;
    public float ChildHSpeed;
    public bool ISCrossPlateFormInput;
    void Start()
    {

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |
        RigidbodyConstraints.FreezePositionX;

    }
    public void ChildMovement_InputCrossPlateForm()
    {


        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        //	float h = Inp;

        Child.transform.localPosition = new Vector3(Child.transform.localPosition.x + h * 100 * Time.deltaTime,
                                                 Child.transform.localPosition.y, Child.transform.localPosition.z);

        Vector3 pos = Child.transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, clampValueMin, clampValueMax);
        Child.transform.localPosition = pos;

        //Vector3 IniRot = Child.transform.localEulerAngles;
        //Quaternion NextRot = Quaternion.Euler(IniRot);
        //if (h < 0)
        //{

        //    Quaternion Rot_R = Quaternion.Euler(new Vector3(0, 0, -30f));
        //    NextRot = Quaternion.Lerp(NextRot, Rot_R, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
        //}
        //else if (h > 0)
        //{


        //    Quaternion Rot_L = Quaternion.Euler(new Vector3(0, 0, 30f));
        //    NextRot = Quaternion.Lerp(NextRot, Rot_L, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
        //}
        //else
        //{


        //    Quaternion Rot_Idle = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        //    NextRot = Quaternion.Lerp(NextRot, Rot_Idle, Time.deltaTime * Mathf.Abs(ChildHSpeed / 10));
        //}

        //Child.transform.localRotation = NextRot;


    }
    public void ChildMovement_InputMouse()
    {
        if (Input.GetMouseButton(0))
        {
            //Vector3 currentMousePos = Input.mousePosition;
            //float h = InputController2.Dir.x;
            //float h = Input.mousePosition.x;

            float h = CrossPlatformInputManager.GetAxis("Horizontal");

            //          Vector3 pos = Child.transform.localPosition - transform.right * ChildHSpeed * h * Time.deltaTime;
            //pos.x = Mathf.Clamp(pos.x, -1.5f, 1.5f);
            //	Child.transform.localPosition = pos;




            Child.transform.localPosition = new Vector3(Child.transform.localPosition.x + h * 100 * Time.deltaTime,
                                                 Child.transform.localPosition.y, Child.transform.localPosition.z);

            Vector3 pos = Child.transform.localPosition;
            pos.x = Mathf.Clamp(pos.x, clampValueMin, clampValueMax);
            Child.transform.localPosition = pos;
            //Vector3 IniRot = Child.transform.localEulerAngles;
            //         Quaternion NextRot = Quaternion.Euler(IniRot);
            //         if (h < 0)
            //         {

            //             Quaternion Rot_R = Quaternion.Euler(new Vector3(0, 0, -30f));
            //             NextRot = Quaternion.Lerp(NextRot, Rot_R, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
            //         }
            //         else if (h > 0)
            //         {


            //             Quaternion Rot_L = Quaternion.Euler(new Vector3(0, 0, 30f));
            //             NextRot = Quaternion.Lerp(NextRot, Rot_L, Time.deltaTime * Mathf.Abs(h * ChildHSpeed));
            //         }
            //         else
            //         {


            //             Quaternion Rot_Idle = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            //             NextRot = Quaternion.Lerp(NextRot, Rot_Idle, Time.deltaTime * Mathf.Abs(ChildHSpeed / 10));
            //         }

            //         Child.transform.localRotation = NextRot;
        }

    }

    private void FixedUpdate()
    {
        if (ISCrossPlateFormInput)
        {
            ChildMovement_InputCrossPlateForm();
        }
        if (ISCrossPlateFormInput == false)
        {
            ChildMovement_InputMouse();
        }
    }







}

