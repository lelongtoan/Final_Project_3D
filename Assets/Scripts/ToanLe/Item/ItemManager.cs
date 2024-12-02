using System.Collections;
using System.Collections.Generic;
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
    public static ItemManager intance { get; set; }

    private void Awake()
    {
        intance = this;
    }
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
        ItemSlot temp = inventory.slots.Find(c => c.item == itemSlot.item);
        if(temp !=null)
        {
            temp.count -= count;
            if (temp.count <= 0)
            {
                temp.Clear();
            }
        }
    }
    public virtual void OnClick(int id)
    {

    }
}
