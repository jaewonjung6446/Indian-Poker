using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardInfo
{
    cost,
    Effect,
    ID
}

public interface CardData
{
    int cost { get; set; }
    string ID { get; set; }
    void Effect();
}