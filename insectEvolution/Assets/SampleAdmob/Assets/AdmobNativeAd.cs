using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

//Banner ad
public class AdmobNativeAd : MonoBehaviour
{

    private NativeAd adNative;
    private bool nativeLoaded = false;

    public string idNative;

    [SerializeField] GameObject adNativePanel;
    [SerializeField] RawImage adIcon;
    [SerializeField] RawImage adChoices;
    [SerializeField] Text adHeadline;
    [SerializeField] Text adCallToAction;
    [SerializeField] Text adAdvertiser;


    void Awake()
    {

    }

    void Start()
    {
        //MobileAds.Initialize(initStatus => { });

        Invoke("RequestNativeAd", 1);
    }

    void Update()
    {
        if (nativeLoaded)
        {
            nativeLoaded = false;

            Texture2D iconTexture = this.adNative.GetIconTexture();
            Texture2D iconAdChoices = this.adNative.GetAdChoicesLogoTexture();
            string headline = this.adNative.GetHeadlineText();
            string cta = this.adNative.GetCallToActionText();
            string advertiser = this.adNative.GetAdvertiserText();
            adIcon.texture = iconTexture;
            adChoices.texture = iconAdChoices;
            adHeadline.text = headline;
            adAdvertiser.text = advertiser;
            adCallToAction.text = cta;

            //register gameobjects
            adNative.RegisterIconImageGameObject(adIcon.gameObject);
            adNative.RegisterAdChoicesLogoGameObject(adChoices.gameObject);
            adNative.RegisterHeadlineTextGameObject(adHeadline.gameObject);
            adNative.RegisterCallToActionGameObject(adCallToAction.gameObject);
            adNative.RegisterAdvertiserTextGameObject(adAdvertiser.gameObject);

            adNativePanel.SetActive(true); //show ad panel
        }
    }



    #region Native Ad Mehods ------------------------------------------------

    private void RequestNativeAd()
    {
        if (adNative != null)
        {
            adNative.Destroy();

        }

        AdLoader adLoader = new AdLoader.Builder(idNative)
            .ForNativeAd()
            .Build();
        adLoader.OnNativeAdLoaded += this.HandleNativeAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;
        adLoader.LoadAd(new AdRequest.Builder().Build());
    }


    private void HandleNativeAdLoaded(object sender, NativeAdEventArgs args)
    {
        Debug.Log("Native ad loaded.");
        this.adNative = args.nativeAd;
        this.nativeLoaded = true;
    }
    #endregion
    private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Native ad failed to load: " + args);
    }
    //------------------------------------------------------------------------
    AdRequest AdRequestBuild()
    {
        return new AdRequest.Builder().Build();
    }



}

