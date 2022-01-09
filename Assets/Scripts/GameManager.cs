using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance => instance ?? (instance = FindObjectOfType<GameManager>());

    private bool pinkPlayerStatus;
    private bool bluePlayerStatus;

    private GameStates gameState;

    public static event Action<GameStates> onGameStateChanged;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        gameState = GameStates.Initilazed;
    }

    public void updateGameState(GameStates newState)
    {
        gameState = newState;

        onGameStateChanged?.Invoke(newState);
    }

    public void StartGame()
    {
        updateGameState(GameStates.Started);
    }

    public void FinishGame()
    {
        updateGameState(GameStates.Finished);
    }

    public void EndGameStart()
    {
        updateGameState(GameStates.EndGameStarted);
    }

    public void EndGameFinish()
    {
        updateGameState(GameStates.EndGameFinished);
    }

    public void RestartGame()
    {
        updateGameState(GameStates.RestartGame);
    }

    public void ChangeBluePlayerStatus(bool Status)
    {
        bluePlayerStatus = Status;
    }

    public void ChangePinkPlayerStatus(bool Status)
    {
        pinkPlayerStatus = Status;
    }

    public GameStates getGameState()
    {
        return gameState;
    }

    public bool getGameSuccessState()
    {
        Debug.Log(" bluePlayerStatus = " + bluePlayerStatus + " pinkPlayerStatus = " + pinkPlayerStatus);
        return pinkPlayerStatus && bluePlayerStatus;
    }
}
