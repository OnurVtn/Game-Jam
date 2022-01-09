using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlueController : MonoBehaviour
{

    [SerializeField] private List<Transform> playerLevelObjects;
    [SerializeField] private Animator animator;

    private Clothes currentClothes;
    private Shoes currentShoes;
    private int CurrentMatchingAssetIndex = -1;

    private GameStates gameStates = GameStates.Initilazed;
    // Start is called before the first frame update
    void Start()
    {
        initPlayer();

        GameManager.onGameStateChanged += GameManager_onGameStateChanged;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void initPlayer()
    {
        currentClothes = Clothes.Nude;
        currentShoes = Shoes.Nude;

        UpdateClothing();
    }

    private void GameManager_onGameStateChanged(GameStates GameState)
    {
        if (GameState == GameStates.Started)
        {
            startGame();
        }
        else if (GameState == GameStates.EndGameStarted)
        {
            EndGameStart();
        }
        else if (GameState == GameStates.RestartGame)
        {
            initPlayer();
        }
    }

    public void startGame()
    {
        playerLevelObjects[CurrentMatchingAssetIndex].gameObject.GetComponent<Animator>().SetBool("isGameStart", true);
    }

    public void EndGameStart()
    {
        if (currentClothes == Clothes.Suit && currentShoes == Shoes.SuitShoes)
        {
            playerLevelObjects[CurrentMatchingAssetIndex].gameObject.GetComponent<Animator>().SetBool("isGameFinishedHappy", true);
            GameManager.Instance.ChangeBluePlayerStatus(true);
        }
        else
        {
            playerLevelObjects[CurrentMatchingAssetIndex].gameObject.GetComponent<Animator>().SetBool("isGameFinishedSad", true);
            GameManager.Instance.ChangeBluePlayerStatus(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Suit"))
        {
            currentClothes = Clothes.Suit;
        }
        else if (other.CompareTag("Dress"))
        {
            currentClothes = Clothes.Dress;
        }
        else if (other.CompareTag("SuitShoes"))
        {
            currentShoes = Shoes.SuitShoes;
        }
        else if (other.CompareTag("Heeled"))
        {
            currentShoes = Shoes.Heeled;
        }
        else if (other.CompareTag("Obstacle"))
        {
            currentClothes = Clothes.Nude;
            currentShoes = Shoes.Nude;
        }

        UpdateClothing();

    }


    private void UpdateClothing()
    {
        string activatedAssetTag = "" + currentClothes.ToString()[0] + currentShoes.ToString()[0];
        activatedAssetTag = activatedAssetTag.ToLower();

        for (int i = 0; i < playerLevelObjects.Count; i++)
        {
            if (playerLevelObjects[i].CompareTag(activatedAssetTag))
            {
                if (CurrentMatchingAssetIndex != i)
                {
                    if (CurrentMatchingAssetIndex != -1)
                        playerLevelObjects[CurrentMatchingAssetIndex].gameObject.SetActive(false);

                    playerLevelObjects[i].gameObject.SetActive(true);
                    if (GameManager.Instance.getGameState() == GameStates.Started)
                    {
                        playerLevelObjects[i].gameObject.GetComponent<Animator>().SetBool("isGameStart", true);
                        playerLevelObjects[i].transform.DOPunchScale(Vector3.one * 0.5f, 0.25f);
                    }
                    CurrentMatchingAssetIndex = i;

                }

                break;
            }
        }
    }

}
