using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [field: SerializeField] private GameManager gameManager;
    [field: SerializeField] private Transform playerTransform;
    [field: SerializeField] private float leftMovementLimit = -1.75f;
    [field: SerializeField] private float rightMovementLimit = 1.75f;
    [field: SerializeField] private float movementSensitivity = 100f;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnFail, OnFail);
        EventManager.AddHandler(GameEvent.OnWin, OnWin);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnFail, OnFail);
        EventManager.RemoveHandler(GameEvent.OnWin, OnWin);
    }
    private void OnFail()
    {
        this.enabled = false;
    }
    private void OnWin()
    {
        this.enabled = false;
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 tempPosition = playerTransform.localPosition;
        tempPosition.x = Mathf.Clamp(tempPosition.x + (eventData.delta.x / movementSensitivity), leftMovementLimit, rightMovementLimit);
        playerTransform.localPosition = tempPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(gameManager.IsPlayerStarted == false)
            EventManager.Broadcast(GameEvent.OnStart);
    }
}
