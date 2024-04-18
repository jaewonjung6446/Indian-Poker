using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Dfd001 : MonoBehaviour, CardData
{
    public string ID { get; set; } = "Dfd001";
    public int cost { get; set; } = 1;
    public void Effect()
    {
        GameManager.gameManager.bet /= 2;
        Debug.Log("방어, 현재 배팅 가능 액: " + GameManager.gameManager.currentPot);
    }
}