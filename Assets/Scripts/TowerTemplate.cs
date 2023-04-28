using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerTemplate : ScriptableObject
{
    public GameObject[] towerPrefab;
    public Weapon[] weapon;

    [System.Serializable]
    public struct Weapon
    {
        public string grade;
        public int gradeNum;

        public float persent;
        public Sprite sprite; // �������� Ÿ�� �̹��� (UI)
        public string towerName;
        public string towerNameE;

        public string info;
        public string infoE;


        public float damage; // ���ݷ�
        public float magicDamage; // ���� ���ݷ�

        public float damageDef; // ��� ���ݷ�
        public float speedNuf; // ���ǵ� ����
        public bool poison;
        public bool poison2;

        public bool fire;
        public bool death;


        public float rate; // ���� �ӵ�
        public float range; // ���� ����
        public string targetRange;

        public int cost; // �ʿ� ��� (0���� : �Ǽ�, 1~���� : ���׷��̵�)
        public int sell; // Ÿ�� �Ǹ� �� ȹ�� ���
    }
}
