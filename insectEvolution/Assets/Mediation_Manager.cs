using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using GoogleMobileAdsMediationTestSuite.Api;
public class Mediation_Manager : MonoBehaviour
{
    [Header("Admob Ads")]
    [SerializeField] string AdmobAppKey = "";
    [SerializeField] string AdmobBanner_Key = "";
    [SerializeField] string AdmobExitBanner_Key = "";
    [SerializeField] string AdmobNativeBanner_Key = "";
    [SerializeField] string AdmobInterstitial_Key = "";
    [SerializeField] string AdmobRewarded_Key = "";
    private bool isShowingAppOpenAd;
    private BannerView bannerView;
    private BannerView exitBannerView;
    private BannerView nativeBannerView;
    private InterstitialAd interstitial;
    public RewardedAd rewardedAd;
    public static Mediation_Manager instance;

    [HideInInspector]
    public bool isRewardAvailable = false, isBannerShow = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        //GameController.onGameplay += HideNativeBanner;
        //GameController.onHome += ShowNativeBanner;
    }
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        MediationTestSuite.OnMediationTestSuiteDismissed += this.HandleMediationTestSuiteDismissed;
        RequestAll();
        exitBannerView.Hide();
        //nativeBannerView.Hide();
        ShowNativeBanner();
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }
    public void ShowExitBanner()
    {
        if (exitBannerView != null)
        {
            Debug.Log("ExitBanner");

            exitBannerView.Show();
        }
        else
        {
            RequestExitBanner();
        }
    }

    public void HideNativeBanner()
    {
        if (this.nativeBannerView != null)
            this.nativeBannerView.Destroy();
    }
    public void HideExitBanner()
    {
        exitBannerView.Hide();
    }
    public void ShowNativeBanner()
    {
        if (nativeBannerView != null && GameController.gameState == GameState.Home)
        {
            if (this.nativeBannerView != null)
                this.nativeBannerView.Show();
        }
        else
        {
            if (this.nativeBannerView != null)
                this.nativeBannerView.Destroy();
        }
    }

    public void RequestAll()
    {
        RequestInterstitial();
        RequestRewarded();
        RequestBanner();
        RequestExitBanner();
        //RequestNativeBanner();
        RequestAndLoadAppOpenAd();

    }
    public void HandleMediationTestSuiteDismissed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleMediationTestSuiteDismissed event received");
    }
    private void ShowMediationTestSuite()
    {
        MediationTestSuite.Show();
    }
    public void showMedSuit()
    {
        ShowMediationTestSuite();

    }
    //Implement code to execute when the loadCallback event triggers:


    public void RequestBanner()
    {
        //AdSize adSize = new AdSize(320, 50);
        this.bannerView = new BannerView(AdmobBanner_Key, AdSize.Banner, AdPosition.Bottom);
        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        // this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
    public void RequestExitBanner()
    {
        AdSize adSizeRequestExitBanner = new AdSize(320, 320);
        this.exitBannerView = new BannerView(AdmobExitBanner_Key, adSizeRequestExitBanner, 35, 150);
        // Called when an ad request has successfully loaded.
        this.exitBannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.exitBannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.exitBannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.exitBannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        // this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.exitBannerView.LoadAd(request);
    }
    public void RequestNativeBanner()
    {
        AdSize adSizeRequestNativeBanner = new AdSize(150, 200);
        this.nativeBannerView = new BannerView(AdmobNativeBanner_Key, adSizeRequestNativeBanner, 20, 150);
        // Called when an ad request has successfully loaded.
        this.nativeBannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.nativeBannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.nativeBannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.nativeBannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        // this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.nativeBannerView.LoadAd(request);
        // rectTransformOfNative = this.nativeBannerView.GetComponent<RectTransform>();
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //.....................
        MonoBehaviour.print("HandleAdLoaded event received");
        isBannerShow = true;
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //...........................................Recvert................................................................



        //MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
        //                    + args.Message);
        //isBannerShow = false;
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {

        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }




    private void RequestInterstitial()
    {
        if (interstitial != null)
        {
            interstitial.Destroy();

        }
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(AdmobInterstitial_Key);
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoadInt;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpenedInt;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosedInt;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    public void HandleOnAdFailedToLoadInt(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleOnAdOpenedInt(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosedInt(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        RequestInterstitial();
    }

    public void RequestRewarded()
    {

        if (rewardedAd != null)
        {
            rewardedAd.Destroy();

        }
        this.rewardedAd = new RewardedAd(AdmobRewarded_Key);


        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        //this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        //Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        //Called when the user should be rewarded for interacting with the ad.


        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        //  Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        //  Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        isRewardAvailable = true;
    }
    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");

    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        isRewardAvailable = false;

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {

        Debug.Log("Reward Closed");
        RequestRewarded();

    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        Debug.Log("Reward Earn");
        AllRewardedAds_Success();



    }

    public void ShowAdmobBanner()
    {
        if (!isAdsPurchased())
        {
            if (this.bannerView != null)
            {
                this.bannerView.Show();
            }
            else
            {
                RequestBanner();
            }

        }

    }
    public void HideBanner()
    {
        if (this.bannerView != null)
        {
            this.bannerView.Hide();
        }
    }


    public void Show_Interstital()
    {
        if (!isAdsPurchased())
        {
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
            }
            else
            {
                RequestInterstitial();
            }
        }
    }
    public void ShowRewardedVideo()
    {
        print("Reward function call...");

        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            RequestRewarded();
        }
    }

    public void AllRewardedAds_Success()
    {
        if (CoinsManager.Instance.AdCase)
        {
            CoinsManager.Instance.AddCoins(CoinsManager.Instance.CollectionValue);
        }
        else
        {
            CoinsManager.Instance.AddCoins(20);

        }
        CoinsManager.Instance.AddCoinOnGamePlay();
        UpgardeManager.instace.InitialCash();

    }
    public bool isAdsPurchased()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Native

    #region APPOPEN ADS
    private readonly TimeSpan APPOPEN_TIMEOUT = TimeSpan.FromHours(4);
    private DateTime appOpenExpireTime;
    private AppOpenAd appOpenAd;
    public bool IsAppOpenAdAvailable
    {
        get
        {
            return (!isShowingAppOpenAd
                    && appOpenAd != null
                    && DateTime.Now < appOpenExpireTime);
        }
    }

    public void OnAppStateChanged(AppState state)
    {
        // Display the app open ad when the app is foregrounded.
        UnityEngine.Debug.Log("App State is " + state);

        // OnAppStateChanged is not guaranteed to execute on the Unity UI thread.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (state == AppState.Foreground)
            {
                ShowAppOpenAd();
            }
        });
    }

    public void RequestAndLoadAppOpenAd()
    {
        // PrintStatus("App Open ad dismissed.");
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-6733515502573961/4864244717";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-6733515502573961/6761018384";
#else
        string adUnitId = "unexpected_platform";
#endif
        // create new app open ad instance
        AppOpenAd.LoadAd(adUnitId,
                         ScreenOrientation.Portrait,
                         CreateAdRequest(),
                         OnAppOpenAdLoad);
    }
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
    }
    private void OnAppOpenAdLoad(AppOpenAd ad, AdFailedToLoadEventArgs error)
    {
        if (error != null)
        {
            //PrintStatus("App Open ad failed to load with error: " + error);
            return;
        }

        //  PrintStatus("App Open ad loaded. Please background the app and return.");
        this.appOpenAd = ad;
        this.appOpenExpireTime = DateTime.Now + APPOPEN_TIMEOUT;
    }

    public void ShowAppOpenAd()
    {
        if (!IsAppOpenAdAvailable)
        {
            RequestAndLoadAppOpenAd();
            return;
        }

        // Register for ad events.
        this.appOpenAd.OnAdDidDismissFullScreenContent += (sender, args) =>
        {
            //PrintStatus("App Open ad dismissed.");
            isShowingAppOpenAd = false;
            if (this.appOpenAd != null)
            {
                this.appOpenAd.Destroy();
                this.appOpenAd = null;
            }
        };
        this.appOpenAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
        {
            // PrintStatus("App Open ad failed to present with error: " + args.AdError.GetMessage());

            isShowingAppOpenAd = false;
            if (this.appOpenAd != null)
            {
                this.appOpenAd.Destroy();
                this.appOpenAd = null;
            }
        };
        this.appOpenAd.OnAdDidPresentFullScreenContent += (sender, args) =>
        {
            //PrintStatus("App Open ad opened.");
        };
        this.appOpenAd.OnAdDidRecordImpression += (sender, args) =>
        {
            RequestAndLoadAppOpenAd();
            //PrintStatus("App Open ad recorded an impression.");
        };
        this.appOpenAd.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "App Open ad received a paid event.",
                                        args.AdValue.CurrencyCode,
                                        args.AdValue.Value);
            // PrintStatus(msg);
        };

        isShowingAppOpenAd = true;
        appOpenAd.Show();
    }

    #endregion
    public void CallRewardOnButton()
    {
        StartCoroutine(Rewarded());
    }
    IEnumerator Rewarded()
    {
        yield return null;
        ShowRewardedVideo();
    }
}