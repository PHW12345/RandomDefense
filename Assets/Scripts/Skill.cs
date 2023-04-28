using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private Collider2D targetcollider;
    private CircleCollider2D projectile;
    //private BoxCollider2D projectilebox;

    public int id;
    //private Animator animator;

    private float magicDamage;
    private float damage;
    private float speedNuf;
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        projectile = GetComponent<CircleCollider2D>();
        //projectilebox = GetComponent<BoxCollider2D>();

        StartCoroutine("Delay", id);
    }
    public void Setup(float magicDamage, float damage, float speedNuf)
    { 
        this.magicDamage = magicDamage;
        this.damage = damage;
        this.speedNuf = speedNuf;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetcollider = collision;

        if (id == 0)// 파이어볼
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, 100, false, false, false, false); // 적 체력을 damage만큼 감소                                                                             //targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage);
        }
        else if (id == 1) // 썬더볼트
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, 150, false, false, false, false);
            targetcollider.GetComponent<Movement2DAni>().TakeSpeedZeroS(25);
        }
        else if (id == 2) // 롤링스톤
        {
            int dex = Random.Range(200, 400);

            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, dex, false, false, false, false); // 적 체력을 damage만큼 감소                                                                             //targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage);
        }
        else if (id == 3) // 블리자드
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, 50, false, false, false, false); // 적 체력을 damage만큼 감소
            targetcollider.GetComponent<Movement2DAni>().TakeSpeedZero(23);
        }
        else if (id == 4) // 포이즌레인
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, 100, true, false, false, false); // 적 체력을 damage만큼 감소                                                                             //targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage);
        }
        else if (id == 5) // 썬더팔콘
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, 250, false, false, false, false); // 적 체력을 damage만큼 감소 
            targetcollider.GetComponent<Movement2DAni>().TakeSpeedZeroS(25);
        }

        else if (id == 10)// 포탄병
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, 0, 0, false, false, false, false);
        }
        else if (id == 30) //물폭탄병
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, 0, 0,  false, false, false, false);
            targetcollider.GetComponent<Movement2DAni>().TakeSpeed(speedNuf);

        }
        else if (id == 17)//영웅 화염 마법사
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage, false, false, false, false);
        }
        else if (id == 18)//전설 화염 마법사
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage * 1.5f, false, false, false, false);
        }
        else if (id == 19)//신화 화염 마법사
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage * 2.0f, false, false, false, false);
        }
        else if (id == 27)//영웅 바람의기사
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, 0, 0, false, false, false, false);
        }
        else if (id == 50)// 진흙 포병
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, 0, 0, false, false, false, false);
            targetcollider.GetComponent<Movement2DAni>().TakeSpeed(speedNuf);

        }
        else if (id == 58) //전설 번개법사
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage * 1.5f, false, false, false, false);
            targetcollider.GetComponent<Movement2DAni>().TakeSpeedZeroS(11);
        }
        else if (id == 59) //신화 번개법사
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage * 1.5f, false, false, false, false);
            targetcollider.GetComponent<Movement2DAni>().TakeSpeedZeroS(11);
        }
        
        
    }
    private IEnumerator Delay(int id)
    {
        if(id == 10)//포탄병
        {
            yield return new WaitForSeconds(0.1f);
            projectile.radius = 0.5f;
        }
        else if (id == 30) // 물폭탄
        {
            yield return new WaitForSeconds(0.1f);
            projectile.radius = 0.5f;
        }
        if (id == 50)//진흙포병
        {
            yield return new WaitForSeconds(0.1f);
            projectile.radius = 0.5f;
        }
        //else if (id == 12)
        //{
        //    yield return new WaitForSeconds(0.1f);
        //    projectile.radius = 1.5f;
        //}
        //else if (id == 13)
        //{
        //    yield return new WaitForSeconds(0.1f);
        //    projectile.radius = 2f;
        //}
        //else if (id == 14)
        //{
        //    yield return new WaitForSeconds(0.1f);
        //    projectile.radius = 2.5f;
        //}
        else if (id == 17) //영웅 화염법사
        {
            yield return new WaitForSeconds(0.1f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0.5f, 0.04f);
            GetComponent<BoxCollider2D>().size = new Vector2(0.95f, 0.25f);
            

        }
        else if (id == 18)
        {
            yield return new WaitForSeconds(0.1f);
            projectile.radius = 0.4f;
        }
        else if (id == 19)
        {
            yield return new WaitForSeconds(0.1f);
            projectile.radius = 0.5f;
        }
        else if (id == 27) // 바람의기사
        {
            yield return new WaitForSeconds(0.1f);
            projectile.radius = 0.4f;
        }
        else if (id == 0)
        {
            yield return new WaitForSeconds(0.11f);
            projectile.radius = 3.0f;
        }
        else if (id == 1)
        {
            yield return new WaitForSeconds(0.05f);
            projectile.radius = 1.5f;
        }
        else if (id == 2)
        {
            yield return new WaitForSeconds(0.26f);
            projectile.radius = 3.0f;
        }
        else if (id == 3) // 블리자드
        {
            yield return new WaitForSeconds(0.01f);
            projectile.radius = 3.0f;
        }
        else if (id == 4)
        {
            yield return new WaitForSeconds(0.33f);
            projectile.radius = 3.0f;
        }
        else if (id == 5)
        {
            yield return new WaitForSeconds(1.5f);
            projectile.radius = 0.5f;
        }
    }

}
