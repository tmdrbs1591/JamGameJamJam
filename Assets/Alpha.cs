using UnityEngine;
using DG.Tweening;

public class Alpha : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float tweenDuration = 2f; // Tween 지속 시간 (초)

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 초기 알파값 설정
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);

        // Tween 시작
        StartAlphaTween();
    }

    void StartAlphaTween()
    {
        // DOTween을 사용하여 알파값을 1로 증가시키고 다시 0으로 감소시킴
        spriteRenderer.DOFade(1f, tweenDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            spriteRenderer.DOFade(0f, tweenDuration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                // 반복 실행하려면 여기에 다시 StartAlphaTween() 호출을 추가할 수 있음
                // StartAlphaTween();
            });
        });
    }
}
