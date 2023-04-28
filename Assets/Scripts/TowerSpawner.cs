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
    private TowerTemplate[] towerTemplate; // Ÿ�� ���� (���ݷ�, ���ݼӵ� ��)
    
    //[SerializeField]
    //private int towerBuildGold = 50; // Ÿ�� �Ǽ��� ���Ǵ� ���
    [SerializeField]
    private EnemySpawner enemySpawner; // ���� �ʿ� �����ϴ� �� ����Ʈ ������ ��� ����..
    [SerializeField]
    private PlayerGold playerGold; // Ÿ�� �Ǽ� �� ��� ���Ҹ� ����..

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

    public Transform canvasTransform; // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform
    void Start()
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
        // Ÿ�� �Ǽ� ���� ���� Ȯ��
        // 1. Ÿ���� �Ǽ��� ��ŭ ���� ������ Ÿ�� �Ǽ� x
        if (25 > playerGold.CurrentGold)
        {
            return;
        }
        Debug.Log("��ȯ�� �뺴 id = " + id);

        Tile tile = tileTransform.GetComponent<Tile>();

        // 2. ���� Ÿ���� ��ġ�� �̹� Ÿ���� �Ǽ��Ǿ� ������ Ÿ�� �Ǽ� x
        if (tile.IsBuildTower == true)
        {
            return;
        }

        // Ÿ���� �Ǽ��Ǿ� �������� ����
        tile.IsBuildTower = true;

        // Ÿ�� �Ǽ��� �ʿ��� ��常ŭ ����
        playerGold.CurrentGold -= 25;

        // ������ Ÿ���� ��ġ�� Ÿ�� �Ǽ� (Ÿ�Ϻ��� z�� -1�� ��ġ�� ��ġ)
        Vector3 position = tileTransform.position + Vector3.back;

        //GameObject clone = Instantiate(towerPrefab, position, Quaternion.identity);
        //int selectNum = 0;
        //StartCoroutine("Spawn", position);
        int selectNum = 0; // �������� ������ ����ȣ
        int choiceNum = 0; // ����ȣ�� ���� ���õ� �뺴��� ��ȣ

        //selectNum = Mathf.RoundToInt(3 * Random.Range(0.0f, 1.0f));
        selectNum = Mathf.RoundToInt(1000 * Random.Range(0.0f, 1.0f));
        if (selectNum <= 700) //70�� 50��
        {
            choiceNum = 0;
        }
        else if (selectNum <= 900) //20�� 30��
        {
            choiceNum = 1;
        }
        else if (selectNum <= 975) //7.5�� 15��
        {
            choiceNum = 2;
        }
        else if (selectNum <= 995) //2�� 4��
        {
            choiceNum = 3;
        }
        else //0.5�� 1��
            choiceNum = 4;


        Debug.Log("���� ����" + selectNum);

        Debug.Log("���� ��ȣ" + choiceNum);

        GameObject clone = Instantiate(towerTemplate[id].towerPrefab[choiceNum], position + Vector3.up, Quaternion.identity);
        
        
        GameObject clone1 = Instantiate(spawnEffect[choiceNum], position + Vector3.up * 0.2f, Quaternion.identity);

        //�˶� ����
        Vector3 position2 = canvasTransform.position;
        GameObject clone2 = Instantiate(alramText1, position2, Quaternion.identity); // �˸��ؽ�Ʈ ���
        Locale currentLocale = LocalizationSettings.SelectedLocale;

        if (choiceNum == 0) // ȸ��
        {
            clone2.GetComponent<Item>().textui1.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", id.ToString(), currentLocale) + "<color=#828282>" + " [" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Normal", currentLocale) + "] " + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasHired", currentLocale) + towerTemplate[id].weapon[choiceNum].persent + "% ";
        }
        else if(choiceNum == 1) // �Ķ�
        {
            clone2.GetComponent<Item>().textui1.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", id.ToString(), currentLocale) + "<color=#0082FF>" + " [" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "High", currentLocale) + "] " + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasHired", currentLocale) + towerTemplate[id].weapon[choiceNum].persent + " % ";
        }
        else if (choiceNum == 2) // ����
        {
            clone2.GetComponent<Item>().textui1.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", id.ToString(), currentLocale) + "<color=#DB00FF>" + " [" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Hero", currentLocale) + "] " + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasHired", currentLocale) + towerTemplate[id].weapon[choiceNum].persent + "% ";
        }
        else if (choiceNum == 3) // ����
        {
            clone2.GetComponent<Item>().textui1.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", id.ToString(), currentLocale) + "<color=#ff0000>" + " [" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Legend", currentLocale) + "] " + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasHired", currentLocale) + towerTemplate[id].weapon[choiceNum].persent + "% ";
        }
        else if (choiceNum == 4) // ���
        {
            clone2.GetComponent<Item>().textui1.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", id.ToString(), currentLocale) + "<color=#FFE000>" + " [" + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Myth", currentLocale) + "] " + "</color>" + LocalizationSettings.StringDatabase.GetLocalizedString("MyAlam", "HasHired", currentLocale) + towerTemplate[id].weapon[choiceNum].persent + "% ";
        }
        clone2.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
        clone2.transform.SetParent(canvasTransform);

        SoundStart(choiceNum); // ���� �뺴�� �´� ���� ���
        towerCount[id]--;
        towerList[choiceNum]++;

        //towerCountText[id].text = "" + towerCount[id];
        // Ÿ�� ���⿡ enemySpawner ���� ����
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner, playerGold, tile);

    }
    private IEnumerator Spawn(Vector3 position)
    {
        GameObject clone1 = Instantiate(spawnEffect[4], position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
    }
}
