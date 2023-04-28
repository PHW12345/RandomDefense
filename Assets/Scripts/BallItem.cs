using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallItem : MonoBehaviour
{
    private Vector3 item00 = new Vector3(9f,-3.6f,0);
    private Vector3 item01 = new Vector3(10.8f, -3.6f, 0);
    private Vector3 item02 = new Vector3(12.5f, -3.6f, 0);
    private Vector3 item03 = new Vector3(9f, -5.3f, 0);
    private Vector3 item04 = new Vector3(10.8f, -5.3f, 0);
    private Vector3 item05 = new Vector3(12.5f, -5.3f, 0);
    private Vector3 item06 = new Vector3(-8.77f, 4.87f, 0);

    private Vector3 vector;
    public GameObject item;
    public int itemNum;

    public float moveSpeed;
    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        vector.Set(item.transform.position.x, item.transform.position.y + (moveSpeed * Time.deltaTime), item.transform.position.z);
        item.transform.position = vector;
        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
            StartCoroutine("GetItem", itemNum);
    }
    IEnumerator GetItem()
    {
        if(itemNum == 0)
        {
            transform.position = Vector3.Lerp(transform.position, item00, 0.2f);
        }
        else if(itemNum == 1)
        {
            transform.position = Vector3.Lerp(transform.position, item01, 0.2f);

        }
        else if (itemNum == 2)
        {
            transform.position = Vector3.Lerp(transform.position, item02, 0.2f);

        }
        else if (itemNum == 3)
        {
            transform.position = Vector3.Lerp(transform.position, item03, 0.2f);

        }
        else if (itemNum == 4)
        {
            transform.position = Vector3.Lerp(transform.position, item04, 0.2f);

        }
        else if (itemNum == 5)
        {
            transform.position = Vector3.Lerp(transform.position, item05, 0.2f);

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, item06, 0.2f);
        }

        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);

        //yield return null;
    }
}
