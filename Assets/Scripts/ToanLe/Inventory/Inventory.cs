﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : ItemManager
{
    private void Update()
    {
        SetIndex();
        ShowInventory();
        ShowEquipment();
    }
    public override void OnClick(int id)
    {
        if (id < inventory.slots.Count)
        {
            if (ItemD.Instance.isMoving)
            {
                Debug.Log("moveitem");
                ItemD.Instance.MoveItem(inventory.slots[id]);
                InGameMenu.inGameMenu.SetDragAndDrop(false);
                return;
            }
            if (inventory.slots[id].item != null)
            {
                InGameMenu.inGameMenu.SetInfoItem();
                ItemD.Instance.SetItem(inventory.slots[id]);
                Debug.Log("Item Info");
            }
            return;
        }
        int equipIndex = id - inventory.slots.Count;

        if (equipIndex >= 0 && equipIndex < 6)
        {
            var equipSlots = new ItemSlot[]
            {
            equipment.swordSlot,
            equipment.shieldSlot,
            equipment.helmetSlot,
            equipment.armourSlot,
            equipment.glovesSlot,
            equipment.bootSlot
            };

            if (equipSlots[equipIndex] != null)
            {
                InGameMenu.inGameMenu.SetInfoItem();
                ItemD.Instance.SetItem(equipSlots[equipIndex],1);
                Debug.Log("Equip Info");
            }
        }
    }

}
