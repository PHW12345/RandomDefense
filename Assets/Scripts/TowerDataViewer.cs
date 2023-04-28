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
        // 출력해야하는 타워 정보를 받아와서 저장
        currentTower = towerWeapon.GetComponent<TowerWeapon>();
        // 타워 정보 Panel On
        gameObject.SetActive(true);
        // 타워 정보를 갱신
        UpdateTowerData();
        // 타워 오브젝트 주변에 표기되는 타워 공격범위 Sprite On
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range + currentTower.i_range);
    }

    public void OffPanel()
    {
        // 타워 정보 Panel Off
        gameObject.SetActive(false);
        // 타워 공격범위 Sprite Off
        towerAttackRange.OffAttackRange();
    }

    private void UpdateTowerData()
    {
        Locale currentLocale = LocalizationSettings.SelectedLocale;
        imageTower.sprite = currentTower.TowerSprite;

        if (currentTower.Grade == "일반")
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
        else if (currentTower.Grade == "상급")
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
        else if (currentTower.Grade == "영웅")
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
        else if (currentTower.Grade == "전설")
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
        else if (currentTower.Grade == "신화")
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
        
        // 업그레이드가 불가능해지면 버튼 비활성화
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
    //        textTowerName.text = "궁수";
    //        textTowerInfo.text = "사정거리가 길며 공격속도가 빠릅니다.";

    //        //textDamage.text = " 공격력 : 11";
    //        //textRate.text = "공격속도 : 0.8초";
    //        //textRange.text = " 사거리 : 3.5";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 1)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "검사";
    //        textTowerInfo.text = "공격력과 공격속도가 빠르나 사정거리가 짧습니다.";

    //        //textDamage.text = " 공격력 : 17";
    //        //textRate.text = "공격속도 : 1.1초";
    //        //textRange.text = " 사거리 : 2";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 2)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "포탄병";
    //        textTowerInfo.text = "높은 공격력과 광역공격이 가능합니다.";

    //        //textDamage.text = " 공격력 : 20";
    //        //textRate.text = "공격속도 : 2.2초";
    //        //textRange.text = " 사거리 : 2.5";
    //        //textTargetRange.text = "범위 : 1.5x1.5";
    //    }
    //    if (id == 3)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "화염 마법사";
    //        textTowerInfo.text = "적의 방어력을 무시하는 마법 공격을 합니다.";

    //        //textDamage.text = " 마법공격력 : 30";
    //        //textRate.text = "공격속도 : 1.6초";
    //        //textRange.text = " 사거리 : 2.5";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 4)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "불화살 궁수";
    //        textTowerInfo.text = "1초당 20에 데미지를 5초간 주는 불화살을 발사 합니다.";

    //        //textDamage.text = " 공격력 : 12";
    //        //textRate.text = "공격속도 : 1초";
    //        //textRange.text = " 사거리 : 3";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 5)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "바람의 기사";
    //        textTowerInfo.text = "바람의힘으로 공격을 하는 근접용병 입니다.";

    //        //textDamage.text = " 공격력 : 20";
    //        //textRate.text = "공격속도 : 1.5초";
    //        //textRange.text = " 사거리 : 2";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 6)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "물 폭탄병";
    //        textTowerInfo.text = "50% 확률로 이동속도를 0.05 감소 시키는 물폭탄를 발사 합니다.";

    //        //textDamage.text = " 공격력 : 15";
    //        //textRate.text = "공격속도 : 2.2초";
    //        //textRange.text = " 사거리 : 2.5";
    //        //textTargetRange.text = "범위 : 1x1";
    //    }
    //    if (id == 7)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "얼음 마법사";
    //        textTowerInfo.text = "30%확률로 3초간 얼리는 얼음마법을 사용 합니다.";

    //        //textDamage.text = " 마법공격력 : 20";
    //        //textRate.text = "공격속도 : 1.7초";
    //        //textRange.text = " 사거리 : 2.5";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 8)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "독화살 궁수";
    //        textTowerInfo.text = "10%확률로 적중시 초당 10의 데미지를 주는 독화살을 사용 합니다.";

    //        //textDamage.text = " 공격력 : 16";
    //        //textRate.text = "공격속도 : 1.1초";
    //        //textRange.text = " 사거리 : 3";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 9)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "성직자";
    //        textTowerInfo.text = "50% 확률로 0.5초간 스턴을 거는 둔기를 사용 합니다.";

    //        //textDamage.text = " 공격력 : 17";
    //        //textRate.text = "공격속도 : 1.1초";
    //        //textRange.text = " 사거리 : 2";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 10)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "진흙 포병";
    //        textTowerInfo.text = "적의 이동속도를 0.1를 감소 시킵니다.";

    //        //textDamage.text = " 공격력 : 20";
    //        //textRate.text = "공격속도 : 2.2초";
    //        //textRange.text = " 사거리 : 2.5";
    //        //textTargetRange.text = "범위 : 1.5x1.5";
    //    }
    //    if (id == 11)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "번개 마법사";
    //        textTowerInfo.text = "1초간 스턴이 있는 번개마법을 사용 합니다.";

    //        //textDamage.text = " 마법공격력 : 30";
    //        //textRate.text = "공격속도 : 1.6초";
    //        //textRange.text = " 사거리 : 2.5";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 12)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "석 궁수";
    //        textTowerInfo.text = "상대를 얼리는 얼음화살과 강력한 한방에 화살을 쏩니다.";

    //        //textDamage.text = " 공격력 : 12";
    //        //textRate.text = "공격속도 : 1초";
    //        //textRange.text = " 사거리 : 3";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 13)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "독창병";
    //        textTowerInfo.text = "5% 확률로 1초당 자신의 데미지의 절반에 해당하는 독데미지를 줍니다.";

    //        //textDamage.text = " 공격력 : 20";
    //        //textRate.text = "공격속도 : 1.5초";
    //        //textRange.text = " 사거리 : 2";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 14)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "군주";
    //        textTowerInfo.text = "광범위의 핵폭탄을 사용 합니다.";

    //        //textDamage.text = " 공격력 : 15";
    //        //textRate.text = "공격속도 : 2.2초";
    //        //textRange.text = " 사거리 : 2.5";
    //        //textTargetRange.text = "범위 : 1x1";
    //    }
    //    if (id == 15)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "어둠 마법사";
    //        textTowerInfo.text = "일정 확률로 몬스터를 즉사 시키는 마법을 사용 합니다.(보스 제외)";

    //        //textDamage.text = " 마법공격력 : 20";
    //        //textRate.text = "공격속도 : 1.7초";
    //        //textRange.text = " 사거리 : 2.5";
    //        //textTargetRange.text = "범위 : 단일";
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
            // 타워 업그레이드 시도 (성공:true, 실패:false)
            bool isSuccess = currentTower.Upgrade();

            if (isSuccess == true)
            {
                // 타워가 업그레이드 되었기 때문에 타워 정보 갱신
                UpdateTowerData();
                // 타워 주변에 보이는 공격범위도 갱신
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
                // 타워 업그레이드에 필요한 비용이 부족하다고 출력
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
            // 타워 판매
            currentTower.TowerSell();
            // 선택한 타워가 사라져서 Panel, 공격범위 Off
            OffPanel();
            upButton.SetActive(false);
            sellButton.SetActive(false);
            check.SetActive(false);
            infoPanel.SetActive(false);

            checkbox[1] = false;
        }
    }
}
