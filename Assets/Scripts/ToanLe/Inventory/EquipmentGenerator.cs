using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Data/Equipment Generator")]
public class EquipmentGenerator : ScriptableObject
{
    [Header("Equipment Slots")]
    public ItemSlot swordSlot;
    public ItemSlot shieldSlot;
    public ItemSlot helmetSlot;
    public ItemSlot armourSlot;
    public ItemSlot glovesSlot;
    public ItemSlot bootSlot;
    public void Clear()
    {
        swordSlot.Clear();
        shieldSlot.Clear();
        helmetSlot.Clear();
        armourSlot.Clear();
        glovesSlot.Clear();
        bootSlot.Clear();
    }
    public void UseEquipment(ItemSlot itemSlot)
    {
        if (itemSlot == null || itemSlot.item == null)
        {
            Debug.LogError("Item or ItemSlot is null!");
            return ;
        }
        ItemSlot targetSlot = GetEquipmentSlot(itemSlot.item.itemType);
        if (targetSlot == null)
        {
            Debug.LogWarning("Invalid or unsupported item type: " + itemSlot.item.itemType);
            return;
        }

        if (targetSlot.item == null)
        {
            targetSlot.Copy(itemSlot);
            GameInstance.instance.playerInfor.UpMaxHP(itemSlot.item.HP);
            GameInstance.instance.playerInfor.UpMaxMP(itemSlot.item.MP);
            GameInstance.instance.playerInfor.UpDame((int)itemSlot.item.Dmg);
            
            itemSlot.Clear();
        }
        else
        {
            Item tempItem = targetSlot.item;
            int tempCount = targetSlot.count;
            int tempDurability = targetSlot.toolDurability;

            targetSlot.Copy(itemSlot);
            GameInstance.instance.playerInfor.UpMaxHP(-targetSlot.item.HP);
            GameInstance.instance.playerInfor.UpMaxMP(-targetSlot.item.MP);
            GameInstance.instance.playerInfor.UpDame((int)-targetSlot.item.Dmg);
            GameInstance.instance.playerInfor.UpMaxHP(itemSlot.item.HP);
            GameInstance.instance.playerInfor.UpMaxMP(itemSlot.item.MP);
            GameInstance.instance.playerInfor.UpDame((int)itemSlot.item.Dmg);
            itemSlot.Set(tempItem, tempDurability, tempCount);
        }
    }
    private ItemSlot GetEquipmentSlot(EQT itemType)
    {
        switch (itemType)
        {
            case EQT.Sword:
                return swordSlot;

            case EQT.Shield:
                return shieldSlot;

            case EQT.Helmet:
                return helmetSlot;

            case EQT.Armour:
                return armourSlot;

            case EQT.Gloves:
                return glovesSlot;

            case EQT.Boot:
                return bootSlot;

            default:
                return null; // Loại không hợp lệ hoặc không được hỗ trợ
        }
    }
    public bool IsValidEquipment()
    {
        return (swordSlot == null || swordSlot.item.itemType == EQT.Sword) &&
               (shieldSlot == null || shieldSlot.item.itemType == EQT.Shield) &&
               (helmetSlot == null || helmetSlot.item.itemType == EQT.Helmet) &&
               (armourSlot == null || armourSlot.item.itemType == EQT.Armour) &&
               (glovesSlot == null || glovesSlot.item.itemType == EQT.Gloves) &&
               (bootSlot == null || bootSlot.item.itemType == EQT.Boot);
    }
}
