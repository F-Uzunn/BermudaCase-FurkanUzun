using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    [field: SerializeField] private SplineFollower splineFollower;

    [field: SerializeField] private GameManager gameManager;

    [field: SerializeField] private PlayerStateEnum playerState;

    [field: SerializeField] private List<GameObject> stateObjects;
    [field: SerializeField] private List<ParticleSystem> stateChangeParticles;

    [field: SerializeField] private float rotationAngle;

    private void Awake()
    {
        splineFollower = GetComponent<SplineFollower>();
        gameManager = FindObjectOfType<GameManager>();

        ChangeCharacterStateObject(0); //default 0
    }
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStart, OnStart);
        EventManager.AddHandler(GameEvent.OnFinish, OnFinish);
        EventManager.AddHandler(GameEvent.OnFail, OnFail);
        EventManager.AddHandler(GameEvent.OnWin, OnWin);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStart, OnStart);
        EventManager.RemoveHandler(GameEvent.OnFinish, OnFinish);
        EventManager.RemoveHandler(GameEvent.OnFail, OnFail);
        EventManager.RemoveHandler(GameEvent.OnWin, OnWin);
    }
    private void OnStart()
    {
        splineFollower.enabled = true;
    }
    private void OnFinish()
    {
        splineFollower.enabled = false;
    }

    private void OnFail()
    {
        splineFollower.enabled = false;
    }
    private void OnWin()
    {
        splineFollower.enabled = false;
    }

    private void ChangeCharacterStateObject(int enumIndex)
    {
        foreach (var item in stateObjects)
        {
            item.SetActive(false);
        }
        stateObjects[enumIndex].SetActive(true);
    }

    public void Effect(int index)
    {
        stateChangeParticles[index].Play();

        int enumIndex = gameManager.GetStateIndex();
        playerState = (PlayerStateEnum)enumIndex;  // change enum type
        ChangeCharacterStateObject(enumIndex);

        gameManager.StateText.text = playerState.ToString().ToUpper();

        EventManager.Broadcast(GameEvent.OnScaleText);
    }
}
