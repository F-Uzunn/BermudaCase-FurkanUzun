using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerAnims : MonoBehaviour
{
    [field: SerializeField] private PlayerManager playerManager;

    [field: SerializeField] private GameManager gameManager;

    [field: SerializeField] private Animator playerAnim;
    [field: SerializeField] private Animator moneyUpAnim;
    [field: SerializeField] private Animator moneyDownAnim;

    [field: SerializeField] public TextMeshPro moneyUpText;
    [field: SerializeField] public TextMeshPro moneyDownText;

    [field: SerializeField] private float timerDelay;
    [field: SerializeField] private bool isChanging;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStart, OnStart);
        EventManager.AddHandler(GameEvent.OnFinish, OnFinish);
        EventManager.AddHandler(GameEvent.OnFail, OnFail);
        EventManager.AddHandler(GameEvent.OnWin, OnWin);
        EventManager.AddHandler(GameEvent.OnStateChange, OnStateChange);
        EventManager.AddHandler(GameEvent.OnTextAnimPlay, OnTextAnimPlay);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStart, OnStart);
        EventManager.RemoveHandler(GameEvent.OnFinish, OnFinish);
        EventManager.RemoveHandler(GameEvent.OnFail, OnFail);
        EventManager.RemoveHandler(GameEvent.OnWin, OnWin);
        EventManager.RemoveHandler(GameEvent.OnStateChange, OnStateChange);
        EventManager.RemoveHandler(GameEvent.OnTextAnimPlay, OnTextAnimPlay);
    }
    private void OnStart()
    {
        UpdateAnimTriggers(playerAnim, "walk");
        UpdateAnimBlendStateFloat();
    }
    private void OnFinish()
    {
        UpdateAnimTriggers(playerAnim, "idle");
    }
    private void OnFail()
    {
        UpdateAnimTriggers(playerAnim, "fail");
    }
    private void OnWin()
    {
        if (gameManager.GetStateIndex() < 1)
            UpdateAnimTriggers(playerAnim, "sadWin");
        else
            UpdateAnimTriggers(playerAnim, "happyWin");
    }

    private void OnStateChange(object happyChangeOrSad)   //if 1 happy , if -1 sad  , if 0 default
    {
        UpdateAnimBlendStateFloat();

        if ((int)happyChangeOrSad == 1)
        {
            if (isChanging)
                UpdateAnimTriggers(playerAnim, "ifchanging");
            UpdateAnimTriggers(playerAnim, "happySpin");
        }
        else if ((int)happyChangeOrSad == -1)
        {
            if (isChanging)
                UpdateAnimTriggers(playerAnim, "ifchanging");
            UpdateAnimTriggers(playerAnim, "sadSpin");
        }
    }
    private void OnTextAnimPlay(object animString)
    {
        timerDelay = 0f;

        switch ((string)animString)
        {
            case "down":
                moneyDownText.text = gameManager.MinusMoneyAmount.ToString() + "$";
                moneyDownAnim.Play("MoneyDownAnim", -1, 0);
                break;

            case "up":
                moneyUpText.text = "+" + gameManager.PlusMoneyAmount.ToString() + "$";
                moneyUpAnim.Play("MoneyUpAnim", -1, 0);
                break;

            default:
                break;
        }
    }

    private void Update()
    {
        if (timerDelay >= 1f)
        {
            gameManager.PlusMoneyAmount = 0;
            gameManager.MinusMoneyAmount = 0;
        }
        timerDelay += Time.deltaTime;
    }

    private void UpdateAnimBlendStateFloat()
    {
        playerAnim.SetFloat("Blend", gameManager.PlayerBar.fillAmount);
    }

    private void UpdateAnimTriggers(Animator anim, string animString)
    {
        anim.SetTrigger(animString);
    }

    //animation event void onstatechange
    public void Index(int i)
    {
        playerManager.Effect(i);
    }

    //if change anim is already playing this will trigger anim out to walking
    public void SetTrigger()
    {
        UpdateAnimTriggers(playerAnim, "ifchanging");
    }

    //change anim is playing or not
    public void ChangeBool(int stat)
    {
        if (stat == -1)
            isChanging = false;
        else
            isChanging = true;
    }
}
