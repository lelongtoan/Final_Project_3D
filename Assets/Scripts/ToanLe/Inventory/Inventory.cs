using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : ItemManager
{
    private void Update()
    {
        SetIndex();
        ShowInventory();
    }
    public override void OnClick(int id)
    {
        if (inventory.slots[id].item != null || ItemD.Instance.itemSlot.count > 0)
            if (ItemD.Instance.itemSlot.count <= 0) 
            {
                InGameMenu.inGameMenu.SetInfoItem();
                ItemD.Instance.SetItem(inventory.slots[id],id);
                Debug.Log("Info");
            }
            else
            {
                Debug.Log("moveitem");
                ItemD.Instance.MoveItem(inventory.slots[id]);
            }
    }
}
