using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField]
    //private GameObject enemyPrefab; // 적 프리팹
    [SerializeField]
    private GameObject enemyHPSliderPrefab; // 적 체력을 나타내는 Slider UI 프리팹
    [SerializeField]
    private Transform canvasTransform; // UI를 표현하는 Canvas 오브젝트의 Transform

    [SerializeField]
    private Transform alramTextTransform;
    //[SerializeField]
    //private float spawnTime; // 적 생성 주기

    [SerializeField]
    private Transform[] wayPoints; // 현재 스테이지의 이동 경로
    [SerializeField]
    private Transform[] wayPoints2; // 현재 스테이지의 이동 경로
    [SerializeField]
    private Transform[] wayPoints3; // 현재 스테이지의 이동 경로

    [SerializeField]
    private PlayerHP playerHP; // 플레이어의 체력 컴포넌트
    [SerializeField]
    private PlayerGold playerGold; // 플레이어의 골드 컴포넌트
    [SerializeField]
    private ObjectDetector objectDetector; // 

    [SerializeField]
    private GameObject[] ballPrefab; // 볼 프리팹

    private Wave currentWave; // 현재 웨이브 정보
    private Wave currentWave2; // 웨이브 정보
    private Wave currentWave3; // 웨이브 정보

    public bool wavedone;
    public bool wavedone2;
    public bool wavedone3;

    private List<Enemy> enemyList; // 현재 맵에 존재하는 모든 적의 정보

    // 적의 생성과 삭제는 EnemySpawner에서 하기 때문에 Set은 필요 없다.
    public List<Enemy> EnemyList => enemyList;
    [SerializeField]
    private GameObject coinPrefab; // 코인 프리팹
    public GameObject alramText;

    private void Awake()
    {
        // 적 리스트 메모리 할당
        enemyList = new List<Enemy>();
        // 적 생성 코루틴 함수 호출
        //StartCoroutine("SpawnEnemy");
    }

    public void StartWave(Wave wave)
    {
        // 매개변수로 받아온 웨이브 정보 저장
        currentWave = wave;
        // 현재 웨이브 시작
        StartCoroutine("SpawnEnemy");
    }
    public void StartWave2(Wave wave)
    {
        // 매개변수로 받아온 웨이브 정보 저장
        currentWave2 = wave;
        // 현재 웨이브 시작
        StartCoroutine("SpawnEnemy2");
    }
    public void StartWave3(Wave wave)
    {
        // 매개변수로 받아온 웨이브 정보 저장
        currentWave3 = wave;
        // 현재 웨이브 시작
        StartCoroutine("SpawnEnemy3");
    }
    private IEnumerator SpawnEnemy()
    {
        wavedone = false;

        yield return new WaitForSeconds(2.0f);

        // 현재 웨이브에서 생성한 적 숫자
        int spawnEnemyCount = 0;

        //while (true)
        // 현재 웨이브에서 생성되어야 하는 적의 숫자만큼 적을 생성하고 코루틴 종료
        while(spawnEnemyCount < currentWave.maxEnemyCount)
        {
            //GameObject clone = Instantiate(enemyPrefab); // 적 오브젝트 생성
            // 웨이브에 등장하는 적의 종류가 여러 종류일 때 임의의 적이 등장하도록 설정하고, 적 오브젝트 생성 
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>(); // 방금 생성된 적의 Enemy 컴포넌트
            
            // this는 나 자신 (자신의 EnemySpawner 정보)
            enemy.Setup(this, wayPoints); // wayPoint 정보를 매개변수로 Setup() 호출
            enemyList.Add(enemy); // 리스트에 방금 생성된 적 정보 저장

            SpawnEnemyHPSlider(clone); // 적 체력을 나타내는 Slider UI 생성 및 설정

            // 현재 웨이브에서 생성한 적의 숫자 +1
            spawnEnemyCount ++;

            //yield return new WaitForSeconds(spawnTime); // spawnTime 시간 동안 대기
            // 각 웨이브마다 spawnTime이 다를 수 있기 때문에 현재 웨이브(currentWave)의 spawnTime 사용
            yield return new WaitForSeconds(currentWave.spawnTime); // spawnTime 시간 동안 대기
        }
        wavedone = true;
    }
    private IEnumerator SpawnEnemy2()
    {
        wavedone2 = false;

        yield return new WaitForSeconds(2.0f);

        // 현재 웨이브에서 생성한 적 숫자
        int spawnEnemyCount = 0;

        //while (true)
        // 현재 웨이브에서 생성되어야 하는 적의 숫자만큼 적을 생성하고 코루틴 종료
        while (spawnEnemyCount < currentWave2.maxEnemyCount)
        {
            //GameObject clone = Instantiate(enemyPrefab); // 적 오브젝트 생성
            // 웨이브에 등장하는 적의 종류가 여러 종류일 때 임의의 적이 등장하도록 설정하고, 적 오브젝트 생성 
            int enemyIndex = Random.Range(0, currentWave2.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave2.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>(); // 방금 생성된 적의 Enemy 컴포넌트

            // this는 나 자신 (자신의 EnemySpawner 정보)
            enemy.Setup(this, wayPoints2); // wayPoint 정보를 매개변수로 Setup() 호출
            enemyList.Add(enemy); // 리스트에 방금 생성된 적 정보 저장

            SpawnEnemyHPSlider(clone); // 적 체력을 나타내는 Slider UI 생성 및 설정

            // 현재 웨이브에서 생성한 적의 숫자 +1
            spawnEnemyCount++;

            //yield return new WaitForSeconds(spawnTime); // spawnTime 시간 동안 대기
            // 각 웨이브마다 spawnTime이 다를 수 있기 때문에 현재 웨이브(currentWave)의 spawnTime 사용
            yield return new WaitForSeconds(currentWave2.spawnTime); // spawnTime 시간 동안 대기
        }
        wavedone2 = true;

    }
    private IEnumerator SpawnEnemy3()
    {
        wavedone3 = false;

        yield return new WaitForSeconds(2.0f);

        // 현재 웨이브에서 생성한 적 숫자
        int spawnEnemyCount = 0;

        //while (true)
        // 현재 웨이브에서 생성되어야 하는 적의 숫자만큼 적을 생성하고 코루틴 종료
        while (spawnEnemyCount < currentWave3.maxEnemyCount)
        {
            //GameObject clone = Instantiate(enemyPrefab); // 적 오브젝트 생성
            // 웨이브에 등장하는 적의 종류가 여러 종류일 때 임의의 적이 등장하도록 설정하고, 적 오브젝트 생성 
            int enemyIndex = Random.Range(0, currentWave3.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave3.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>(); // 방금 생성된 적의 Enemy 컴포넌트

            // this는 나 자신 (자신의 EnemySpawner 정보)
            enemy.Setup(this, wayPoints3); // wayPoint 정보를 매개변수로 Setup() 호출
            enemyList.Add(enemy); // 리스트에 방금 생성된 적 정보 저장

            SpawnEnemyHPSlider(clone); // 적 체력을 나타내는 Slider UI 생성 및 설정

            // 현재 웨이브에서 생성한 적의 숫자 +1
            spawnEnemyCount++;

            //yield return new WaitForSeconds(spawnTime); // spawnTime 시간 동안 대기
            // 각 웨이브마다 spawnTime이 다를 수 있기 때문에 현재 웨이브(currentWave)의 spawnTime 사용
            yield return new WaitForSeconds(currentWave3.spawnTime); // spawnTime 시간 동안 대기
        }
        wavedone3 = true;

    }
    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold, int heart)
    {
        // 적이 목표지점까지 도착했을 때
        if(type == EnemyDestroyType.Arrive)
        {
            Handheld.Vibrate();
            // 플레이어의 체력 -1
            playerHP.TakeDamage(heart);
            
        }
        // 적이 플레이어의 발사체에게 사망했을 때
        else if (type == EnemyDestroyType.kill)
        {
            // 적의 종류에 따라 사망 시 골드 획득
            playerGold.CurrentGold += gold;
            GameObject clone = Instantiate(coinPrefab, enemy.transform.position /*+ Vector3.back*/, Quaternion.identity);

            int selectNum = 0; // 랜덤으로 돌려서 고른번호
            int choiceNum = 0; // 고른번호에 따라 선택된 용병등급 번호

            selectNum = Mathf.RoundToInt(1000 * Random.Range(0.0f, 1.0f));
            Locale currentLocale = LocalizationSettings.SelectedLocale;

            if (selectNum <= 15)
            {
                choiceNum = 0;
                GameObject clone1 = Instantiate(ballPrefab[choiceNum], enemy.transform.position, Quaternion.identity);
                objectDetector.skillCount[choiceNum]++;

                objectDetector.skillCountText[choiceNum].text = "" + objectDetector.skillCount[choiceNum];

                Vector3 position2 = alramTextTransform.position;
                GameObject clone2 = Instantiate(alramText, position2, Quaternion.identity);
                clone2.GetComponent<Item>().textui1.text = "<color=#ff0000>" + "["+ LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "FlameOrb", currentLocale) + "]" + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasObtained", currentLocale) + "<color=#ff0000>" + LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", "0", currentLocale) + "</color>" + "+1";
                clone2.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
                clone2.transform.SetParent(canvasTransform);
                SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                theSound.Play("Get");
            }
            else if (selectNum <= 18)
            {
                choiceNum = 1;
                GameObject clone1 = Instantiate(ballPrefab[choiceNum], enemy.transform.position, Quaternion.identity);
                objectDetector.skillCount[choiceNum]++;

                objectDetector.skillCountText[choiceNum].text = "" + objectDetector.skillCount[choiceNum];
                Vector3 position2 = alramTextTransform.position;
                GameObject clone2 = Instantiate(alramText, position2, Quaternion.identity);
                clone2.GetComponent<Item>().textui1.text = "<color=#F600FF>" + "[" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "LightningOrb", currentLocale) + "]" + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasObtained", currentLocale) + "<color=#F600FF>" + LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", "1", currentLocale) + "</color>" + "+1";
                clone2.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
                clone2.transform.SetParent(canvasTransform);
                SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                theSound.Play("Get");
            }
            else if (selectNum <= 21)
            {
                choiceNum = 2;
                GameObject clone1 = Instantiate(ballPrefab[choiceNum], enemy.transform.position, Quaternion.identity);
                objectDetector.skillCount[choiceNum]++;

                objectDetector.skillCountText[choiceNum].text = "" + objectDetector.skillCount[choiceNum];
                Vector3 position2 = alramTextTransform.position;
                GameObject clone2 = Instantiate(alramText, position2, Quaternion.identity);
                clone2.GetComponent<Item>().textui1.text = "<color=#853800>" + "[" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "EarthOrb", currentLocale) + "]" + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasObtained", currentLocale) + "<color=#853800>" + LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", "2", currentLocale) + "</color>" + "+1";
                clone2.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
                clone2.transform.SetParent(canvasTransform);
                SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                theSound.Play("Get");
            }
            else if (selectNum <= 24)
            {
                choiceNum = 3;
                GameObject clone1 = Instantiate(ballPrefab[choiceNum], enemy.transform.position, Quaternion.identity);
                objectDetector.skillCount[choiceNum]++;

                objectDetector.skillCountText[choiceNum].text = "" + objectDetector.skillCount[choiceNum];
                Vector3 position2 = alramTextTransform.position;
                GameObject clone2 = Instantiate(alramText, position2, Quaternion.identity);
                clone2.GetComponent<Item>().textui1.text = "<color=#009AFF>" + "[" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "IceOrb", currentLocale) + "]" + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasObtained", currentLocale) + "<color=#009AFF>" + LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", "3", currentLocale) + "</color>" + "+1";
                clone2.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
                clone2.transform.SetParent(canvasTransform);
                SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                theSound.Play("Get");
            }
            else if (selectNum <= 27)
            {
                choiceNum = 4;
                GameObject clone1 = Instantiate(ballPrefab[choiceNum], enemy.transform.position, Quaternion.identity);
                objectDetector.skillCount[choiceNum]++;

                objectDetector.skillCountText[choiceNum].text = "" + objectDetector.skillCount[choiceNum];
                Vector3 position2 = alramTextTransform.position;
                GameObject clone2 = Instantiate(alramText, position2, Quaternion.identity);
                clone2.GetComponent<Item>().textui1.text = "<color=#00AE00>" + "[" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "PoisonOrb", currentLocale) + "]" + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasObtained", currentLocale) + "<color=#00AE00>" + LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", "4", currentLocale) + "</color>" + "+1";
                clone2.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
                clone2.transform.SetParent(canvasTransform);
                SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                theSound.Play("Get");
            }
            else if (selectNum <= 28)
            {
                choiceNum = 5;
                GameObject clone1 = Instantiate(ballPrefab[choiceNum], enemy.transform.position, Quaternion.identity);
                objectDetector.skillCount[choiceNum]++;

                objectDetector.skillCountText[choiceNum].text = "" + objectDetector.skillCount[choiceNum];
                Vector3 position2 = alramTextTransform.position;
                GameObject clone2 = Instantiate(alramText, position2, Quaternion.identity);
                clone2.GetComponent<Item>().textui1.text = "<color=#FFD600>" + "[" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "FalconOrb", currentLocale) + "]" + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasObtained", currentLocale) + "<color=#FFD600>" + LocalizationSettings.StringDatabase.GetLocalizedString("MySkill", "5", currentLocale) + "</color>" + "+1";
                clone2.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
                clone2.transform.SetParent(canvasTransform);
                SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                theSound.Play("Get");
            }
            else if (selectNum <= 31)
            {
                choiceNum = 6;
                GameObject clone1 = Instantiate(ballPrefab[choiceNum], enemy.transform.position, Quaternion.identity);
                playerGold.CurrentGold += 50;
                Vector3 position2 = alramTextTransform.position;
                GameObject clone2 = Instantiate(alramText, position2, Quaternion.identity);
                clone2.GetComponent<Item>().textui1.text = "<color=#FFD600>" + "[" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "MoneyBag", currentLocale) + "]" + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasObtained2", currentLocale) + "<color=#FFD600>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "Gold", currentLocale) + "</color>" + "+50";
                clone2.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
                clone2.transform.SetParent(canvasTransform);
                SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                theSound.Play("Get");
            }
            else if (selectNum <= 32)
            {
                choiceNum = 7;
                GameObject clone1 = Instantiate(ballPrefab[choiceNum], enemy.transform.position, Quaternion.identity);
                playerGold.CurrentGold += 100;
                Vector3 position2 = alramTextTransform.position;
                GameObject clone2 = Instantiate(alramText, position2, Quaternion.identity);
                clone2.GetComponent<Item>().textui1.text = "<color=#FFD600>" + "[" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "Golden", currentLocale) + "]" + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasObtained", currentLocale) + "<color=#FFD600>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "Gold", currentLocale) + "</color>" + "+100";
                clone2.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
                clone2.transform.SetParent(canvasTransform);
                SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                theSound.Play("Get");
            }

        }

        // 리스트에서 사망하는 적 정보 삭제
        enemyList.Remove(enemy);
        // 적 오브젝트 삭제
        Destroy(enemy.gameObject);
    }
    
    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        // 적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        // Slider UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
        // Tip. UI는 캔버스의 자식 오브젝트로 설정되어 있어야 화면에 보인다
        sliderClone.transform.SetParent(canvasTransform);
        // 계층 설정으로 바뀐 크기를 다시 (1, 1, 1)로 설정
        sliderClone.transform.localScale = Vector3.one;

        // Slider UI가 쫓아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        // Slider UI에 자신의 체력 정보를 표시하도록 설정
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}
