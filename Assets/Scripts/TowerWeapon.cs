using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public enum WeaponState { SearchTarget = 0, AttackToTarget }
public class TowerWeapon : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate towerTemplate; // 타워 정보 (공격력, 공격속도 등)

    [SerializeField]
    private GameObject projectilePrefab; // 발사체 프리팹
    [SerializeField]
    private GameObject projectilePrefab01; // 발사체 프리팹
    [SerializeField]
    private Transform spawnPoint; // 발사체 생성 위치
    //public SoundManager theSound;
    public float i_damage;
    public float i_mdamage;
    public float i_rate;
    public float i_range;

    //[SerializeField]
    //private float attackRate = 0.5f; // 공격 속도
    //[SerializeField]
    //private float attackRange = 2.0f; // 공격 범위
    //[SerializeField]
    //private int attackDamage = 1; // 공격력
    //[SerializeField]
    //private string targetRange = "1x1"; // 타겟 범위
    public float moveSpeed;
    public float destroyTime;
    private Vector3 vector;

    public GameObject[] spawnEffect;
    public GameObject[] bellEffect;
    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();
        level = towerNum;
        //if (level == 0)
        //{
        //    theSound.Play("nomal");
        //}
        //else if (level == 1)
        //{
        //    theSound.Play("epic");
        //}
        //else if (level == 2)
        //{
        //    theSound.Play("regend");
        //}
    }

    private int level = 0; // 타워 레벨
    public int towerNum;
    public string towerName;
    //[SerializeField]
    //private string towerName = "Archer Tower"; // 타워 이름
    //[SerializeField]
    //private string info = "This Tower is very good"; // 타워 내용



    private WeaponState weaponState = WeaponState.SearchTarget; //타워 무기의 상태
    private Transform attackTarget = null; // 공격 대상
    private SpriteRenderer spriteRenderer; // 타워 오브젝트 이미지 변경용
    private EnemySpawner enemySpawner; // 게임에 존재하는 적 정보를 획득용
    private PlayerGold playerGold; // 플레이어의 골드 정보 획득 및 설정
    private TowerSpawner towerSpawner; // 타워스포너의 타워갯수 가감

    internal int Maxlevel = 6;
    private Tile ownerTile; // 현재 타워가 배치되어 있는 타일

    public Sprite TowerSprite => towerTemplate.weapon[level].sprite;
    //public float Damage => attackDamage;
    public float Damage => towerTemplate.weapon[level].damage;
    //public float Rate => attackRate;
    public float DamageDef => towerTemplate.weapon[level].damageDef;
    public float MagicDamage => towerTemplate.weapon[level].magicDamage;
    public float SpeedNuf => towerTemplate.weapon[level].speedNuf;
    public bool Poison => towerTemplate.weapon[level].poison;
    public bool Poison2 => towerTemplate.weapon[level].poison2;
    public bool Fire => towerTemplate.weapon[level].fire;

    public bool Death => towerTemplate.weapon[level].death;


    public float Rate => towerTemplate.weapon[level].rate;
    //public float Range => attackRange;
    public float Range => towerTemplate.weapon[level].range;
    public string TargetRange => towerTemplate.weapon[level].targetRange;
    public string Name => towerTemplate.weapon[level].towerName;
    public string NameE => towerTemplate.weapon[level].towerNameE;

    public string Grade => towerTemplate.weapon[level].grade;
    public string Info => towerTemplate.weapon[level].info;
    public string InfoE => towerTemplate.weapon[level].infoE;


    public int Cost => towerTemplate.weapon[level].cost;
    public int Sell => towerTemplate.weapon[level].sell;


    public int Level => level + 1;

    private AudioSource audioSource;

    public int towerType;

    private SoundManager theSound;

    public bool upDone;
    public int choiceType; // 뽑은 강화 타입 종류
    //public int choiceLevel; // 뽑은 강화 레벨 등급
    public void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile/*, TowerSpawner towerSpawner*/)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.enemySpawner = enemySpawner;
        this.playerGold = playerGold;
        this.ownerTile = ownerTile;
        //this.towerSpawner = towerSpawner;

        // 최초 상태를 WeaponState.SearchTarget으로 설정
        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        // 이전에 재상중이던 상태 종료
        StopCoroutine(weaponState.ToString());
        // 상태 변경
        weaponState = newState;
        // 새로운 상태 재생
        StartCoroutine(weaponState.ToString());

    }

    private void Update()
    {
        if (destroyTime >= 0)
        {
            vector.Set(this.transform.position.x, this.transform.position.y - (moveSpeed * Time.deltaTime), this.transform.position.z);
            this.transform.position = vector;
            destroyTime -= Time.deltaTime;
        }

    }

    //private void RotateToTarget()
    //{
    //    // 원점으로부터의 거리와 수평축으로부터의 각도를 이용해 위치를 구하는 극 좌표계 이용
    //    // 각도 = arctan(y/x)
    //    // x, y 변위값 구하기
    //    float dx = attackTarget.position.x - transform.position.x;
    //    float dy = attackTarget.position.y - transform.position.y;
    //    // x, y 변위값을 바탕으로 각도 구하기
    //    // 각도가 radian 단위이기 때문에 Mathf.Rad2Deg를 곱해 도 단위를 구함
    //    float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(0, 0, degree);
    //}
    private IEnumerator SearchTarget()
    {
        while (true)
        {
            // 제일 가까이 있는 적을 찾기 위해 최초 거리를 최대한 크게 설정
            float closestDistSqr = Mathf.Infinity;
            // EnemySpawner의 EnemyList에 있는 현재 맵에 존재하는 모든 적 검사
            for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                // 현재 검사중인 적과의 거리가 공격범위 내에 있고, 현재까지 검사한 적보다 거리가 가까우면
                //if (distance <= attackRange && distance <= closestDistSqr)
                if ( distance <= towerTemplate.weapon[level].range + i_range && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    attackTarget = enemySpawner.EnemyList[i].transform;
                }
            }
            if (attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }

            yield return null;
        }
    }
    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            // 1. target이 있는지 검사 (다른 발사체에 의해 제거, Goal 지점까지 이동해 삭제 등)
            if(attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // 2. target이 공격 범위 안에 있는지 검사 (공격 범위를 벗어나면 새로운 적 탐색)
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            //if (distance > attackRange)
            if (distance > towerTemplate.weapon[level].range + i_range)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // 3. attackRate 시간만큼 대기
            //yield return new WaitForSeconds(attackRate);
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate - i_rate);

            // 4. 공격 (발사체 생성)
            SpawnProjectile();

        }
    }

    private void SpawnProjectile()
    {
        if (towerName == "wind")
        {
            int dex = Random.Range(0, 100);
            if (dex <= 20)
            {
                GameObject clone = Instantiate(projectilePrefab01, spawnPoint.position, Quaternion.identity);

                // 생성된 발사체에게 공격대상(attackTarget) 정보 제공
                clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, towerTemplate.weapon[level].magicDamage + i_mdamage, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].poison2, towerTemplate.weapon[level].fire, towerTemplate.weapon[level].death, true);
                //clone.GetComponent<Movement2D>().MoveTo(new Vector3(0,0.1f,0), new Vector3(0, 0, 0));

                StartCoroutine("Delay");
            }
            else
            {
                GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

                // 생성된 발사체에게 공격대상(attackTarget) 정보 제공
                clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, towerTemplate.weapon[level].magicDamage + i_mdamage, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].poison2, towerTemplate.weapon[level].fire, towerTemplate.weapon[level].death, false);

                StartCoroutine("Delay");
            }
        }
        if (towerName == "thunder")
        {
            int dex = Random.Range(0, 100);
            if (dex <= 20)
            {
                //GameObject clone = Instantiate(projectilePrefab01, spawnPoint.position, Quaternion.identity);
                GameObject clone1 = Instantiate(projectilePrefab01, spawnPoint.position + Vector3.right * 0.5f, Quaternion.identity);
                GameObject clone2 = Instantiate(projectilePrefab01, spawnPoint.position + Vector3.left * 0.5f, Quaternion.identity);
                GameObject clone3 = Instantiate(projectilePrefab01, spawnPoint.position + Vector3.up * 0.5f, Quaternion.identity);
                GameObject clone4 = Instantiate(projectilePrefab01, spawnPoint.position + Vector3.down * 0.5f, Quaternion.identity);
                // 생성된 발사체에게 공격대상(attackTarget) 정보 제공
                //clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, towerTemplate.weapon[level].magicDamage + i_mdamage, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].fire, true);
                clone1.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, (towerTemplate.weapon[level].magicDamage + i_mdamage) / 2, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].poison2, towerTemplate.weapon[level].fire, towerTemplate.weapon[level].death, true);
                clone2.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, (towerTemplate.weapon[level].magicDamage + i_mdamage) / 2, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].poison2, towerTemplate.weapon[level].fire, towerTemplate.weapon[level].death, true);
                clone3.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, (towerTemplate.weapon[level].magicDamage + i_mdamage) / 2, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].poison2, towerTemplate.weapon[level].fire, towerTemplate.weapon[level].death, true);
                clone4.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, (towerTemplate.weapon[level].magicDamage + i_mdamage) / 2, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].poison2, towerTemplate.weapon[level].fire, towerTemplate.weapon[level].death, true);

                StartCoroutine("Delay");
            }
            else
            {
                GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

                // 생성된 발사체에게 공격대상(attackTarget) 정보 제공
                clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, towerTemplate.weapon[level].magicDamage + i_mdamage, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].poison2, towerTemplate.weapon[level].fire, towerTemplate.weapon[level].death, false);

                StartCoroutine("Delay");
            }
        }
        else
        {
            GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

            // 생성된 발사체에게 공격대상(attackTarget) 정보 제공
            clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, towerTemplate.weapon[level].magicDamage + i_mdamage, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].poison2, towerTemplate.weapon[level].fire, towerTemplate.weapon[level].death, false);

            StartCoroutine("Delay");
        }
        //GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

        //// 생성된 발사체에게 공격대상(attackTarget) 정보 제공
        //clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, towerTemplate.weapon[level].magicDamage, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].fire);

        //StartCoroutine("Delay");
    }

    private void SpawnProjectileUp()
    {
        GameObject clone = Instantiate(projectilePrefab01, spawnPoint.position, Quaternion.identity);
        //audioSource.Play();

        // 생성된 발사체에게 공격대상(attackTarget) 정보 제공
        //clone.GetComponent<Projectile>().Setup(attackTarget, attackDamage);
        clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage, towerTemplate.weapon[level].damageDef, towerTemplate.weapon[level].magicDamage, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].poison2, towerTemplate.weapon[level].fire, towerTemplate.weapon[level].death, false);
        
        AudioManager manager = GameObject.Find("GameManager").GetComponent<AudioManager>();
        if (towerType == 1)
        {
            manager.SfxPlay(AudioManager.Sfx.Arrow);
        }
        else if (towerType == 2)
        {
            manager.SfxPlay(AudioManager.Sfx.Sword);
        }
        //else if (towerType == 3)
        //{
        //    manager.SfxPlay(AudioManager.Sfx.Boom);
        //}
        else if (towerType == 4)
        {
            manager.SfxPlay(AudioManager.Sfx.Magic);
        }
    }
    public bool Upgrade()
    {
        // 타워 업그레이드 필요한 골드가 충분한지 검사
        if (playerGold.CurrentGold < towerTemplate.weapon[level].cost)
        {
            Debug.Log("골드가 부족합니다");
            return false;
        }
        //궁병
        if (towerType == 1)
        {
            int uptype = Random.Range(0, 3); // 0 공격력, 1 공격속도, 2 사거리
            if (uptype == 0)
            {
                //int uplevel = Random.Range(5, 16);
                int uplevel = Random.Range(0, 5);
                if (uplevel == 0)
                {
                    i_damage += 5;
                }
                else if (uplevel == 1)
                {
                    i_damage += 10;
                }
                else if (uplevel == 2)
                {
                    i_damage += 15f;
                }
                else if (uplevel == 3)
                {
                    i_damage += 20;
                }
                else if (uplevel == 4)
                {
                    i_damage += 25;
                }

                GameObject clone = Instantiate(bellEffect[uplevel], transform.position + Vector3.up * 0.7f, Quaternion.identity);

            }
            else if (uptype == 1)
            {
                int uplevel = Random.Range(0, 5);

                if (uplevel == 0)
                {
                    i_rate += 0.05f;
                }
                else if (uplevel == 1)
                {
                    i_rate += 0.1f;
                }
                else if (uplevel == 2)
                {
                    i_rate += 0.15f;
                }
                else if (uplevel == 3)
                {
                    i_rate += 0.2f;
                }
                else if (uplevel == 4)
                {
                    i_rate += 0.25f;
                }
                GameObject clone = Instantiate(bellEffect[uplevel], transform.position + Vector3.up * 0.7f, Quaternion.identity);
            }
            else if (uptype == 2)
            {
                //int uplevel = Mathf.RoundToInt(5 * Random.Range(0.0f, 1.0f));
                int uplevel = Random.Range(0, 5);

                if (uplevel == 0)
                {
                    i_range += 0.5f;
                }
                else if (uplevel == 1)
                {
                    i_range += 1.0f;
                }
                else if (uplevel == 2)
                {
                    i_range += 1.5f;
                }
                else if (uplevel == 3)
                {
                    i_range += 2.0f;
                }
                else if (uplevel == 4)
                {
                    i_range += 2.5f;
                }
                GameObject clone = Instantiate(bellEffect[uplevel], transform.position + Vector3.up * 0.7f, Quaternion.identity);
            }
            choiceType = uptype;
        }
        //보병
        else if (towerType == 2)
        {
            int uptype = Random.Range(0, 2); // 0 공격력, 1 공격속도
            if (uptype == 0)
            {
                //int uplevel = Random.Range(5, 16);
                int uplevel = Random.Range(0, 5);
                if (uplevel == 0)
                {
                    i_damage += 5;
                }
                else if (uplevel == 1)
                {
                    i_damage += 10;
                }
                else if (uplevel == 2)
                {
                    i_damage += 15f;
                }
                else if (uplevel == 3)
                {
                    i_damage += 20;
                }
                else if (uplevel == 4)
                {
                    i_damage += 25;
                }

                GameObject clone = Instantiate(bellEffect[uplevel], transform.position + Vector3.up * 0.7f, Quaternion.identity);

            }
            else if (uptype == 1)
            {
                int uplevel = Random.Range(0, 5);

                if (uplevel == 0)
                {
                    i_rate += 0.05f;
                }
                else if (uplevel == 1)
                {
                    i_rate += 0.1f;
                }
                else if (uplevel == 2)
                {
                    i_rate += 0.15f;
                }
                else if (uplevel == 3)
                {
                    i_rate += 0.2f;
                }
                else if (uplevel == 4)
                {
                    i_rate += 0.25f;
                }
                GameObject clone = Instantiate(bellEffect[uplevel], transform.position + Vector3.up * 0.7f, Quaternion.identity);
            }
            choiceType = uptype;
        }
        //포병
        else if (towerType == 3)
        {
            int uptype = Random.Range(0, 2); // 0 공격력, 1 사거리
            if (uptype == 0)
            {
                //int uplevel = Random.Range(5, 16);
                int uplevel = Random.Range(0, 5);
                if (uplevel == 0)
                {
                    i_damage += 5;
                }
                else if (uplevel == 1)
                {
                    i_damage += 10;
                }
                else if (uplevel == 2)
                {
                    i_damage += 15f;
                }
                else if (uplevel == 3)
                {
                    i_damage += 20;
                }
                else if (uplevel == 4)
                {
                    i_damage += 25;
                }
                GameObject clone = Instantiate(bellEffect[uplevel], transform.position + Vector3.up * 0.7f, Quaternion.identity);
                
            }
            else if (uptype == 1)
            {
                int uplevel = Random.Range(0, 5);

                if (uplevel == 0)
                {
                    i_range += 0.5f;
                }
                else if (uplevel == 1)
                {
                    i_range += 1.0f;
                }
                else if (uplevel == 2)
                {
                    i_range += 1.5f;
                }
                else if (uplevel == 3)
                {
                    i_range += 2.0f;
                }
                else if (uplevel == 4)
                {
                    i_range += 2.5f;
                }
                GameObject clone = Instantiate(bellEffect[uplevel], transform.position + Vector3.up * 0.7f, Quaternion.identity);
            }
            choiceType = uptype;
        }
        //마법병
        else if (towerType == 4) 
        {
            int uptype = Random.Range(0, 3); // 0 공격력, 1 공격속도, 2 사거리
            if (uptype == 0)
            {
                //int uplevel = Random.Range(10, 21);
                int uplevel = Random.Range(0, 5);
                if (uplevel == 0)
                {
                    i_mdamage += 10;
                }
                else if (uplevel == 1)
                {
                    i_mdamage += 15;
                }
                else if (uplevel == 2)
                {
                    i_mdamage += 20f;
                }
                else if (uplevel == 3)
                {
                    i_mdamage += 25;
                }
                else if (uplevel == 4)
                {
                    i_mdamage += 30;
                }
                GameObject clone = Instantiate(bellEffect[uplevel], transform.position + Vector3.up * 0.7f, Quaternion.identity);
            }
            else if (uptype == 1)
            {
                int uplevel = Random.Range(0, 5);

                if (uplevel == 0)
                {
                    i_rate += 0.05f;
                }
                else if (uplevel == 1)
                {
                    i_rate += 0.1f;
                }
                else if (uplevel == 2)
                {
                    i_rate += 0.15f;
                }
                else if (uplevel == 3)
                {
                    i_rate += 0.2f;
                }
                else if (uplevel == 4)
                {
                    i_rate += 0.25f;
                }
                GameObject clone = Instantiate(bellEffect[uplevel], transform.position + Vector3.up * 0.7f, Quaternion.identity);
            }
            else if (uptype == 2)
            {
                int uplevel = Random.Range(0, 5);

                if (uplevel == 0)
                {
                    i_range += 1.0f;
                }
                else if (uplevel == 1)
                {
                    i_range += 1.5f;
                }
                else if (uplevel == 2)
                {
                    i_range += 2.0f;
                }
                else if (uplevel == 3)
                {
                    i_range += 2.5f;
                }
                else if (uplevel == 4)
                {
                    i_range += 3.0f;
                }
                GameObject clone = Instantiate(bellEffect[uplevel], transform.position + Vector3.up * 0.7f, Quaternion.identity);
            }
            choiceType = uptype;
        }
        TowerSpawner theTowerSpawner = GameObject.Find("TowerSpawner").GetComponent<TowerSpawner>();
        //알람 생성
        Vector3 position2 = theTowerSpawner.canvasTransform.position;
        GameObject clone2 = Instantiate(theTowerSpawner.alramText, position2, Quaternion.identity); // 알림텍스트 출력
        
        Locale currentLocale = LocalizationSettings.SelectedLocale;

        if (towerType == 1) //궁수
        {
            if (choiceType == 0)
            {
                if(PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "power", currentLocale) + "<color=#ff0000>" + " [" + i_damage + "] " + "</color>";
                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "power", currentLocale) + "<color=#ff0000>" + " [" + i_damage + "] " + "</color>" + "증가 했습니다.";
                }
            }
            else if (choiceType == 1)
            {
                if (PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "speed", currentLocale) + "<color=#0000FF>" + " [" + i_rate + "] " + "</color>" ;
                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + "의 공격 속도가" + "<color=#0000FF>" + " [" + i_rate + "] " + "</color>" + "초 감소 했습니다.";
                }
            }
            else if (choiceType == 2)
            {
                if (PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "range", currentLocale) + "<color=#00FF00>" + " [" + i_range + "] " + "</color>";
                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + "의 사정거리가" + "<color=#00FF00>" + " [" + i_range + "] " + "</color>" + "증가 했습니다.";
                }
            }
        }
        else if (towerType == 2) //보병
        {
            if (choiceType == 0)
            {
                if (PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "power", currentLocale) + "<color=#ff0000>" + " [" + i_damage + "] " + "</color>";

                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + "의 공격력이" + "<color=#ff0000>" + " [" + i_damage + "] " + "</color>" + "증가 했습니다.";

                }
            }
            else if (choiceType == 1)
            {
                if (PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "speed", currentLocale) + "<color=#0000FF>" + " [" + i_rate + "] " + "</color>";

                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + "의 공격 속도가" + "<color=#0000FF>" + " [" + i_rate + "] " + "</color>" + "초 감소 했습니다.";

                }
            }
        }
        else if (towerType == 3) //포병
        {
            if (choiceType == 0)
            {
                if (PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "power", currentLocale) + "<color=#ff0000>" + " [" + i_damage + "] " + "</color>";
                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + "의 공격력이" + "<color=#ff0000>" + " [" + i_damage + "] " + "</color>" + "증가 했습니다.";
                }
            }
            else if (choiceType == 1)
            {
                if (PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "range", currentLocale) + "<color=#00FF00>" + " [" + i_range + "] " + "</color>";

                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + "의 사정거리가" + "<color=#00FF00>" + " [" + i_range + "] " + "</color>" + "증가 했습니다.";

                }
            }
        }
        else if (towerType == 4) //마법병
        {
            if (choiceType == 0)
            {
                if (PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Mpower", currentLocale) + "<color=#ff0000>" + " [" + i_mdamage + "] " + "</color>";
                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "power", currentLocale) + "<color=#ff0000>" + " [" + i_mdamage + "] " + "</color>" + "증가 했습니다.";
                }
            }
            else if (choiceType == 1)
            {
                if (PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "speed", currentLocale) + "<color=#0000FF>" + " [" + i_rate + "] " + "</color>";

                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + "의 공격 속도가" + "<color=#0000FF>" + " [" + i_rate + "] " + "</color>" + "초 감소 했습니다.";

                }
            }
            else if (choiceType == 2)
            {
                if (PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "range", currentLocale) + "<color=#00FF00>" + " [" + i_range + "] " + "</color>";

                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + "의 사정거리가" + "<color=#00FF00>" + " [" + i_range + "] " + "</color>" + "증가 했습니다.";

                }
            }
        }
        
        clone2.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
        clone2.transform.SetParent(theTowerSpawner.canvasTransform);

        InchanteSoundStart();
        // 골드 차감
        playerGold.CurrentGold -= towerTemplate.weapon[level].cost;
        
        //choiceLevel = uplevel;

        upDone = true;
        StartCoroutine("Inchante");
        return true;
    }
    public void InchanteSoundStart()
    {
        SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        theSound.Play("Inchante");
    }
    public IEnumerator Inchante()
    {
        //GameObject clone1 = Instantiate(bellEffect[choiceLevel], transform.position + Vector3.up * 0.75f, Quaternion.identity);

        yield return new WaitForSeconds(1.0f);

        GameObject clone = Instantiate(spawnEffect[choiceType], transform.position/* + Vector3.down * 1.7f*/, Quaternion.identity);
        clone.transform.SetParent(transform);
        clone.transform.localScale = Vector3.one * 1.8f;

    }
    public void TowerSell()
    {
        // 골드 증가
        playerGold.CurrentGold += towerTemplate.weapon[level].sell;
        
        TowerSpawner theTowerSpawner = GameObject.Find("TowerSpawner").GetComponent<TowerSpawner>();

        theTowerSpawner.towerList[towerTemplate.weapon[level].gradeNum]--;
        
        // 현재 타일에 다시 타워 건설이 가능하도록 설정
        ownerTile.IsBuildTower = false;

        //towerSpawner.tower01Count++;
        SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        theSound.Play("BuySell");
        // 타워 파괴
        Destroy(gameObject);
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        AudioManager manager = GameObject.Find("GameManager").GetComponent<AudioManager>();
        if (towerType == 1)
        {
            manager.SfxPlay(AudioManager.Sfx.Arrow);
        }
        else if (towerType == 2)
        {
            manager.SfxPlay(AudioManager.Sfx.Sword);
        }
        //else if (towerType == 3)
        //{
        //    manager.SfxPlay(AudioManager.Sfx.Boom);
        //}
        else if (towerType == 4)
        {
            manager.SfxPlay(AudioManager.Sfx.Magic);
        }
    }
}
