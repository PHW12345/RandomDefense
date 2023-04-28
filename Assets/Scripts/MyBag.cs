using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class MyBag : MonoBehaviour
{
    private int[] towerCount = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    [SerializeField]
    private TextMeshProUGUI[] towerCountText;

    [SerializeField]
    private Image imageTower;
    [SerializeField]
    private TextMeshProUGUI textTowerName;
    [SerializeField]
    private TextMeshProUGUI textTowerInfo;
    public Sprite[] towerImage;

    public Button[] tButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateTowerData00(int id)
    {
        Locale currentLocale = LocalizationSettings.SelectedLocale;

        if (id == 0)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "0", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "ArcherInfo", currentLocale);

            //Archer "�ü�";
            //long range and fast attack speed. "�����Ÿ��� ��� ���ݼӵ��� �����ϴ�.";

            //textDamage.text = " ���ݷ� : 11";
            //textRate.text = "���ݼӵ� : 0.8��";
            //textRange.text = " ��Ÿ� : 3.5";
            //textTargetRange.text = "���� : ����";
        }
        if (id == 1)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "1", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "SwordManInfo", currentLocale);

            //textDamage.text = " ���ݷ� : 17";
            //textRate.text = "���ݼӵ� : 1.1��";
            //textRange.text = " ��Ÿ� : 2";
            //textTargetRange.text = "���� : ����";
        }
        if (id == 2)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "2", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "BombManInfo", currentLocale);

            //textDamage.text = " ���ݷ� : 20";
            //textRate.text = "���ݼӵ� : 2.2��";
            //textRange.text = " ��Ÿ� : 2.5";
            //textTargetRange.text = "���� : 1.5x1.5";
        }
        if (id == 3)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "3", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "FireWizerdInfo", currentLocale);

            //textDamage.text = " �������ݷ� : 30";
            //textRate.text = "���ݼӵ� : 1.6��";
            //textRange.text = " ��Ÿ� : 2.5";
            //textTargetRange.text = "���� : ����";
        }
        if (id == 4)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "4", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "FireArcherInfo", currentLocale);

            //textDamage.text = " ���ݷ� : 12";
            //textRate.text = "���ݼӵ� : 1��";
            //textRange.text = " ��Ÿ� : 3";
            //textTargetRange.text = "���� : ����";
        }
        if (id == 5)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "5", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "WindSwordInfo", currentLocale);

            //textDamage.text = " ���ݷ� : 20";
            //textRate.text = "���ݼӵ� : 1.5��";
            //textRange.text = " ��Ÿ� : 2";
            //textTargetRange.text = "���� : ����";
        }
        if (id == 6)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "6", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "WaterBombManInfo", currentLocale);

            //textDamage.text = " ���ݷ� : 15";
            //textRate.text = "���ݼӵ� : 2.2��";
            //textRange.text = " ��Ÿ� : 2.5";
            //textTargetRange.text = "���� : 1x1";
        }
        if (id == 7)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "7", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "IceWizerdInfo", currentLocale);

            //textDamage.text = " �������ݷ� : 20";
            //textRate.text = "���ݼӵ� : 1.7��";
            //textRange.text = " ��Ÿ� : 2.5";
            //textTargetRange.text = "���� : ����";
        }
        if (id == 8)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "8", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "PoisonArcherInfo", currentLocale);

            //textDamage.text = " ���ݷ� : 16";
            //textRate.text = "���ݼӵ� : 1.1��";
            //textRange.text = " ��Ÿ� : 3";
            //textTargetRange.text = "���� : ����";
        }
        if (id == 9)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "9", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "PriestInfo", currentLocale);

            //textDamage.text = " ���ݷ� : 17";
            //textRate.text = "���ݼӵ� : 1.1��";
            //textRange.text = " ��Ÿ� : 2";
            //textTargetRange.text = "���� : ����";
        }
        if (id == 10)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "10", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "MudBombManInfo", currentLocale);

            //textDamage.text = " ���ݷ� : 20";
            //textRate.text = "���ݼӵ� : 2.2��";
            //textRange.text = " ��Ÿ� : 2.5";
            //textTargetRange.text = "���� : 1.5x1.5";
        }
        if (id == 11)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "11", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "LightningWizerdInfo", currentLocale);

            //textDamage.text = " �������ݷ� : 30";
            //textRate.text = "���ݼӵ� : 1.6��";
            //textRange.text = " ��Ÿ� : 2.5";
            //textTargetRange.text = "���� : ����";
        }
        if (id == 12)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "12", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "CrossBowManInfo", currentLocale);

            //textDamage.text = " ���ݷ� : 12";
            //textRate.text = "���ݼӵ� : 1��";
            //textRange.text = " ��Ÿ� : 3";
            //textTargetRange.text = "���� : ����";
        }
        if (id == 13)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "13", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "PoisonSpearManInfo", currentLocale);

            //textDamage.text = " ���ݷ� : 20";
            //textRate.text = "���ݼӵ� : 1.5��";
            //textRange.text = " ��Ÿ� : 2";
            //textTargetRange.text = "���� : ����";
        }
        if (id == 14)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "14", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "EmperorInfo", currentLocale);

            //textDamage.text = " ���ݷ� : 15";
            //textRate.text = "���ݼӵ� : 2.2��";
            //textRange.text = " ��Ÿ� : 2.5";
            //textTargetRange.text = "���� : 1x1";
        }
        if (id == 15)
        {
            imageTower.sprite = towerImage[id];
            textTowerName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", "15", currentLocale);
            textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyBag", "DarkWizerdInfo", currentLocale);

            //textDamage.text = " �������ݷ� : 20";
            //textRate.text = "���ݼӵ� : 1.7��";
            //textRange.text = " ��Ÿ� : 2.5";
            //textTargetRange.text = "���� : ����";
        }
    }
    // Update is called once per frame
    void Update()
    {
        // ������ Ÿ������ �ҷ��ͼ� �������� ���۽� Ȱ��ȭ
        towerCount[0] = PlayerPrefs.GetInt("tower0");
        towerCount[1] = PlayerPrefs.GetInt("tower1");
        towerCount[2] = PlayerPrefs.GetInt("tower2");
        towerCount[3] = PlayerPrefs.GetInt("tower3");
        towerCount[4] = PlayerPrefs.GetInt("tower4");
        towerCount[5] = PlayerPrefs.GetInt("tower5");
        towerCount[6] = PlayerPrefs.GetInt("tower6");
        towerCount[7] = PlayerPrefs.GetInt("tower7");
        towerCount[8] = PlayerPrefs.GetInt("tower8");
        towerCount[9] = PlayerPrefs.GetInt("tower9");
        towerCount[10] = PlayerPrefs.GetInt("tower10");
        towerCount[11] = PlayerPrefs.GetInt("tower11");
        towerCount[12] = PlayerPrefs.GetInt("tower12");
        towerCount[13] = PlayerPrefs.GetInt("tower13");
        towerCount[14] = PlayerPrefs.GetInt("tower14");
        towerCount[15] = PlayerPrefs.GetInt("tower15");

        for (int i = 0; i < towerCount.Length; i++)
        {
            towerCountText[i].text = "" + towerCount[i];
        }
        

        for (int i = 0; i < towerCount.Length; i++)
        {
            if (towerCount[i] == 0)
            {
                tButton[i].interactable = false;
            }
            else
                tButton[i].interactable = true;

        }

    }
}
