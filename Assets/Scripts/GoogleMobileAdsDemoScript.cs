using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;
//using TMPro;

public class GoogleMobileAdsDemoScript : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public StageData player;
    public WinReward winReward;
    public Canvas myCanvas;
    //public Countdown countdown;
    public GameObject timer;
    public CountdownTime countdown;

    public GameObject rewardLodingPanel;
    public GameObject rewardPanel;
    //public TextMeshProUGUI rewardText;

    public void CreateAndLoadRewardedAd()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3878404163477087/6915955650"; // 정식버전
        //adUnitId = "ca-app-pub-3940256099942544/5224354917"; // 테스트버전

#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }
    public void CreateAndLoadRewardedAd1()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3878404163477087/4472775081"; // 정식버전
        //adUnitId = "ca-app-pub-3940256099942544/5224354917"; // 테스트버전

#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward1;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        

    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.LoadAdError);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
        

    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             /*+ args.Message*/);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        
    }

    public void HandleUserEarnedReward(object sender, Reward args) //상점
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        Debug.Log("상점" + args);

        timer.SetActive(true);

        
        countdown.setTime += 30.0f;
        PlayerPrefs.SetInt("PlayerDia", player.diamond + 100);
        player.diamond = PlayerPrefs.GetInt("PlayerDia");
        //rewardPanel.SetActive(true);
    }
    public void HandleUserEarnedReward1(object sender, Reward args) //승리
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        Debug.Log("승리" + args);

        PlayerPrefs.SetInt("PlayerDia", PlayerPrefs.GetInt("PlayerDia") + winReward.diamond * 10);
        LodingSceneController.LoadScene("LobbyScene");
    }
    public void UserChoseToWatchAd()
    {
        CreateAndLoadRewardedAd();
        StartCoroutine(showInterstitial());

        IEnumerator showInterstitial()
        {
            rewardLodingPanel.SetActive(true);
            while (!this.rewardedAd.IsLoaded())
            {
                yield return new WaitForSeconds(0.2f);
            }
            rewardLodingPanel.SetActive(false);
            
            this.rewardedAd.Show();
            myCanvas.sortingOrder = -1;
        }
    }
    public void UserChoseToWatchAd1()
    {
        CreateAndLoadRewardedAd1();
        StartCoroutine(showInterstitial());

        IEnumerator showInterstitial()
        {
            rewardLodingPanel.SetActive(true);
            while (!this.rewardedAd.IsLoaded())
            {
                yield return new WaitForSeconds(0.2f);
            }
            rewardLodingPanel.SetActive(false);
            this.rewardedAd.Show();
            myCanvas.sortingOrder = -1;
        }
    }

    public void TestDia()
    {
        PlayerPrefs.SetInt("PlayerDia", player.diamond + 1000);
        player.diamond = PlayerPrefs.GetInt("PlayerDia");
    }
}