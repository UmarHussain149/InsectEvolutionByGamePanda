using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{

    Home,
    Gameplay,
    Complete,
    Fail,
    Revive

}

public class GameController : MonoBehaviour
{

    public static GameState gameState = GameState.Home;
    public static Action<GameState> changeGameState;

    public static event Action onHome, onGameplay, onLevelComplete, onLevelFail, OnRevive;


    void Awake()
    {
        changeGameState += ChangeGameState;
        Mediation_Manager.instance?.RequestNativeBanner();
    }
    private void Start()
    {
        changeGameState?.Invoke(GameState.Home);
    }
    void ChangeGameState(GameState state)
    {


        gameState = state;
        switch (gameState)
        {
            case GameState.Home:
                onHome?.Invoke();
                break;

            case GameState.Gameplay:
                onGameplay?.Invoke();
                Mediation_Manager.instance.HideNativeBanner();
                break;

            case GameState.Complete:
                onLevelComplete?.Invoke();
                break;
            case GameState.Revive:
                OnRevive?.Invoke();
                break;

            case GameState.Fail:
                {
                    onLevelFail?.Invoke();
                    break;
                }
        }
    }


    void OnDestroy()
    {
        onLevelComplete = null;
        changeGameState = null;
        onLevelFail = null;
        onGameplay = null;
        onHome = null;
        OnRevive = null;
    }

}