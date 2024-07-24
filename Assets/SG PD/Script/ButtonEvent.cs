using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // �ڽ� ������Ʈ�� ������ ����
    public GameObject childObject;

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ⿡�� �ڽ� ������Ʈ ��Ȱ��ȭ
        if (childObject != null)
        {
            childObject.SetActive(false);
        }
    }

    // ���콺�� ��ư ���� �ö��� �� ȣ��Ǵ� �޼���
    public void OnPointerEnter(PointerEventData eventData)
    {
        // �ڽ� ������Ʈ Ȱ��ȭ
        if (childObject != null)
        {
            childObject.SetActive(true);
        }
    }

    // ���콺�� ��ư�� ����� �� ȣ��Ǵ� �޼���
    public void OnPointerExit(PointerEventData eventData)
    {
        // �ڽ� ������Ʈ ��Ȱ��ȭ
        if (childObject != null)
        {
            childObject.SetActive(false);
        }
    }
}
