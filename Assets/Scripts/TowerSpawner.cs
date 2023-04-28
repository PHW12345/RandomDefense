using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate[] towerTemplate; // 타워 정보 (공격력, 공격속도 등)
    
    //[SerializeField]
    //private int towerBuildGold = 50; // 타워 건설에 사용되는 골드
    [SerializeField]
    private EnemySpawner enemySpawner; // 현재 맵에 존재하는 적 리스트 정보를 얻기 위해..
    [SerializeField]
    private PlayerGold playerGold; // 타워 건설 시 골드 감소를 위해..

    private int[] towerCount = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

    [SerializeField]
    private TextMeshProUGUI[] towerCountText;

    public GameObject[] towers;
    public GameObject[] spawnEffect;

    [SerializeField]
    private TextMeshProUGUI[] towerListText;
    public int[] towerList = { 0, 0, 0, 0, 0 };

    public SoundManager theSound;

    public GameObject alramText;
    public GameObject alramText1;

    public Transform canvasTransform; // UI를 표현하는 Canvas 오브젝트의 Transform
    void Start()
    {
        // 보유한 타워갯수 불러와서 스테이지 시작시 활성화
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
    }
    private void Update()
    {
        for(int i = 0; i < towerCount.Length; i++)
        {
            if (towerCount[i] == 0)
            {
                towers[i].SetActive(false);
            }
        }
        for (int i = 0; i < towerCount.Length; i++)
        {
            towerCountText[i].text = "" + towerCount[i];
        }
        for (int i = 0; i < towerList.Length; i++)
        {
            towerListText[i].text = towerList[i].ToString();
        }

    }
    public void SoundStart(int selectNum2)
    {
        if (selectNum2 == 0)
        {
            theSound.Play("nomal");
        }
        else if (selectNum2 == 1)
        {
            theSound.Play("high");
        }
        else if (selectNum2 == 2)
        {
            theSound.Play("hero");
        }
        else if (selectNum2 == 3 || selectNum2 == 4)
        {
            theSound.Play("legend");
        }
    }
    public void SpawnTower(Transform tileTransform, int id)
    {
        // 타워 건설 가능 여부 확인
        // 1. 타워를 건설할 만큼 돈이 없으면 타워 건설 x
        if (25 > playerGold.CurrentGold)
        {
            return;
        }
        Debug.Log("소환된 용병 id = " + id);

        Tile tile = tileTransform.GetComponent<Tile>();

        // 2. 현재 타일의 위치에 이미 타워가 건설되어 있으면 타워 건설 x
        if (tile.IsBuildTower == true)
        {
            return;
        }

        // 타워가 건설되어 있음으로 설정
        tile.IsBuildTower = true;

        // 타워 건설에 필요한 골드만큼 감소
        playerGold.CurrentGold -= 25;

        // 선택한 타일의 위치에 타워 건설 (타일보다 z축 -1의 위치에 배치)
        Vector3 position = tileTransform.position + Vector3.back;

        //GameObject clone = Instantiate(towerPrefab, position, Quaternion.identity);
        //int selectNum = 0;
        //StartCoroutine("Spawn", position);
        int selectNum = 0; // 랜덤으로 돌려서 고른번호
        int choiceNum = 0; // 고른번호에 따라 선택된 용병등급 번호

        //selectNum = Mathf.RoundToInt(3 * Random.Range(0.0f, 1.0f));
        selectNum = Mathf.RoundToInt(1000 * Random.Range(0.0f, 1.0f));
        if (selectNum <= 700) //70퍼 50퍼
        {
            choiceNum = 0;
        }
        else if (selectNum <= 900) //20퍼 30퍼
        {
            choiceNum = 1;
        }
        else if (selectNum <= 975) //7.5퍼 15퍼
        {
            choiceNum = 2;
        }
        else if (selectNum <= 995) //2퍼 4퍼
        {
            choiceNum = 3;
        }
        else //0.5퍼 1퍼
            choiceNum = 4;


        Debug.Log("뽑힌 숫자" + selectNum);

        Debug.Log("뽑힌 번호" + choiceNum);

        GameObject clone = Instantiate(towerTemplate[id].towerPrefab[choiceNum], position + Vector3.up, Quaternion.identity);
        
        
        GameObject clone1 = Instantiate(spawnEffect[choiceNum], position + Vector3.up * 0.2f, Quaternion.identity);

        //알람 생성
        Vector3 position2 = canvasTransform.position;
        GameObject clone2 = Instantiate(alramText1, position2, Quaternion.identity); // 알림텍스트 출력
        Locale currentLocale = LocalizationSettings.SelectedLocale;

        if (choiceNum == 0) // 회색
        {
            clone2.GetComponent<Item>().textui1.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", id.ToString(), currentLocale) + "<color=#828282>" + " [" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Normal", currentLocale) + "] " + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasHired", currentLocale) + towerTemplate[id].weapon[choiceNum].persent + "% ";
        }
        else if(choiceNum == 1) // 파랑
        {
            clone2.GetComponent<Item>().textui1.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", id.ToString(), currentLocale) + "<color=#0082FF>" + " [" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "High", currentLocale) + "] " + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasHired", currentLocale) + towerTemplate[id].weapon[choiceNum].persent + " % ";
        }
        else if (choiceNum == 2) // 보라
        {
            clone2.GetComponent<Item>().textui1.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", id.ToString(), currentLocale) + "<color=#DB00FF>" + " [" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Hero", currentLocale) + "] " + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasHired", currentLocale) + towerTemplate[id].weapon[choiceNum].persent + "% ";
        }
        else if (choiceNum == 3) // 빨강
        {
            clone2.GetComponent<Item>().textui1.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", id.ToString(), currentLocale) + "<color=#ff0000>" + " [" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Legend", currentLocale) + "] " + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasHired", currentLocale) + towerTemplate[id].weapon[choiceNum].persent + "% ";
        }
        else if (choiceNum == 4) // 노랑
        {
            clone2.GetComponent<Item>().textui1.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", id.ToString(), currentLocale) + "<color=#FFE000>" + " [" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Myth", currentLocale) + "] " + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasHired", currentLocale) + towerTemplate[id].weapon[choiceNum].persent + "% ";
        }
        clone2.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
        clone2.transform.SetParent(canvasTransform);

        SoundStart(choiceNum); // 뽑힌 용병에 맞는 사운드 재생
        towerCount[id]--;
        towerList[choiceNum]++;

        //towerCountText[id].text = "" + towerCount[id];
        // 타워 무기에 enemySpawner 정보 전달
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner, playerGold, tile);

    }
    private IEnumerator Spawn(Vector3 position)
    {
        GameObject clone1 = Instantiate(spawnEffect[4], position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
    }
}
