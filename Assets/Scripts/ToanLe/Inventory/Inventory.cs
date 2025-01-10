using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Inventory : ItemManager
{
    private void Start()
    {
        ShowInven();
    }
    private void Update()
    {
        ShowInven();
    }
    public void ShowInven()
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
                GameInstance.instance.gameMenu.SetDragAndDrop(false);
                return;
            }
            if (inventory.slots[id].item != null)
            {
                GameInstance.instance.gameMenu.SetInfoItem();
                ItemD.Instance.SetItem(inventory.slots[id],id);
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

            if (equipSlots[equipIndex].item != null)
            {
                GameInstance.instance.gameMenu.SetInfoItem();
                ItemD.Instance.SetItem(equipSlots[equipIndex],id,1);
                Debug.Log("Equip Info");
            }
        }
        //ShowInven();
    }

}
