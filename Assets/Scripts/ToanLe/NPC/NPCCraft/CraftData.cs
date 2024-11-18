using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Craft/CraftData")]
public class CraftData : ScriptableObject
{
    public string nameCraft;
    public int resultCount;
    public Item resultItem;
    public List<ItemSlot> requiredItems = new List<ItemSlot>();
}
