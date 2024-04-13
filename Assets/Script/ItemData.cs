using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemData : MonoBehaviour
{
    public List<Item> Items;
    public static ItemData Instance;
    public void Awake()
    {
        Instance = this;
    }
    public void InitializeItems(GameObject put_In,string des, int maxuse)
    {
        Items = new List<Item>();
    }
    public void AddItem(GameObject put_In, string des, int maxuse){
        Item newItem = new Item(put_In, des, maxuse);
        Items.Add(newItem);
    }
}
