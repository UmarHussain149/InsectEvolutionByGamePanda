using GameDevUtils;
using System.Collections;
using System.Collections.Generic;
using THEBADDEST.InteractSyetem;
using UnityEngine;


public class RigedBodyDestroy : TriggerEffector
{

    THEBADDEST.CharacterController3.CharacterController characterController;
    [SerializeField] Transform target;
    protected override void OnEffect(Collider other, IEffectContainer container)
    {
        //triggered = true;
        base.OnEffect(other, container);
        if (other.CompareTag("Player"))
        {

            characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();
            characterController.canvasPlayer.SetActive(false);
            //characterController.GenrateLarwa(22);
            //Destroy(characterController.m_Rigidbody);
            //characterController.transform.AddComponent<Rigidbody>();
        }
    }
}