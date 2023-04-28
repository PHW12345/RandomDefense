using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class CountdownTime : MonoBehaviour
{
    public float setTime = 0f;
    [SerializeField] TextMeshProUGUI countdownText;
    public Button ad;
    public GameObject timer;
    public bool timerOn;

    // Start is called before the first frame update
    void Start()
    {
        //setTime += 10.0f;
        //Locale currentLocale = LocalizationSettings.SelectedLocale;
        //countdownText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTable", "ADsWait", currentLocale) + Mathf.Round(setTime).ToString() + LocalizationSettings.StringDatabase.GetLocalizedString("MyTable", "Time", currentLocale);
        //countdownText.text = "±§∞Ì ¥Î±‚\n" + setTime.ToString() + "√ ";
    }

    // Update is called once per frame
    void Update()
    {
        Locale currentLocale = LocalizationSettings.SelectedLocale;
        if (setTime > 0)
        {
            setTime -= Time.deltaTime;
            //Locale currentLocale = LocalizationSettings.SelectedLocale;
            //countdownText.text = "±§∞Ì ¥Î±‚\n" + Mathf.Round(setTime).ToString() + "√ ";
            countdownText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTable", "ADsWait", currentLocale) +"\n"+ Mathf.Round(setTime)/*.ToString()*/ + " " + LocalizationSettings.StringDatabase.GetLocalizedString("MyTable", "Time", currentLocale);
            ad.interactable = false;
            timerOn = true;
        }
        else if (setTime <= 0) 
        {
            //Time.timeScale = 0.0f;
            ad.interactable = true;
            //if (timeClose == false)
            //{
            //    timer.SetActive(false);
            //    timeClose = true;
            //}
            //else
            //{
            // ADsWait "±§∞Ì ¥Î±‚\n"
            //}
            //timeClose = true;

            //Locale currentLocale = LocalizationSettings.SelectedLocale;
            if (timerOn == true)
            {
                countdownText.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTable", "ViewADs", currentLocale);
                timerOn = false;
            }
            //countdownText.text = "±§∞Ì ∫∏±‚";
        }
    }
}
