using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEditor.Advertisements;
using UnityEngine.Serialization;

public class AdsManager : SingletonMonoBehaviour<AdsManager>, IUnityAdsInitializationListener, IUnityAdsLoadListener,
    IUnityAdsShowListener
{
    [SerializeField] private string gameID = "";

    [SerializeField] private bool testMode = true;

    [SerializeField] string _androidBasicId = "Interstitial_Android";
    [SerializeField] string _androidRewardId = "Rewarded_Android";
    [SerializeField] string _androidBannerId = "Banner_Android";

    private Action RewardAction;

    public override void Awake()
    {
        base.Awake();

        Advertisement.Initialize(gameID, testMode, this);
    }

    public void OnInitializationComplete()
    {
//        Debug.Log("Inited Success");
        //ShowBanner();
    }

    public void PlayInterstitialAd()
    {
        Advertisement.Load(_androidBasicId, this);
    }

    public void PlayRewardAd(Action onComplete = null)
    {
        RewardAction = onComplete;
        Advertisement.Load(_androidRewardId, this);
    }

    public void ShowBanner()
    {
        Advertisement.Banner.Load(_androidBannerId,
            new BannerLoadOptions
            {
                loadCallback = OnBannerLoad,
                errorCallback = OnBannerLoadFail
                
            }
            );
    }

    private void OnBannerLoad()
    {
        Advertisement.Banner.Show(_androidBannerId);
    }

    private void OnBannerLoadFail(string message)
    {
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
//        Debug.Log("Inited Failed");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
//        Debug.Log("OnUnityAdsShowFailure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
   //     Debug.Log($"OnUnityAdsShowStart {placementId}");
       // Time.timeScale = 0;
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        //     Debug.Log("OnUnityAdsShowClick");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
//            Debug.Log($"COMPLETED {placementId}");
            RewardAction?.Invoke();
        }

      //  Time.timeScale = 1;
    }


    public void OnUnityAdsAdLoaded(string placementId)
    {
//        Debug.Log("Loaded Success" + placementId);

        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
   //     Debug.Log("Loaded Failed" + placementId);
    }
}