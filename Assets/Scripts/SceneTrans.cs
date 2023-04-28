using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using TMPro;
using System;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using System.Reflection;
using Unity.VisualScripting;

public class SceneTrans : MonoBehaviour
{
    public GameObject comPopup;
    public GameObject comPopup1;

    public GameObject menuel01;
    public GameObject menuel02;
    public GameObject menuel03;
    public GameObject menuel04;

    public GameObject menu;

    public GameObject tipBox;

    public GameObject[] dialog;
    static bool firstDialog;

    private InterstitialAd interstitial;

    [SerializeField]
    private TextMeshProUGUI newGameText;

    [SerializeField]
    private BGMController bgmController; // 배경음악 설정 (보스 등장 시 변경)
    public bool isPause = false;
    public Canvas myCanvas;
    public static int adcount = 0;

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3878404163477087/8749510252";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public void NewLobbySceneStarter()
    {
        PlayerPrefs.DeleteAll();
        Locale currentLocale = LocalizationSettings.SelectedLocale;
        PlayerPrefs.SetInt("Local", Convert.ToInt32(LocalizationSettings.StringDatabase.GetLocalizedString("MyTable", "LocaleNum", currentLocale)));

        PlayerPrefs.SetInt("PlayerDia", 300);
        //PlayerPrefs.SetInt("PlayerStar", 10);
        PlayerPrefs.SetInt("GameOn", 1); // 게임에 존재 비존재

        PlayerPrefs.SetInt("tower0", 5);
        PlayerPrefs.SetInt("tower1", 5);
        PlayerPrefs.SetInt("tower2", 5);
        PlayerPrefs.SetInt("tower3", 5);
        //PlayerPrefs.SetInt("tower4", 1);
        //PlayerPrefs.SetInt("tower5", 1);
        //PlayerPrefs.SetInt("tower6", 1);
        //PlayerPrefs.SetInt("tower7", 1);
        LodingSceneController.LoadScene("LobbyScene");
    }
    
    public void LobbySceneStarter()
    {
        //RequestInterstitial();
        //if (this.interstitial.IsLoaded())
        //{
        //    this.interstitial.Show();
        //    myCanvas.sortingOrder = -1;
        //}  
        //adcount++;
        //if (adcount > 2)
        //{
        //    adcount = 0;
            RequestInterstitial();
            //When you want call Interstitial show
            StartCoroutine(showInterstitial());
        //}
        //else
        //{
        //    SceneManager.LoadScene("LobbyScene");
        //}

        IEnumerator showInterstitial()
        {
            while (!this.interstitial.IsLoaded())
            {
                yield return new WaitForSeconds(0.2f);
            }
            this.interstitial.Show();
            myCanvas.sortingOrder = -1;
        }
    }
    public void LobbySceneStarterNotAds()
    {
        if (PlayerPrefs.GetInt("GameOn") == 1)//게임이 존재하면
        {
            LodingSceneController.LoadScene("LobbyScene");
        }
        else
        {
            comPopup1.SetActive(true);
        }
    }
    public void StageReStarter(int stageNum)
    {
        LodingSceneController.LoadScene("Stage" + stageNum);
    }
    public void StageTestStarter()
    {
        LodingSceneController.LoadScene("StageTest");
    }
    public void Stage01Starter()
    {
        SceneManager.LoadScene("Stage01");
    }
    public void Stage02Starter()
    {
        SceneManager.LoadScene("Stage02");
    }
    public void Stage03Starter()
    {
        SceneManager.LoadScene("Stage03");
    }
    public void Stage04Starter()
    {
        SceneManager.LoadScene("Stage04");
    }

    public void MainSceneStarter()
    {
        SceneManager.LoadScene("MainScene");

    }
    public void GameQuit()
    {
        Application.Quit();
    }

    public void PopupOn()
    {
        comPopup.SetActive(true);
        Locale currentLocale = LocalizationSettings.SelectedLocale;

        if (PlayerPrefs.GetInt("GameOn") == 1) //게임이 존재하면
        {
            //newGameText.text = "기존 게임기록을 삭제하고\n새 게임을 하시겠습니까?";
            newGameText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTable", "newGameText", currentLocale);
        }
        else
        {
            newGameText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTable", "newGameText1", currentLocale);
        }
    }
    public void PopupOff()
    {
        comPopup.SetActive(false);
    }

    public void PopupOff1()
    {
        comPopup1.SetActive(false);
    }
    public void Manuel01()
    {
        menuel01.SetActive(false);
        menuel02.SetActive(true);
    }
    public void Manuel02()
    {
        menuel02.SetActive(false);
        menuel03.SetActive(true);
    }
    public void Manuel03()
    {
        menuel03.SetActive(false);
        menuel04.SetActive(true);
    }
    public void Manuel04()
    {
        menuel04.SetActive(false);
        if(firstDialog == false)
        {
            dialog[0].SetActive(true);
            dialog[1].SetActive(true);
            dialog[2].SetActive(true);
        }
        
        tipBox.SetActive(true);
        firstDialog = true;
    }

    public void TipBoxOpen()
    {
        tipBox.SetActive(false);
        menuel01.SetActive(true);
        //dialog[0].SetActive(false);
        //dialog[1].SetActive(false);
        //dialog[2].SetActive(false);
    }
    public void MenuOn()
    {
        menu.SetActive(true);
    }
    public void MenuOff()
    {
        menu.SetActive(false);
    }
    public void IsPause()
    {
        isPause = !isPause;
        if(isPause)
        {
            Time.timeScale = 0f;
            bgmController.audioSource.Pause();
            
            //menu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            bgmController.audioSource.UnPause();
            //menu.SetActive(false);
        }
        Time.fixedDeltaTime = 0.02f *Time.timeScale;


    }
}