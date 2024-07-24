using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboManager : MonoBehaviour
{
    public static ComboManager instance;

    [SerializeField] TMP_Text comboText;
    [SerializeField] Animator comboAnimator;  // 애니메이터 추가

    public int _currentCombo;

    public int currentCombo
    {
        get { return _currentCombo; }
        set
        {
            _currentCombo = value;
            comboText.text = _currentCombo.ToString();
            comboAnimator.SetTrigger("ComboChanged");  // 값이 변경될 때 애니메이션 트리거
        }
    }

    void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (currentCombo <= 0) 
        {
            comboText.gameObject.SetActive(false);
        }
        else
        {
            comboText.gameObject.SetActive(true);

        }
    }
}
