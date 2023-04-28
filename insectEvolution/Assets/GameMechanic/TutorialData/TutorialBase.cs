using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
    public abstract void PerfomAction();
    public Tutorial tutorialType;
    public Transform insectPos;

}
public enum Tutorial
{
    OnSameLevel = 1,
    OnGreaterLevel = 2,
    OnFruitEat = 3,
    OnUpgradeCharacter = 4,
    OnUpgradeSpeed = 5,
    //OnProgression = 6,
    None = 7
}
