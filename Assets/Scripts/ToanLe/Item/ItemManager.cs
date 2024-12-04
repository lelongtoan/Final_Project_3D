using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public EquipmentGenerator equipment;
    public EquipButton equipSword;
    public EquipButton equipShield;
    public EquipButton equipHelmet;
    public EquipButton equipArmour;
    public EquipButton equipGloves;
    public EquipButton equipBoot;
    public ItemContainer inventory;
    public List<InventoryButton> buttons;
    private void Start()
    {
        SetIndex();
        ShowInventory();
        ShowEquipment();
    }
    private void OnEnable()
    {
        ShowInventory();
    }
    public virtual void ShowInventory()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            if (inventory.slots[i].item == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }
    public virtual void ShowEquipment()
    {
        // Danh sách các slot trang bị và nút tương ứng
        var equipmentSlots = new ItemSlot[]
        {
        equipment.swordSlot,
        equipment.shieldSlot,
        equipment.helmetSlot,
        equipment.armourSlot,
        equipment.glovesSlot,
        equipment.bootSlot
        };

        var equipButtons = new EquipButton[]
        {
        equipSword,
        equipShield,
        equipHelmet,
        equipArmour,
        equipGloves,
        equipBoot
        };

        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i] == null || equipmentSlots[i].item == null)
            {
                equipButtons[i].Clean();
            }
            else
            {
                equipButtons[i].Set(equipmentSlots[i]);
            }
        }
    }

    public void SetIndex()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
        int startIndex = inventory.slots.Count;
        var equipButtons = new EquipButton[] { equipSword, equipShield, equipHelmet, equipArmour, equipGloves, equipBoot };

        for (int i = 0; i < equipButtons.Length; i++)
        {
            equipButtons[i].SetIndex(startIndex + i);
        }
    }
    public void RemoveItem(ItemSlot itemSlot,int count)
    {
        if(itemSlot != null)
        {
            itemSlot.count -= count;
            if (itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }
    }
    public void RemoveItemCraft(ItemSlot itemSlot,int count)
    {
        List<ItemSlot> temp = inventory.slots
        .Where(c => c.item == itemSlot.item && c.count > 0)
        .OrderBy(c => c.count)
        .ToList();
        int remaining = count;
        foreach (var slot in temp)
        {
            if (remaining <= 0) break;

            if (slot.count <= remaining)
            {
                remaining -= slot.count;
                slot.Clear();
            }
            else
            {
                slot.count -= remaining;
                remaining = 0;
            }
        }
    }
    public virtual void OnClick(int id)
    {

    }
}
