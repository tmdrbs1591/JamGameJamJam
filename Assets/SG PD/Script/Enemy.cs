using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;  // ������ �̵� �ӵ�
    private Transform player;  // �÷��̾��� Transform�� �����մϴ�.

    [SerializeField] GameObject bloodPtc;
    [SerializeField] GameObject lightning;
    [SerializeField] GameObject cloud;


    [SerializeField] float addScore;

    [SerializeField] GameObject ScorePlusText;
    public Animator cloudAnim;



    public bool isCloud;
    // �±� "Player"�� ���� ������Ʈ�� ã���ϴ�.

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
        // �÷��̾ ������ ��ũ��Ʈ�� �����մϴ�.
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

        // �÷��̾�� ���� ������ ���� ���͸� ����մϴ�.
        Vector3 directionToPlayer = player.position - transform.position;

        // ������ ���ʰ� ������ ���͸� ����մϴ�.
        Vector3 leftDirection = -transform.right;
        Vector3 rightDirection = transform.right;

        // �÷��̾ ������ ���ʿ� �ִ��� �����ʿ� �ִ��� Ȯ���մϴ�.
        float dotProductLeft = Vector3.Dot(directionToPlayer, leftDirection);
        float dotProductRight = Vector3.Dot(directionToPlayer, rightDirection);

        if (dotProductLeft > dotProductRight)
        {
            // �÷��̾ ���ʿ� ���� �� �������� �̵�
            MoveInDirection(leftDirection);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            // �÷��̾ �����ʿ� ���� �� ���������� �̵�
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
        // �������� �̵��մϴ�.
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
