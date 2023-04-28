using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHP : MonoBehaviour
{

    public float maxHP; // �ִ� ü��

    public float def; // ����
    
    public float mRegi; // �������׷�
   
    private float currentHP; // ���� ü��

    public int enemyID; // ���� �� �ѹ�

    private bool isDie = false; // ���� ��� �����̸� isDie�� true�� ����
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    public bool poison = false;
    public bool poison2 = false;

    public bool fire = false;

    public GameObject effect;

    public GameObject damageText;

    //public Transform canvasTransform; // UI�� ǥ���ϴ� Canvas ������Ʈ�� Transform
    //public Text damageText;

    private void Awake()
    {
        currentHP = maxHP; // ���� ü���� �ִ� ü�°� ���� ����
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void TakeDamage(float damage, float damageDef, float magicDamage, bool poison, bool poison2, bool fire, bool death)
    {
        // Tip. ���� ü���� damage ��ŭ �����ؼ� ���� ��Ȳ�� �� ���� Ÿ���� ������ ���ÿ� ������
        // enemy.OnDie() �Լ��� ���� �� ����� �� �ִ�.

        // ���� ���� ���°� ��� �����̸� �Ʒ� �ڵ带 �������� �ʴ´�.
        if (isDie == true) return;
        
        if (def >= 2)
        {
            def -= damageDef;
        }
        
        // ���� ü���� damage��ŭ ����
        if(damage > 0)
        {
            //float num = damage - def;
            currentHP -= damage - def;

            GameObject clone = Instantiate(damageText, transform.position + Vector3.up * 0.4f, Quaternion.identity);
            clone.GetComponent<Item>().textui1.text = Mathf.Round(damage - def).ToString();
            Transform damageui = GameObject.Find("HpUI").transform;
            clone.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
            clone.transform.SetParent(damageui);
        }
        if (magicDamage > 0)
        {
            //float num = magicDamage - (magicDamage * mRegi);
            
            currentHP -= magicDamage - (magicDamage * mRegi);

            GameObject clone = Instantiate(damageText, transform.position + Vector3.up * 0.54f, Quaternion.identity);
            clone.GetComponent<Item>().textui1.text = "<color=#FFA200>" + Mathf.Round(magicDamage - (magicDamage * mRegi)).ToString() + "</color>";
            Transform damageui = GameObject.Find("HpUI").transform;
            clone.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
            clone.transform.SetParent(damageui);
        }

        if (death == true && enemyID <= 20)
        {
            currentHP = 0;
        }

        if (poison == true)
        {
            this.poison = true;
            StopCoroutine("Poison");
            StartCoroutine("Poison");
        }
        else if (poison2 == true)
        {
            this.poison2 = true;
            StopCoroutine("Poison2");
            StartCoroutine("Poison2", damage);
        }
        else if (fire == true)
        {
            this.fire = true;
            StopCoroutine("Fire");
            StartCoroutine("Fire");
        }
        else
        {
            StopCoroutine("HitAlphaAnimation");
            StartCoroutine("HitAlphaAnimation");
        }
        // ü���� 0����  = �� ĳ���� ���
        if (currentHP <= 0)
        {
            isDie = true;
            GameObject clone = Instantiate(effect, transform.position, Quaternion.identity);
            if(enemyID >= 21)
            {
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("boss03die");
            }
            // �� ĳ���� ���
            enemy.OnDie(EnemyDestroyType.kill);
        }

    }
    private IEnumerator HitAlphaAnimation()
    {
        // ���� ���� ������ color ������ ����
        Color color = spriteRenderer.color;

        // ���� ������ 40%�� ����
        color.a = 0.3f;
        spriteRenderer.color = color;

        // 0.05�� ���� ���
        yield return new WaitForSeconds(0.05f);

        // ���� ������ 100%�� ����
        color.a = 1.0f;
        spriteRenderer.color = color;

    }
    private IEnumerator Poison()
    {
        for(int i = 0; i < 50;)
        {
            currentHP -= 10;
            
            //������ �ؽ�Ʈ ����
            GameObject clone = Instantiate(damageText, transform.position + Vector3.up * 0.4f, Quaternion.identity);
            clone.GetComponent<Item>().textui1.text = "<color=#00AE00>" + 10.ToString() + "</color>";
            Transform damageui = GameObject.Find("HpUI").transform;
            clone.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
            clone.transform.SetParent(damageui);

            // ���� ���� ������ color ������ ����
            Color color = spriteRenderer.color;

            // ���� ������ 40%�� ����
            color.r = 0.1f;
            color.b = 0.1f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.1f);
            
            // ���� ������ 100%�� ����
            color.r = 1.0f;
            color.b = 1.0f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.9f);

            i++;

            if (currentHP <= 0)
            {
                isDie = true;
                
                // �� ĳ���� ���
                enemy.OnDie(EnemyDestroyType.kill);
            }
        }
    }
    private IEnumerator Poison2(int _damage)
    {
        for (int i = 0; i < 50;)
        {
            currentHP -= _damage / 2;

            //������ �ؽ�Ʈ ����
            GameObject clone = Instantiate(damageText, transform.position + Vector3.up * 0.4f, Quaternion.identity);
            clone.GetComponent<Item>().textui1.text = "<color=#00AE00>" + Mathf.Round(_damage / 2).ToString() + "</color>";
            Transform damageui = GameObject.Find("HpUI").transform;
            clone.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
            clone.transform.SetParent(damageui);

            // ���� ���� ������ color ������ ����
            Color color = spriteRenderer.color;

            // ���� ������ 40%�� ����
            color.r = 0.1f;
            color.b = 0.1f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.1f);

            // ���� ������ 100%�� ����
            color.r = 1.0f;
            color.b = 1.0f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.9f);

            i++;

            if (currentHP <= 0)
            {
                isDie = true;

                // �� ĳ���� ���
                enemy.OnDie(EnemyDestroyType.kill);
            }
        }
    }
    private IEnumerator Fire()
    {
        for (int i = 0; i < 4;)
        {
            currentHP -= 20;

            //������ �ؽ�Ʈ ����
            GameObject clone = Instantiate(damageText, transform.position + Vector3.up * 0.4f, Quaternion.identity);
            clone.GetComponent<Item>().textui1.text = "<color=#ff0000>" + 20.ToString() + "</color>";
            Transform damageui = GameObject.Find("HpUI").transform;
            clone.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
            clone.transform.SetParent(damageui);

            //���� ���� ������ color ������ ����
            Color color = spriteRenderer.color;

            //���� ������ 40 % �� ����
            color.g = 0.1f;
            color.b = 0.1f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.1f);

            //���� ������ 100 % �� ����
            color.g = 1.0f;
            color.b = 1.0f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.9f);

            i++;

            if (currentHP <= 0)
            {
                isDie = true;
                // �� ĳ���� ���
                enemy.OnDie(EnemyDestroyType.kill);
            }
        }

    }
}
