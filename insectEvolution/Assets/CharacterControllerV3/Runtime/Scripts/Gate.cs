using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using System.Collections.Generic;
using THEBADDEST.InteractSyetem;
using UnityEngine;

public class Gate : TriggerEffector
{
    [SerializeField] Gates gateType;
    THEBADDEST.CharacterController3.CharacterController characterController;

    protected override void OnEffect(Collider other, IEffectContainer container)
    {
        Vibration.VibrateNope();


        triggered = true;
        base.OnEffect(other, container);
        characterController = other.GetComponent<THEBADDEST.CharacterController3.CharacterController>();
        if (gateType == Gates.upGradeGate)
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.upGrade, 1);
            characterController.currentLevelCount++;

        }
        if (gateType == Gates.DownGradeGate)
        {
            //SoundManager.Instance.PlayOneShot(SoundManager.Instance.upGrade, 1);

            if (characterController.currentLevelCount != 0)
            {
                characterController.currentLevelCount--;

            }
        }
        characterController.upGradelevelParticle.Play();
        characterController.currentLevel = (LevelType)characterController.currentLevelCount;
        characterController.UpgardeLevelOfCharacter(characterController.currentLevel);
    }
}
public enum Gates
{
    upGradeGate,
    DownGradeGate
}