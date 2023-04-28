using GameAssets.GameSet.GameDevUtils.Managers;
using UnityEngine;

public class SwitchToggle : ToggleController
{

    private void Start()
    {
        Init_Toggles();
    }

    /// <summary> #region initialization of vibration for first time
    /// 
    /// You must have to writwe these two lines in the Initialization method of any script of your code
    /// 
    ///ToggleController.Vibration = 1; // issue was that the script is on the panel and 
    ///ToggleController.SFX = 1;/////// unless player open the panel the vibration will not work
    ///
    /// </summary>

    private void Init_Toggles()
    {
        togleStructs[0].handlePosition = togleStructs[0].handleRectTransform.anchoredPosition;
        togleStructs[1].handlePosition = togleStructs[1].handleRectTransform.anchoredPosition;

        togleStructs[0].toggle.onValueChanged.AddListener(SwitchVibbration);
        togleStructs[1].toggle.onValueChanged.AddListener(SwitchSFX);

        //if (FirstInit == 0)
        //{
        //    FirstInit = 1;
        //    SwitchVibbration(true);
        //    SwitchSFX(true);
        //}
        //else
        {
            VibrationState();
            SFX_State();
        }
    }

    private void VibrationState()
    {

        if (Vibration == 1)
        {
            SwitchVibbration(true);

            //ActivateReminder();
        }
        else
        {
            //ActivateReminder();

            SwitchVibbration(false);
        }

        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
    }

    private void SFX_State()
    {

        if (SFX == 1)
        {
            SwitchSFX(true);
            ActivateReminder();
        }
        else
        {
            SwitchSFX(false);
            ActivateReminder();
        }

        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
    }

    //private int FirstInit
    //{
    //    get
    //    {
    //        return PlayerPrefs.GetInt("firstTogle");
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetInt("firstTogle", value);
    //    }
    //}

}
