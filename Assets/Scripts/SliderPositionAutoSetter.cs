using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SliderPositionAutoSetter : MonoBehaviour
{
    //[SerializeField]
    private Vector3 distance = Vector3.down * 0.4f;
    private Transform targetTransform;
    private RectTransform rectTransform;
    //public Transform plusTransform;

    public void Setup(Transform target)
    {
        // slider UI�� �Ѿƴٴ� target ����
        targetTransform = target;
        // RectTransform ������Ʈ ���� ������
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // ���� �ı��Ǿ� �Ѿƴٴ� ����� ������� Slider UI�� ����
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        // ������Ʈ�� ��ġ�� ���ŵ� ���Ŀ� Slider UI�� �Բ� ��ġ�� �����ϵ��� �ϱ� ����
        // LateUpdate()���� ȣ���Ѵ�

        // ������Ʈ�� ���� ��ǥ�� �������� ȭ�鿡���� ��ǥ ���� ����
        Vector3 screenPosition = /*Camera.main.WorldToScreenPoint(*/targetTransform.position/*)*/ /*+ plusTransform.position*/;
        
        // ȭ�鳻���� ��ǥ + distance��ŭ ������ ��ġ�� Slider UI�� ��ġ�� ����
        rectTransform.position = screenPosition/* + distance*/;
        rectTransform.localScale = targetTransform.localScale * 1.2f;
    }

}
