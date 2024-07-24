using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTween 네임스페이스 추가

public class CursorChange : MonoBehaviour
{
    [SerializeField] Texture2D hamerCursorImage;
    [SerializeField] Texture2D GhostcursorImage;
    [SerializeField] Texture2D FancursorImage;
    [SerializeField] Texture2D SadPTcursorImage;

    [SerializeField] Image[] targetImage;
    [SerializeField] Transform[] parentTransforms; // 부모 Transform 배열로 변경''

    [SerializeField] Material outline;
    [SerializeField] Material nomet;

    [SerializeField] Button hamerBtn;
    [SerializeField] Button ghostBtn;
    [SerializeField] Button fanBtn;
    [SerializeField] Button umbrellraBtn;


    private void Awake()
    {
        // 초기화 코드 추가
    }

    void Start()
    {
        // 시작 코드 추가
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
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // 기본 커서로 변경
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
                // 다트윈을 사용하여 해당 부모 오브젝트의 스케일을 점점 커지게 함
                parentTransforms[i].DOScale(Vector3.one * 1.3f, 0.5f); // 예시: 1.3배 크기로 0.5초 동안 점점 커지게 함
            }
            else
            {
                targetImage[i].gameObject.SetActive(false);
                // 다트윈을 사용하여 다른 부모 오브젝트의 스케일을 원래대로 돌릴 수 있음 (옵션)
                parentTransforms[i].DOScale(Vector3.one, 0.5f); // 예시: 원래 크기로 0.5초 동안 점점 돌아오게 함
            }
        }
    }

    void DeactivateAllTargetImages()
    {
        for (int i = 0; i < targetImage.Length; i++)
        {
            targetImage[i].gameObject.SetActive(false);
            // 다트윈을 사용하여 모든 부모 오브젝트의 스케일을 원래대로 돌릴 수 있음 (옵션)
            parentTransforms[i].DOScale(Vector3.one, 0.5f); // 예시: 원래 크기로 0.5초 동안 점점 돌아오게 함
        }
    }
}
