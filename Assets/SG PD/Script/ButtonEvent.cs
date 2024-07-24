using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 자식 오브젝트를 저장할 변수
    public GameObject childObject;

    // Start is called before the first frame update
    void Start()
    {
        // 초기에는 자식 오브젝트 비활성화
        if (childObject != null)
        {
            childObject.SetActive(false);
        }
    }

    // 마우스가 버튼 위에 올라갔을 때 호출되는 메서드
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 자식 오브젝트 활성화
        if (childObject != null)
        {
            childObject.SetActive(true);
        }
    }

    // 마우스가 버튼을 벗어났을 때 호출되는 메서드
    public void OnPointerExit(PointerEventData eventData)
    {
        // 자식 오브젝트 비활성화
        if (childObject != null)
        {
            childObject.SetActive(false);
        }
    }
}
