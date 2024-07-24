using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] Transform attackPos;

    [SerializeField] Vector2 attackBoxSize;

    [SerializeField] public Animator anim;
    [SerializeField] Rigidbody2D rigid;

    [SerializeField] float jumpPower;
    [SerializeField] GameObject attackeft;
    [SerializeField] GameObject attackeft2;
    [SerializeField] GameObject rainPtc;

    [SerializeField] float maxHp;
    [SerializeField] public float currentHp;

    [SerializeField] Slider hpBar;

    [SerializeField] GameObject DiePanel;
    [SerializeField] GameObject RedPanel;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = currentHp / maxHp;

        if (currentHp <= 0)
        {
            DiePanel.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        if (StateManager.instance.currentCursorState == CurserState.Hamer)
        {
            StartCoroutine(EffectCor());
            Damage();
            AudioManager.instance.PlaySound(transform.position, 0, Random.Range(1f, 1f), 1);
            AudioManager.instance.PlaySound(transform.position, 1, Random.Range(1f, 1f), 0.01f);
            StateManager.instance.expressionImage.sprite = StateManager.instance.hamerSprite;
            CameraShake.instance.Shake();
            Invoke("IdleSprite", 0.8f);
            anim.SetTrigger("isAttack");
            Debug.Log("Asd");
        }
        if (StateManager.instance.currentCursorState == CurserState.SadPT)
        {
            anim.SetTrigger("isSad");
            StartCoroutine(Rain());
        }
    }
    public void RedPanelMT()
    {
        StartCoroutine (RedPanelat());
    }
    IEnumerator RedPanelat()
    {
        RedPanel.SetActive(true);
        CameraShake.instance.Shake();
        yield return new WaitForSeconds(0.3f);
        RedPanel.SetActive(false);

    }


    IEnumerator Rain()
    {
        rainPtc.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        rainPtc.SetActive(false);

    }
    IEnumerator StopCor()
    {
        Debug.Log("Set timeScale 0");
        Time.timeScale = 0.13f;
        yield return new WaitForSecondsRealtime(0.3f);
        Time.timeScale = 1;
        Debug.Log("set TimeScale 1");
    }

    void IdleSprite()
    {
        StateManager.instance.expressionImage.sprite = StateManager.instance.idleSprite;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackPos.position, attackBoxSize);
    }

    void Damage()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackPos.position, attackBoxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider != null)
            {
                if (collider.tag == "Enemy")
                {
                    Debug.Log("d");
                    StartCoroutine(StopCor());
                    collider.GetComponent<Enemy>().Destroy();
                }
            }
        }
    }

    public void Jump()
    {
        Vector2 velocity = rigid.velocity;
        // y 속도를 점프 힘으로 설정하여 위로 점프하도록 합니다.
        velocity.y = jumpPower;
        // 새로운 속도를 Rigidbody2D에 적용합니다.
        rigid.velocity = velocity;
    }

    IEnumerator EffectCor()
    {
        if (attackeft.activeSelf)
        {
            // attackeft가 이미 활성화된 상태이면 attackeft2를 활성화
            attackeft2.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            attackeft2.SetActive(false);
        }
        else
        {
            // attackeft를 활성화
            attackeft.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            attackeft.SetActive(false);
        }
    }
}
