using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ItemInShop
{
    public Item itemShop;
    public int priceItem;
    public bool locked;
    public Quest questShop;
}
[CreateAssetMenu(menuName = "ListItemShop")]
public class ListItemShop : ScriptableObject
{
    public List<ItemInShop> itemsInShop;
}
