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

        if (id == 0)// ���̾
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, 100, false, false, false, false); // �� ü���� damage��ŭ ����                                                                             //targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage);
        }
        else if (id == 1) // �����Ʈ
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, 150, false, false, false, false);
            targetcollider.GetComponent<Movement2DAni>().TakeSpeedZeroS(25);
        }
        else if (id == 2) // �Ѹ�����
        {
            int dex = Random.Range(200, 400);

            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, dex, false, false, false, false); // �� ü���� damage��ŭ ����                                                                             //targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage);
        }
        else if (id == 3) // ���ڵ�
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, 50, false, false, false, false); // �� ü���� damage��ŭ ����
            targetcollider.GetComponent<Movement2DAni>().TakeSpeedZero(23);
        }
        else if (id == 4) // ��������
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, 100, true, false, false, false); // �� ü���� damage��ŭ ����                                                                             //targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage);
        }
        else if (id == 5) // �������
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, 250, false, false, false, false); // �� ü���� damage��ŭ ���� 
            targetcollider.GetComponent<Movement2DAni>().TakeSpeedZeroS(25);
        }

        else if (id == 10)// ��ź��
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, 0, 0, false, false, false, false);
        }
        else if (id == 30) //����ź��
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, 0, 0,  false, false, false, false);
            targetcollider.GetComponent<Movement2DAni>().TakeSpeed(speedNuf);

        }
        else if (id == 17)//���� ȭ�� ������
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage, false, false, false, false);
        }
        else if (id == 18)//���� ȭ�� ������
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage * 1.5f, false, false, false, false);
        }
        else if (id == 19)//��ȭ ȭ�� ������
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage * 2.0f, false, false, false, false);
        }
        else if (id == 27)//���� �ٶ��Ǳ��
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, 0, 0, false, false, false, false);
        }
        else if (id == 50)// ���� ����
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(damage, 0, 0, false, false, false, false);
            targetcollider.GetComponent<Movement2DAni>().TakeSpeed(speedNuf);

        }
        else if (id == 58) //���� ��������
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage * 1.5f, false, false, false, false);
            targetcollider.GetComponent<Movement2DAni>().TakeSpeedZeroS(11);
        }
        else if (id == 59) //��ȭ ��������
        {
            targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, magicDamage * 1.5f, false, false, false, false);
            targetcollider.GetComponent<Movement2DAni>().TakeSpeedZeroS(11);
        }
        
        
    }
    private IEnumerator Delay(int id)
    {
        if(id == 10)//��ź��
        {
            yield return new WaitForSeconds(0.1f);
            projectile.radius = 0.5f;
        }
        else if (id == 30) // ����ź
        {
            yield return new WaitForSeconds(0.1f);
            projectile.radius = 0.5f;
        }
        if (id == 50)//��������
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
        else if (id == 17) //���� ȭ������
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
        else if (id == 27) // �ٶ��Ǳ��
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
        else if (id == 3) // ���ڵ�
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
