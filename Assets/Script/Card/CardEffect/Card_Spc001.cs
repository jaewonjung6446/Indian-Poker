using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Spc001 : MonoBehaviour, CardData
{
    public string ID { get; set; } = "Spc001";
    public int cost { get; set; } = 1;
    public void Effect()
    {
        GameManager.gameManager.bet = 3;
        Debug.Log("Ư��, ���� ���� ���� ��: " + GameManager.gameManager.bet);
    }
}
