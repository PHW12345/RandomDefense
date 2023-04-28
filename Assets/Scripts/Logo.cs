using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;


public class Logo : MonoBehaviour
{
    void Start()
    { 
        StartCoroutine("LogoTime");
    }
    IEnumerator LogoTime()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("Local")];
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainScene");
    }
}
