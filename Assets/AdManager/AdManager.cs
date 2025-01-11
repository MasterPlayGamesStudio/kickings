using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{


    public static AdManager instance;
    private GoogleMobileAdsScript googleMobileAds = null;
    [SerializeField] private string androidInterstitialID;
    [SerializeField] private string androidRewardedID;
    [SerializeField] private string iOSInterstitialID;
    [SerializeField] private string iOSRewardedID;


    private string interstitialAdId;
    private string bannerAdId;
    private string rewardedAdId;
    [SerializeField] private int adIntervalLevel;
    private int currentAdIntervalLevel;

    private bool isInterstitialAlreadyLoaded = false;

    public enum PageType
    {
        Menu,
        InGame,
        LevelComplete,
        LevelFail
    }


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
#if UNITY_ANDROID
            interstitialAdId = androidInterstitialID;
            rewardedAdId = androidRewardedID;
#else
        interstitialAdId = iOSInterstitialID;
        rewardedAdId = iOSRewardedID;
#endif
        googleMobileAds = new GoogleMobileAdsScript(interstitialAdId, rewardedAdId, bannerAdId);
        googleMobileAds.InitializeAds();


        RequestRewardedAd();
        LoadNextScene();
    }


    async Task LoadNextScene()
    {
        await Task.Delay(2000);
        SceneManager.LoadScene(1);
    }

    

    public void RunActions(PageType pageType, int levelNumber, Action callback = null)
    {
        switch (pageType)
        {
            case PageType.Menu:

                break;
            case PageType.InGame:
                LoadInterstitialAd();
                break;
            case PageType.LevelComplete:
                currentAdIntervalLevel++;
                if (currentAdIntervalLevel >= adIntervalLevel)
                {
                    ShowInterstitialAd((onclosed) =>
                    {
                        isInterstitialAlreadyLoaded = false;
                        callback?.Invoke();
                    });
                    currentAdIntervalLevel = 0;
                }
                else
                {
                    callback?.Invoke();
                }
                break;
            case PageType.LevelFail:
                ShowInterstitialAd(null);
                break;
            default:

                break;
        }
    }

    private void LoadInterstitialAd()
    {
        if (!isInterstitialAlreadyLoaded)
        {
            googleMobileAds.LoadInterstitialAd((isLoaded) =>
            {
                isInterstitialAlreadyLoaded = true;
            });
        }
        
    }




    public void ShowInterstitialAd(Action<bool> onClosed)
    {
        googleMobileAds.ShowInterstitialAd((callback) =>
        {
            onClosed?.Invoke(true);
        });
    }

    public void ShowRewardVideoWithCallback(Action<bool> SuccessCallback)
    {
        Debug.Log("SuccessCallback " + SuccessCallback);
        googleMobileAds.ShowRewardedAd(SuccessCallback);
        SuccessCallback = null;
        RequestRewardedAd();
    }

    private void RequestRewardedAd()
    {
        googleMobileAds?.LoadRewardedAd();
    }


    public void PrivacyPolicy()
    {
        Application.OpenURL("https://masterplaygamesstudio123.blogspot.com/p/privacy-policy.html");
    }

}
