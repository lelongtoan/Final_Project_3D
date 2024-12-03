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
    public void Add(Item item,int toolDurability = 100, int count = 1)
    {
        if (item.stackable == true)
        {
            ItemSlot itemSlot = slots.Find(c => c.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += count;
                Debug.Log("Success");
                return ;
            }
            else
            {
                itemSlot = slots.Find(c => c.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.toolDurability = toolDurability;
                    itemSlot.count = count;
                    Debug.Log("Success");
                    return ;
                }
            }
        }
        else
        {
            ItemSlot itemSlot = slots.Find(c => c.item == null);
            if (itemSlot != null)
            {
                itemSlot.item = item;
                itemSlot.toolDurability = toolDurability;
                itemSlot.count = count;
                Debug.Log("Success");
                return ;
            }
        }
        Debug.Log("Inventory Full");
    }
    public bool CheckFull()
    {

        bool ckeck = slots.Find(c => c.item == null) != null ? true : false;
        if(!ckeck)
        {
            GameInstance.instance.gameReport.SetReport("Kho Đồ Đã Hết Chỗ ! Gỡ Trang Bị Thất Bại");
        }
        return ckeck;
    }
    public bool CheckItem(Item item)
    {
        return slots.Find(c => c.item == item) != null ? true : false;
    }
    public bool CheckItemQuantity(Item item,int count)
    {
        int itemCount = slots.Find(c => c.item == item).count;
        return count > itemCount ? false : true;
    }
    public void Delete(int id)
    {
        slots[id].Clear();
    }
    public bool CheckCraft(CraftData craftData)
    {
        for (int i = 0; i < craftData.requiredItems.Count; i++)
        {
            ItemSlot temp = craftData.requiredItems[i];
            if (!CheckItem(temp.item) || !CheckItemQuantity(temp.item, temp.count))
            {
                return false;
            }
        }
        return true;
    }
}
