using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Begin,
    InGame
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private GameObject mainMenuUI, inGameUI;

    private GameState _currentState;
    public GameState CurrentState => _currentState;

    public static Action<GameState> OnSwitchState;
    public static Action OnPlayerDied;


    private void Start()
    {
        OnSwitchState?.Invoke(GameState.Begin);
    }

    private void OnEnable()
    {
        OnSwitchState += SwitchState;
        OnSwitchState += SwitchUI;
    }

    private void OnDisable()
    {
        OnSwitchState -= SwitchState;
        OnSwitchState -= SwitchUI;
    }

    private void SwitchState(GameState state)
    {
        _currentState = state;
    }

    private void SwitchUI(GameState state)
    {
        switch (state)
        {
            case GameState.Begin:
                mainMenuUI.SetActive(true);
                inGameUI.SetActive(false);
                return;
            
            case GameState.InGame:
                mainMenuUI.SetActive(false);
                inGameUI.SetActive(true);
                return;
        }
    }
}
