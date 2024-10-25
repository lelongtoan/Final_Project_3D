using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] public ItemSlot itemSlot;
    [SerializeField] GameObject dragItem;
    public static DragAndDrop dragAndDrop { get; set; }
    private void Awake()
    {
        dragAndDrop = this;
        itemSlot = new ItemSlot();
    }

    public bool CheckItemConvert(Item item, int count = 5)
    {
        if (item.stackable)
        {
            return itemSlot.item == item && itemSlot.count >= count;
        }
        return itemSlot.item == item;
    }

    internal void OnClick(ItemSlot itemSlot)
    {
        if (this.itemSlot.item == null)
        {
            if (itemSlot.item == null)
            {
                return;
            }
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();
        }
        else if (this.itemSlot.item == itemSlot.item && itemSlot.item.stackable == true)
        {
            itemSlot.count += this.itemSlot.count;
            this.itemSlot.Clear();
        }
        else
        {
            Item item = itemSlot.item;
            int toolDurability = itemSlot.toolDurability;
            int count = itemSlot.count;
            itemSlot.Copy(this.itemSlot);
            this.itemSlot.Set(item, toolDurability, count);
        }
    }
    internal void UnStack(ItemSlot itemSlot)
    {
        if (this.itemSlot == null)
        {
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();
        }
        else
        {
            Item item = itemSlot.item;
            int toolDurability = itemSlot.toolDurability;
            int count = itemSlot.count / 2;
            itemSlot.count -= count;
            this.itemSlot.Set(item, toolDurability, count);
        }
    }
}
