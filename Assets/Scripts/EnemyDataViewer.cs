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
        // ����ؾ��ϴ� Ÿ�� ������ �޾ƿͼ� ����
        currentEnemy = enemydata.GetComponent<EnemyHP>();
        currentEnemy1 = enemydata.GetComponent<Movement2DAni>();
        currentEnemy2 = enemydata.GetComponent<Enemy>();

        // Ÿ�� ���� Panel On
        gameObject.SetActive(true);
        // Ÿ�� ������ ����
        UpdateEnemyData();
    }

    public void OffPanel()
    {
        // Ÿ�� ���� Panel Off
        gameObject.SetActive(false);

    }

    //private void UpdateTowerData()
    //{
    //    imageTower.sprite = currentTower.TowerSprite;
    //    textTowerName.text = currentTower.Name;
    //    textTowerInfo.text = currentTower.Info;
    //    if(currentTower.Damage > 0)
    //    {
    //        textDamage.text = " ���ݷ� : " + currentTower.Damage;
    //    }
    //    else if(currentTower.MagicDamage > 0)
    //    {
    //        textDamage.text = " �������ݷ� : " + currentTower.MagicDamage;
    //    }
            
    //    textRate.text = "���ݼӵ� : " + currentTower.Rate + "��";
    //    textRange.text = " ��Ÿ� : " + currentTower.Range;
    //    textTargetRange.text = "���� : " + currentTower.TargetRange;
    //    textUpGrade.text = "" + currentTower.Cost * 1.5;
    //    textSell.text = "" + currentTower.Sell;

    //    // ���׷��̵尡 �Ұ��������� ��ư ��Ȱ��ȭ
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
            else if (currentEnemy.enemyID == 33) // Ȳ�� ��ũ
            {
                textEnemyName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", currentEnemy.enemyID.ToString(), currentLocale) + "<color=#FF0000>" + "\n(" + LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Mid-Boss", currentLocale) + ")" + "</color>";
            }
            else if (currentEnemy.enemyID == 34) //���� �ذ� Ȳ��
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
            //textEnemyName.text = "���� ��ũ �ü�" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
            //textEnemyInfo.text = "��ũ �ü��� ü���� �ణ ���ƿ�.";
        }
        //if (currentEnemy.enemyID == 22)
        //{
        //    imageEnemy.sprite = enemyImage[5];
        //    textEnemyName.text = "���� �ذ� ����" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "�ذ� ����� ������ �ణ ���ƿ�.";
        //}

        //if (currentEnemy.enemyID == 23)
        //{
        //    imageEnemy.sprite = enemyImage[6];
        //    textEnemyName.text = "���� ��Ƽ" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "��Ƽ�� ���� ���׷��� �ణ ���ƿ�.";
        //}

        //if (currentEnemy.enemyID == 24)
        //{
        //    imageEnemy.sprite = enemyImage[7];
        //    textEnemyName.text = "���� ���� ��Ʈ" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "���� ��Ʈ�� �̵��ӵ��� �ణ �����.";
        //}

        //if (currentEnemy.enemyID == 25)
        //{
        //    imageEnemy.sprite = enemyImage[8];
        //    textEnemyName.text = "���� ��ũ ������" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "��ũ �������� ü���� ���ƿ�.";
        //}

        //if (currentEnemy.enemyID == 26)
        //{
        //    imageEnemy.sprite = enemyImage[9];
        //    textEnemyName.text = "���� ����Ų���" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "����Ų���� ������ ���ƿ�.";
        //}

        //if (currentEnemy.enemyID == 27)
        //{
        //    imageEnemy.sprite = enemyImage[10];
        //    textEnemyName.text = "���� ���̵�" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "���̵�� ���� ���׷��� ���ƿ�.";
        //}

        //if (currentEnemy.enemyID == 28)
        //{
        //    imageEnemy.sprite = enemyImage[11];
        //    textEnemyName.text = "���� ���̵� ��Ʈ" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "���̵� ��Ʈ�� �̵��ӵ��� �����.";
        //}
        //if (currentEnemy.enemyID == 29)
        //{
        //    imageEnemy.sprite = enemyImage[12];
        //    textEnemyName.text = "���� ��ũ ������" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "��ũ �������� ü���� �ſ� ���ƿ�.";
        //}
        //if (currentEnemy.enemyID == 30)
        //{
        //    imageEnemy.sprite = enemyImage[13];
        //    textEnemyName.text = "���� ��������" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "���������� ������ �ſ� ���ƿ�.";

        //}
        //if (currentEnemy.enemyID == 31)
        //{
        //    imageEnemy.sprite = enemyImage[14];
        //    textEnemyName.text = "���� �� ���̵�" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "�� ���̵�� ���� ���׷��� �ſ� ���ƿ�.";
        //}
        //if (currentEnemy.enemyID == 32)
        //{
        //    imageEnemy.sprite = enemyImage[15];
        //    textEnemyName.text = "���� ������ ��Ʈ" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "������ ��Ʈ�� �̵��ӵ��� �ſ� �����.";
        //}
        //if (currentEnemy.enemyID == 33)
        //{
        //    imageEnemy.sprite = enemyImage[16];
        //    textEnemyName.text = "Ȳ�� ��ũ" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "��ũ ŷ�� ü���� �ſ�ſ� ���ƿ�.";
        //}
        //if (currentEnemy.enemyID == 34)
        //{
        //    imageEnemy.sprite = enemyImage[17];
        //    textEnemyName.text = "���� �ذ� Ȳ��" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "���� �ذ������ ������ �ſ�ſ� ���ƿ�.";
        //}
        //if (currentEnemy.enemyID == 35)
        //{
        //    imageEnemy.sprite = enemyImage[18];
        //    textEnemyName.text = "Ȳ�� �� ������" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "�� ������� ���� ���׷��� �ſ�ſ� ���ƿ�.";
        //}
        //if (currentEnemy.enemyID == 36)
        //{
        //    imageEnemy.sprite = enemyImage[19];
        //    textEnemyName.text = "Ȳ�� ����" + "<color=#ff0000>" + "\n(�߰�����)" + "</color>";
        //    textEnemyInfo.text = "������ �̵��ӵ��� �ſ�ſ� �����";
        //}
        //if (currentEnemy.enemyID == 37)
        //{
        //    imageEnemy.sprite = enemyImage[20];
        //    textEnemyName.text = "Ȳ�� �����" + "<color=#ff0000>" + "\n(����)" + "</color>";
        //    textEnemyInfo.text = "����";
        //}
        //if (currentEnemy.enemyID == 38)
        //{
        //    imageEnemy.sprite = enemyImage[21];
        //    textEnemyName.text = "Ȳ�� ���ڵ�" + "<color=#ff0000>" + "\n(����)" + "</color>";
        //    textEnemyInfo.text = "����";
        //}

        textGold.text = currentEnemy2.gold.ToString();
        textHeart.text = currentEnemy2.heart.ToString();

        textHp.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Stamina", currentLocale) + currentEnemy.maxHP;
        textSpeed.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "MoveSpeed", currentLocale) + currentEnemy1.moveSpeed;
        textDef.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "Armor", currentLocale) + currentEnemy.def;
        textMregi.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMonster", "MagicResi", currentLocale) + currentEnemy.mRegi * 100 +"%";
    }
   
}
