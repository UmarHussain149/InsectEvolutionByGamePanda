using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameAssets.GameSet.GameDevUtils.Managers;
using MergeSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] bool isLizardScene;
    [SerializeField] GameObject splashScreen;
    [SerializeField] GameObject noThanksLizard;
    [SerializeField] GameObject homePanel;
    [SerializeField] GameObject gamplayPanel;
    [SerializeField] GameObject competePanel;
    [SerializeField] GameObject levelFailPanel;
    [SerializeField] GameObject popUpForLizardScene;
    [SerializeField] GameObject newInsectPanel;
    [SerializeField] GameObject MainCanvus;
    [SerializeField] ParticleSystem particle;
    public Image progressionBar;
    public int levelNo;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject nextButtonlizad;
    [SerializeField] GameObject revivNoThanks;

    [SerializeField] GameObject letsPlayLizard;
    [SerializeField] GameObject letsclaimButton;
    [Header("Text Fields"), SerializeField] Text levelNoText;
    bool switchButton;
    [SerializeField] int buildNumber;
    void Awake()
    {
        GameController.onLevelComplete += OnLevelComplete;
        GameController.onGameplay += Gameplay;
        GameController.onLevelFail += LevelFail;
        GameController.onHome += Home;
        GameController.OnRevive += Gameplay;
        Levelprogression.OnCompleteProgression += ButtonStateChange;
        Vibration.Init();
        ////levelNo = PlayerPrefManager.GetLevelNumber(PlayerPrefManager.GetSceneName());

        ////if (levelNo % 2 == 0)
        ////{
        ////    Debug.Log("Banner");
        ////    Mediation_Manager.instance.RequestBanner();
        ////}
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        // Mediation_Manager.instance.RequestBanner();

    }
    //Events Definations
    void Home()
    {
        ActivePanel(home: true);
    }
    void LevelFail()
    {

        StartCoroutine(DelayONFail());
        if (AnalyticsMediator.instance) AnalyticsMediator.instance.LogGA_Event("LevelFail:" + levelNo + "Build" + buildNumber, levelNo);
    }


    void Gameplay()
    {
        //levelNo = PlayerPrefs.GetInt("LevelNumber");
        levelNo = PlayerPrefManager.GetLevelNumber(PlayerPrefManager.GetSceneName());
        levelNo += 1;
        levelNoText.text = $"Level {levelNo.ToString("00")}";
        ActivePanel(gameplay: true);
        if (AnalyticsMediator.instance) AnalyticsMediator.instance.LogGA_Event("LevelStart" + levelNo + "Build" + buildNumber, levelNo);

    }

    void OnLevelComplete()
    {
        StartCoroutine(Delay());
        if (AnalyticsMediator.instance) AnalyticsMediator.instance.LogGA_Event("LevelComplete" + levelNo + "Build" + buildNumber, levelNo);
    }


    //Active Panels
    void ActivePanel(bool gameplay = false, bool home = false, bool complete = false, bool fail = false)
    {
        gamplayPanel.SetActive(gameplay);
        homePanel.SetActive(home);
        competePanel.SetActive(complete);
        levelFailPanel.SetActive(fail);


    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        if (Mediation_Manager.instance) Mediation_Manager.instance.Show_Interstital();
        yield return new WaitForSeconds(.3f);
        ActivePanel(complete: true);
        yield return new WaitForSeconds(.5f);
        if (!isLizardScene)
        {
            Vibration.VibratePop();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.progression, 0.5f);
            ReferenceManager.instance.levelprogression.UpdateProgressionBar();
        }
        else
        {
            ReferenceManager.instance.characterController.win.Play();

        }
        yield return new WaitForSeconds(3f);
        if (Boss.OneTime == 1)
        {
            NewInsectPanel();
            Boss.OneTime = 0;
        }
        else
        {

            if (nextButton) nextButton.SetActive(true);
        }
        ////ChangeButton();


    }
    IEnumerator DelayONFail()
    {
        yield return new WaitForSeconds(0.5f);
        if (Mediation_Manager.instance) Mediation_Manager.instance.Show_Interstital();
        yield return new WaitForSeconds(.3f);
        ActivePanel(fail: true);

    }

    public void TapToPlay()
    {
        GameController.changeGameState.Invoke(GameState.Gameplay);

    }
    public void RewardedAds()
    {
        if (Mediation_Manager.instance) Mediation_Manager.instance.ShowRewardedVideo();
    }
    public void RewardedAdsOfLizard()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
        Vibration.VibrateNope();
        StartCoroutine(DelayForLizard());
    }
    public void PopUpLizardPanel()
    {
        Vibration.VibrateNope();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);

        if (popUpForLizardScene) popUpForLizardScene.SetActive(true);
        if (MainCanvus) MainCanvus.SetActive(false);
        FunctionTimer.Create(() => { if (noThanksLizard) noThanksLizard.SetActive(true); }, 3);


    }

    public void NewInsectPanel()
    {
        Vibration.VibrateNope();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);

        if (newInsectPanel) newInsectPanel.SetActive(true);
        if (MainCanvus) MainCanvus.SetActive(false);


    }
    IEnumerator DelayForLizard()
    {
        letsclaimButton.SetActive(false);
        if (Mediation_Manager.instance) Mediation_Manager.instance.ShowRewardedVideo();

        yield return new WaitForSeconds(.3f);
        if (particle) particle.Play();
        if (letsPlayLizard) letsPlayLizard.SetActive(true);
        Vibration.VibrateNope();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.upGrade, 0.5f);

    }
    public void ShowSplash()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClip, 0.5f);
        Vibration.VibrateNope();
        if (popUpForLizardScene) popUpForLizardScene.SetActive(false);
        if (splashScreen) splashScreen.SetActive(true);
    }
    int BanerCount
    {
        get { return PlayerPrefs.GetInt("BanerCount"); }
        set { PlayerPrefs.SetInt("BanerCount", value); }
    }
    public void ChangeButton()
    {
        if (switchButton)
        {

            if (nextButton) nextButton.SetActive(false);
            if (nextButtonlizad) nextButtonlizad.SetActive(true);
        }
        else
        {
            if (nextButton) nextButton.SetActive(true);
            if (nextButtonlizad) nextButtonlizad.SetActive(false);
        }

    }
    void ButtonStateChange()
    {
        switchButton = true;
        if (nextButton) nextButton.SetActive(false);
        PopUpLizardPanel();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        if (particle) particle.Play();
    }
}



//if (CoinsManager.Instance.AdCase)
//{
//    CoinsManager.Instance.AddCoins(CoinsManager.Instance.CollectionValue);
//}
//else
//{
//    CoinsManager.Instance.AddCoins(20);

//}
//CoinsManager.Instance.AddCoinOnGamePlay();
//UpgardeManager.instace.InitialCash();
