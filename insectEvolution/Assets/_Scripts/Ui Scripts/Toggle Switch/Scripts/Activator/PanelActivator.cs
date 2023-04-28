using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;

public class PanelActivator : MonoBehaviour // place this script on the Ui manager and pass the method refrence to the settings button 
{
    [SerializeField] GameObject settingsPanel;
    private bool isvibratePanel = false;

    private void Awake()
    {
        if (GameInitial == 0)
        {
            GameInitial = 1;
            ToggleController.Vibration = 1; // issue was that the script is on the panel and 
            ToggleController.SFX = 1;/////// unless player open the panel the vibration will not work
        }
    }

    public void ActivateVibratePanel()
    {
        if (settingsPanel)
        {
            isvibratePanel = (isvibratePanel == false) ? true : false;

            if (isvibratePanel)
            {
                settingsPanel.SetActive(isvibratePanel);
                settingsPanel.transform.GetChild(0).GetComponent<DOTweenAnimation>().DORestartById("out");
            }
            else
            {
                // panel will disable in On complete deleget in editor 
                settingsPanel.transform.GetChild(0).GetComponent<DOTweenAnimation>().DORestartById("in");
            }

            Vibration.VibratePop();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
        }
        else
        {
            Debug.LogError("panel not Assigned");
        }
    }

    private int GameInitial
    {
        get
        {
            return PlayerPrefs.GetInt("firstInstall");
        }
        set
        {
            PlayerPrefs.SetInt("firstInstall", value);
        }
    }

}
