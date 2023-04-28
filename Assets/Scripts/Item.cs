using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;

    public float moveSpeed;
    public float destroyTime;

    //public TextMeshProUGUI gradeText;
    //public TextMeshProUGUI nameText;
    //public TextMeshProUGUI[] textui;
    public Text textui1;

    public GameObject item;
    private Vector3 vector;
    private string text;
    

    // Update is called once per frame
    void Update()
    {
        vector.Set(item.transform.position.x, item.transform.position.y + (moveSpeed * Time.deltaTime), item.transform.position.z);
        item.transform.position = vector;

        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
            Destroy(this.gameObject);
    }
    public void Start()
    {
        //textui[0] = GetComponent<TextMeshProUGUI>();
        textui1 = GetComponent<Text>();
        //StartCoroutine("GetItem");

    }
    public void Setup(string _text)
    {
        //this.textui[0].text = _text;
        this.textui1.text = _text;

    }

    private void RotateToTarget()
    {
        // �������κ����� �Ÿ��� ���������κ����� ������ �̿��� ��ġ�� ���ϴ� �� ��ǥ�� �̿�
        // ���� = arctan(y/x)
        // x, y ������ ���ϱ�
        float dx = target.position.x - transform.position.x;
        float dy = target.position.y - transform.position.y;
        // x, y �������� �������� ���� ���ϱ�
        // ������ radian �����̱� ������ Mathf.Rad2Deg�� ���� �� ������ ����
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    //IEnumerator GetItem()
    //{
    //    transform.position = Vector3.Lerp (transform.position,skillPoint.transform.position, 0.2f);
    //    yield return null;
    //}
}
