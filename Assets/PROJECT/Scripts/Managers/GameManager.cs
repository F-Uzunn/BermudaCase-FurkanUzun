using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class GameManager : InstanceManager<GameManager>
{
    [field: SerializeField] private GameData gamedata;
    [field: SerializeField] private Image playerBar { get; set; }
    [field: SerializeField] private TextMeshProUGUI stateText { get; set; }

    [field: SerializeField] private Gradient gradient;
    [field: SerializeField] private int collectedTotalMoneyAmount { get; set; }
    [field: SerializeField] private int plusMoneyAmount { get; set; }
    [field: SerializeField] private int minusMoneyAmount { get; set; }
    private bool isPlayerStarted { get; set; }

    public Image PlayerBar
    {
        get { return playerBar; }
    }
    public bool IsPlayerStarted
    {
        get { return isPlayerStarted; }
    }
    public TextMeshProUGUI StateText
    {
        get { return stateText; }
    }
    public int CollectedTotalMoneyAmount
    {
        get { return collectedTotalMoneyAmount; }
        set { collectedTotalMoneyAmount = value; }
    }
    public int PlusMoneyAmount
    {
        get { return plusMoneyAmount; }
        set { plusMoneyAmount = value; }
    }
    public int MinusMoneyAmount
    {
        get { return minusMoneyAmount; }
        set { minusMoneyAmount = value; }
    }
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStart, OnStart);
        EventManager.AddHandler(GameEvent.OnPlayerBarUpdate, OnPlayerBarUpdate);
        EventManager.AddHandler(GameEvent.OnUpdateMoney, OnUpdateMoney);
        EventManager.AddHandler(GameEvent.OnSave, OnSave);
        EventManager.AddHandler(GameEvent.OnScaleText, OnScaleText);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStart, OnStart);
        EventManager.RemoveHandler(GameEvent.OnPlayerBarUpdate, OnPlayerBarUpdate);
        EventManager.RemoveHandler(GameEvent.OnUpdateMoney, OnUpdateMoney);
        EventManager.RemoveHandler(GameEvent.OnSave, OnSave);
        EventManager.RemoveHandler(GameEvent.OnScaleText, OnScaleText);
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Vibration.Init();
        playerBar.fillAmount = 0.25f;
    }

    private void Start()
    {
        //loading data
#if !UNITY_EDITOR
        OnLoad();
#endif
    }
    private void OnStart()
    {
        isPlayerStarted = true;
    }
    private void OnPlayerBarUpdate(object val)
    {
        int tempStateIndex = GetStateIndex();

        playerBar.fillAmount += (float)val;

        if (CheckIfGameOver())
            EventManager.Broadcast(GameEvent.OnFail);

        GetStateIndex();

        int currentStateIndex = GetStateIndex();

        if (tempStateIndex != currentStateIndex)
        {
            int x = 0;
            if (IsEnumIncreasing(tempStateIndex, currentStateIndex))
            {
                //sevinme rotate
                x = 1;
            }
            else if (IsEnumDecreasing(tempStateIndex, currentStateIndex))
            {
                //üzülme rotate
                x = -1;
            }
            EventManager.Broadcast(GameEvent.OnStateChange, x);
        }
    }
    private void OnUpdateMoney(object moneyAmount, object plusOrMinus)
    {
        switch ((string)plusOrMinus)
        {
            case "up":
                plusMoneyAmount += (int)moneyAmount;
                break;

            case "down":
                minusMoneyAmount += (int)moneyAmount;
                break;

            default:
                break;
        }
        collectedTotalMoneyAmount += (int)moneyAmount;
    }

    private void OnSave()
    {
        SaveManager.SaveData(gamedata);
    }

    private void OnLoad()
    {
        SaveManager.LoadData(gamedata);
    }

    private void OnScaleText()
    {
        ScaleObject(stateText);
    }

    private void Update()
    {
        UpdateImageandTextColor();
    }
    private void UpdateImageandTextColor()
    {
        //player bar and text color change
        Color newColor = gradient.Evaluate(playerBar.fillAmount);
        playerBar.color = newColor;
        stateText.color = newColor;
    }

    public int GetStateIndex()
    {
        if (playerBar.fillAmount < 0.35f)
            return 0;
        else if (playerBar.fillAmount >= 0.35f && playerBar.fillAmount < 0.7f)
            return 1;
        else
            return 2;
    }
    private bool CheckIfGameOver()
    {
        if (playerBar.fillAmount <= 0)
            return true;
        else
            return false;
    }
    public bool IsEnumIncreasing(int temp, int current)
    {
        return temp < current;
    }

    public bool IsEnumDecreasing(int temp, int current)
    {
        return temp > current;
    }

    public void ScaleObject(Component type)
    {
        if (type is Image image)
        {
            //image.transform.DOScale(Vector3.one * 1.1f, 0.1f).OnComplete(() => { image.transform.DOScale(Vector3.one, 0.1f); });
        }

        if (type is TextMeshProUGUI textmeshPro)
        {
            textmeshPro.transform.DOScale(Vector3.one * 1.2f, 0.1f).OnComplete(() =>
            {
                textmeshPro.transform.DOScale(Vector3.one, 0.1f);
            }
            );
        }
    }
}
