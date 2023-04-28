using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHP : MonoBehaviour
{

    public float maxHP; // 최대 체력

    public float def; // 방어력
    
    public float mRegi; // 마법저항력
   
    private float currentHP; // 현재 체력

    public int enemyID; // 현재 적 넘버

    private bool isDie = false; // 적이 사망 상태이면 isDie를 true로 설정
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    public bool poison = false;
    public bool poison2 = false;

    public bool fire = false;

    public GameObject effect;

    public GameObject damageText;

    //public Transform canvasTransform; // UI를 표현하는 Canvas 오브젝트의 Transform
    //public Text damageText;

    private void Awake()
    {
        currentHP = maxHP; // 현재 체력을 최대 체력과 같게 설정
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void TakeDamage(float damage, float damageDef, float magicDamage, bool poison, bool poison2, bool fire, bool death)
    {
        // Tip. 적의 체력이 damage 만큼 감소해서 죽을 상황일 때 여러 타워의 공격을 동시에 받으면
        // enemy.OnDie() 함수가 여러 번 실행될 수 있다.

        // 현재 적의 상태가 사망 상태이면 아래 코드를 실행하지 않는다.
        if (isDie == true) return;
        
        if (def >= 2)
        {
            def -= damageDef;
        }
        
        // 현재 체력을 damage만큼 감소
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
        // 체력이 0이하  = 적 캐릭터 사망
        if (currentHP <= 0)
        {
            isDie = true;
            GameObject clone = Instantiate(effect, transform.position, Quaternion.identity);
            if(enemyID >= 21)
            {
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("boss03die");
            }
            // 적 캐릭터 사망
            enemy.OnDie(EnemyDestroyType.kill);
        }

    }
    private IEnumerator HitAlphaAnimation()
    {
        // 현재 적의 색상을 color 변수에 저장
        Color color = spriteRenderer.color;

        // 적의 투명도를 40%로 설정
        color.a = 0.3f;
        spriteRenderer.color = color;

        // 0.05초 동안 대기
        yield return new WaitForSeconds(0.05f);

        // 적의 투명도를 100%로 설정
        color.a = 1.0f;
        spriteRenderer.color = color;

    }
    private IEnumerator Poison()
    {
        for(int i = 0; i < 50;)
        {
            currentHP -= 10;
            
            //데미지 텍스트 띄우기
            GameObject clone = Instantiate(damageText, transform.position + Vector3.up * 0.4f, Quaternion.identity);
            clone.GetComponent<Item>().textui1.text = "<color=#00AE00>" + 10.ToString() + "</color>";
            Transform damageui = GameObject.Find("HpUI").transform;
            clone.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
            clone.transform.SetParent(damageui);

            // 현재 적의 색상을 color 변수에 저장
            Color color = spriteRenderer.color;

            // 적의 투명도를 40%로 설정
            color.r = 0.1f;
            color.b = 0.1f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.1f);
            
            // 적의 투명도를 100%로 설정
            color.r = 1.0f;
            color.b = 1.0f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.9f);

            i++;

            if (currentHP <= 0)
            {
                isDie = true;
                
                // 적 캐릭터 사망
                enemy.OnDie(EnemyDestroyType.kill);
            }
        }
    }
    private IEnumerator Poison2(int _damage)
    {
        for (int i = 0; i < 50;)
        {
            currentHP -= _damage / 2;

            //데미지 텍스트 띄우기
            GameObject clone = Instantiate(damageText, transform.position + Vector3.up * 0.4f, Quaternion.identity);
            clone.GetComponent<Item>().textui1.text = "<color=#00AE00>" + Mathf.Round(_damage / 2).ToString() + "</color>";
            Transform damageui = GameObject.Find("HpUI").transform;
            clone.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
            clone.transform.SetParent(damageui);

            // 현재 적의 색상을 color 변수에 저장
            Color color = spriteRenderer.color;

            // 적의 투명도를 40%로 설정
            color.r = 0.1f;
            color.b = 0.1f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.1f);

            // 적의 투명도를 100%로 설정
            color.r = 1.0f;
            color.b = 1.0f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.9f);

            i++;

            if (currentHP <= 0)
            {
                isDie = true;

                // 적 캐릭터 사망
                enemy.OnDie(EnemyDestroyType.kill);
            }
        }
    }
    private IEnumerator Fire()
    {
        for (int i = 0; i < 4;)
        {
            currentHP -= 20;

            //데미지 텍스트 띄우기
            GameObject clone = Instantiate(damageText, transform.position + Vector3.up * 0.4f, Quaternion.identity);
            clone.GetComponent<Item>().textui1.text = "<color=#ff0000>" + 20.ToString() + "</color>";
            Transform damageui = GameObject.Find("HpUI").transform;
            clone.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
            clone.transform.SetParent(damageui);

            //현재 적의 색상을 color 변수에 저장
            Color color = spriteRenderer.color;

            //적의 투명도를 40 % 로 설정
            color.g = 0.1f;
            color.b = 0.1f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.1f);

            //적의 투명도를 100 % 로 설정
            color.g = 1.0f;
            color.b = 1.0f;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.9f);

            i++;

            if (currentHP <= 0)
            {
                isDie = true;
                // 적 캐릭터 사망
                enemy.OnDie(EnemyDestroyType.kill);
            }
        }

    }
}
