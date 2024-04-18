using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Dfd001 : MonoBehaviour, CardData
{
    public string ID { get; set; } = "Dfd001";
    public int cost { get; set; } = 1;
    public void Effect()
    {
        GameManager.gameManager.currentPot = GameManager.gameManager.currentPot / 2;
        Debug.Log("¹æ¾î, ÇöÀç ÆÌ: " + GameManager.gameManager.currentPot);
    }
}