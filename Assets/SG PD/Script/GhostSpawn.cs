using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform leftSpawnPos;
    [SerializeField] Transform rightSpawnPos;
    [SerializeField] GameObject ghostPrefab; // 소환할 오브젝트
    [SerializeField] BoxCollider2D screenCollider; // 화면 콜라이더
    [SerializeField] CurserState currentState;
    [SerializeField] float time;

    void Update()
    {
        if (StateManager.instance.currentCursorState == currentState)
        {
            screenCollider.gameObject.SetActive(true);
        }
        else
        {
            screenCollider.gameObject.SetActive(false);

        }



        // 마우스 좌클릭 입력 감지
        if (Input.GetMouseButtonDown(0) && StateManager.instance.currentCursorState == currentState)
        {
            // 마우스 위치를 화면 좌표로 가져옵니다.
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Z 축을 0으로 설정 (2D 게임에서 Z 값은 필요 없으므로)

            // 화면 콜라이더 안에 클릭 위치가 있는지 확인합니다.
            if (screenCollider.bounds.Contains(mousePosition))
            {
                // 화면의 중앙 좌표를 가져옵니다.
                float screenWidth = Screen.width;
                float screenCenterX = screenWidth / 2;

                // 클릭된 위치의 X 좌표를 가져옵니다.
                float clickX = Input.mousePosition.x;

                // 클릭된 위치가 화면의 중앙을 기준으로 왼쪽 또는 오른쪽인지 판단합니다.
                if (clickX < screenCenterX)
                {
                    // 왼쪽 클릭 시 왼쪽 위치에 소환
                    Debug.Log("Click on left side");
                    SpawnObjectAt(leftSpawnPos.position, false); // 왼쪽 소환 시 flipX를 true로 설정\

                    if (currentState == CurserState.Ghost) { 
                        player.transform.localScale = new Vector3(1, 1, 1);
                        StateManager.instance.expressionImage.sprite = StateManager.instance.GhostSprite;

                        var _player = player.GetComponent<Player>();
                        _player.anim.SetTrigger("isScare");

                        AudioManager.instance.PlaySound(transform.position, 3, Random.Range(1f, 1.2f), 0.01f);


                        Invoke("IdleSprite",0.8f);
                    }

                    if (currentState == CurserState.Fan)
                    {
                        var _player = player.GetComponent<Player>();
                        _player.anim.SetTrigger("isSpriz");
                        _player.Jump();
                        AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1f, 1f), 0.01f);
                        StateManager.instance.expressionImage.sprite = StateManager.instance.FanSprite;
                        Invoke("IdleSprite", 0.8f);

                    }
                }
                else
                {
                    // 오른쪽 클릭 시 오른쪽 위치에 소환
                    Debug.Log("Click on right side");
                    SpawnObjectAt(rightSpawnPos.position, true); // 오른쪽 소환 시 flipX를 false로 설정

                    if (currentState == CurserState.Ghost) { 
                    player.transform.localScale = new Vector3(-1, 1, 1);
                    StateManager.instance.expressionImage.sprite = StateManager.instance.GhostSprite;
                        Invoke("IdleSprite", 0.8f);

                        AudioManager.instance.PlaySound(transform.position, 3, Random.Range(1f, 1.2f), 0.01f);

                        var _player = player.GetComponent<Player>();
                        _player.anim.SetTrigger("isScare");
                    }
                    if (currentState == CurserState.Fan)
                    {
                        var _player = player.GetComponent<Player>();
                        _player.anim.SetTrigger("isSpriz");
                        _player.Jump();
                        AudioManager.instance.PlaySound(transform.position, 2, Random.Range(1f, 1f), 0.01f);
                        StateManager.instance.expressionImage.sprite = StateManager.instance.FanSprite;
                        Invoke("IdleSprite", 0.8f);

                    }
                }
            }
        }
    }

    void SpawnObjectAt(Vector3 spawnPosition, bool flipX)
    {
        if (ghostPrefab != null)
        {
            // 오브젝트를 소환합니다.
            GameObject spawnedObject = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);

            Destroy(spawnedObject, time);
            // SpriteRenderer를 가져와서 flipX 설정을 적용합니다.
            SpriteRenderer spriteRenderer = spawnedObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = flipX;
            }
            else
            {
                Debug.LogWarning("SpriteRenderer component not found on the prefab!");
            }

            Debug.Log("Spawning object at: " + spawnPosition + " with flipX: " + flipX);
        }
        else
        {
            Debug.LogWarning("ghostPrefab is not assigned!");
        }
    }

    void IdleSprite()
    {
        StateManager.instance.expressionImage.sprite = StateManager.instance.idleSprite;

    }
}
