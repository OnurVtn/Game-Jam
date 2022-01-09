using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject EngGameMenuSuccess;
    [SerializeField] GameObject EngGameMenuFail;
    [SerializeField] GameObject CameraMainMenu;
    [SerializeField] GameObject CameraEndGame;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.onGameStateChanged += GameManager_onGameStateChanged;
    }

    private void GameManager_onGameStateChanged(GameStates GameState)
    {
        if (GameState == GameStates.Started)
        {
            StartGame();
        }
        else if(GameState == GameStates.EndGameFinished)
        {
            EndGameFinish();
        }
        else if (GameState == GameStates.RestartGame)
        {
            RestartGame();
        }
    }

    private void StartGame()
    {
        MainMenu.SetActive(false);
        CameraMainMenu.SetActive(false);
    }

    private void EndGameFinish()
    {
        //MainMenu.SetActive(false);
        CameraEndGame.SetActive(true);

        if (GameManager.Instance.getGameSuccessState())
        {
            EngGameMenuSuccess.SetActive(true);
            Debug.Log("Game Success!");
        }
        else
        {
            EngGameMenuFail.SetActive(true);
            Debug.Log("Game Fail!");
        }
    }

    private void RestartGame()
    {
        MainMenu.SetActive(true);
        CameraMainMenu.SetActive(true);
        CameraEndGame.SetActive(false);
        EngGameMenuFail.SetActive(false);
        EngGameMenuSuccess.SetActive(false);

    }

}
