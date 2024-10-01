using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[Serializable]
public class ItemSlot
{
    public Item item;
    public int toolDurability;
    public int count;
    public void Set(Item item, int toolDurability, int count)
    {
        this.item = item;
        this.toolDurability = toolDurability;
        this.count = count;
    }
    public void Copy(ItemSlot itemSlot)
    {
        item = itemSlot.item;
        toolDurability = itemSlot.toolDurability;
        count = itemSlot.count;
    }
    public void Clear()
    {
        item = null;
        toolDurability = 0;
        count = 0;
    }
}
[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;
    public static ItemContainer itemContainer { get; set; }

    private void Awake()
    {
        itemContainer = this;
    }
    public bool Add(Item item, int count)
    {
        if (item.stackable == true)
        {
            ItemSlot itemSlot = slots.Find(c => c.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += count;
                return true;
            }
            else
            {
                itemSlot = slots.Find(c => c.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.toolDurability = 100;
                    itemSlot.count = count;
                    return true;
                }
            }
        }
        else
        {
            ItemSlot itemSlot = slots.Find(c => c.item == null);
            if (itemSlot != null)
            {
                itemSlot.item = item;
                itemSlot.toolDurability = 100;
                itemSlot.count = count;
                return true;
            }
        }
        Debug.Log("Inventory Full");
        return false;
    }

    internal bool CheckFreeSpace()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                return true;
            }
        }
        return false;
    }

    internal bool CheckItem(ItemSlot itemSlot)
    {
        ItemSlot item = slots.Find(c => c.item == itemSlot.item);
        if (item == null)
        {
            return false;
        }
        if (itemSlot.item.stackable)
        {
            return item.count >= itemSlot.count;
        }
        return true;
    }
    public void CraftRemove(Item item, int count)
    {
        if (item.stackable)
        {
            ItemSlot itemSlot = slots.Find(c => c.item == item);
            if (itemSlot == null)
            {
                return;
            }
            itemSlot.count -= count;
            if (itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }
        else
        {
            while (count > 0)
            {
                count -= 1;
                ItemSlot itemSlot = slots.Find(c => c.item == item);
                if (itemSlot == null) { break; }
                itemSlot.Clear();
            }
        }
    }
}
