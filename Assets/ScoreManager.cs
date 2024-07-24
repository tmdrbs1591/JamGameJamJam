using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public float currentScore;

    [SerializeField] TMP_Text scoreText;

    [SerializeField] TMP_Text clearPanelScoreText;
    [SerializeField] TMP_Text clearPanelHitText;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        currentScore = 0;
        StartCoroutine(ScoreAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "����:" + currentScore.ToString();
        clearPanelHitText.text = "�ִ� ��Ʈ:" + ComboManager.instance.currentCombo.ToString();
    }

    public IEnumerator ScoreAnimation()
    {
        float score;
        score = 0;

        while (true)
        {
            score += 10;
            if (score >= currentScore)
            {
                score = currentScore;
                clearPanelScoreText.text = "����:" + score.ToString();
                break;
            }
            clearPanelScoreText.text = "����:" + score.ToString();
            yield return new WaitForSeconds(0.02f);

        }
    }
}
