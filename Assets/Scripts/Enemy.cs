using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnemyDestroyType { kill = 0, Arrive }

public class Enemy : MonoBehaviour
{
    private int wayPointCount; // �̵� ��� ����
    private Transform[] wayPoints; // �̵� ��� ����
    private int currentIndex = 0; // ���� ��ǥ���� �ε���
    private Movement2DAni movement2D; // ������Ʈ �̵� ����
    private EnemySpawner enemySpawner; // ���� ������ ������ ���� �ʰ� EnemySpawner�� �˷��� ����

    public int gold; // �� ��� �� ȹ�� ������ ���

    public int heart; // �� ��� �� ���̴� �����
    //private Animator anim;
    //public Vector3 MonsterPos { get; set; }

    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2DAni>();
        this.enemySpawner = enemySpawner;

        // �� �̵� ��� WayPoints ���� ����
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // ���� ��ġ�� ù��° wayPoint ��ġ�� ����
        transform.position = wayPoints[currentIndex].position;

        // �� �̵�/��ǥ���� ���� �ڷ�ƾ �Լ� ����
        StartCoroutine("OnMove");        
    }

    private IEnumerator OnMove()
    {
        // ���� �̵� ���� ����

        while (true)
        {
            //�� ������Ʈ ȸ��
            //transform.Rotate(Vector3.forward * 2);

            //���� ������ġ�� ��ǥ��ġ�� �Ÿ��� 0.02 * movement2D.moveSpeed���� ���� �� if ���ǹ� ����
            //tip. movement2D.moveSpeed�� �����ִ� ������ �ӵ��� ������ �� �����ӿ� 0.02���� ũ�� �����̱� ������
            //if ���ǹ��� �ɸ��� �ʰ� ��θ� Ż���ϴ� ������Ʈ�� �߻��� �� �ִ�.
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.05f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        //���� �̵��� waypoints�� �����ִٸ�
        if (currentIndex < wayPointCount - 1)
        {
            //���� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
            transform.position = wayPoints[currentIndex].position;
            //�̵� ���� ���� => ���� ��ǥ����(wayPoints)
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        //���� ��ġ�� ������ wayPoint�̸�
        else
        {
            // ��ǥ������ �����ؼ� ����� ���� ���� ���� �ʵ���
            gold = 0;
            //�� ������Ʈ ����
            //Destroy(gameObject);
            OnDie(EnemyDestroyType.Arrive);
        }
    }

    public void OnDie(EnemyDestroyType type)
    {
        // EnemySpawner���� ����Ʈ�� �� ������ �����ϱ� ������ Destroy()�� �������� �ʰ�
        // EnemySpawner���� ������ ������ �� �ʿ��� ó���� �ϵ��� DestroyEnemy() �Լ� ȣ��
        enemySpawner.DestroyEnemy(type, this, gold, heart);

    }
    //void Animate(Vector3 currentPos, Vector3 newPos)
    //{
    //    if (currentPos.y >= newPos.y)
    //    { //Moving down
    //        anim.SetBool("MonsterDown", true);
    //    }
    //    else if (currentPos.y < newPos.y)
    //    {  //Moving up
    //        anim.SetBool("MonsterDown", false);
    //    }

    //    if (currentPos.x > newPos.x)
    //    {  //Move to left
    //        transform.rotation = Quaternion.Euler(0, 180, 0);
    //    }
    //    else if (currentPos.x < newPos.x)
    //    {  //Move to right
    //        transform.rotation = Quaternion.Euler(0, 0, 0);
    //    }

    //}
}
