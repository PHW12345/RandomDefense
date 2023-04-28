using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    private Collider2D targetcollider;
    private CircleCollider2D projectile;

    //private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        projectile = GetComponent<CircleCollider2D>();
        StartCoroutine("Delay");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetcollider = collision;

        
        //Destroy(gameObject);
        targetcollider.GetComponent<EnemyHP>().TakeDamage(0, 0, 10, false, false, false, false); // 적 체력을 damage만큼 감소
        targetcollider.GetComponent<Movement2DAni>().TakeSpeedZeroS(26);



    }
    private IEnumerator Delay()
    {

        yield return new WaitForSeconds(0.2f);
        projectile.radius = 3.0f;

    }
}
