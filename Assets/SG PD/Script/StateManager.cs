using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CurserState
{
    Hamer,
    Ghost,
    Fan,
    SadPT,
}

public class StateManager : MonoBehaviour
{
    [SerializeField] public Image expressionImage;
    [SerializeField] public Sprite idleSprite;
    [SerializeField] public Sprite hamerSprite;
    [SerializeField] public Sprite GhostSprite;
    [SerializeField] public Sprite FanSprite;

    public TMP_Text warningText;


    public static StateManager instance;

    public CurserState currentCursorState;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurserStateHamerChange();
            AudioManager.instance.PlaySound(transform.position, 4, Random.Range(1.2f, 1.2f), 1);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurserStateGhostChange();
            AudioManager.instance.PlaySound(transform.position, 4, Random.Range(1.2f, 1.2f), 1);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurserStateFanChange();
            AudioManager.instance.PlaySound(transform.position, 4, Random.Range(1.2f, 1.2f), 1);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CurserStateSadPTChange();
            AudioManager.instance.PlaySound(transform.position, 4, Random.Range(1.2f, 1.2f), 1);

        }
    }

    public void CurserStateHamerChange()
    {
        currentCursorState = CurserState.Hamer;
    }
    public void CurserStateGhostChange()
    {
        currentCursorState = CurserState.Ghost;
    }
    public void CurserStateFanChange()
    {
        currentCursorState = CurserState.Fan;
    }
    public void CurserStateSadPTChange()
    {
        currentCursorState = CurserState.SadPT;
    }
}
