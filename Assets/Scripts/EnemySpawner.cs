using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField]
    //private GameObject enemyPrefab; // �� ������
    [SerializeField]
    private GameObject enemyHPSliderPrefab; // �� ü���� ��Ÿ���� Slider UI ������
    [SerializeField]
    private Transform canvasTransform; // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform

    [SerializeField]
    private Transform alramTextTransform;
    //[SerializeField]
    //private float spawnTime; // �� ���� �ֱ�

    [SerializeField]
    private Transform[] wayPoints; // ���� ���������� �̵� ���
    [SerializeField]
    private Transform[] wayPoints2; // ���� ���������� �̵� ���
    [SerializeField]
    private Transform[] wayPoints3; // ���� ���������� �̵� ���

    [SerializeField]
    private PlayerHP playerHP; // �÷��̾��� ü�� ������Ʈ
    [SerializeField]
    private PlayerGold playerGold; // �÷��̾��� ��� ������Ʈ
    [SerializeField]
    private ObjectDetector objectDetector; // 

    [SerializeField]
    private GameObject[] ballPrefab; // �� ������

    private Wave currentWave; // ���� ���̺� ����
    private Wave currentWave2; // ���̺� ����
    private Wave currentWave3; // ���̺� ����

    public bool wavedone;
    public bool wavedone2;
    public bool wavedone3;

    private List<Enemy> enemyList; // ���� �ʿ� �����ϴ� ��� ���� ����

    // ���� ������ ������ EnemySpawner���� �ϱ� ������ Set�� �ʿ� ����.
    public List<Enemy> EnemyList => enemyList;
    [SerializeField]
    private GameObject coinPrefab; // ���� ������
    public GameObject alramText;

    private void Awake()
    {
        // �� ����Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy>();
        // �� ���� �ڷ�ƾ �Լ� ȣ��
        //StartCoroutine("SpawnEnemy");
    }

    public void StartWave(Wave wave)
    {
        // �Ű������� �޾ƿ� ���̺� ���� ����
        currentWave = wave;
        // ���� ���̺� ����
        StartCoroutine("SpawnEnemy");
    }
    public void StartWave2(Wave wave)
    {
        // �Ű������� �޾ƿ� ���̺� ���� ����
        currentWave2 = wave;
        // ���� ���̺� ����
        StartCoroutine("SpawnEnemy2");
    }
    public void StartWave3(Wave wave)
    {
        // �Ű������� �޾ƿ� ���̺� ���� ����
        currentWave3 = wave;
        // ���� ���̺� ����
        StartCoroutine("SpawnEnemy3");
    }
    private IEnumerator SpawnEnemy()
    {
        wavedone = false;

        yield return new WaitForSeconds(2.0f);

        // ���� ���̺꿡�� ������ �� ����
        int spawnEnemyCount = 0;

        //while (true)
        // ���� ���̺꿡�� �����Ǿ�� �ϴ� ���� ���ڸ�ŭ ���� �����ϰ� �ڷ�ƾ ����
        while(spawnEnemyCount < currentWave.maxEnemyCount)
        {
            //GameObject clone = Instantiate(enemyPrefab); // �� ������Ʈ ����
            // ���̺꿡 �����ϴ� ���� ������ ���� ������ �� ������ ���� �����ϵ��� �����ϰ�, �� ������Ʈ ���� 
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>(); // ��� ������ ���� Enemy ������Ʈ
            
            // this�� �� �ڽ� (�ڽ��� EnemySpawner ����)
            enemy.Setup(this, wayPoints); // wayPoint ������ �Ű������� Setup() ȣ��
            enemyList.Add(enemy); // ����Ʈ�� ��� ������ �� ���� ����

            SpawnEnemyHPSlider(clone); // �� ü���� ��Ÿ���� Slider UI ���� �� ����

            // ���� ���̺꿡�� ������ ���� ���� +1
            spawnEnemyCount ++;

            //yield return new WaitForSeconds(spawnTime); // spawnTime �ð� ���� ���
            // �� ���̺긶�� spawnTime�� �ٸ� �� �ֱ� ������ ���� ���̺�(currentWave)�� spawnTime ���
            yield return new WaitForSeconds(currentWave.spawnTime); // spawnTime �ð� ���� ���
        }
        wavedone = true;
    }
    private IEnumerator SpawnEnemy2()
    {
        wavedone2 = false;

        yield return new WaitForSeconds(2.0f);

        // ���� ���̺꿡�� ������ �� ����
        int spawnEnemyCount = 0;

        //while (true)
        // ���� ���̺꿡�� �����Ǿ�� �ϴ� ���� ���ڸ�ŭ ���� �����ϰ� �ڷ�ƾ ����
        while (spawnEnemyCount < currentWave2.maxEnemyCount)
        {
            //GameObject clone = Instantiate(enemyPrefab); // �� ������Ʈ ����
            // ���̺꿡 �����ϴ� ���� ������ ���� ������ �� ������ ���� �����ϵ��� �����ϰ�, �� ������Ʈ ���� 
            int enemyIndex = Random.Range(0, currentWave2.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave2.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>(); // ��� ������ ���� Enemy ������Ʈ

            // this�� �� �ڽ� (�ڽ��� EnemySpawner ����)
            enemy.Setup(this, wayPoints2); // wayPoint ������ �Ű������� Setup() ȣ��
            enemyList.Add(enemy); // ����Ʈ�� ��� ������ �� ���� ����

            SpawnEnemyHPSlider(clone); // �� ü���� ��Ÿ���� Slider UI ���� �� ����

            // ���� ���̺꿡�� ������ ���� ���� +1
            spawnEnemyCount++;

            //yield return new WaitForSeconds(spawnTime); // spawnTime �ð� ���� ���
            // �� ���̺긶�� spawnTime�� �ٸ� �� �ֱ� ������ ���� ���̺�(currentWave)�� spawnTime ���
            yield return new WaitForSeconds(currentWave2.spawnTime); // spawnTime �ð� ���� ���
        }
        wavedone2 = true;

    }
    private IEnumerator SpawnEnemy3()
    {
        wavedone3 = false;

        yield return new WaitForSeconds(2.0f);

        // ���� ���̺꿡�� ������ �� ����
        int spawnEnemyCount = 0;

        //while (true)
        // ���� ���̺꿡�� �����Ǿ�� �ϴ� ���� ���ڸ�ŭ ���� �����ϰ� �ڷ�ƾ ����
        while (spawnEnemyCount < currentWave3.maxEnemyCount)
        {
            //GameObject clone = Instantiate(enemyPrefab); // �� ������Ʈ ����
            // ���̺꿡 �����ϴ� ���� ������ ���� ������ �� ������ ���� �����ϵ��� �����ϰ�, �� ������Ʈ ���� 
            int enemyIndex = Random.Range(0, currentWave3.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave3.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>(); // ��� ������ ���� Enemy ������Ʈ

            // this�� �� �ڽ� (�ڽ��� EnemySpawner ����)
            enemy.Setup(this, wayPoints3); // wayPoint ������ �Ű������� Setup() ȣ��
            enemyList.Add(enemy); // ����Ʈ�� ��� ������ �� ���� ����

            SpawnEnemyHPSlider(clone); // �� ü���� ��Ÿ���� Slider UI ���� �� ����

            // ���� ���̺꿡�� ������ ���� ���� +1
            spawnEnemyCount++;

            //yield return new WaitForSeconds(spawnTime); // spawnTime �ð� ���� ���
            // �� ���̺긶�� spawnTime�� �ٸ� �� �ֱ� ������ ���� ���̺�(currentWave)�� spawnTime ���
            yield return new WaitForSeconds(currentWave3.spawnTime); // spawnTime �ð� ���� ���
        }
        wavedone3 = true;

    }
    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold, int heart)
    {
        // ���� ��ǥ�������� �������� ��
        if(type == EnemyDestroyType.Arrive)
        {
            Handheld.Vibrate();
            // �÷��̾��� ü�� -1
            playerHP.TakeDamage(heart);
            
        }
        // ���� �÷��̾��� �߻�ü���� ������� ��
        else if (type == EnemyDestroyType.kill)
        {
            // ���� ������ ���� ��� �� ��� ȹ��
            playerGold.CurrentGold += gold;
            GameObject clone = Instantiate(coinPrefab, enemy.transform.position /*+ Vector3.back*/, Quaternion.identity);

            int selectNum = 0; // �������� ������ ����ȣ
            int choiceNum = 0; // ����ȣ�� ���� ���õ� �뺴��� ��ȣ

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

        // ����Ʈ���� ����ϴ� �� ���� ����
        enemyList.Remove(enemy);
        // �� ������Ʈ ����
        Destroy(enemy.gameObject);
    }
    
    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        // �� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        // Slider UI ������Ʈ�� parent("Canvas" ������Ʈ)�� �ڽ����� ����
        // Tip. UI�� ĵ������ �ڽ� ������Ʈ�� �����Ǿ� �־�� ȭ�鿡 ���δ�
        sliderClone.transform.SetParent(canvasTransform);
        // ���� �������� �ٲ� ũ�⸦ �ٽ� (1, 1, 1)�� ����
        sliderClone.transform.localScale = Vector3.one;

        // Slider UI�� �Ѿƴٴ� ����� �������� ����
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        // Slider UI�� �ڽ��� ü�� ������ ǥ���ϵ��� ����
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}
