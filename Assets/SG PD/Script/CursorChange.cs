using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTween ���ӽ����̽� �߰�

public class CursorChange : MonoBehaviour
{
    [SerializeField] Texture2D hamerCursorImage;
    [SerializeField] Texture2D GhostcursorImage;
    [SerializeField] Texture2D FancursorImage;
    [SerializeField] Texture2D SadPTcursorImage;

    [SerializeField] Image[] targetImage;
    [SerializeField] Transform[] parentTransforms; // �θ� Transform �迭�� ����''

    [SerializeField] Material outline;
    [SerializeField] Material nomet;

    [SerializeField] Button hamerBtn;
    [SerializeField] Button ghostBtn;
    [SerializeField] Button fanBtn;
    [SerializeField] Button umbrellraBtn;


    private void Awake()
    {
        // �ʱ�ȭ �ڵ� �߰�
    }

    void Start()
    {
        // ���� �ڵ� �߰�
    }

    void Update()
    {
        switch (StateManager.instance.currentCursorState)
        {
            case CurserState.Hamer:
                Cursor.SetCursor(hamerCursorImage, new Vector2(hamerCursorImage.width / 2, hamerCursorImage.height / 2), CursorMode.ForceSoftware);
                hamerBtn.image.material = outline;
                ghostBtn.image.material = nomet;
                fanBtn.image.material = nomet;
                umbrellraBtn.image.material = nomet;
                ActivateTargetImage(0);
                break;
            case CurserState.Ghost:
                Cursor.SetCursor(GhostcursorImage, new Vector2(GhostcursorImage.width / 2, GhostcursorImage.height / 2), CursorMode.ForceSoftware);
                ghostBtn.image.material = outline;
                hamerBtn.image.material = nomet;
                fanBtn.image.material = nomet;
                umbrellraBtn.image.material = nomet;
                ActivateTargetImage(1);
                break;
            case CurserState.Fan:
                Cursor.SetCursor(FancursorImage, new Vector2(FancursorImage.width / 2, FancursorImage.height / 2), CursorMode.ForceSoftware);
                fanBtn.image.material = outline;
                hamerBtn.image.material = nomet;
                ghostBtn.image.material = nomet;
                umbrellraBtn.image.material = nomet;
                ActivateTargetImage(2);
                break;
            case CurserState.SadPT:
                Cursor.SetCursor(SadPTcursorImage, new Vector2(SadPTcursorImage.width / 2, SadPTcursorImage.height / 2), CursorMode.ForceSoftware);
                umbrellraBtn.image.material = outline;
                hamerBtn.image.material = nomet;
                ghostBtn.image.material = nomet;
                fanBtn.image.material = nomet;
                ActivateTargetImage(3);
                break;
            default:
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // �⺻ Ŀ���� ����
                DeactivateAllTargetImages();
                break;
        }
    }

    void ActivateTargetImage(int index)
    {
        for (int i = 0; i < targetImage.Length; i++)
        {
            if (i == index)
            {
                targetImage[i].gameObject.SetActive(true);
                // ��Ʈ���� ����Ͽ� �ش� �θ� ������Ʈ�� �������� ���� Ŀ���� ��
                parentTransforms[i].DOScale(Vector3.one * 1.3f, 0.5f); // ����: 1.3�� ũ��� 0.5�� ���� ���� Ŀ���� ��
            }
            else
            {
                targetImage[i].gameObject.SetActive(false);
                // ��Ʈ���� ����Ͽ� �ٸ� �θ� ������Ʈ�� �������� ������� ���� �� ���� (�ɼ�)
                parentTransforms[i].DOScale(Vector3.one, 0.5f); // ����: ���� ũ��� 0.5�� ���� ���� ���ƿ��� ��
            }
        }
    }

    void DeactivateAllTargetImages()
    {
        for (int i = 0; i < targetImage.Length; i++)
        {
            targetImage[i].gameObject.SetActive(false);
            // ��Ʈ���� ����Ͽ� ��� �θ� ������Ʈ�� �������� ������� ���� �� ���� (�ɼ�)
            parentTransforms[i].DOScale(Vector3.one, 0.5f); // ����: ���� ũ��� 0.5�� ���� ���� ���ƿ��� ��
        }
    }
}
