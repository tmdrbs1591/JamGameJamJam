using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;  // 몬스터의 이동 속도
    private Transform player;  // 플레이어의 Transform을 참조합니다.

    [SerializeField] GameObject bloodPtc;
    [SerializeField] GameObject lightning;
    [SerializeField] GameObject cloud;


    [SerializeField] float addScore;

    [SerializeField] GameObject ScorePlusText;
    public Animator cloudAnim;



    public bool isCloud;
    // 태그 "Player"를 가진 오브젝트를 찾습니다.

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {

            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found in the scene.");
        }
    }

    void Update()
    {
        // 플레이어가 없으면 스크립트를 종료합니다.
        if (player == null)
            return;

        if (isCloud)
        {
            cloud.SetActive(true);
        }
        else
        {
            cloud.SetActive(false);
        }

        // 플레이어와 몬스터 사이의 방향 벡터를 계산합니다.
        Vector3 directionToPlayer = player.position - transform.position;

        // 몬스터의 왼쪽과 오른쪽 벡터를 계산합니다.
        Vector3 leftDirection = -transform.right;
        Vector3 rightDirection = transform.right;

        // 플레이어가 몬스터의 왼쪽에 있는지 오른쪽에 있는지 확인합니다.
        float dotProductLeft = Vector3.Dot(directionToPlayer, leftDirection);
        float dotProductRight = Vector3.Dot(directionToPlayer, rightDirection);

        if (dotProductLeft > dotProductRight)
        {
            // 플레이어가 왼쪽에 있을 때 왼쪽으로 이동
            MoveInDirection(leftDirection);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            // 플레이어가 오른쪽에 있을 때 오른쪽으로 이동
            MoveInDirection(rightDirection);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerDamageBox"))
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

                var playerScript = playerObject.GetComponent<Player>();
            playerScript.currentHp--;
            playerScript.RedPanelMT();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("rain"))
        {

            StartCoroutine(Cloud());
        }
    }
    IEnumerator Cloud()
    {
        cloudAnim.SetTrigger("isDie");
        yield return new WaitForSeconds(0.2f);
        isCloud = false;
    }
    void MoveInDirection(Vector3 direction)
    {
        // 방향으로 이동합니다.
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void Destroy()
    {
        Instantiate(bloodPtc, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
        Instantiate(lightning, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
        ComboManager.instance.currentCombo++;

        ScoreManager.instance.currentScore += addScore;

        CameraShake.instance.Shake();

        var scoreText = Instantiate(ScorePlusText,transform.position,Quaternion.identity);
        var s = scoreText.GetComponent<scoreaddText>();
        s.addscore = addScore;

        if (isCloud)
        {
            StateManager.instance.warningText.gameObject.SetActive(false);
            StateManager.instance.warningText.gameObject.SetActive(true);

        }

        if (!isCloud)
        Destroy(gameObject);
    }

  
}
