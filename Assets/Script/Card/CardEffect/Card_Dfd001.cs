using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Dfd001 : MonoBehaviour, CardData
{
    public string ID { get; set; } = "Dfd1";
    public int cost { get; set; } = 1;
    public void Effect()
    {
        Debug.Log("2번 방어 타입 발동");
    }
}