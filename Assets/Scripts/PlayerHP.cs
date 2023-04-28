using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 30; // 최대체력
    [SerializeField]
    public static float currentHP; // 현재체력
    [SerializeField]
    private BGMController bgmController; // 배경음악 설정 (보스 등장 시 변경)
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    public GameObject LosePopup;
    [SerializeField]
    private SceneTrans sceneTrans; // 
    //public AudioSource loseSound;

    private void Awake()
    {
        currentHP = maxHP; // 현제 체력을 최대 체력과 같게 설정
    }
    public void Start()
    {
    }
    public void TakeDamage(float damage)
    {
        // 현재 체력을 damage만큼 감소
        currentHP -= damage;

        // 체력이 0이 되면 게임오버
        if(currentHP <= 0)
        {
            bgmController.StopBGM();

            //loseSound.enabled = true;
            LosePopup.SetActive(true);
            sceneTrans.IsPause();
        }
    }
}
