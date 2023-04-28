using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class AnalyticsMediator : MonoBehaviour
{
    public static AnalyticsMediator instance;
    private void Awake()
    {
        if (!instance)
        {
            //InitializeFlurry();
            //IniFB();
            GameAnalytics.Initialize();
            instance = this;
            DontDestroyOnLoad(gameObject);

            Debug.LogError("Done_Initialization");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (GameStartOneTime == 0)
        {
            GameStartOneTime = 1;
            LogGA_Event("GameStart", GameStartOneTime);
        }
    }



    #region "FB initialization"

    public void IniFB()
    {
        //FB.Init(this.OnInitComplete, this.OnHideUnity);
        //if (!FB.IsInitialized)
        //    FB.Init(this.CheckFBStatus, null);
    }

    public void CheckFBStatus()
    {
        //Debug.LogError("FB.IsInitialized" + FB.IsInitialized);
    }
    #endregion

    #region GameAnalytics

    public void LogGA_Event(string info, float Value)
    {
        GameAnalytics.NewDesignEvent(info, Value);
    }

    #endregion


    int GameStartOneTime
    {
        set { PlayerPrefs.SetInt("GameStartOneTime", value); }
        get { return PlayerPrefs.GetInt("GameStartOneTime"); }
    }
}
