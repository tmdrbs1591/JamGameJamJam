using UnityEngine;
using DG.Tweening;

public class Alpha : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float tweenDuration = 2f; // Tween ���� �ð� (��)

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �ʱ� ���İ� ����
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);

        // Tween ����
        StartAlphaTween();
    }

    void StartAlphaTween()
    {
        // DOTween�� ����Ͽ� ���İ��� 1�� ������Ű�� �ٽ� 0���� ���ҽ�Ŵ
        spriteRenderer.DOFade(1f, tweenDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            spriteRenderer.DOFade(0f, tweenDuration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                // �ݺ� �����Ϸ��� ���⿡ �ٽ� StartAlphaTween() ȣ���� �߰��� �� ����
                // StartAlphaTween();
            });
        });
    }
}
