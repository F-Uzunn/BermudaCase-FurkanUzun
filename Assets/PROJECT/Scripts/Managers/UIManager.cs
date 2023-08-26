using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] private GameData gamedata;
    [field: SerializeField] private GameManager gameManager;

    [field: SerializeField] private GameObject swipeTutorialObject;
    [field: SerializeField] private GameObject gamePanel;
    [field: SerializeField] private GameObject winPanel;
    [field: SerializeField] private GameObject failPanel;

    [field: SerializeField] private TextMeshProUGUI moneyText;
    [field: SerializeField] private TextMeshProUGUI collectedMoneyText;
    [field: SerializeField] private TextMeshProUGUI levelText;

    [field: SerializeField] private Image moneyImage;

    private void Awake()
    {
        OnUpdateMoneyText();
    }

    private void Start()
    {
        levelText.text = "LEVEL " + gamedata.FakeLevelIndex.ToString();
    }
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStart, OnStart);
        EventManager.AddHandler(GameEvent.OnFinish, OnFinish);
        EventManager.AddHandler(GameEvent.OnFail, OnFail);
        EventManager.AddHandler(GameEvent.OnWin, OnWin);
        EventManager.AddHandler(GameEvent.OnUpdateMoneyText, OnUpdateMoneyText);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStart, OnStart);
        EventManager.RemoveHandler(GameEvent.OnFinish, OnFinish);
        EventManager.RemoveHandler(GameEvent.OnFail, OnFail);
        EventManager.RemoveHandler(GameEvent.OnWin, OnWin);
        EventManager.RemoveHandler(GameEvent.OnUpdateMoneyText, OnUpdateMoneyText);
    }

    private void OnStart()
    {
        swipeTutorialObject.SetActive(false);
    }
    private void OnFinish()
    {
        gamePanel.SetActive(false);
    }
    private void OnFail()
    {
        failPanel.SetActive(true);
    }
    private void OnWin()
    {
        collectedMoneyText.text = gameManager.CollectedTotalMoneyAmount.ToString();
        winPanel.SetActive(true);
    }
    void OnUpdateMoneyText()
    {
        moneyText.text = gamedata.TotalMoney.ToString("0.0");
    }
    public void NextButton()
    {
        gamedata.TotalMoney += gameManager.CollectedTotalMoneyAmount;
        gamedata.LevelIndex++;
        gamedata.FakeLevelIndex++;
        EventManager.Broadcast(GameEvent.OnSave);

        SceneManager.LoadScene(gamedata.LevelIndex);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
