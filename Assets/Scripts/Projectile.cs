using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;

    private float damage;
    private float damageDef;
    private float magicDamage;

    private float speedNuf;
    private bool poison;
    private bool poison2;

    private bool fire;
    private bool death;

    private bool pass;

    public int id; // 발사체 구분 아이디
    public Sprite transImage;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D projectile;
    private Collider2D targetcollider;
    private Animator animator;
    public GameObject effect;
    public GameObject activeEffect;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        projectile = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }
    public void Setup(Transform target, float damage, float damageDef, float magicDamage, float speedNuf, bool poison, bool poison2, bool fire, bool death, bool pass)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target; // 타워가 설정해준 target
        this.damage = damage; // 타워가 설정해준 공격력
        this.damageDef = damageDef;
        this.magicDamage = magicDamage;
        this.speedNuf = speedNuf;
        this.poison = poison;
        this.poison2 = poison2;

        this.fire = fire;
        this.death = death;
        this.pass = pass;
    }
    private void Update()
    {
        if (target != null && id == 17) // 영웅 화염 마법사
        {
            RotateToTarget2();
            Destroy(gameObject);
        }
        //else if (target != null && id == 25) // 
        //{
        //    RotateToTarget3();
        //    Vector3 direction = (target.position - transform.position).normalized;
        //    Vector3 direction1 = target.position;

        //    movement2D.MoveTo(direction, direction1);
        //}
        else if (target != null) // target이 존재하면
        {
            RotateToTarget();

            // 발사체를 target의 위치로 이동
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 direction1 = target.position.normalized;

            movement2D.MoveTo(direction, direction1);  
        }
        else // 여러 이유로 target이 사라지면
        {
            // 발사체 오브젝트 삭제
            Destroy(gameObject);
        }
    }
    private void RotateToTarget()
    {
        // 원점으로부터의 거리와 수평축으로부터의 각도를 이용해 위치를 구하는 극 좌표계 이용
        // 각도 = arctan(y/x)
        // x, y 변위값 구하기
        float dx = target.position.x - transform.position.x;
        float dy = target.position.y - transform.position.y;
        // x, y 변위값을 바탕으로 각도 구하기
        // 각도가 radian 단위이기 때문에 Mathf.Rad2Deg를 곱해 도 단위를 구함
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);  
    }
    private void RotateToTarget1() //타겟에서 소환
    {
        // 원점으로부터의 거리와 수평축으로부터의 각도를 이용해 위치를 구하는 극 좌표계 이용
        // 각도 = arctan(y/x)
        // x, y 변위값 구하기
        float dx = target.position.x - transform.position.x;
        float dy = target.position.y - transform.position.y;
        // x, y 변위값을 바탕으로 각도 구하기
        // 각도가 radian 단위이기 때문에 Mathf.Rad2Deg를 곱해 도 단위를 구함
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
        GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.Euler(0, 0, degree));
    }
    private void RotateToTarget2() //본체에서 소환
    {
        // 원점으로부터의 거리와 수평축으로부터의 각도를 이용해 위치를 구하는 극 좌표계 이용
        // 각도 = arctan(y/x)
        // x, y 변위값 구하기
        float dx = target.position.x - transform.position.x;
        float dy = target.position.y - transform.position.y;
        Vector3 firebat = new Vector3(dx * 0.6f, dy * 0.6f, 0);

        // x, y 변위값을 바탕으로 각도 구하기
        // 각도가 radian 단위이기 때문에 Mathf.Rad2Deg를 곱해 도 단위를 구함
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
        GameObject clone = Instantiate(effect, this.transform.position/* + firebat*/, Quaternion.Euler(0, 0, degree));
        clone.GetComponent<Skill>().Setup(magicDamage, damage, speedNuf);      
    }
    private void RotateToTarget3() //본체에서 소환 바람
    {
        // 원점으로부터의 거리와 수평축으로부터의 각도를 이용해 위치를 구하는 극 좌표계 이용
        // 각도 = arctan(y/x)
        // x, y 변위값 구하기
        float dx = target.position.x - transform.position.x;
        float dy = target.position.y - transform.position.y;
        Vector3 firebat = new Vector3(dx * 0.6f, dy * 0.6f, 0);

        // x, y 변위값을 바탕으로 각도 구하기
        // 각도가 radian 단위이기 때문에 Mathf.Rad2Deg를 곱해 도 단위를 구함
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
        GameObject clone = Instantiate(activeEffect, this.transform.position/* + firebat*/, Quaternion.Euler(0, 0, degree));
        clone.GetComponent<Skill>().Setup(magicDamage, damage, speedNuf);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetcollider = collision;

        if (!collision.CompareTag("Enemy")) return; // 적이 아닌 대상과 부딪치면 통과

        if (collision.transform != target && id != 26) return; // 현재 target인 적이 아닐 때 그냥 통과

        //collision.GetComponent<Enemy>().OnDie(); // 적 사망 함수 호출
        //targetcollider.GetComponent<Movement2DAni>().TakeSpeed(speedNuf);

        // 궁수 0
        if (id == 0) 
        {
            int dex = Random.Range(0, 100);
            if (dex <= 5)
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage * 2.0f, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                GameObject clone1 = Instantiate(activeEffect, targetcollider.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }  
        }
        // 검사 1
        else if (id == 5) 
        {
            int dex = Random.Range(0, 100);

            if (dex <= 5)
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage * 2.0f, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                RotateToTarget1();
                GameObject clone1 = Instantiate(activeEffect, targetcollider.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");

                Destroy(gameObject);
            }
            else
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                RotateToTarget1();
                Destroy(gameObject);
            }           
        }
        // 포병 2
        else if (id == 10 || id == 11 || id == 12 || id == 13 || id == 14 ) 
        {
            GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
            clone.GetComponent<Skill>().Setup(magicDamage, damage,speedNuf);
            Destroy(gameObject);
        }
        // 화염 마법사 3
        else if (id == 15 || id == 16 || id == 18 || id == 19) 
        {
            int dex = Random.Range(0, 100);
            if((id == 15 && dex <= 5) || (id == 16 && dex <= 10))
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage + 30.0f, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if (id == 18 && dex <= 10) // 전설 화염법사
            {
                GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                clone.GetComponent<Skill>().Setup(magicDamage, damage, speedNuf);
                Destroy(gameObject);
            }
            else if (id == 19 && dex <= 10) // 신화 화염법사
            {
                GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                clone.GetComponent<Skill>().Setup(magicDamage, damage, speedNuf);
                Destroy(gameObject);
            }
            else
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        // 불화살 궁수 4
        else if (id == 20) 
        {
            int dex = Random.Range(0, 100);
            if (dex <= 20)
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소

                //GameObject clone1 = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                //Destroy(gameObject);
                animator.enabled = true;
                StartCoroutine("Delay",5);
            }
            else
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, false, death); // 적 체력을 damage만큼 감소
                GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        // 바람의기사 5
        else if (id == 25 || id == 26)
        {
            if(pass == true)
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage * 2, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                //RotateToTarget1();
                Destroy(gameObject);

            }
            else
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                RotateToTarget1();
                Destroy(gameObject);
            }
        }
        // 물 폭탄병 6
        else if (id == 30 || id == 31 || id == 32 || id == 33 || id == 34) 
        {
            GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
            clone.GetComponent<Skill>().Setup(magicDamage, damage, speedNuf);
            Destroy(gameObject);
        }
        // 얼음 마법사 7
        else if (id == 35 || id == 36 || id == 37 || id == 38 || id == 39) 
        {
            int dex = Random.Range(0, 100);
            
            if(dex <= 5)
            {
                targetcollider.GetComponent<Movement2DAni>().TakeSpeedZero(7) ;
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }     
        }
        // 맹독 궁수 8
        else if (id == 40) 
        {
            int dex = Random.Range(0, 100);
            if (dex <= 10)
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소

                GameObject clone1 = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);

            }
            else
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, false, poison2, fire, death); // 적 체력을 damage만큼 감소
                GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

        }
        // 성직자 9
        else if (id == 45) 
        {
            int dex = Random.Range(0, 100);

            if (dex <= 20)
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                targetcollider.GetComponent<Movement2DAni>().TakeSpeedZeroS(9);
                RotateToTarget1();
                spriteRenderer.sprite = transImage;
                StartCoroutine("Delay", 0.5);
            }
            else
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                RotateToTarget1();
                Destroy(gameObject);
            }
        }
        // 진흙 포병 10
        else if (id == 50 || id == 51 || id == 52 || id == 53 || id == 54) 
        {
            int dex = Random.Range(0, 100);

            if (dex <= 20)
            {
                GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                clone.GetComponent<Skill>().Setup(magicDamage, damage, speedNuf);
                GameObject clone1 = Instantiate(activeEffect, targetcollider.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else
            {
                GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                clone.GetComponent<Skill>().Setup(magicDamage, damage, 0);
                Destroy(gameObject);
            }

        }
        // 번개 마법사 11
        else if (id == 55 || id == 56 || id == 57 || id == 58 || id == 59) 
        {
            int dex = Random.Range(0, 100);

            if ((id == 55 && dex <= 5) || (id == 56 && dex <= 10)) // 일반 상급
            {
                targetcollider.GetComponent<Movement2DAni>().TakeSpeedZeroS(11); //
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            //else if (id == 57 && dex <= 10) // 영웅
            //{
            //    GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
            //    clone.GetComponent<Skill>().Setup(magicDamage, damage, speedNuf);
            //    Destroy(gameObject);
            //}
            else if (id == 58 && dex <= 10) // 전설
            {
                GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                clone.GetComponent<Skill>().Setup(magicDamage, damage, speedNuf);
                Destroy(gameObject);
            }
            else if (id == 59 && dex <= 10) // 신화
            {
                GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                clone.GetComponent<Skill>().Setup(magicDamage, damage, speedNuf);
                Destroy(gameObject);
            }
            else
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        // 석궁수 12
        else if (id == 60)
        {
            int dex = Random.Range(0, 100);
            if (dex <= 5)
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage * 5.0f, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                //GameObject clone1 = Instantiate(activeEffect, targetcollider.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else if (dex <= 10)
            {
                targetcollider.GetComponent<Movement2DAni>().TakeSpeedZero(7);
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                //GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        // 독창병 13
        else if (id == 65 || id == 66 || id == 67 || id == 68 || id == 69)
        {
            int dex = Random.Range(0, 100);

            if (dex <= 5 && id == 65)
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                RotateToTarget1();
                GameObject clone1 = Instantiate(activeEffect, targetcollider.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else if (dex <= 5 && id == 66)
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                RotateToTarget1();
                GameObject clone1 = Instantiate(activeEffect, targetcollider.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else if (dex <= 5 && id == 67)
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                RotateToTarget1();
                GameObject clone1 = Instantiate(activeEffect, targetcollider.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else if (dex <= 5 && id == 68)
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                RotateToTarget1();
                GameObject clone1 = Instantiate(activeEffect, targetcollider.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else if (dex <= 5 && id == 69)
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death); // 적 체력을 damage만큼 감소
                RotateToTarget1();
                GameObject clone1 = Instantiate(activeEffect, targetcollider.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, false, fire, death); // 적 체력을 damage만큼 감소
                RotateToTarget1();
                Destroy(gameObject);
            }
        }
        // 군주 14
        else if (id == 70 || id == 71 || id == 72 || id == 73 || id == 74) 
        {
            GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
            clone.GetComponent<Skill>().Setup(magicDamage, damage, speedNuf);
            Destroy(gameObject);
        }
        // 어둠 마법사 15
        else if (id == 75 || id == 76 || id == 77 || id == 78 || id == 79) 
        {
            int dex = Random.Range(0, 100);

            if (dex <= 2 && id == 75) // 일반 
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death);
                GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else if (dex <= 4 && id == 76) //상급
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death);
                GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else if (dex <= 6 && id == 77) //영웅
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death);
                GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else if (dex <= 8 && id == 78) //전설
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death);
                GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else if (dex <= 10 && id == 79) //신화
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, death);
                GameObject clone = Instantiate(activeEffect, targetcollider.transform.position, Quaternion.identity);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Cri");
                Destroy(gameObject);
            }
            else
            {
                targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, damageDef, magicDamage, poison, poison2, fire, false); 
                GameObject clone = Instantiate(effect, targetcollider.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }    
        else
            Destroy(gameObject); // 발사체 오브젝트 삭제
    }
    private IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
