using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening; // DOTween ���ӽ����̽� �߰�

public class scoreaddText : MonoBehaviour
{
    public TMP_Text text;
    public float addscore;
    public float moveDuration = 1.0f;
    public float moveDistance = 50.0f; // ���� �̵��� �Ÿ�

    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = "+" + addscore.ToString();

        // ���� �߰� �ؽ�Ʈ�� ���� ���� �ö󰡸鼭 ������� �ִϸ��̼� ����
        Sequence sequence = DOTween.Sequence();
        sequence.Append(text.rectTransform.DOMoveY(text.rectTransform.position.y + moveDistance, moveDuration));
        sequence.Join(text.DOFade(0, moveDuration));

        // �ִϸ��̼��� ������ GameObject �ı�
        sequence.OnComplete(() => Destroy(gameObject));
    }
}
