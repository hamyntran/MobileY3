using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEditor.Advertisements;  
public class AdsManager : SingletonMonoBehaviour<AdsManager>, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string gameID = "";

    [SerializeField] private bool testMode = true;
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";

    public override void Awake()
    {
        base.Awake();
        
        Advertisement.Initialize(gameID,testMode,this);
    }

    public void PlayInterstitialAd()
    {
        Advertisement.Load(_androidAdUnitId,this);

    }

    public void OnInitializationComplete()
    {
        Debug.Log("Inited Success");

        PlayInterstitialAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Inited Failed");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Loaded Success");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("OnUnityAdsFailedToLoad");
        Debug.Log($"{error} {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart");


    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick");

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete");

    }
    
    
}
