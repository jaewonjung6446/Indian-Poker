using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public GameObject Obj;
    public string Description;
    public int MaxUse;
    public Item(GameObject a, string b, int c)
    {
        Obj = a;
        Description = b;
        MaxUse = c;
    }
}