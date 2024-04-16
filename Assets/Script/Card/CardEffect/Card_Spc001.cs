using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Spc001 : MonoBehaviour, CardData
{
    public string ID { get; set; } = "Spc1";
    public int cost { get; set; } = 1;
    public void Effect()
    {
        Debug.Log("3번 특수 타입 발동");
    }
}
