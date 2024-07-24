using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform leftSpawnPos;
    [SerializeField] Transform rightSpawnPos;
    [SerializeField] GameObject ghostPrefab; // ��ȯ�� ������Ʈ
    [SerializeField] BoxCollider2D screenCollider; // ȭ�� �ݶ��̴�
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



        // ���콺 ��Ŭ�� �Է� ����
        if (Input.GetMouseButtonDown(0) && StateManager.instance.currentCursorState == currentState)
        {
            // ���콺 ��ġ�� ȭ�� ��ǥ�� �����ɴϴ�.
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Z ���� 0���� ���� (2D ���ӿ��� Z ���� �ʿ� �����Ƿ�)

            // ȭ�� �ݶ��̴� �ȿ� Ŭ�� ��ġ�� �ִ��� Ȯ���մϴ�.
            if (screenCollider.bounds.Contains(mousePosition))
            {
                // ȭ���� �߾� ��ǥ�� �����ɴϴ�.
                float screenWidth = Screen.width;
                float screenCenterX = screenWidth / 2;

                // Ŭ���� ��ġ�� X ��ǥ�� �����ɴϴ�.
                float clickX = Input.mousePosition.x;

                // Ŭ���� ��ġ�� ȭ���� �߾��� �������� ���� �Ǵ� ���������� �Ǵ��մϴ�.
                if (clickX < screenCenterX)
                {
                    // ���� Ŭ�� �� ���� ��ġ�� ��ȯ
                    Debug.Log("Click on left side");
                    SpawnObjectAt(leftSpawnPos.position, false); // ���� ��ȯ �� flipX�� true�� ����\

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
                    // ������ Ŭ�� �� ������ ��ġ�� ��ȯ
                    Debug.Log("Click on right side");
                    SpawnObjectAt(rightSpawnPos.position, true); // ������ ��ȯ �� flipX�� false�� ����

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
            // ������Ʈ�� ��ȯ�մϴ�.
            GameObject spawnedObject = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);

            Destroy(spawnedObject, time);
            // SpriteRenderer�� �����ͼ� flipX ������ �����մϴ�.
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
