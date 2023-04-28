using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager instance;
    public Levelprogression levelprogression;
    public THEBADDEST.CharacterController3.CharacterController characterController;
    public UIManager uIManager;
    public TutorialManager tutorialManager;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
