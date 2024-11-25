using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ItemInShop
{
    public string nameItem;
    public int priceItem;
    public bool locked;
    public Item itemShop;
}
[CreateAssetMenu(menuName = "ListItemShop")]
public class ListItemShop : ScriptableObject
{
    public List<ItemInShop> itemsInShop;
}
