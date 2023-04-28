using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using System.Xml.Linq;

public class TowerDataViewer : MonoBehaviour
{
    [SerializeField]
    private Image imageTower;
    [SerializeField]
    private Image imageDamage;
    [SerializeField]
    private Image imageRate;
    [SerializeField]
    private Image imageRange;
    [SerializeField]
    private Image imageTargetRange;

    [SerializeField]
    private TextMeshProUGUI textTowerName;
    [SerializeField]
    private TextMeshProUGUI textTowerInfo;
    [SerializeField]
    private TextMeshProUGUI textDamage;
    [SerializeField]
    private TextMeshProUGUI textRate;
    [SerializeField]
    private TextMeshProUGUI textRange;
    [SerializeField]
    private TextMeshProUGUI textTargetRange;
    [SerializeField]
    private TextMeshProUGUI textUpGrade;
    [SerializeField]
    private TextMeshProUGUI textSell;

    [SerializeField]
    private TowerAttackRange towerAttackRange;

    private TowerWeapon currentTower;
    public GameObject upButton;
    public GameObject sellButton;

    public Sprite[] towerImage;
    public Sprite[] skillImage;

    public GameObject check;
    public GameObject infoPanel;
    public bool[] checkbox;

    private void Awake()
    {
        OffPanel();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }  
    public void OnPanel(Transform towerWeapon)
    {
        // ����ؾ��ϴ� Ÿ�� ������ �޾ƿͼ� ����
        currentTower = towerWeapon.GetComponent<TowerWeapon>();
        // Ÿ�� ���� Panel On
        gameObject.SetActive(true);
        // Ÿ�� ������ ����
        UpdateTowerData();
        // Ÿ�� ������Ʈ �ֺ��� ǥ��Ǵ� Ÿ�� ���ݹ��� Sprite On
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range + currentTower.i_range);
    }

    public void OffPanel()
    {
        // Ÿ�� ���� Panel Off
        gameObject.SetActive(false);
        // Ÿ�� ���ݹ��� Sprite Off
        towerAttackRange.OffAttackRange();
    }

    private void UpdateTowerData()
    {
        Locale currentLocale = LocalizationSettings.SelectedLocale;
        imageTower.sprite = currentTower.TowerSprite;

        if (currentTower.Grade == "�Ϲ�")
        {
            if (PlayerPrefs.GetInt("Local") == 0)
            {
                textTowerName.text = "<color=#B7B7B7>" + "[" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Normal", currentLocale) + "] " + "</color>" + currentTower.NameE;
            }
            else
            {
                textTowerName.text = "<color=#B7B7B7>" + "[" + currentTower.Grade + "] " + "</color>" + currentTower.Name;
            }
        }
        else if (currentTower.Grade == "���")
        {
            if (PlayerPrefs.GetInt("Local") == 0)
            {
                textTowerName.text = "<color=#0082FF>" + "[" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "High", currentLocale) + "] " + "</color>" + currentTower.NameE;
            }
            else
            {
                textTowerName.text = "<color=#0082FF>" + "[" + currentTower.Grade + "] " + "</color>" + currentTower.Name;
            }
        }
        else if (currentTower.Grade == "����")
        {
            if (PlayerPrefs.GetInt("Local") == 0)
            {
                textTowerName.text = "<color=#DB00FF>" + "[" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Hero", currentLocale) + "] " + "</color>" + currentTower.NameE;
            }
            else
            {
                textTowerName.text = "<color=#DB00FF>" + "[" + currentTower.Grade + "] " + "</color>" + currentTower.Name;
            }
        }
        else if (currentTower.Grade == "����")
        {
            if (PlayerPrefs.GetInt("Local") == 0)
            {
                textTowerName.text = "<color=#ff0000>" + "[" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Legend", currentLocale) + "] " + "</color>" + currentTower.NameE;
            }
            else
            {
                textTowerName.text = "<color=#ff0000>" + "[" + currentTower.Grade + "] " + "</color>" + currentTower.Name;
            }
        }
        else if (currentTower.Grade == "��ȭ")
        {
            if (PlayerPrefs.GetInt("Local") == 0)
            {
                textTowerName.text = "<color=#FFE000>" + "[" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Myth", currentLocale) + "] " + "</color>" + currentTower.NameE;
            }
            else
            {
                textTowerName.text = "<color=#FFE000>" + "[" + currentTower.Grade + "] " + "</color>" + currentTower.Name;
            }
        }

        if (PlayerPrefs.GetInt("Local") == 0)
        {
            textTowerInfo.text = " " + currentTower.InfoE;
        }
        else
        {
            textTowerInfo.text = " " + currentTower.Info;
        }

        if (currentTower.Damage > 0 && currentTower.i_damage > 0)
        {
            textDamage.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "atk", currentLocale) + currentTower.Damage + "+" + currentTower.i_damage;
        }
        else if (currentTower.Damage > 0)
        {
            textDamage.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "atk", currentLocale) + currentTower.Damage;
        }
        else if (currentTower.MagicDamage > 0 && currentTower.i_mdamage > 0)
        {
            textDamage.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "matk", currentLocale) + currentTower.MagicDamage + "+" + currentTower.i_mdamage;
        }
        else if(currentTower.MagicDamage > 0)
        {
            textDamage.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "matk", currentLocale) + currentTower.MagicDamage;
        }

        if(currentTower.i_rate > 0)
        {
            textRate.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "spe", currentLocale) + currentTower.Rate + "-" + currentTower.i_rate + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "sec", currentLocale);
        }
        else
        {
            textRate.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "spe", currentLocale) + currentTower.Rate + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "sec", currentLocale);
        }

        if (currentTower.i_range > 0)
        {
            textRange.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "ran", currentLocale) + currentTower.Range + "+" + currentTower.i_range;
        }
        else
        {
            textRange.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "ran", currentLocale) + currentTower.Range;
        }

        textTargetRange.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "ext", currentLocale) + currentTower.TargetRange;
        textUpGrade.text = "" + currentTower.Cost;
        textSell.text = "" + currentTower.Sell;
        
        // ���׷��̵尡 �Ұ��������� ��ư ��Ȱ��ȭ
        //buttonUpgrade.interactable = currentTower.Level < currentTower.Maxlevel ? true : false;
        //if (currentTower.upDone == true)
        //{
        //    upButton2.interactable = false;
        //}
        //else
        //    upButton2.interactable = true;
    }
    //public void UpdateTowerData00(int id)
    //{
    //    if(id == 0)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "�ü�";
    //        textTowerInfo.text = "�����Ÿ��� ��� ���ݼӵ��� �����ϴ�.";

    //        //textDamage.text = " ���ݷ� : 11";
    //        //textRate.text = "���ݼӵ� : 0.8��";
    //        //textRange.text = " ��Ÿ� : 3.5";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 1)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "�˻�";
    //        textTowerInfo.text = "���ݷ°� ���ݼӵ��� ������ �����Ÿ��� ª���ϴ�.";

    //        //textDamage.text = " ���ݷ� : 17";
    //        //textRate.text = "���ݼӵ� : 1.1��";
    //        //textRange.text = " ��Ÿ� : 2";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 2)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "��ź��";
    //        textTowerInfo.text = "���� ���ݷ°� ���������� �����մϴ�.";

    //        //textDamage.text = " ���ݷ� : 20";
    //        //textRate.text = "���ݼӵ� : 2.2��";
    //        //textRange.text = " ��Ÿ� : 2.5";
    //        //textTargetRange.text = "���� : 1.5x1.5";
    //    }
    //    if (id == 3)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "ȭ�� ������";
    //        textTowerInfo.text = "���� ������ �����ϴ� ���� ������ �մϴ�.";

    //        //textDamage.text = " �������ݷ� : 30";
    //        //textRate.text = "���ݼӵ� : 1.6��";
    //        //textRange.text = " ��Ÿ� : 2.5";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 4)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "��ȭ�� �ü�";
    //        textTowerInfo.text = "1�ʴ� 20�� �������� 5�ʰ� �ִ� ��ȭ���� �߻� �մϴ�.";

    //        //textDamage.text = " ���ݷ� : 12";
    //        //textRate.text = "���ݼӵ� : 1��";
    //        //textRange.text = " ��Ÿ� : 3";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 5)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "�ٶ��� ���";
    //        textTowerInfo.text = "�ٶ��������� ������ �ϴ� �����뺴 �Դϴ�.";

    //        //textDamage.text = " ���ݷ� : 20";
    //        //textRate.text = "���ݼӵ� : 1.5��";
    //        //textRange.text = " ��Ÿ� : 2";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 6)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "�� ��ź��";
    //        textTowerInfo.text = "50% Ȯ���� �̵��ӵ��� 0.05 ���� ��Ű�� ����ź�� �߻� �մϴ�.";

    //        //textDamage.text = " ���ݷ� : 15";
    //        //textRate.text = "���ݼӵ� : 2.2��";
    //        //textRange.text = " ��Ÿ� : 2.5";
    //        //textTargetRange.text = "���� : 1x1";
    //    }
    //    if (id == 7)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "���� ������";
    //        textTowerInfo.text = "30%Ȯ���� 3�ʰ� �󸮴� ���������� ��� �մϴ�.";

    //        //textDamage.text = " �������ݷ� : 20";
    //        //textRate.text = "���ݼӵ� : 1.7��";
    //        //textRange.text = " ��Ÿ� : 2.5";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 8)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "��ȭ�� �ü�";
    //        textTowerInfo.text = "10%Ȯ���� ���߽� �ʴ� 10�� �������� �ִ� ��ȭ���� ��� �մϴ�.";

    //        //textDamage.text = " ���ݷ� : 16";
    //        //textRate.text = "���ݼӵ� : 1.1��";
    //        //textRange.text = " ��Ÿ� : 3";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 9)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "������";
    //        textTowerInfo.text = "50% Ȯ���� 0.5�ʰ� ������ �Ŵ� �б⸦ ��� �մϴ�.";

    //        //textDamage.text = " ���ݷ� : 17";
    //        //textRate.text = "���ݼӵ� : 1.1��";
    //        //textRange.text = " ��Ÿ� : 2";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 10)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "���� ����";
    //        textTowerInfo.text = "���� �̵��ӵ��� 0.1�� ���� ��ŵ�ϴ�.";

    //        //textDamage.text = " ���ݷ� : 20";
    //        //textRate.text = "���ݼӵ� : 2.2��";
    //        //textRange.text = " ��Ÿ� : 2.5";
    //        //textTargetRange.text = "���� : 1.5x1.5";
    //    }
    //    if (id == 11)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "���� ������";
    //        textTowerInfo.text = "1�ʰ� ������ �ִ� ���������� ��� �մϴ�.";

    //        //textDamage.text = " �������ݷ� : 30";
    //        //textRate.text = "���ݼӵ� : 1.6��";
    //        //textRange.text = " ��Ÿ� : 2.5";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 12)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "�� �ü�";
    //        textTowerInfo.text = "��븦 �󸮴� ����ȭ��� ������ �ѹ濡 ȭ���� ���ϴ�.";

    //        //textDamage.text = " ���ݷ� : 12";
    //        //textRate.text = "���ݼӵ� : 1��";
    //        //textRange.text = " ��Ÿ� : 3";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 13)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "��â��";
    //        textTowerInfo.text = "5% Ȯ���� 1�ʴ� �ڽ��� �������� ���ݿ� �ش��ϴ� ���������� �ݴϴ�.";

    //        //textDamage.text = " ���ݷ� : 20";
    //        //textRate.text = "���ݼӵ� : 1.5��";
    //        //textRange.text = " ��Ÿ� : 2";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 14)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "����";
    //        textTowerInfo.text = "�������� ����ź�� ��� �մϴ�.";

    //        //textDamage.text = " ���ݷ� : 15";
    //        //textRate.text = "���ݼӵ� : 2.2��";
    //        //textRange.text = " ��Ÿ� : 2.5";
    //        //textTargetRange.text = "���� : 1x1";
    //    }
    //    if (id == 15)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "��� ������";
    //        textTowerInfo.text = "���� Ȯ���� ���͸� ��� ��Ű�� ������ ��� �մϴ�.(���� ����)";

    //        //textDamage.text = " �������ݷ� : 20";
    //        //textRate.text = "���ݼӵ� : 1.7��";
    //        //textRange.text = " ��Ÿ� : 2.5";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //}
    public void UpdateSkillData(int id)
    {
        Locale currentLocale = LocalizationSettings.SelectedLocale;
        
        imageTower.sprite = skillImage[id];
        textTowerInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", id+"Info", currentLocale);
        textRange.text = "<color=#ff0000>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "MagicDamage", currentLocale) + "</color>";
        textDamage.text = "";
        textRate.text = "";
        if (id == 0) 
        {
            textTowerName.text = "<color=#ff0000>" + LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", id.ToString(), currentLocale) + "</color>";
            textTargetRange.text = "<color=#ff0000>" + " 100" + "</color>";
        }
        if (id == 1)
        {
            textTowerName.text = "<color=#F600FF>" + LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", id.ToString(), currentLocale) + "</color>";
            textTargetRange.text = "<color=#ff0000>" + " 150" + "</color>";
        }
        if (id == 2)
        {
            textTowerName.text = "<color=#853800>" + LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", id.ToString(), currentLocale) + "</color>";
            textTargetRange.text = "<color=#ff0000>" + " 200 ~ 400" + "</color>";
        }
        if (id == 3)
        {
            textTowerName.text = "<color=#009AFF>" + LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", id.ToString(), currentLocale) + "</color>";
            textTargetRange.text = "<color=#ff0000>" + " 50" + "</color>";
        }
        if (id == 4)
        {
            textTowerName.text = "<color=#00AE00>" + LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", id.ToString(), currentLocale) + "</color>";
            textTargetRange.text = "<color=#ff0000>" + " 100" + "</color>";
        }
        if (id == 5)
        {
            textTowerName.text = "<color=#FFD600>" + LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", id.ToString(), currentLocale) + "</color>";
            textTargetRange.text = "<color=#ff0000>" + " 250" + "</color>";
        }
    }
    public void OnClickEventTowerUpgrade()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        if (checkbox[0] == false)
        {
            check.transform.position = clickObject.transform.position;
            check.SetActive(true);
            infoPanel.SetActive(true);

            //towerDataViewer.UpdateTowerData01();
            checkbox[0] = true;
            checkbox[1] = false;

        }
        else if (checkbox[0] == true)
        {
            // Ÿ�� ���׷��̵� �õ� (����:true, ����:false)
            bool isSuccess = currentTower.Upgrade();

            if (isSuccess == true)
            {
                // Ÿ���� ���׷��̵� �Ǿ��� ������ Ÿ�� ���� ����
                UpdateTowerData();
                // Ÿ�� �ֺ��� ���̴� ���ݹ����� ����
                towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range + currentTower.i_range);
                upButton.SetActive(false);
                sellButton.SetActive(false);
                towerAttackRange.OffAttackRange();
                check.SetActive(false);
                //infoPanel.SetActive(false);
                //GameObject clone1 = Instantiate(bellEffect[0], currentTower.transform.position + Vector3.up * 0.75f, Quaternion.identity);

                //GameObject clone = Instantiate(spawnEffect[currentTower.choiceType], currentTower.transform.position/* + Vector3.down * 1.7f*/, Quaternion.identity);
                //clone.transform.SetParent(currentTower.transform);
                //StartCoroutine("Inchante");
                checkbox[0] = false;

            }
            else
            {
                // Ÿ�� ���׷��̵忡 �ʿ��� ����� �����ϴٰ� ���
            }
        }

    }
    public void OnClickEventTowerSell()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        if (checkbox[1] == false)
        {
            check.transform.position = clickObject.transform.position;
            check.SetActive(true);
            infoPanel.SetActive(true);

            //towerDataViewer.UpdateTowerData01();
            checkbox[1] = true;
            checkbox[0] = false;

        }
        else if (checkbox[1] == true)
        {
            // Ÿ�� �Ǹ�
            currentTower.TowerSell();
            // ������ Ÿ���� ������� Panel, ���ݹ��� Off
            OffPanel();
            upButton.SetActive(false);
            sellButton.SetActive(false);
            check.SetActive(false);
            infoPanel.SetActive(false);

            checkbox[1] = false;
        }
    }
}
