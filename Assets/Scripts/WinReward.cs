using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinReward : MonoBehaviour
{
    public Sprite[] skillSprite;
    public Image[] displayItemSlot;

    public Sprite diaSprite;
    public Image diaImageSlot;
    public TextMeshProUGUI diaNum ;
    public int diamond;
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            int selectNum;
            selectNum = Random.Range(0, 12);
            Debug.Log("���� �뺴 ��ȣ" + selectNum);
            displayItemSlot[i].sprite = skillSprite[selectNum]; // �� �̹��� �귿�� ǥ��
            PlayerPrefs.SetInt("tower" + selectNum, PlayerPrefs.GetInt("tower" + selectNum) + 1);
        }
        int selectNum1;
        selectNum1 = Random.Range(10, 16);
        Debug.Log("���� ���̾� ����" + selectNum1 * 10);
        diamond = selectNum1;
        diaImageSlot.sprite = diaSprite; // �� �̹��� �귿�� ǥ��
        diaNum.text = "" + selectNum1 * 10;
        PlayerPrefs.SetInt("PlayerDia", PlayerPrefs.GetInt("PlayerDia") + selectNum1 * 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
