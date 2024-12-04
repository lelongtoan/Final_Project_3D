using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnemyDrop : MonoBehaviour
{
    public List<ItemSlot> itemSlots;
    public int quantity = 1;
    public void Set(Drop drop)
    {
        
        for (int i = 0; i < itemSlots.Count; i++)
        {
            float rate = Random.Range(1, 101);
            if (itemSlots[i].item.rateDrop <= rate) 
            {
                ItemSlot slot = new ItemSlot();
                slot.item = itemSlots[i].item;
                slot.toolDurability = Random.Range(1, itemSlots[i].count);
                drop.itemSlots.Add(itemSlots[i]);
            }
        }
    }
}
