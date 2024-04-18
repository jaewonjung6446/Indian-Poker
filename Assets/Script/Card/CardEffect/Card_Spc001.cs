using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Spc001 : MonoBehaviour, CardData
{
    public string ID { get; set; } = "Spc001";
    public int cost { get; set; } = 1;
    public void Effect()
    {
        GameManager.gameManager.currentPot = GameManager.gameManager.currentPot =3;
        Debug.Log("Æ¯¼ö, ÇöÀç ÆÌ: " + GameManager.gameManager.currentPot);
    }
}
