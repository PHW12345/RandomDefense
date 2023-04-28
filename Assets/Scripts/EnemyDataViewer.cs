using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class EnemyDataViewer : MonoBehaviour
{
    [SerializeField]
    private Image imageEnemy;
    [SerializeField]
    private TextMeshProUGUI textEnemyName;
    [SerializeField]
    private TextMeshProUGUI textEnemyInfo;
    [SerializeField]
    private TextMeshProUGUI textHp;
    [SerializeField]
    private TextMeshProUGUI textSpeed;
    [SerializeField]
    private TextMeshProUGUI textDef;
    [SerializeField]
    private TextMeshProUGUI textMregi;

    private EnemyHP currentEnemy;
    private Movement2DAni currentEnemy1;

    private Enemy currentEnemy2;

    public Sprite[] enemyImage;

    public GameObject enemyInfoPanel;
    //public bool[] checkbox;

    [SerializeField]
    private Image imageGold;
    [SerializeField]
    private TextMeshProUGUI textGold;
    [SerializeField]
    private Image imageHeart;
    [SerializeField]
    private TextMeshProUGUI textHeart;
    private void Awake()
    {
        OffPanel();
    }

    public void OnPanel(Transform enemydata/*, Transform enemydata1, Transform enemydata2*/)
    {
        // 출력해야하는 타워 정보를 받아와서 저장
        currentEnemy = enemydata.GetComponent<EnemyHP>();
        currentEnemy1 = enemydata.GetComponent<Movement2DAni>();
        currentEnemy2 = enemydata.GetComponent<Enemy>();

        // 타워 정보 Panel On
        gameObject.SetActive(true);
        // 타워 정보를 갱신
        UpdateEnemyData();
    }

    public void OffPanel()
    {
        // 타워 정보 Panel Off
        gameObject.SetActive(false);

    }

    //private void UpdateTowerData()
    //{
    //    imageTower.sprite = currentTower.TowerSprite;
    //    textTowerName.text = currentTower.Name;
    //    textTowerInfo.text = currentTower.Info;
    //    if(currentTower.Damage > 0)
    //    {
    //        textDamage.text = " 공격력 : " + currentTower.Damage;
    //    }
    //    else if(currentTower.MagicDamage > 0)
    //    {
    //        textDamage.text = " 마법공격력 : " + currentTower.MagicDamage;
    //    }
            
    //    textRate.text = "공격속도 : " + currentTower.Rate + "초";
    //    textRange.text = " 사거리 : " + currentTower.Range;
    //    textTargetRange.text = "범위 : " + currentTower.TargetRange;
    //    textUpGrade.text = "" + currentTower.Cost * 1.5;
    //    textSell.text = "" + currentTower.Sell;

    //    // 업그레이드가 불가능해지면 버튼 비활성화
    //    //buttonUpgrade.interactable = currentTower.Level < currentTower.Maxlevel ? true : false;
    //    if(currentTower.Level == currentTower.Maxlevel)
    //    {
    //        buttonUpgrade.SetActive(false);
    //    }
    //    else
    //        buttonUpgrade.SetActive(true);

    //}
    public void UpdateEnemyData()
    {
        Locale currentLocale = LocalizationSettings.SelectedLocale;

        if (currentEnemy.enemyID <= 20)
        {
            imageEnemy.sprite = enemyImage[currentEnemy.enemyID - 1];
            textEnemyInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", currentEnemy.enemyID.ToString() + "Info", currentLocale);

            if (currentEnemy.enemyID <= 4)
            {
                textEnemyName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", currentEnemy.enemyID.ToString(), currentLocale) + "<color=#FFE000>" + "\n(" + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Level", currentLocale) + "1)" + "</color>";
            }
            else if(currentEnemy.enemyID <= 8) 
            {
                textEnemyName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", currentEnemy.enemyID.ToString(), currentLocale) + "<color=#FFE000>" + "\n(" + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Level", currentLocale) + "2)" + "</color>";
            }
            else if (currentEnemy.enemyID <= 12)
            {
                textEnemyName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", currentEnemy.enemyID.ToString(), currentLocale) + "<color=#FFE000>" + "\n(" + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Level", currentLocale) + "3)" + "</color>";
            }
            else if (currentEnemy.enemyID <= 16)
            {
                textEnemyName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", currentEnemy.enemyID.ToString(), currentLocale) + "<color=#FFE000>" + "\n(" + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Level", currentLocale) + "4)" + "</color>";
            }
            else
            {
                textEnemyName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", currentEnemy.enemyID.ToString(), currentLocale) + "<color=#FFE000>" + "\n(" + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Level", currentLocale) + "5)" + "</color>";
            }
        }
       
        else if (currentEnemy.enemyID >= 21)
        {
            imageEnemy.sprite = enemyImage[currentEnemy.enemyID - 17];
            textEnemyInfo.text = ""; /*LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", currentEnemy.enemyID.ToString() + "Info", currentLocale);*/
            if (currentEnemy.enemyID <= 32)
            {
                int num = currentEnemy.enemyID - 16;
                textEnemyName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Hero", currentLocale) + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", num.ToString() , currentLocale) + "<color=#FF0000>" + "\n(" + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Mid-Boss", currentLocale) + ")" + "</color>";
            }
            else if (currentEnemy.enemyID == 33) // 황제 오크
            {
                textEnemyName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", currentEnemy.enemyID.ToString(), currentLocale) + "<color=#FF0000>" + "\n(" + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Mid-Boss", currentLocale) + ")" + "</color>";
            }
            else if (currentEnemy.enemyID == 34) //암흑 해골 황제
            {
                textEnemyName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", currentEnemy.enemyID.ToString(), currentLocale) + "<color=#FF0000>" + "\n(" + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Mid-Boss", currentLocale) + ")" + "</color>";
            }
            else if (currentEnemy.enemyID <= 36)
            {
                int num = currentEnemy.enemyID - 16;
                textEnemyName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Emperor", currentLocale) + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", num.ToString(), currentLocale) + "<color=#FF0000>" + "\n(" + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Mid-Boss", currentLocale) + ")" + "</color>";
            }
            else
            {
                textEnemyName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", currentEnemy.enemyID.ToString(), currentLocale) + "<color=#FF0000>" + "\n(" + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Boss", currentLocale) + ")" + "</color>";
            }
            //textEnemyName.text = "영웅 오크 궁수" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
            //textEnemyInfo.text = "오크 궁수는 체력이 약간 높아요.";
        }
        //if (currentEnemy.enemyID == 22)
        //{
        //    imageEnemy.sprite = enemyImage[5];
        //    textEnemyName.text = "영웅 해골 전사" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "해골 전사는 방어력이 약간 높아요.";
        //}

        //if (currentEnemy.enemyID == 23)
        //{
        //    imageEnemy.sprite = enemyImage[6];
        //    textEnemyName.text = "영웅 이티" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "이티는 마법 저항력이 약간 높아요.";
        //}

        //if (currentEnemy.enemyID == 24)
        //{
        //    imageEnemy.sprite = enemyImage[7];
        //    textEnemyName.text = "영웅 에그 고스트" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "에그 고스트는 이동속도가 약간 빨라요.";
        //}

        //if (currentEnemy.enemyID == 25)
        //{
        //    imageEnemy.sprite = enemyImage[8];
        //    textEnemyName.text = "영웅 오크 도끼병" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "오크 도끼병는 체력이 높아요.";
        //}

        //if (currentEnemy.enemyID == 26)
        //{
        //    imageEnemy.sprite = enemyImage[9];
        //    textEnemyName.text = "영웅 펌프킨헤드" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "펌프킨헤드는 방어력이 높아요.";
        //}

        //if (currentEnemy.enemyID == 27)
        //{
        //    imageEnemy.sprite = enemyImage[10];
        //    textEnemyName.text = "영웅 메이드" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "메이드는 마법 저항력이 높아요.";
        //}

        //if (currentEnemy.enemyID == 28)
        //{
        //    imageEnemy.sprite = enemyImage[11];
        //    textEnemyName.text = "영웅 레이디 고스트" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "레이디 고스트는 이동속도가 빨라요.";
        //}
        //if (currentEnemy.enemyID == 29)
        //{
        //    imageEnemy.sprite = enemyImage[12];
        //    textEnemyName.text = "영웅 오크 메이지" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "오크 메이지는 체력이 매우 높아요.";
        //}
        //if (currentEnemy.enemyID == 30)
        //{
        //    imageEnemy.sprite = enemyImage[13];
        //    textEnemyName.text = "영웅 먼지세포" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "먼지세포는 방어력이 매우 높아요.";

        //}
        //if (currentEnemy.enemyID == 31)
        //{
        //    imageEnemy.sprite = enemyImage[14];
        //    textEnemyName.text = "영웅 흑 메이드" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "흑 메이드는 마법 저항력이 매우 높아요.";
        //}
        //if (currentEnemy.enemyID == 32)
        //{
        //    imageEnemy.sprite = enemyImage[15];
        //    textEnemyName.text = "영웅 섀도우 고스트" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "섀도우 고스트는 이동속도가 매우 빨라요.";
        //}
        //if (currentEnemy.enemyID == 33)
        //{
        //    imageEnemy.sprite = enemyImage[16];
        //    textEnemyName.text = "황제 오크" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "오크 킹은 체력이 매우매우 높아요.";
        //}
        //if (currentEnemy.enemyID == 34)
        //{
        //    imageEnemy.sprite = enemyImage[17];
        //    textEnemyName.text = "암흑 해골 황제" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "암흑 해골전사는 방어력이 매우매우 높아요.";
        //}
        //if (currentEnemy.enemyID == 35)
        //{
        //    imageEnemy.sprite = enemyImage[18];
        //    textEnemyName.text = "황제 흑 마법사" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "흑 마법사는 마법 저항력이 매우매우 높아요.";
        //}
        //if (currentEnemy.enemyID == 36)
        //{
        //    imageEnemy.sprite = enemyImage[19];
        //    textEnemyName.text = "황제 팬텀" + "<color=#ff0000>" + "\n(중간보스)" + "</color>";
        //    textEnemyInfo.text = "팬텀은 이동속도가 매우매우 빨라요";
        //}
        //if (currentEnemy.enemyID == 37)
        //{
        //    imageEnemy.sprite = enemyImage[20];
        //    textEnemyName.text = "황제 고양이" + "<color=#ff0000>" + "\n(보스)" + "</color>";
        //    textEnemyInfo.text = "보스";
        //}
        //if (currentEnemy.enemyID == 38)
        //{
        //    imageEnemy.sprite = enemyImage[21];
        //    textEnemyName.text = "황제 리자드" + "<color=#ff0000>" + "\n(보스)" + "</color>";
        //    textEnemyInfo.text = "보스";
        //}

        textGold.text = currentEnemy2.gold.ToString();
        textHeart.text = currentEnemy2.heart.ToString();

        textHp.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Stamina", currentLocale) + currentEnemy.maxHP;
        textSpeed.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "MoveSpeed", currentLocale) + currentEnemy1.moveSpeed;
        textDef.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Armor", currentLocale) + currentEnemy.def;
        textMregi.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "MagicResi", currentLocale) + currentEnemy.mRegi * 100 +"%";
    }
   
}
