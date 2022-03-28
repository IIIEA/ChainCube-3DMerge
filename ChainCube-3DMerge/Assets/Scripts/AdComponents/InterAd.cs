using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class InterAd : MonoBehaviour
{
    private InterstitialAd _interstitialAd;

    private string _interstitialUnitId = "ca-app-pub-3940256099942544/1033173712";

    public bool _bHasRequestedInterstitialAd = false;
    public bool _bHasShownInterstitialAd = false;
    public bool _bHasActiveNoAdsSubcription = false;

    public void ShowAd(ref int points, ref int countPointsToAd)
    {
        if (_bHasActiveNoAdsSubcription == false)
        {
            if (!_bHasRequestedInterstitialAd)
            { 
                Debug.Log("Should be requesting InterstitialAd now");
                RequestInterstitial();
                _bHasRequestedInterstitialAd = true;
                _bHasShownInterstitialAd = false;
            }
        }

        if (points >= countPointsToAd)
        {
            if (_bHasShownInterstitialAd == false && _bHasActiveNoAdsSubcription == false)
            {
                Debug.Log("Trying to show InterstitialAd now");
                this.ShowInterstitial();
                _bHasShownInterstitialAd = true;
                _bHasRequestedInterstitialAd = false;
                countPointsToAd = UnityEngine.Random.Range(10,20);
                points = 0;
            }
        }
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    private void ShowInterstitial()
    {
        if (this._interstitialAd.IsLoaded())
        {
            Debug.Log("InterstitialAd is ready and should be shoing now");
            this._interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial is not ready yet");
        }
    }

    private void RequestInterstitial()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
        }

        Debug.Log("Trying to get new InterstitialAd with adUnitId: " + _interstitialUnitId);
        _interstitialAd = new InterstitialAd(_interstitialUnitId);

        _interstitialAd.OnAdLoaded += HandleInterstitialLoaded;
        _interstitialAd.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        _interstitialAd.OnAdOpening += HandleInterstitialOpened;
        _interstitialAd.OnAdClosed += HandleInterstitialClosed;

        _interstitialAd.LoadAd(CreateAdRequest());
    }

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialFailedToLoad event received with message: " + args);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }

    #endregion Interstitial callback handlers
}
