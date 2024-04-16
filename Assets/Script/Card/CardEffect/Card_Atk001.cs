using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Atk001 : MonoBehaviour, CardData
{
    public string ID { get; set; } = "Atk1";
    public int cost { get; set; } = 1;
    public void Effect()
    {
        Debug.Log("1번 공격 타입 발동");
    }
}
