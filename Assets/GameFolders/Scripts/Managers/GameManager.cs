using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public bool IsRunning { get; private set; }
    public Action OnGameStart;
    public Action OnSuccessfulDelivery;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SuccessfulDelivery()
    {
        //Points to player
        OnSuccessfulDelivery?.Invoke();
    }

    public void StartGame()
    {
        IsRunning = true;
        OnGameStart?.Invoke();
    }

    private void EndGame()
    {

    }

}
