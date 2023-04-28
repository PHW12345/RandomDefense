using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 moveDirection1;
    //private Animator animator;
    private Vector3 vector;
    private Projectile projectile;

    public float MoveSpeed => moveSpeed; // moveSpeed 변수의 프로퍼티(Property) (Get 가능)
    void Start()
    {
        projectile = GetComponent<Projectile>();
       
    }
    //animator.SetFloat("DirX", moveDirection.x);
    //animator.SetFloat("DirY", moveDirection.y);

    // Update is called once per frame
    private void Update()
    {
        if(projectile.id == 99)
        {
            transform.position += moveDirection1 * moveSpeed * 0.1f  * Time.deltaTime;

        }
        else
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    
        //animator.SetFloat("DirX", moveDirection.x);
        //animator.SetFloat("DirY", moveDirection.y);
    }

    public void MoveTo(Vector3 direction, Vector3 direction1)
    {
        moveDirection1 = direction1;
        moveDirection = direction;
    }

}
