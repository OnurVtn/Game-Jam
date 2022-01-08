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
    private int previousMatchingAssetIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        currentClothes = Clothes.Nude;
        currentShoes = Shoes.Nude;

        UpdateClothing();

    }

    // Update is called once per frame
    void Update()
    {

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
                if(previousMatchingAssetIndex != i)
                {
                    if (previousMatchingAssetIndex != -1)
                        playerLevelObjects[previousMatchingAssetIndex].gameObject.SetActive(false);

                    playerLevelObjects[i].gameObject.SetActive(true);
                    playerLevelObjects[i].transform.DOPunchScale(Vector3.one * 0.5f, 0.25f);
                    previousMatchingAssetIndex = i;

                }

                break;
            }
        }
    }

}
