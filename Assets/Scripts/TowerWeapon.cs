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
    private TowerTemplate towerTemplate; // Ÿ�� ���� (���ݷ�, ���ݼӵ� ��)

    [SerializeField]
    private GameObject projectilePrefab; // �߻�ü ������
    [SerializeField]
    private GameObject projectilePrefab01; // �߻�ü ������
    [SerializeField]
    private Transform spawnPoint; // �߻�ü ���� ��ġ
    //public SoundManager theSound;
    public float i_damage;
    public float i_mdamage;
    public float i_rate;
    public float i_range;

    //[SerializeField]
    //private float attackRate = 0.5f; // ���� �ӵ�
    //[SerializeField]
    //private float attackRange = 2.0f; // ���� ����
    //[SerializeField]
    //private int attackDamage = 1; // ���ݷ�
    //[SerializeField]
    //private string targetRange = "1x1"; // Ÿ�� ����
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

    private int level = 0; // Ÿ�� ����
    public int towerNum;
    public string towerName;
    //[SerializeField]
    //private string towerName = "Archer Tower"; // Ÿ�� �̸�
    //[SerializeField]
    //private string info = "This Tower is very good"; // Ÿ�� ����



    private WeaponState weaponState = WeaponState.SearchTarget; //Ÿ�� ������ ����
    private Transform attackTarget = null; // ���� ���
    private SpriteRenderer spriteRenderer; // Ÿ�� ������Ʈ �̹��� �����
    private EnemySpawner enemySpawner; // ���ӿ� �����ϴ� �� ������ ȹ���
    private PlayerGold playerGold; // �÷��̾��� ��� ���� ȹ�� �� ����
    private TowerSpawner towerSpawner; // Ÿ���������� Ÿ������ ����

    internal int Maxlevel = 6;
    private Tile ownerTile; // ���� Ÿ���� ��ġ�Ǿ� �ִ� Ÿ��

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
    public int choiceType; // ���� ��ȭ Ÿ�� ����
    //public int choiceLevel; // ���� ��ȭ ���� ���
    public void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile/*, TowerSpawner towerSpawner*/)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.enemySpawner = enemySpawner;
        this.playerGold = playerGold;
        this.ownerTile = ownerTile;
        //this.towerSpawner = towerSpawner;

        // ���� ���¸� WeaponState.SearchTarget���� ����
        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        // ������ ������̴� ���� ����
        StopCoroutine(weaponState.ToString());
        // ���� ����
        weaponState = newState;
        // ���ο� ���� ���
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
    //    // �������κ����� �Ÿ��� ���������κ����� ������ �̿��� ��ġ�� ���ϴ� �� ��ǥ�� �̿�
    //    // ���� = arctan(y/x)
    //    // x, y ������ ���ϱ�
    //    float dx = attackTarget.position.x - transform.position.x;
    //    float dy = attackTarget.position.y - transform.position.y;
    //    // x, y �������� �������� ���� ���ϱ�
    //    // ������ radian �����̱� ������ Mathf.Rad2Deg�� ���� �� ������ ����
    //    float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(0, 0, degree);
    //}
    private IEnumerator SearchTarget()
    {
        while (true)
        {
            // ���� ������ �ִ� ���� ã�� ���� ���� �Ÿ��� �ִ��� ũ�� ����
            float closestDistSqr = Mathf.Infinity;
            // EnemySpawner�� EnemyList�� �ִ� ���� �ʿ� �����ϴ� ��� �� �˻�
            for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                // ���� �˻����� ������ �Ÿ��� ���ݹ��� ���� �ְ�, ������� �˻��� ������ �Ÿ��� ������
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
            // 1. target�� �ִ��� �˻� (�ٸ� �߻�ü�� ���� ����, Goal �������� �̵��� ���� ��)
            if(attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // 2. target�� ���� ���� �ȿ� �ִ��� �˻� (���� ������ ����� ���ο� �� Ž��)
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            //if (distance > attackRange)
            if (distance > towerTemplate.weapon[level].range + i_range)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // 3. attackRate �ð���ŭ ���
            //yield return new WaitForSeconds(attackRate);
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate - i_rate);

            // 4. ���� (�߻�ü ����)
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

                // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
                clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, towerTemplate.weapon[level].magicDamage + i_mdamage, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].poison2, towerTemplate.weapon[level].fire, towerTemplate.weapon[level].death, true);
                //clone.GetComponent<Movement2D>().MoveTo(new Vector3(0,0.1f,0), new Vector3(0, 0, 0));

                StartCoroutine("Delay");
            }
            else
            {
                GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

                // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
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
                // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
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

                // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
                clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, towerTemplate.weapon[level].magicDamage + i_mdamage, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].poison2, towerTemplate.weapon[level].fire, towerTemplate.weapon[level].death, false);

                StartCoroutine("Delay");
            }
        }
        else
        {
            GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

            // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
            clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, towerTemplate.weapon[level].magicDamage + i_mdamage, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].poison2, towerTemplate.weapon[level].fire, towerTemplate.weapon[level].death, false);

            StartCoroutine("Delay");
        }
        //GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

        //// ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
        //clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage + i_damage, towerTemplate.weapon[level].damageDef, towerTemplate.weapon[level].magicDamage, towerTemplate.weapon[level].speedNuf, towerTemplate.weapon[level].poison, towerTemplate.weapon[level].fire);

        //StartCoroutine("Delay");
    }

    private void SpawnProjectileUp()
    {
        GameObject clone = Instantiate(projectilePrefab01, spawnPoint.position, Quaternion.identity);
        //audioSource.Play();

        // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
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
        // Ÿ�� ���׷��̵� �ʿ��� ��尡 ������� �˻�
        if (playerGold.CurrentGold < towerTemplate.weapon[level].cost)
        {
            Debug.Log("��尡 �����մϴ�");
            return false;
        }
        //�ú�
        if (towerType == 1)
        {
            int uptype = Random.Range(0, 3); // 0 ���ݷ�, 1 ���ݼӵ�, 2 ��Ÿ�
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
        //����
        else if (towerType == 2)
        {
            int uptype = Random.Range(0, 2); // 0 ���ݷ�, 1 ���ݼӵ�
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
        //����
        else if (towerType == 3)
        {
            int uptype = Random.Range(0, 2); // 0 ���ݷ�, 1 ��Ÿ�
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
        //������
        else if (towerType == 4) 
        {
            int uptype = Random.Range(0, 3); // 0 ���ݷ�, 1 ���ݼӵ�, 2 ��Ÿ�
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
        //�˶� ����
        Vector3 position2 = theTowerSpawner.canvasTransform.position;
        GameObject clone2 = Instantiate(theTowerSpawner.alramText, position2, Quaternion.identity); // �˸��ؽ�Ʈ ���
        
        Locale currentLocale = LocalizationSettings.SelectedLocale;

        if (towerType == 1) //�ü�
        {
            if (choiceType == 0)
            {
                if(PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "power", currentLocale) + "<color=#ff0000>" + " [" + i_damage + "] " + "</color>";
                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "power", currentLocale) + "<color=#ff0000>" + " [" + i_damage + "] " + "</color>" + "���� �߽��ϴ�.";
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
                    clone2.GetComponent<Item>().textui1.text = Name + "�� ���� �ӵ���" + "<color=#0000FF>" + " [" + i_rate + "] " + "</color>" + "�� ���� �߽��ϴ�.";
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
                    clone2.GetComponent<Item>().textui1.text = Name + "�� �����Ÿ���" + "<color=#00FF00>" + " [" + i_range + "] " + "</color>" + "���� �߽��ϴ�.";
                }
            }
        }
        else if (towerType == 2) //����
        {
            if (choiceType == 0)
            {
                if (PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "power", currentLocale) + "<color=#ff0000>" + " [" + i_damage + "] " + "</color>";

                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + "�� ���ݷ���" + "<color=#ff0000>" + " [" + i_damage + "] " + "</color>" + "���� �߽��ϴ�.";

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
                    clone2.GetComponent<Item>().textui1.text = Name + "�� ���� �ӵ���" + "<color=#0000FF>" + " [" + i_rate + "] " + "</color>" + "�� ���� �߽��ϴ�.";

                }
            }
        }
        else if (towerType == 3) //����
        {
            if (choiceType == 0)
            {
                if (PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "power", currentLocale) + "<color=#ff0000>" + " [" + i_damage + "] " + "</color>";
                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + "�� ���ݷ���" + "<color=#ff0000>" + " [" + i_damage + "] " + "</color>" + "���� �߽��ϴ�.";
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
                    clone2.GetComponent<Item>().textui1.text = Name + "�� �����Ÿ���" + "<color=#00FF00>" + " [" + i_range + "] " + "</color>" + "���� �߽��ϴ�.";

                }
            }
        }
        else if (towerType == 4) //������
        {
            if (choiceType == 0)
            {
                if (PlayerPrefs.GetInt("Local") == 0)
                {
                    clone2.GetComponent<Item>().textui1.text = NameE + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "Mpower", currentLocale) + "<color=#ff0000>" + " [" + i_mdamage + "] " + "</color>";
                }
                else
                {
                    clone2.GetComponent<Item>().textui1.text = Name + LocalizationSettings.StringDatabase.GetLocalizedString("MyTower", "power", currentLocale) + "<color=#ff0000>" + " [" + i_mdamage + "] " + "</color>" + "���� �߽��ϴ�.";
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
                    clone2.GetComponent<Item>().textui1.text = Name + "�� ���� �ӵ���" + "<color=#0000FF>" + " [" + i_rate + "] " + "</color>" + "�� ���� �߽��ϴ�.";

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
                    clone2.GetComponent<Item>().textui1.text = Name + "�� �����Ÿ���" + "<color=#00FF00>" + " [" + i_range + "] " + "</color>" + "���� �߽��ϴ�.";

                }
            }
        }
        
        clone2.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
        clone2.transform.SetParent(theTowerSpawner.canvasTransform);

        InchanteSoundStart();
        // ��� ����
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
        // ��� ����
        playerGold.CurrentGold += towerTemplate.weapon[level].sell;
        
        TowerSpawner theTowerSpawner = GameObject.Find("TowerSpawner").GetComponent<TowerSpawner>();

        theTowerSpawner.towerList[towerTemplate.weapon[level].gradeNum]--;
        
        // ���� Ÿ�Ͽ� �ٽ� Ÿ�� �Ǽ��� �����ϵ��� ����
        ownerTile.IsBuildTower = false;

        //towerSpawner.tower01Count++;
        SoundManager theSound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        theSound.Play("BuySell");
        // Ÿ�� �ı�
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
