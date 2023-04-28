using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;



public class RouletteMgr : MonoBehaviour
{
    public GameObject roulettePlate;
    public GameObject roulettePanel;
    public Transform needle;

    public Sprite[] skillSprite;
    //public string[] skillText;
    public Image[] displayItemSlot;

    public TextMeshProUGUI[] displayItemName;


    List<int> startList = new List<int>();
    List<int> resultIndexList = new List<int>();

    int ItemCnt = 6;

    public StageData player;

    public Button rouletStart;
    public Button rouletExit;

    public GameObject resultPanel;
    public Image resultImage;
    public TextMeshProUGUI resultName;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void RoulatteStart()
    {
        // ���̾� ���� �˻� �� ������ ��ȯ ������ ����
        if (100 > player.diamond)
        {
            Debug.Log("���̾ư� ���� �մϴ�");
            return;
        }
        Locale currentLocale = LocalizationSettings.SelectedLocale;

        PlayerPrefs.SetInt("PlayerDia", player.diamond - 100);
        player.diamond = PlayerPrefs.GetInt("PlayerDia");
        rouletStart.interactable = false;
        rouletExit.interactable = false;

        startList.Clear();
        resultIndexList.Clear();
        // ȹ�� �� �뺴���� �� ��ŭ ����
        for (int i = 1; i < 16; i++)
        {
            startList.Add(i);
        }
        // �귿 �� ������ŭ ����
        for (int i = 0; i < ItemCnt; i++)
        {
            //Color color = displayItemSlot[i].color;
            //color.a = 0.5f;
            int randomIndex = Random.Range(0, startList.Count); // ���� �뺴 ��ȣ �������� ��
            resultIndexList.Add(startList[randomIndex]); // ��� ����Ʈ�� ����
            displayItemSlot[i].sprite = skillSprite[startList[randomIndex]]; // �� �̹��� �귿�� ǥ��
            //displayItemName[i].text = skillText[startList[randomIndex]];
            displayItemName[i].text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", startList[randomIndex].ToString(), currentLocale);
            startList.RemoveAt(randomIndex);
        }
        
        StartCoroutine(StartRoulette()); // �귿 ��ŸƮ
    }
    IEnumerator StartRoulette()
    {
        yield return new WaitForSeconds(1f);
        SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        manager.Play("Roulette");
        float randomSpd = Random.Range(1.0f, 5.0f);

        float rotateSpeed = 100f * randomSpd;

        while(true) 
        {
            yield return null;
            if (rotateSpeed <= 0.01f)
            {
                break;
            }
            rotateSpeed = Mathf.Lerp(rotateSpeed, 0, Time.deltaTime * 2f);
            roulettePlate.transform.Rotate(0, 0, rotateSpeed);
        }
        //yield return new WaitForSeconds(0.3f);
        Result();
    }
    void Result()
    {
        int closetIndex = -1;
        float closetDis = 500f;
        float currentDis = 0f;

        for(int i = 0; i < ItemCnt;i++)
        {
            currentDis = Vector2.Distance(displayItemSlot[i].transform.position, needle.position);
            if (closetDis > currentDis)
            {
                closetDis = currentDis;
                closetIndex = i;
            }
        }
        if(closetIndex == -1)
        {
            Debug.Log("��� is ���");
        }
        //displayItemSlot[ItemCnt].sprite = displayItemSlot[closetIndex].sprite;
        SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        manager.Play("hero");
        Debug.Log("Lv up index : " + resultIndexList[closetIndex]);

        PlayerPrefs.SetInt("tower" + resultIndexList[closetIndex], PlayerPrefs.GetInt("tower" + resultIndexList[closetIndex]) + 1);
        resultPanel.SetActive(true);
        resultImage.sprite = skillSprite[resultIndexList[closetIndex]]; // �� �̹��� �귿�� ǥ��
        //resultName.text = skillText[resultIndexList[closetIndex]];
        Locale currentLocale = LocalizationSettings.SelectedLocale;
        resultName.text = LocalizationSettings.StringDatabase.GetLocalizedString("MyRM", resultIndexList[closetIndex].ToString(), currentLocale);
        //// ��ŸƮ ����Ʈ�� ���� �Ȱ� ����
        //for (int i = 0; i < 8; i++)
        //{
        //    startList.RemoveAt(i);
        //}

        // ��ŸƮ ����Ʈ�� ���� �Ȱ� ����
        //for (int i = 0; i < 6; i++)
        //{
        //    resultIndexList.RemoveAt(i);
        //}
    }
    public void BottonOpen()
    {
        rouletStart.interactable = true;
        rouletExit.interactable = true;

        resultPanel.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
