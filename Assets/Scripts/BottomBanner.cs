using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BottomBanner : MonoBehaviour
{
    private BannerView bannerView;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }

    private void RequestBanner()
    {
        #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3878404163477087/9707368707"; // ���Ĺ���
        //string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // �׽�Ʈ ����

#elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
                string adUnitId = "unexpected_platform";
#endif

        //Clean up banner ad before creating a new one.
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        //this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.BottomLeft);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
    public void bannerDelete()
    {
        this.bannerView.Hide();

    }
    // Update is called once per frame
    void Update()
    {

    }
}