using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class StageData : MonoBehaviour
{
    private int[] stage = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] stageScore = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    //private int sum = 0;
    public GameObject[] stageButton;
    public Image[] stageImage;
    public Sprite flag;

    public Image[] stageStar;
    public Sprite clearStar;

    public GameObject stagePanel;
    
    [SerializeField]
    private TextMeshProUGUI mapName;
    [SerializeField]
    private TextMeshProUGUI mapInfo;
    [SerializeField]
    private TextMeshProUGUI rewardInfo;
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private TextMeshProUGUI[] monsterInfo;

    [SerializeField]
    private Image map;
    [SerializeField]
    private Image reward;
    [SerializeField]
    private Image[] monster;

    public Sprite[] mapImage;
    public Sprite[] rewardImage;
    public Sprite[] monsterImage;

    private int stageNum;
    public Button startButton;
    //[SerializeField]
    //private GameObject[] myDeck;

    [SerializeField]
    private TextMeshProUGUI textPlayerStar; // Text - TextMeshPro UI [�÷��̾��� �����]
    [SerializeField]
    private TextMeshProUGUI textPlayerDiamond; // Text - TextMeshPro UI [�÷��̾��� ���̾Ƹ��]

    public int starSum; //���� ����
    public int diamond; //���̾Ƹ��

    //private int[] towerCount = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //[SerializeField]
    //private TextMeshProUGUI[] towerCountText;

    //[SerializeField]
    //private Image imageTower;
    //[SerializeField]
    //private TextMeshProUGUI textTowerName;
    //[SerializeField]
    //private TextMeshProUGUI textTowerInfo;
    //public Sprite[] towerImage;
    // Start is called before the first frame update
    void Start()
    {
        stage[0] = PlayerPrefs.GetInt("Stage0");
        stage[1] = PlayerPrefs.GetInt("Stage1");
        stage[2] = PlayerPrefs.GetInt("Stage2");
        stage[3] = PlayerPrefs.GetInt("Stage3");
        stage[4] = PlayerPrefs.GetInt("Stage4");
        stage[5] = PlayerPrefs.GetInt("Stage5");
        stage[6] = PlayerPrefs.GetInt("Stage6");
        stage[7] = PlayerPrefs.GetInt("Stage7");
        stage[8] = PlayerPrefs.GetInt("Stage8");
        stage[9] = PlayerPrefs.GetInt("Stage9");
        stage[10] = PlayerPrefs.GetInt("Stage10");
        stage[11] = PlayerPrefs.GetInt("Stage11");

        stageScore[0] = PlayerPrefs.GetInt("StageScore00");
        stageScore[1] = PlayerPrefs.GetInt("StageScore01");
        stageScore[2] = PlayerPrefs.GetInt("StageScore02");
        stageScore[3] = PlayerPrefs.GetInt("StageScore03");
        stageScore[4] = PlayerPrefs.GetInt("StageScore04");
        stageScore[5] = PlayerPrefs.GetInt("StageScore05");
        stageScore[6] = PlayerPrefs.GetInt("StageScore06");
        stageScore[7] = PlayerPrefs.GetInt("StageScore07");
        stageScore[8] = PlayerPrefs.GetInt("StageScore08");
        stageScore[9] = PlayerPrefs.GetInt("StageScore09");
        stageScore[10] = PlayerPrefs.GetInt("StageScore10");
        stageScore[11] = PlayerPrefs.GetInt("StageScore11");

        //star = PlayerPrefs.GetInt("PlayerStar");
        diamond = PlayerPrefs.GetInt("PlayerDia");
        for (int i = 0; i < stage.Length; i++)
        {
            //sum += stage[i];

            if (stage[i] == 1)
            {
                stageImage[i].sprite = flag;
                //myDeck[i].SetActive(true);
            }
        }
        for (int i = 1; i < stageScore.Length; i++)
        {
            starSum += stageScore[i];
        }

        textPlayerStar.text = starSum.ToString() + "/33";
        // ��01 ����츲
        if (stageScore[1] == 1)
        {
            stageStar[0].sprite = clearStar;
        }
        else if (stageScore[1] == 2)
        {
            stageStar[0].sprite = clearStar;
            stageStar[1].sprite = clearStar;
        }
        else if (stageScore[1] == 3)
        {
            stageStar[0].sprite = clearStar;
            stageStar[1].sprite = clearStar;
            stageStar[2].sprite = clearStar;
        }
        // ��02 ���㰡 �� �ʿ�
        if (stageScore[2] == 1)
        {
            stageStar[3].sprite = clearStar;
        }
        else if (stageScore[2] == 2)
        {
            stageStar[3].sprite = clearStar;
            stageStar[4].sprite = clearStar;
        }
        else if (stageScore[2] == 3)
        {
            stageStar[3].sprite = clearStar;
            stageStar[4].sprite = clearStar;
            stageStar[5].sprite = clearStar;
        }
        // ��03 �����̴� ����
        if (stageScore[3] == 1)
        {
            stageStar[6].sprite = clearStar;
        }
        else if (stageScore[3] == 2)
        {
            stageStar[6].sprite = clearStar;
            stageStar[7].sprite = clearStar;
        }
        else if (stageScore[3] == 3)
        {
            stageStar[6].sprite = clearStar;
            stageStar[7].sprite = clearStar;
            stageStar[8].sprite = clearStar;
        }
        // ��04 �͵��� ��
        if (stageScore[4] == 1)
        {
            stageStar[9].sprite = clearStar;
        }
        else if (stageScore[4] == 2)
        {
            stageStar[9].sprite = clearStar;
            stageStar[10].sprite = clearStar;
        }
        else if (stageScore[4] == 3)
        {
            stageStar[9].sprite = clearStar;
            stageStar[10].sprite = clearStar;
            stageStar[11].sprite = clearStar;
        }
        // ��05 ��Ұ� �����ǵ�
        if (stageScore[5] == 1)
        {
            stageStar[12].sprite = clearStar;
        }
        else if (stageScore[5] == 2)
        {
            stageStar[12].sprite = clearStar;
            stageStar[13].sprite = clearStar;
        }
        else if (stageScore[5] == 3)
        {
            stageStar[12].sprite = clearStar;
            stageStar[13].sprite = clearStar;
            stageStar[14].sprite = clearStar;
        }
        // ��06 ��������
        if (stageScore[6] == 1)
        {
            stageStar[15].sprite = clearStar;
        }
        else if (stageScore[6] == 2)
        {
            stageStar[15].sprite = clearStar;
            stageStar[16].sprite = clearStar;
        }
        else if (stageScore[6] == 3)
        {
            stageStar[15].sprite = clearStar;
            stageStar[16].sprite = clearStar;
            stageStar[17].sprite = clearStar;
        }
        // ��07 ������ Ÿ��
        if (stageScore[7] == 1)
        {
            stageStar[18].sprite = clearStar;
        }
        else if (stageScore[7] == 2)
        {
            stageStar[18].sprite = clearStar;
            stageStar[19].sprite = clearStar;
        }
        else if (stageScore[7] == 3)
        {
            stageStar[18].sprite = clearStar;
            stageStar[19].sprite = clearStar;
            stageStar[20].sprite = clearStar;
        }
        // ��08 �縷�� ���ƽý�
        if (stageScore[8] == 1)
        {
            stageStar[21].sprite = clearStar;
        }
        else if (stageScore[8] == 2)
        {
            stageStar[21].sprite = clearStar;
            stageStar[22].sprite = clearStar;
        }
        else if (stageScore[8] == 3)
        {
            stageStar[21].sprite = clearStar;
            stageStar[22].sprite = clearStar;
            stageStar[23].sprite = clearStar;
        }
        // ��09 ���� ������
        if (stageScore[9] == 1)
        {
            stageStar[24].sprite = clearStar;
        }
        else if (stageScore[9] == 2)
        {
            stageStar[24].sprite = clearStar;
            stageStar[25].sprite = clearStar;
        }
        else if (stageScore[9] == 3)
        {
            stageStar[24].sprite = clearStar;
            stageStar[25].sprite = clearStar;
            stageStar[26].sprite = clearStar;
        }
        // ��10 �������
        if (stageScore[10] == 1)
        {
            stageStar[27].sprite = clearStar;
        }
        else if (stageScore[10] == 2)
        {
            stageStar[27].sprite = clearStar;
            stageStar[28].sprite = clearStar;
        }
        else if (stageScore[10] == 3)
        {
            stageStar[27].sprite = clearStar;
            stageStar[28].sprite = clearStar;
            stageStar[29].sprite = clearStar;
        }
        
        // ��11 ������ ����
        if (stageScore[11] == 1)
        {
            stageStar[30].sprite = clearStar;
        }
        else if (stageScore[11] == 2)
        {
            stageStar[30].sprite = clearStar;
            stageStar[31].sprite = clearStar;
        }
        else if (stageScore[11] == 3)
        {
            stageStar[30].sprite = clearStar;
            stageStar[31].sprite = clearStar;
            stageStar[32].sprite = clearStar;
        }
        //for (int i = 0; i < towerCount.Length; i++)
        //{
        //    towerCountText[i].text = "" + towerCount[i];
        //}

        if (stage[0] == 1) //Ʃ�丮�� Ŭ����� ������, ������ ����
        {
            stageButton[0].SetActive(true);
            stageButton[1].SetActive(true);

            stageButton[2].SetActive(true);
            stageButton[3].SetActive(true);
            stageButton[4].SetActive(true);
        }

        if (stage[1] == 1 && stage[2] == 1) //������ �� Ŭ����� �������� ����
        {
            stageButton[5].SetActive(true);
            stageButton[6].SetActive(true);
        }

        if (stage[3] == 1 && stage[4] == 1 && stage[5] == 1) //������ �� Ŭ����� �縷���� ����
        {
            stageButton[7].SetActive(true);
            stageButton[8].SetActive(true);
        }

        if (stage[6] == 1 && stage[7] == 1 && stage[8] == 1 && stage[9] == 1) //��������, �縷���� �� Ŭ����� ������� �� ����
        {
            stageButton[9].SetActive(true);
        }
        if (stage[10] == 1 ) //������� �� Ŭ����� ������ ����
        {
            stageButton[10].SetActive(true);
        }
    }
    // �������� ����â �ѱ�
    public void StageInfoOn(int id)
    {
        stagePanel.SetActive(true);
        Locale currentLocale = LocalizationSettings.SelectedLocale;


        mapName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMap", "Map"+ id, currentLocale);
        mapInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMap", "MapInfo" +id, currentLocale);
        map.sprite = mapImage[id];
        stageNum = id;
        //if (id == 0)
        //{
        //    mapName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMap", "Map0", currentLocale);
        //    mapInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMap", "MapInfo0", currentLocale);
        //    rewardInfo.text = " Ʃ�丮�� ���������� ������ �����ϴ�";
        //    score.text = "" + stageScore[0];
        //    map.sprite = mapImage[0];
        //    reward.sprite = rewardImage[0];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " ��ũâ��(Lv.1)-ü����";
        //    monsterInfo[1].text = " �Ʊ��ذ�(Lv.1)-�����";
        //    monsterInfo[2].text = " ��Ʈ(Lv.1)-���ǵ���";
        //    monsterInfo[3].text = " �Ʊ���Ƽ(Lv.1)-���������";

        //    monster[0].sprite = monsterImage[1];
        //    monster[1].sprite = monsterImage[2];
        //    monster[2].sprite = monsterImage[3];
        //    monster[3].sprite = monsterImage[4];

        //    stageNum = id;
        //}
        //if (id == 1)
        //{
        //    mapName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMap", "Map1", currentLocale);
        //    mapInfo.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyMap", "MapInfo1", currentLocale);
        //    rewardInfo.text = " < ��ȭ�� �ü� >\n\n 1�� �� 5�� �������� 5�� �ִ� ��ȭ���� ���ϴ�.\n ���׷��̵� �� 1�� �� 10�� �������� 5�� �ִ� ��ȭ���� ���ϴ�.";
        //    score.text = "" + stageScore[1];
        //    map.sprite = mapImage[1];
        //    reward.sprite = rewardImage[1];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " �Ʊ��ذ�(Lv.1)-�����";
        //    monsterInfo[1].text = " �ذ�����(Lv.2)-�����";
        //    monsterInfo[2].text = " ����Ų����(Lv.3)-�����";
        //    monsterInfo[3].text = " �����뺴�� �Ϲݿ뺴 �Ѵ� ���� ���̴±���!";

        //    monster[0].sprite = monsterImage[2];
        //    monster[1].sprite = monsterImage[6];
        //    monster[2].sprite = monsterImage[10];
        //    monster[3].sprite = monsterImage[0];

        //    stageNum = id;
        //}

        //if (id == 2)
        //{
        //    mapName.text = "< ���㰡 �� �ʿ� >";
        //    mapInfo.text = " ���͵��� �� ���� �۾��� ������ ���㰡 �� �ʿ��� ��� ���� ������.";

        //    rewardInfo.text = " < ���� ���� >\n\n ���� ������ 2���̴� ������ ��ô �մϴ�.(�ߺ� ����)\n ���׷��̵� �� ���� ������ 3���̴� ������ ��ô �մϴ�.(�ߺ� ����)";
        //    score.text = "" + stageScore[2];
        //    map.sprite = mapImage[2];
        //    reward.sprite = rewardImage[2];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " ��Ʈ(Lv.1)-���ǵ���";
        //    monsterInfo[1].text = " ���װ�Ʈ(Lv.2)-���ǵ���";
        //    monsterInfo[2].text = " ���̵��Ʈ(Lv.3)-���ǵ���";
        //    monsterInfo[3].text = " ���� �뺴���� Ȱ���� ���ƿ�!";

        //    monster[0].sprite = monsterImage[4];
        //    monster[1].sprite = monsterImage[8];
        //    monster[2].sprite = monsterImage[12];
        //    monster[3].sprite = monsterImage[0];

        //    stageNum = id;
        //}
        //if (id == 3)
        //{
        //    mapName.text = "< �����̴� ���� >";
        //    mapInfo.text = " ������ ��ü�� ���� �ұ��� �����Դϴ�";

        //    rewardInfo.text = " < ��ȭ�� �ü� >\n\n 1�� �� 2�� �������� �ִ� ��ȭ���� ���ϴ�.\n ���׷��̵� �� 1�� �� 3�� �������� �ִ� ��ȭ���� ���ϴ�.";
        //    //mapName.text = "< ������ ��� >";
        //    //mapInfo.text = " �縷 ������ ������ ������ �Դϴ�";
        //    //rewardInfo.text = " < ������ ��ź�� >\n\n 20% Ȯ���� ���� �̵��ӵ���0.3 ���� ��Ű�� ������ ��������ź�� ���ϴ�.(�ߺ� �Ұ�)\n ���׷��̵� �� 30% Ȯ���� ���� �̵��ӵ���0.4 ���� ��Ű�� ������ ��������ź�� ���ϴ�.(�ߺ� �Ұ�)";
        //    score.text = "" + stageScore[3];
        //    map.sprite = mapImage[3];
        //    reward.sprite = rewardImage[3];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " ��ũâ��(Lv.1)-ü����";
        //    monsterInfo[1].text = " ��ũ�ü�(Lv.2)-ü����";
        //    monsterInfo[2].text = " ��ũ������(Lv.3)-ü����";
        //    monsterInfo[3].text = " �ú����� �� ��ġ�ϸ� ���� �� ���ƿ�!";

        //    monster[0].sprite = monsterImage[1];
        //    monster[1].sprite = monsterImage[5];
        //    monster[2].sprite = monsterImage[9];
        //    monster[3].sprite = monsterImage[0];

        //    stageNum = id;
        //}
        //if (id == 4)
        //{
        //    mapName.text = "< �͵��� �� >";
        //    mapInfo.text = " �͵��� ������ �ذ��� �˴ϴ�";
        //    rewardInfo.text = " < ��ȭ�� �ü� >\n\n 1�� �� 2�� �������� �ִ� ��ȭ���� ���ϴ�.\n ���׷��̵� �� 1�� �� 3�� �������� �ִ� ��ȭ���� ���ϴ�.";
        //    //mapName.text = "< ������ Ÿ�� >";
        //    //mapInfo.text = " ������ ��ũ������ ��� �߿� �����Դϴ�";
        //    //rewardInfo.text = " < ���� ������ >\n\n 30% Ȯ���� 3�ʵ��� ���� �󸮴� ���� ������ ��� �մϴ�.\n ���׷��̵� �� 40% Ȯ���� 5�ʵ��� ���� �󸮴� ���� ������ ��� �մϴ�.";
        //    score.text = "" + stageScore[4];
        //    map.sprite = mapImage[4];
        //    reward.sprite = rewardImage[4];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " �Ʊ���Ƽ(Lv.1)-���������";
        //    monsterInfo[1].text = " ��Ƽ(Lv.2)-���������";
        //    monsterInfo[2].text = " ���̵�(Lv.3)-���������";
        //    monsterInfo[3].text = " �����뺴�� ���� ȿ�������� ���Ұ� ���ƿ�!";

        //    monster[0].sprite = monsterImage[3];
        //    monster[1].sprite = monsterImage[7];
        //    monster[2].sprite = monsterImage[11];
        //    monster[3].sprite = monsterImage[0];

        //    stageNum = id;
        //}

        //if (id == 5)
        //{
        //    mapName.text = "< ������ �� >";
        //    mapInfo.text = " ������ ������ ������ �������� �Ͼ� �����?";
        //    rewardInfo.text = " < ��ȭ�� �ü� >\n\n 1�� �� 2�� �������� �ִ� ��ȭ���� ���ϴ�.\n ���׷��̵� �� 1�� �� 3�� �������� �ִ� ��ȭ���� ���ϴ�.";
        //    score.text = "" + stageScore[5];
        //    map.sprite = mapImage[5];
        //    reward.sprite = rewardImage[5];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " ��ũ�ü�(Lv.2)-ü����";
        //    monsterInfo[1].text = " ��ũ������(Lv.3)-ü����";
        //    monsterInfo[2].text = " ��Ƽ(Lv.2)-���������";
        //    monsterInfo[3].text = " ���̵�(Lv.3)-���������";

        //    monster[0].sprite = monsterImage[5];
        //    monster[1].sprite = monsterImage[9];
        //    monster[2].sprite = monsterImage[7];
        //    monster[3].sprite = monsterImage[11];

        //    stageNum = id;
        //}

        //if (id == 6)
        //{
        //    mapName.text = "< ���� ���� >";
        //    mapInfo.text = " ��ġ ������ �ɸ� �� ���� ������ ����";
        //    rewardInfo.text = " < ������ >\n\n 50% Ȯ���� 0.5�ʰ� ������Ű�� �б⸦ ��ô �մϴ�.\n ���׷��̵� �� 70% Ȯ���� 1�ʰ� ������Ű�� �б⸦ ��ô �մϴ�.";
        //    score.text = "" + stageScore[6];
        //    map.sprite = mapImage[6];
        //    reward.sprite = rewardImage[6];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " ��ũ�ü�(Lv.2)-ü����";
        //    monsterInfo[1].text = " ��ũ������(Lv.3)-ü����";
        //    monsterInfo[2].text = " �ذ�����(Lv.2)-�����";
        //    monsterInfo[3].text = " ����Ų���(Lv.3)-�����";

        //    monster[0].sprite = monsterImage[5];
        //    monster[1].sprite = monsterImage[9];
        //    monster[2].sprite = monsterImage[6];
        //    monster[3].sprite = monsterImage[10];

        //    stageNum = id;
        //}
        //if (id == 7)
        //{
        //    mapName.text = "< ������ Ÿ�� >";
        //    mapInfo.text = " ������ ��ũ������ ��� �߿� �����Դϴ�";
        //    rewardInfo.text = " < ���� ���� >\n\n ���� �̵��ӵ��� 0.05 ���� ��Ű�� �������� ������ ��ô �մϴ�.(�ߺ� ����)\n ���׷��̵� �� ���� �̵��ӵ��� 0.1 ���� ��Ű�� �������� ������ ��ô �մϴ�.(�ߺ� ����)";
        //    score.text = "" + stageScore[7];
        //    map.sprite = mapImage[7];
        //    reward.sprite = rewardImage[7];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " ���װ�Ʈ(Lv.2)-���ǵ���";
        //    monsterInfo[1].text = " ���̵��Ʈ(Lv.3)-���ǵ���";
        //    monsterInfo[2].text = " �ذ�����(Lv.2)-�����";
        //    monsterInfo[3].text = " ����Ų���(Lv.3)-�����";

        //    monster[0].sprite = monsterImage[8];
        //    monster[1].sprite = monsterImage[12];
        //    monster[2].sprite = monsterImage[6];
        //    monster[3].sprite = monsterImage[10];

        //    stageNum = id;
        //}
        //if (id == 8)
        //{
        //    mapName.text = "< ����� �縷 >";
        //    mapInfo.text = " �縷 ������ ������ ������ �Դϴ�.";
        //    rewardInfo.text = " < ��� ��Ʈ >\n\n 200�� ������������ ���� �� �ִ� '�����Ʈ' �Դϴ�.\n ���� ������ 100% Ȯ���� 1�ʰ� ���Ͽ� �ɸ��ϴ�.";
        //    score.text = "" + stageScore[8];
        //    map.sprite = mapImage[8];
        //    reward.sprite = rewardImage[8];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " ��Ƽ(Lv.2)-���������";
        //    monsterInfo[1].text = " ���̵�(Lv.3)-���������";
        //    monsterInfo[2].text = " ���װ�Ʈ(Lv.2)-���ǵ���";
        //    monsterInfo[3].text = " ���̵��Ʈ(Lv.3)-���ǵ���";

        //    monster[0].sprite = monsterImage[7];
        //    monster[1].sprite = monsterImage[11];
        //    monster[2].sprite = monsterImage[8];
        //    monster[3].sprite = monsterImage[12];

        //    stageNum = id;
        //}
        //if (id == 9)
        //{
        //    mapName.text = "< ���� ������ >";
        //    mapInfo.text = " �������� ���ù����� �������� ���Ϳ�";
        //    rewardInfo.text = " < ��� ��Ʈ >\n\n 200�� ������������ ���� �� �ִ� '�����Ʈ' �Դϴ�.\n ���� ������ 100% Ȯ���� 1�ʰ� ���Ͽ� �ɸ��ϴ�.";
        //    score.text = "" + stageScore[9];
        //    map.sprite = mapImage[9];
        //    reward.sprite = rewardImage[9];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " ��Ƽ(Lv.2)-���������";
        //    monsterInfo[1].text = " ���̵�(Lv.3)-���������";
        //    monsterInfo[2].text = " ���װ�Ʈ(Lv.2)-���ǵ���";
        //    monsterInfo[3].text = " ���̵��Ʈ(Lv.3)-���ǵ���";

        //    monster[0].sprite = monsterImage[7];
        //    monster[1].sprite = monsterImage[11];
        //    monster[2].sprite = monsterImage[8];
        //    monster[3].sprite = monsterImage[12];

        //    stageNum = id;
        //}
        //if (id == 10)
        //{
        //    mapName.text = "< ��� ���� >";
        //    mapInfo.text = " ���� ����̰� �����ϴ� ���̿���";
        //    rewardInfo.text = " < ��� ��Ʈ >\n\n 200�� ������������ ���� �� �ִ� '�����Ʈ' �Դϴ�.\n ���� ������ 100% Ȯ���� 1�ʰ� ���Ͽ� �ɸ��ϴ�.";
        //    score.text = "" + stageScore[10];
        //    map.sprite = mapImage[10];
        //    reward.sprite = rewardImage[10];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " ��Ƽ(Lv.2)-���������";
        //    monsterInfo[1].text = " ���̵�(Lv.3)-���������";
        //    monsterInfo[2].text = " ���װ�Ʈ(Lv.2)-���ǵ���";
        //    monsterInfo[3].text = " ���̵��Ʈ(Lv.3)-���ǵ���";

        //    monster[0].sprite = monsterImage[7];
        //    monster[1].sprite = monsterImage[11];
        //    monster[2].sprite = monsterImage[8];
        //    monster[3].sprite = monsterImage[12];

        //    stageNum = id;
        //}
        //if (id == 11)
        //{
        //    mapName.text = "< ����� ����(Boss) >";
        //    mapInfo.text = " �������� Ȳ�� ���ڵ带 ó�� �ؾ� �ؿ�.";
        //    rewardInfo.text = " < ��� ��Ʈ >\n\n 200�� ������������ ���� �� �ִ� '�����Ʈ' �Դϴ�.\n ���� ������ 100% Ȯ���� 1�ʰ� ���Ͽ� �ɸ��ϴ�.";
        //    score.text = "" + stageScore[11];
        //    map.sprite = mapImage[11];
        //    reward.sprite = rewardImage[11];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " ��Ƽ(Lv.2)-���������";
        //    monsterInfo[1].text = " ���̵�(Lv.3)-���������";
        //    monsterInfo[2].text = " ���װ�Ʈ(Lv.2)-���ǵ���";
        //    monsterInfo[3].text = " ���̵��Ʈ(Lv.3)-���ǵ���";

        //    monster[0].sprite = monsterImage[7];
        //    monster[1].sprite = monsterImage[11];
        //    monster[2].sprite = monsterImage[8];
        //    monster[3].sprite = monsterImage[12];

        //    stageNum = id;
        //}
        //if (id == 12)
        //{
        //    mapName.text = "< �������� 12 >";
        //    mapInfo.text = " ��ġ ������ �ɸ� �� ���� ������ ����";
        //    rewardInfo.text = " < ��� ��Ʈ >\n\n 200�� ������������ ���� �� �ִ� '�����Ʈ' �Դϴ�.\n ���� ������ 100% Ȯ���� 1�ʰ� ���Ͽ� �ɸ��ϴ�.";
        //    score.text = "" + stageScore[12];
        //    map.sprite = mapImage[12];
        //    reward.sprite = rewardImage[12];

        //    // �ֿ� ���� ���� ���� �ҷ�����
        //    monsterInfo[0].text = " ��Ƽ(Lv.2)-���������";
        //    monsterInfo[1].text = " ���̵�(Lv.3)-���������";
        //    monsterInfo[2].text = " ���װ�Ʈ(Lv.2)-���ǵ���";
        //    monsterInfo[3].text = " ���̵��Ʈ(Lv.3)-���ǵ���";

        //    monster[0].sprite = monsterImage[7];
        //    monster[1].sprite = monsterImage[11];
        //    monster[2].sprite = monsterImage[8];
        //    monster[3].sprite = monsterImage[12];

        //    stageNum = id;
        //}
    }
    //public void UpdateTowerData00(int id)
    //{
    //    if (id == 0)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "�ü�";
    //        textTowerInfo.text = "��Ÿ��� ��� ���ݼӵ��� �����ϴ�.";

    //        //textDamage.text = " ���ݷ� : 11";
    //        //textRate.text = "���ݼӵ� : 0.8��";
    //        //textRange.text = " ��Ÿ� : 3.5";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 1)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "�˻�";
    //        textTowerInfo.text = "���ݷ°� ���ݼӵ��� ������ ��Ÿ��� ª���ϴ�.";

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
    //        textTowerInfo.text = "1�ʴ� 5�� �������� 5�ʰ� �ִ� ��ȭ���� �߻� �մϴ�.";

    //        //textDamage.text = " ���ݷ� : 12";
    //        //textRate.text = "���ݼӵ� : 1��";
    //        //textRange.text = " ��Ÿ� : 3";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 5)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "�ٶ��� ���";
    //        textTowerInfo.text = "�ٶ��� ��ų�� ���� ��� �Դϴ�.";

    //        //textDamage.text = " ���ݷ� : 20";
    //        //textRate.text = "���ݼӵ� : 1.5��";
    //        //textRange.text = " ��Ÿ� : 2";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 6)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "�� ��ź��";
    //        textTowerInfo.text = "20% Ȯ���� �̵��ӵ��� 0.3 ���� ��Ű�� �������� �߻� �մϴ�.";

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
    //        textTowerInfo.text = "�ʴ� 2�� �������� �ִ� ��ȭ���� ��� �մϴ�.";

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
    //        textTowerInfo.text = "���� �̵��ӵ��� 0.05�� ���� ��ŵ�ϴ�.(�ߺ� ����)";

    //        //textDamage.text = " ���ݷ� : 20";
    //        //textRate.text = "���ݼӵ� : 2.2��";
    //        //textRange.text = " ��Ÿ� : 2.5";
    //        //textTargetRange.text = "���� : 1.5x1.5";
    //    }
    //    if (id == 11)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "���� ������";
    //        textTowerInfo.text = "�ֺ� 3���� ���Ϳ��� 7�������� ƨ��� ���������� ��� �մϴ�.";

    //        //textDamage.text = " �������ݷ� : 30";
    //        //textRate.text = "���ݼӵ� : 1.6��";
    //        //textRange.text = " ��Ÿ� : 2.5";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 12)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "�� �ü�";
    //        textTowerInfo.text = "��븦 �󸮴� ����ȭ��� ������ ������ ���ϴ�.";

    //        //textDamage.text = " ���ݷ� : 12";
    //        //textRate.text = "���ݼӵ� : 1��";
    //        //textRange.text = " ��Ÿ� : 3";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //    if (id == 13)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "��â��";
    //        textTowerInfo.text = "�ʴ� ���� �ִ�ü���� 8%�� �������� �ִ� ���� �����ϴ�.";

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
    //        textTowerInfo.text = "���� ������ 50% ��� ������ ��� �մϴ�.";

    //        //textDamage.text = " �������ݷ� : 20";
    //        //textRate.text = "���ݼӵ� : 1.7��";
    //        //textRange.text = " ��Ÿ� : 2.5";
    //        //textTargetRange.text = "���� : ����";
    //    }
    //}
    void Update()
    {
        textPlayerDiamond.text = diamond.ToString();
    }
    public void StageStarter()
    {
        //if (1 > star)
        //{
        //    Debug.Log("���� �����մϴ�");
        //    return;
        //}
        //PlayerPrefs.SetInt("PlayerStar", star - 1);
        //star = PlayerPrefs.GetInt("PlayerStar");
        LodingSceneController.LoadScene("Stage" + stageNum);
    }
    public void TutorialStarter()
    {
        LodingSceneController.LoadScene("Stage00");
    }
    // �������� ����â ����
    public void StageInfoOff()
    {
        stagePanel.SetActive(false);
    }
   
}
