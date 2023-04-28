using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameDB : MonoBehaviour
{
    private int[] stage = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public GameObject[] towerButton; // 추가 용병과 추가스킬 버튼들
    public Button[] tButton;
    public Button[] sButton;

    public PlayerGold playerGold;
    
    //private TowerWeapon currentTower;
    public Button upButton;


    // Start is called before the first frame update
    void Start()
    {
        // 보유한 타워 불러와서 스테이지 시작시 활성화
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
        stage[12] = PlayerPrefs.GetInt("Stage12");
        stage[13] = PlayerPrefs.GetInt("Stage13");
        stage[14] = PlayerPrefs.GetInt("Stage14");
        stage[15] = PlayerPrefs.GetInt("Stage15");
        stage[16] = PlayerPrefs.GetInt("Stage16");

        for (int i = 1; i < stage.Length; i++)
        {
        
            if (stage[i] == 1)
            {
                towerButton[i-1].SetActive(true);
                
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        // 현재 골드량에 따라 생산버튼 연결,비연결
        if (playerGold.CurrentGold < 25)
        {
            for (int i = 0; i < tButton.Length; i++)
            {
                tButton[i].interactable = false;
            }
        }
        else
        {
            for (int i = 0; i < tButton.Length; i++)
            {
                tButton[i].interactable = true;
            }
        }

        //// 현재 골드량에 따라 생산버튼 연결,비연결
        //if (playerGold.CurrentGold < 60)
        //{
        //    tButton[0].interactable = false;
        //    tButton[1].interactable = false;
        //}
        
        //if (playerGold.CurrentGold > 59)
        //{
        //    tButton[0].interactable = true;
        //    tButton[1].interactable = true;
        //}

        //if (playerGold.CurrentGold < 80)
        //{
        //    tButton[2].interactable = false;
        //    tButton[3].interactable = false;
        //    tButton[4].interactable = false;
        //    tButton[5].interactable = false;
        //}

        //if (playerGold.CurrentGold > 79)
        //{
        //    tButton[2].interactable = true;
        //    tButton[3].interactable = true;
        //    tButton[4].interactable = true;
        //    tButton[5].interactable = true;
        //}

        //if (playerGold.CurrentGold < 100)
        //{
        //    tButton[6].interactable = false;
        //    tButton[7].interactable = false;
        //    tButton[8].interactable = false;
        //    tButton[9].interactable = false;
        //}

        //if (playerGold.CurrentGold > 99)
        //{
        //    tButton[6].interactable = true;
        //    tButton[7].interactable = true;
        //    tButton[8].interactable = true;
        //    tButton[9].interactable = true;
        //}
        //if (playerGold.CurrentGold < 120)
        //{
        //    tButton[10].interactable = false;
        //    tButton[11].interactable = false;
        //    tButton[12].interactable = false;
        //    tButton[13].interactable = false;
        //}

        //if (playerGold.CurrentGold > 119)
        //{
        //    tButton[10].interactable = true;
        //    tButton[11].interactable = true;
        //    tButton[12].interactable = true;
        //    tButton[13].interactable = true;
        //}
        //if (playerGold.CurrentGold < 140)
        //{
        //    tButton[14].interactable = false;
        //    tButton[15].interactable = false;
        //    }

        //if (playerGold.CurrentGold > 139)
        //{
        //    tButton[14].interactable = true;
        //    tButton[15].interactable = true;
        //}


        //if (playerGold.CurrentGold +1 > currentTower.Cost * 2)
        //{
        //    upButton.interactable = true;
        //}
        //if (playerGold.CurrentGold < currentTower.Cost * 2)
        //{
        //    upButton.interactable = false;
        //}
    }
}
