using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening; // DOTween 네임스페이스 추가

public class scoreaddText : MonoBehaviour
{
    public TMP_Text text;
    public float addscore;
    public float moveDuration = 1.0f;
    public float moveDistance = 50.0f; // 위로 이동할 거리

    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = "+" + addscore.ToString();

        // 점수 추가 텍스트가 점점 위로 올라가면서 사라지는 애니메이션 적용
        Sequence sequence = DOTween.Sequence();
        sequence.Append(text.rectTransform.DOMoveY(text.rectTransform.position.y + moveDistance, moveDuration));
        sequence.Join(text.DOFade(0, moveDuration));

        // 애니메이션이 끝나면 GameObject 파괴
        sequence.OnComplete(() => Destroy(gameObject));
    }
}
