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
    private TextMeshProUGUI textPlayerStar; // Text - TextMeshPro UI [플레이어의 생명력]
    [SerializeField]
    private TextMeshProUGUI textPlayerDiamond; // Text - TextMeshPro UI [플레이어의 다이아몬드]

    public int starSum; //별의 총합
    public int diamond; //다이아몬드

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
        // 맵01 열대우림
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
        // 맵02 폐허가 된 초원
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
        // 맵03 끈적이는 연못
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
        // 맵04 맹독의 숲
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
        // 맵05 어둠과 마법의돌
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
        // 맵06 마법지대
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
        // 맵07 설원의 타워
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
        // 맵08 사막의 오아시스
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
        // 맵09 진흙 구덩이
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
        // 맵10 용암지대
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
        
        // 맵11 암흑의 제왕
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

        if (stage[0] == 1) //튜토리얼 클리어시 숲지역, 늪지역 오픈
        {
            stageButton[0].SetActive(true);
            stageButton[1].SetActive(true);

            stageButton[2].SetActive(true);
            stageButton[3].SetActive(true);
            stageButton[4].SetActive(true);
        }

        if (stage[1] == 1 && stage[2] == 1) //숲지역 올 클리어시 설원지역 오픈
        {
            stageButton[5].SetActive(true);
            stageButton[6].SetActive(true);
        }

        if (stage[3] == 1 && stage[4] == 1 && stage[5] == 1) //늪지역 올 클리어시 사막지역 오픈
        {
            stageButton[7].SetActive(true);
            stageButton[8].SetActive(true);
        }

        if (stage[6] == 1 && stage[7] == 1 && stage[8] == 1 && stage[9] == 1) //설원지역, 사막지역 올 클리어시 용암지대 맵 오픈
        {
            stageButton[9].SetActive(true);
        }
        if (stage[10] == 1 ) //용암지대 맵 클리어시 보스맵 오픈
        {
            stageButton[10].SetActive(true);
        }
    }
    // 스테이지 정보창 켜기
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
        //    rewardInfo.text = " 튜토리얼 스테이지는 보상이 없습니다";
        //    score.text = "" + stageScore[0];
        //    map.sprite = mapImage[0];
        //    reward.sprite = rewardImage[0];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 오크창병(Lv.1)-체력형";
        //    monsterInfo[1].text = " 아기해골(Lv.1)-방어형";
        //    monsterInfo[2].text = " 고스트(Lv.1)-스피드형";
        //    monsterInfo[3].text = " 아기이티(Lv.1)-마법방어형";

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
        //    rewardInfo.text = " < 불화살 궁수 >\n\n 1초 당 5에 데미지를 5번 주는 불화살을 쏩니다.\n 업그레이드 시 1초 당 10에 데미지를 5번 주는 불화살을 쏩니다.";
        //    score.text = "" + stageScore[1];
        //    map.sprite = mapImage[1];
        //    reward.sprite = rewardImage[1];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 아기해골(Lv.1)-방어형";
        //    monsterInfo[1].text = " 해골전사(Lv.2)-방어형";
        //    monsterInfo[2].text = " 펌프킨헤드왕(Lv.3)-방어형";
        //    monsterInfo[3].text = " 마법용병과 일반용병 둘다 좋아 보이는군요!";

        //    monster[0].sprite = monsterImage[2];
        //    monster[1].sprite = monsterImage[6];
        //    monster[2].sprite = monsterImage[10];
        //    monster[3].sprite = monsterImage[0];

        //    stageNum = id;
        //}

        //if (id == 2)
        //{
        //    mapName.text = "< 폐허가 된 초원 >";
        //    mapInfo.text = " 몬스터들이 한 차례 휩쓸고 가버린 폐허가 된 초원에 어느 마을 같군요.";

        //    rewardInfo.text = " < 도끼 여병 >\n\n 적의 방어력이 2깍이는 도끼를 투척 합니다.(중복 가능)\n 업그레이드 시 적의 방어력이 3깍이는 도끼를 투척 합니다.(중복 가능)";
        //    score.text = "" + stageScore[2];
        //    map.sprite = mapImage[2];
        //    reward.sprite = rewardImage[2];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 고스트(Lv.1)-스피드형";
        //    monsterInfo[1].text = " 에그고스트(Lv.2)-스피드형";
        //    monsterInfo[2].text = " 레이디고스트(Lv.3)-스피드형";
        //    monsterInfo[3].text = " 골고루 용병들을 활용해 보아요!";

        //    monster[0].sprite = monsterImage[4];
        //    monster[1].sprite = monsterImage[8];
        //    monster[2].sprite = monsterImage[12];
        //    monster[3].sprite = monsterImage[0];

        //    stageNum = id;
        //}
        //if (id == 3)
        //{
        //    mapName.text = "< 끈적이는 연못 >";
        //    mapInfo.text = " 끈끈한 액체가 뭔가 불길한 느낌입니다";

        //    rewardInfo.text = " < 독화살 궁수 >\n\n 1초 당 2에 데미지를 주는 독화살을 쏩니다.\n 업그레이드 시 1초 당 3에 데미지를 주는 독화살을 쏩니다.";
        //    //mapName.text = "< 투쟁의 평원 >";
        //    //mapInfo.text = " 사막 같지만 조용한 전쟁터 입니다";
        //    //rewardInfo.text = " < 끈끈이 포탄병 >\n\n 20% 확률로 적의 이동속도를0.3 감소 시키는 광범위 끈끈이포탄을 쏩니다.(중복 불가)\n 업그레이드 시 30% 확률로 적의 이동속도를0.4 감소 시키는 광범위 끈끈이포탄을 쏩니다.(중복 불가)";
        //    score.text = "" + stageScore[3];
        //    map.sprite = mapImage[3];
        //    reward.sprite = rewardImage[3];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 오크창병(Lv.1)-체력형";
        //    monsterInfo[1].text = " 오크궁수(Lv.2)-체력형";
        //    monsterInfo[2].text = " 오크도끼병(Lv.3)-체력형";
        //    monsterInfo[3].text = " 궁병들을 잘 배치하면 좋을 거 같아요!";

        //    monster[0].sprite = monsterImage[1];
        //    monster[1].sprite = monsterImage[5];
        //    monster[2].sprite = monsterImage[9];
        //    monster[3].sprite = monsterImage[0];

        //    stageNum = id;
        //}
        //if (id == 4)
        //{
        //    mapName.text = "< 맹독의 숲 >";
        //    mapInfo.text = " 맹독에 빠지면 해골이 됩니다";
        //    rewardInfo.text = " < 독화살 궁수 >\n\n 1초 당 2에 데미지를 주는 독화살을 쏩니다.\n 업그레이드 시 1초 당 3에 데미지를 주는 독화살을 쏩니다.";
        //    //mapName.text = "< 설원의 타워 >";
        //    //mapInfo.text = " 마법사 아크쉐인이 사는 추운 지방입니다";
        //    //rewardInfo.text = " < 얼음 마법사 >\n\n 30% 확률로 3초동안 적을 얼리는 얼음 마법을 사용 합니다.\n 업그레이드 시 40% 확률로 5초동안 적을 얼리는 얼음 마법을 사용 합니다.";
        //    score.text = "" + stageScore[4];
        //    map.sprite = mapImage[4];
        //    reward.sprite = rewardImage[4];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 아기이티(Lv.1)-마법방어형";
        //    monsterInfo[1].text = " 이티(Lv.2)-마법방어형";
        //    monsterInfo[2].text = " 메이드(Lv.3)-마법방어형";
        //    monsterInfo[3].text = " 마법용병은 별로 효율적이지 못할거 같아요!";

        //    monster[0].sprite = monsterImage[3];
        //    monster[1].sprite = monsterImage[7];
        //    monster[2].sprite = monsterImage[11];
        //    monster[3].sprite = monsterImage[0];

        //    stageNum = id;
        //}

        //if (id == 5)
        //{
        //    mapName.text = "< 마법의 돌 >";
        //    mapInfo.text = " 마법의 돌들을 모으면 무슨일이 일어 날까요?";
        //    rewardInfo.text = " < 독화살 궁수 >\n\n 1초 당 2에 데미지를 주는 독화살을 쏩니다.\n 업그레이드 시 1초 당 3에 데미지를 주는 독화살을 쏩니다.";
        //    score.text = "" + stageScore[5];
        //    map.sprite = mapImage[5];
        //    reward.sprite = rewardImage[5];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 오크궁수(Lv.2)-체력형";
        //    monsterInfo[1].text = " 오크도끼병(Lv.3)-체력형";
        //    monsterInfo[2].text = " 이티(Lv.2)-마법방어형";
        //    monsterInfo[3].text = " 메이드(Lv.3)-마법방어형";

        //    monster[0].sprite = monsterImage[5];
        //    monster[1].sprite = monsterImage[9];
        //    monster[2].sprite = monsterImage[7];
        //    monster[3].sprite = monsterImage[11];

        //    stageNum = id;
        //}

        //if (id == 6)
        //{
        //    mapName.text = "< 마법 지대 >";
        //    mapInfo.text = " 마치 마법에 걸릴 것 같은 분위기 군요";
        //    rewardInfo.text = " < 성직자 >\n\n 50% 확률로 0.5초간 기절시키는 둔기를 투척 합니다.\n 업그레이드 시 70% 확률로 1초간 기절시키는 둔기를 투척 합니다.";
        //    score.text = "" + stageScore[6];
        //    map.sprite = mapImage[6];
        //    reward.sprite = rewardImage[6];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 오크궁수(Lv.2)-체력형";
        //    monsterInfo[1].text = " 오크도끼병(Lv.3)-체력형";
        //    monsterInfo[2].text = " 해골전사(Lv.2)-방어형";
        //    monsterInfo[3].text = " 펌프킨헤드(Lv.3)-방어형";

        //    monster[0].sprite = monsterImage[5];
        //    monster[1].sprite = monsterImage[9];
        //    monster[2].sprite = monsterImage[6];
        //    monster[3].sprite = monsterImage[10];

        //    stageNum = id;
        //}
        //if (id == 7)
        //{
        //    mapName.text = "< 설원의 타워 >";
        //    mapInfo.text = " 마법사 아크쉐인이 사는 추운 지방입니다";
        //    rewardInfo.text = " < 진흙 포병 >\n\n 적의 이동속도를 0.05 감소 시키는 광범위한 진흙을 투척 합니다.(중복 가능)\n 업그레이드 시 적의 이동속도를 0.1 감소 시키는 광범위한 진흙을 투척 합니다.(중복 가능)";
        //    score.text = "" + stageScore[7];
        //    map.sprite = mapImage[7];
        //    reward.sprite = rewardImage[7];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 에그고스트(Lv.2)-스피드형";
        //    monsterInfo[1].text = " 레이디고스트(Lv.3)-스피드형";
        //    monsterInfo[2].text = " 해골전사(Lv.2)-방어형";
        //    monsterInfo[3].text = " 펌프킨헤드(Lv.3)-방어형";

        //    monster[0].sprite = monsterImage[8];
        //    monster[1].sprite = monsterImage[12];
        //    monster[2].sprite = monsterImage[6];
        //    monster[3].sprite = monsterImage[10];

        //    stageNum = id;
        //}
        //if (id == 8)
        //{
        //    mapName.text = "< 고요한 사막 >";
        //    mapInfo.text = " 사막 같지만 조용한 전쟁터 입니다.";
        //    rewardInfo.text = " < 썬더 볼트 >\n\n 200에 마법데미지를 입힐 수 있는 '썬더볼트' 입니다.\n 맞은 적들은 100% 확률로 1초간 스턴에 걸립니다.";
        //    score.text = "" + stageScore[8];
        //    map.sprite = mapImage[8];
        //    reward.sprite = rewardImage[8];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 이티(Lv.2)-마법방어형";
        //    monsterInfo[1].text = " 메이드(Lv.3)-마법방어형";
        //    monsterInfo[2].text = " 에그고스트(Lv.2)-스피드형";
        //    monsterInfo[3].text = " 레이디고스트(Lv.3)-스피드형";

        //    monster[0].sprite = monsterImage[7];
        //    monster[1].sprite = monsterImage[11];
        //    monster[2].sprite = monsterImage[8];
        //    monster[3].sprite = monsterImage[12];

        //    stageNum = id;
        //}
        //if (id == 9)
        //{
        //    mapName.text = "< 진흙 구덩이 >";
        //    mapInfo.text = " 동굴에선 무시무시한 괴물들이 나와요";
        //    rewardInfo.text = " < 썬더 볼트 >\n\n 200에 마법데미지를 입힐 수 있는 '썬더볼트' 입니다.\n 맞은 적들은 100% 확률로 1초간 스턴에 걸립니다.";
        //    score.text = "" + stageScore[9];
        //    map.sprite = mapImage[9];
        //    reward.sprite = rewardImage[9];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 이티(Lv.2)-마법방어형";
        //    monsterInfo[1].text = " 메이드(Lv.3)-마법방어형";
        //    monsterInfo[2].text = " 에그고스트(Lv.2)-스피드형";
        //    monsterInfo[3].text = " 레이디고스트(Lv.3)-스피드형";

        //    monster[0].sprite = monsterImage[7];
        //    monster[1].sprite = monsterImage[11];
        //    monster[2].sprite = monsterImage[8];
        //    monster[3].sprite = monsterImage[12];

        //    stageNum = id;
        //}
        //if (id == 10)
        //{
        //    mapName.text = "< 용암 지대 >";
        //    mapInfo.text = " 마왕 고양이가 지배하는 땅이예요";
        //    rewardInfo.text = " < 썬더 볼트 >\n\n 200에 마법데미지를 입힐 수 있는 '썬더볼트' 입니다.\n 맞은 적들은 100% 확률로 1초간 스턴에 걸립니다.";
        //    score.text = "" + stageScore[10];
        //    map.sprite = mapImage[10];
        //    reward.sprite = rewardImage[10];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 이티(Lv.2)-마법방어형";
        //    monsterInfo[1].text = " 메이드(Lv.3)-마법방어형";
        //    monsterInfo[2].text = " 에그고스트(Lv.2)-스피드형";
        //    monsterInfo[3].text = " 레이디고스트(Lv.3)-스피드형";

        //    monster[0].sprite = monsterImage[7];
        //    monster[1].sprite = monsterImage[11];
        //    monster[2].sprite = monsterImage[8];
        //    monster[3].sprite = monsterImage[12];

        //    stageNum = id;
        //}
        //if (id == 11)
        //{
        //    mapName.text = "< 어둠의 성소(Boss) >";
        //    mapInfo.text = " 최종보스 황제 리자드를 처단 해야 해요.";
        //    rewardInfo.text = " < 썬더 볼트 >\n\n 200에 마법데미지를 입힐 수 있는 '썬더볼트' 입니다.\n 맞은 적들은 100% 확률로 1초간 스턴에 걸립니다.";
        //    score.text = "" + stageScore[11];
        //    map.sprite = mapImage[11];
        //    reward.sprite = rewardImage[11];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 이티(Lv.2)-마법방어형";
        //    monsterInfo[1].text = " 메이드(Lv.3)-마법방어형";
        //    monsterInfo[2].text = " 에그고스트(Lv.2)-스피드형";
        //    monsterInfo[3].text = " 레이디고스트(Lv.3)-스피드형";

        //    monster[0].sprite = monsterImage[7];
        //    monster[1].sprite = monsterImage[11];
        //    monster[2].sprite = monsterImage[8];
        //    monster[3].sprite = monsterImage[12];

        //    stageNum = id;
        //}
        //if (id == 12)
        //{
        //    mapName.text = "< 스테이지 12 >";
        //    mapInfo.text = " 마치 마법에 걸릴 것 같은 분위기 군요";
        //    rewardInfo.text = " < 썬더 볼트 >\n\n 200에 마법데미지를 입힐 수 있는 '썬더볼트' 입니다.\n 맞은 적들은 100% 확률로 1초간 스턴에 걸립니다.";
        //    score.text = "" + stageScore[12];
        //    map.sprite = mapImage[12];
        //    reward.sprite = rewardImage[12];

        //    // 주요 등장 몬스터 정보 불러오기
        //    monsterInfo[0].text = " 이티(Lv.2)-마법방어형";
        //    monsterInfo[1].text = " 메이드(Lv.3)-마법방어형";
        //    monsterInfo[2].text = " 에그고스트(Lv.2)-스피드형";
        //    monsterInfo[3].text = " 레이디고스트(Lv.3)-스피드형";

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
    //        textTowerName.text = "궁수";
    //        textTowerInfo.text = "사거리가 길며 공격속도가 빠릅니다.";

    //        //textDamage.text = " 공격력 : 11";
    //        //textRate.text = "공격속도 : 0.8초";
    //        //textRange.text = " 사거리 : 3.5";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 1)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "검사";
    //        textTowerInfo.text = "공격력과 공격속도가 빠르나 사거리가 짧습니다.";

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
    //        textTowerInfo.text = "1초당 5에 데미지를 5초간 주는 불화살을 발사 합니다.";

    //        //textDamage.text = " 공격력 : 12";
    //        //textRate.text = "공격속도 : 1초";
    //        //textRange.text = " 사거리 : 3";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 5)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "바람의 기사";
    //        textTowerInfo.text = "바람의 스킬은 쓰는 기사 입니다.";

    //        //textDamage.text = " 공격력 : 20";
    //        //textRate.text = "공격속도 : 1.5초";
    //        //textRange.text = " 사거리 : 2";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 6)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "물 폭탄병";
    //        textTowerInfo.text = "20% 확률로 이동속도를 0.3 감소 시키는 접착제를 발사 합니다.";

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
    //        textTowerInfo.text = "초당 2의 데미지를 주는 독화살을 사용 합니다.";

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
    //        textTowerInfo.text = "적의 이동속도를 0.05를 감소 시킵니다.(중복 가능)";

    //        //textDamage.text = " 공격력 : 20";
    //        //textRate.text = "공격속도 : 2.2초";
    //        //textRange.text = " 사거리 : 2.5";
    //        //textTargetRange.text = "범위 : 1.5x1.5";
    //    }
    //    if (id == 11)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "번개 마법사";
    //        textTowerInfo.text = "주변 3명의 몬스터에게 7데미지의 튕기는 번개마법을 사용 합니다.";

    //        //textDamage.text = " 마법공격력 : 30";
    //        //textRate.text = "공격속도 : 1.6초";
    //        //textRange.text = " 사거리 : 2.5";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 12)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "석 궁수";
    //        textTowerInfo.text = "상대를 얼리는 얼음화살과 강력한 석궁을 쏩니다.";

    //        //textDamage.text = " 공격력 : 12";
    //        //textRate.text = "공격속도 : 1초";
    //        //textRange.text = " 사거리 : 3";
    //        //textTargetRange.text = "범위 : 단일";
    //    }
    //    if (id == 13)
    //    {
    //        imageTower.sprite = towerImage[id];
    //        textTowerName.text = "독창병";
    //        textTowerInfo.text = "초당 적의 최대체력의 8%의 데미지를 주는 독을 입힙니다.";

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
    //        textTowerInfo.text = "마법 방어력을 50% 깍는 마법을 사용 합니다.";

    //        //textDamage.text = " 마법공격력 : 20";
    //        //textRate.text = "공격속도 : 1.7초";
    //        //textRange.text = " 사거리 : 2.5";
    //        //textTargetRange.text = "범위 : 단일";
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
        //    Debug.Log("별이 부족합니다");
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
    // 스테이지 정보창 끄기
    public void StageInfoOff()
    {
        stagePanel.SetActive(false);
    }
   
}
