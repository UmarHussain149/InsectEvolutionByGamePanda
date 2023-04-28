using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using GameAssets.GameSet.GameDevUtils.Managers;

public class ToggleController : MonoBehaviour
{
    public static event Action<bool> onVibrate, onSFX;

    [Serializable]
    public struct TogleStruct
    {
        public RectTransform handleRectTransform;
        public Toggle toggle;
        //public GameObject ReminderImage;
        public GameObject OnImage, OffImage;
        [HideInInspector] public Vector2 handlePosition;

    }
    bool state1;
    bool state2;
    public TogleStruct[] togleStructs;
    //public GameObject reminder;
    public void SwitchVibbration(bool on)
    {

        if (on)
        {
            state1 = false;
            Vibration = 1;
            togleStructs[0].handleRectTransform.DOAnchorPos(togleStructs[0].handlePosition * -1, .4f).SetEase(Ease.InOutBack);
            ShuffleImage(true, togleStructs[0].OnImage, togleStructs[0].OffImage);
            togleStructs[0].OnImage.GetComponent<DOTweenAnimation>().DORestart();
            //ReferenceManager.instance.gameManager.vibrate = true;
            //ActivateReminder();
        }
        else
        {
            state1 = true;
            Vibration = 0;
            ShuffleImage(false, togleStructs[0].OnImage, togleStructs[0].OffImage);
            togleStructs[0].handleRectTransform.DOAnchorPos(togleStructs[0].handlePosition, .4f).SetEase(Ease.InOutBack);

            //ReferenceManager.instance.gameManager.vibrate = false; 
        }
        //ReferenceManager.instance.soundManager.SelectSound();
    }
    public void SwitchSFX(bool on)
    {

        if (on)
        {
            state2 = false;
            SFX = 1;
            togleStructs[1].handleRectTransform.DOAnchorPos(togleStructs[1].handlePosition * -1, .4f).SetEase(Ease.InOutBack);
            ShuffleImage(true, togleStructs[1].OnImage, togleStructs[1].OffImage);
            togleStructs[1].OnImage.GetComponent<DOTweenAnimation>().DORestart();
            SoundManager.Instance.EnableAudio(true);

            ActivateReminder();
        }
        else
        {
            state2 = true;
            SFX = 0;
            ShuffleImage(false, togleStructs[1].OnImage, togleStructs[1].OffImage);
            togleStructs[1].handleRectTransform.DOAnchorPos(togleStructs[1].handlePosition, .4f).SetEase(Ease.InOutBack);

            SoundManager.Instance.EnableAudio(false);
        }
        //ReferenceManager.instance.soundManager.SelectSound();
    }
    private void ShuffleImage(bool state, GameObject Obj1, GameObject Obj2)
    {
        switch (state)
        {
            case true:
                Obj1.SetActive(true);
                Obj2.SetActive(false);
                break;
            case false:
                Obj1.SetActive(false);
                Obj2.SetActive(true);
                break;
        }
    }


    public void ActivateReminder()
    {
        //if (reminder != null)
        //{
        //    if (state1 == true || state2 == true)
        //        reminder.SetActive(true);
        //    else
        //        reminder.SetActive(false);
        //}
    }

    public void OnVibrate(bool active)
    {
        onVibrate?.Invoke(active);
    }

    public void OnSFX(bool active)
    {
        onSFX?.Invoke(active);
    }


    private void OnDestroy()
    {
        onVibrate = null;
        onSFX = null;
    }

    #region Prefs

    public static int Vibration
    {
        get
        {
            return PlayerPrefs.GetInt("Vibrate");
        }
        set
        {
            PlayerPrefs.SetInt("Vibrate", value);
        }
    }

    public static int SFX
    {
        get
        {
            return PlayerPrefs.GetInt("SFX");
        }
        set
        {
            PlayerPrefs.SetInt("SFX", value);
        }
    }
    #endregion
}
