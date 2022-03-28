using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAd : MonoBehaviour
{
    private BannerView _bannerViewBottom;
    private BannerView _bannerViewUp;

    private string _bannerUnitId = "ca-app-pub-3940256099942544/6300978111";

    private void OnEnable()
    {
        AdRequest adRequest = new AdRequest.Builder().Build();
        _bannerViewBottom = CreateBanner(AdPosition.Bottom, adRequest);
        _bannerViewUp = CreateBanner(AdPosition.Top, adRequest);

        StartCoroutine(ShowBanner(_bannerViewUp));
        StartCoroutine(ShowBanner(_bannerViewBottom));
    }

    private BannerView CreateBanner(AdPosition adPosition, AdRequest adRequest)
    {
        var bannerView = new BannerView(_bannerUnitId, AdSize.SmartBanner, adPosition);
        bannerView.LoadAd(adRequest);

        return bannerView;
    }

    private IEnumerator ShowBanner(BannerView bannerView)
    {
        yield return new WaitForSeconds(3.0f);
        bannerView.Show();
    }
}
