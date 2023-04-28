using System.Collections;
using System.Collections.Generic;
using THEBADDEST.InteractSyetem;
using UnityEngine;

public class TutorialTrigger : TutorialBase
{

    public override void PerfomAction()
    {
        ReferenceManager.instance.tutorialManager.InvokTutorial(true, tutorialType, insectPos);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<THEBADDEST.CharacterController3.CharacterController>(out THEBADDEST.CharacterController3.CharacterController player))
        {
            this.GetComponent<Collider>().enabled = false;
            PerfomAction();

        }
    }

}
