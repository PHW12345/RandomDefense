using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 30; // �ִ�ü��
    [SerializeField]
    public static float currentHP; // ����ü��
    [SerializeField]
    private BGMController bgmController; // ������� ���� (���� ���� �� ����)
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    public GameObject LosePopup;
    [SerializeField]
    private SceneTrans sceneTrans; // 
    //public AudioSource loseSound;

    private void Awake()
    {
        currentHP = maxHP; // ���� ü���� �ִ� ü�°� ���� ����
    }
    public void Start()
    {
    }
    public void TakeDamage(float damage)
    {
        // ���� ü���� damage��ŭ ����
        currentHP -= damage;

        // ü���� 0�� �Ǹ� ���ӿ���
        if(currentHP <= 0)
        {
            bgmController.StopBGM();

            //loseSound.enabled = true;
            LosePopup.SetActive(true);
            sceneTrans.IsPause();
        }
    }
}
