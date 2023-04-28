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
        public Sprite sprite; // 보여지는 타워 이미지 (UI)
        public string towerName;
        public string towerNameE;

        public string info;
        public string infoE;


        public float damage; // 공격력
        public float magicDamage; // 마법 공격력

        public float damageDef; // 방깎 공격력
        public float speedNuf; // 스피드 너프
        public bool poison;
        public bool poison2;

        public bool fire;
        public bool death;


        public float rate; // 공격 속도
        public float range; // 공격 범위
        public string targetRange;

        public int cost; // 필요 골드 (0레벨 : 건설, 1~레벨 : 업그레이드)
        public int sell; // 타워 판매 시 획득 골드
    }
}
