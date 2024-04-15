using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager cardManager;
    public GameObject[] cardPrefabs;
    private void Awake()
    {
        cardManager = this;
    }
    public CardData GetCardData(string ID)
    {
        foreach (GameObject card in cardPrefabs){
            CardData cardData = card.GetComponent<CardData>();
            if(cardData != null && cardData.ID + "(Clone)" == ID) {
                return cardData;
            }
        }
        return null;
    }
}
