using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2DAni : MonoBehaviour
{
    
    public float moveSpeed;
    private float currentMoveSpeed;


    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;
    private Vector3 vector;

    private bool sangtae = false; // 끈끈이병
    private bool sangtaeice = false; // 얼음 마법사
    private bool sangtaeSturn = false; // 성직자

    public float MoveSpeed => moveSpeed; // moveSpeed 변수의 프로퍼티(Property) (Get 가능)
    void Start()
    {
        currentMoveSpeed = moveSpeed;
        animator = GetComponent<Animator>();
    }
    //animator.SetFloat("DirX", moveDirection.x);
    //animator.SetFloat("DirY", moveDirection.y);

    // Update is called once per frame
    private void Update()
    {
        transform.position += moveDirection * currentMoveSpeed * Time.deltaTime;
        //vector.Set;
        animator.SetFloat("DirX", moveDirection.x);
        animator.SetFloat("DirY", moveDirection.y);
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
    public void TakeSpeed(float speedNuf)
    {

        if (sangtae == false && sangtaeice == false && sangtaeSturn == false)
        {

            if (speedNuf > 0)
            {
                sangtae = true;

                currentMoveSpeed -= speedNuf;
                StartCoroutine("Ori04");

            }
            else
                return;
        }
    }
    public void TakeSpeed01(float speedNuf)
    {
        currentMoveSpeed -= speedNuf;

    }
    public void TakeSpeedZero(int id) // 얼음 마법사
    {
        if (sangtaeice == false)
        {
            sangtaeice = true;

            currentMoveSpeed = 0f;
            animator.enabled = false;
            if(id == 7)
            {
                StartCoroutine("Ori");

            }
            else if(id == 23)
            {
                StartCoroutine("Ori01");

            }
        }
       
    }
    public void TakeSpeedZeroS(int id) // 성직자, 번개마법사
    {
        if (sangtaeSturn == false)
        {
            sangtaeSturn = true;

            currentMoveSpeed = 0f;
            animator.enabled = false;
            if (id == 9)
            {
                StartCoroutine("Ori02");

            }
            else if (id == 25)
            {
                StartCoroutine("Ori03");

            }
            else if (id == 11)
            {
                StartCoroutine("Ori02");

            }
        }

    }
    private IEnumerator Ori() // 얼음 마법사
    {
        yield return new WaitForSeconds(2.0f);
        sangtaeice = false;
        currentMoveSpeed = moveSpeed;
        animator.enabled = true;
    }
    private IEnumerator Ori01() // 얼음 마법사
    {
        yield return new WaitForSeconds(3.0f);
        sangtaeice = false;
        currentMoveSpeed = moveSpeed;
        animator.enabled = true;
    }

    private IEnumerator Ori02() // 성직자
    {
        yield return new WaitForSeconds(0.5f);
        sangtaeSturn = false;
        currentMoveSpeed = moveSpeed;
        animator.enabled = true;
    }
    private IEnumerator Ori03() // 상급 성직자
    {
        yield return new WaitForSeconds(1.0f);
        sangtaeSturn = false;
        currentMoveSpeed = moveSpeed;
        animator.enabled = true;
    }
    private IEnumerator Ori04() // 포탄병
    {
        yield return new WaitForSeconds(3.0f);
        sangtae = false;
        currentMoveSpeed = moveSpeed;
    }

}
