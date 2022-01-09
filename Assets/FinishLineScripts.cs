using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineScripts : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlesLeft;
    [SerializeField] private ParticleSystem particlesRight;

    private void Start()
    {
        particlesLeft.Stop();
        particlesRight.Stop();

        GameManager.onGameStateChanged += GameManager_onGameStateChanged;

    }

    private void GameManager_onGameStateChanged(GameStates GameState)
    {
        if (GameState == GameStates.RestartGame)
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        particlesLeft.Stop();
        particlesRight.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PinkPlayer"))
        {
            GameManager.Instance.FinishGame();
            particlesLeft.Play();
            particlesRight.Play();
        }
       
    }
}
