using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTemp : MonoBehaviour
{

    public PlayerData playerData;

    [Header("Skill Levels")]
    public PlayerSkillData skillData;

    [Header("Quests")]
    public List<Quest> quests;

    [Header("Inventory Slots")]
    public ItemContainer inventorySlots;
    public EquipmentGenerator equipments;
    public SetIdItem setIdItem;

    public SaveData saveData;

    private void OnEnable()
    {
        SetIDItem();
    }
    public void SetIDItem()
    {
        for (int i = 0; i < setIdItem.idItemSlots.Count; i++)
        {
            setIdItem.idItemSlots[i].id = i.ToString();
        }
    }
    public void SetSaveData(int id = -1)
    {
        saveData = new SaveData();
        // Player Data
        saveData.maxHP = playerData.maxHP;
        saveData.healthPoint = playerData.healthPoint;
        saveData.manaPoint = playerData.manaPoint;
        saveData.maxMP = playerData.maxMP;
        saveData.def = playerData.def;
        saveData.dame = playerData.dame;
        saveData.level = playerData.level;
        saveData.exp = playerData.exp;
        saveData.money = playerData.money;
        saveData.levelSkill1 = skillData.levelSkill1;
        saveData.levelSkill2 = skillData.levelSkill2;
        saveData.levelSkill3 = skillData.levelSkill3;

        // Quest Data
        for (int i = 0; i < quests.Count; i++)
        {
            int state = 0;
            if (quests[i].stateQuest == StateQuest.Completed)
            {
                state = 1;
            }
            else if (quests[i].stateQuest == StateQuest.Taked)
            {
                state = 2;
            }
            QuestSave questSave = new QuestSave();
            questSave.Set(quests[i].questId, quests[i].isShowQuest, state);
            saveData.quests.Add(questSave);
        }

        // Inventory Data
        for (int i = 0; i < inventorySlots.slots.Count; i++)
        {
            InventorySave invenSave = new InventorySave();
            if (inventorySlots.slots[i].item != null)
            {
                invenSave.Set(i, inventorySlots.slots[i].item.id, inventorySlots.slots[i].count, inventorySlots.slots[i].toolDurability);
            }
            else
            {
                invenSave.Set(i);
            }
            saveData.inventory.Add(invenSave);
        }

        // Equipment Data
        saveData.equipment = new List<EquipmentSave>();
        AddEquipmentSave(equipments.swordSlot, 0);
        AddEquipmentSave(equipments.shieldSlot, 1);
        AddEquipmentSave(equipments.helmetSlot, 2);
        AddEquipmentSave(equipments.armourSlot, 3);
        AddEquipmentSave(equipments.glovesSlot, 4);
        AddEquipmentSave(equipments.bootSlot, 5);

        if (id == -1)
        {
            SaveLoadJson.SaveToJson(saveData, saveData.idSave);
            Debug.Log("111111");
        }
        else
        {
            SaveLoadJson.SaveToJson(saveData, id);
            Debug.Log("111111 Error");

        }
    }
    private void AddEquipmentSave(ItemSlot slot, int idSlot)
    {
        EquipmentSave equipSave = new EquipmentSave();
        if (slot != null && slot.item != null)
        {
            equipSave.Set(idSlot, slot.item.id, slot.count, slot.toolDurability);
            saveData.equipment.Add(equipSave);
        }
        else
        {
            equipSave.Set(idSlot);
            saveData.equipment.Add(equipSave);
        }
    }

    public void SetLoadData(int idLoad)
    {
        saveData = SaveLoadJson.LoadFromJson(idLoad);
        // Load Player Data
        playerData.maxHP = saveData.maxHP;
        playerData.healthPoint = saveData.healthPoint;
        playerData.manaPoint = saveData.manaPoint;
        playerData.maxMP = saveData.maxMP;
        playerData.def = saveData.def;
        playerData.dame = saveData.dame;
        playerData.level = saveData.level;
        playerData.exp = saveData.exp;
        playerData.money = saveData.money;

        skillData.levelSkill1 = saveData.levelSkill1;
        skillData.levelSkill2 = saveData.levelSkill2;
        skillData.levelSkill3 = saveData.levelSkill3;

        // Load Quest Data
        for (int i = 0; i < saveData.quests.Count; i++)
        {
            int state = saveData.quests[i].state;
            quests[i].isShowQuest = saveData.quests[i].isShow;
            quests[i].stateQuest = (StateQuest)state;
        }

        // Load Inventory Data
        for (int i = 0; i < saveData.inventory.Count; i++)
        {
            InventorySave invenSave = saveData.inventory[i];
            Item inventoryItem = setIdItem.idItemSlots.Find(s => s.id == invenSave.idItem);
            if (inventoryItem != null)
            {
                inventorySlots.slots[invenSave.idSlot].item = inventoryItem;
                inventorySlots.slots[invenSave.idSlot].count = invenSave.quantity;
                inventorySlots.slots[invenSave.idSlot].toolDurability = invenSave.durality;
            }
        }

        // Load Equipment Data
        LoadEquipment(equipments.swordSlot, 0);
        LoadEquipment(equipments.shieldSlot, 1);
        LoadEquipment(equipments.helmetSlot, 2);
        LoadEquipment(equipments.armourSlot, 3);
        LoadEquipment(equipments.glovesSlot, 4);
        LoadEquipment(equipments.bootSlot, 5);
    }

    // Helper method to load equipment save data
    private void LoadEquipment(ItemSlot slot, int idSlot)
    {
        EquipmentSave equipSave = saveData.equipment.Find(e => e.idSlot == idSlot);
        if (equipSave != null)
        {
            Item equipmentItem = setIdItem.idItemSlots.Find(s => s.id == equipSave.idItem);
            if (equipmentItem != null)
            {
                slot.item = equipmentItem;
                slot.count = equipSave.quantity;
                slot.toolDurability = equipSave.durality;
            }
        }
    }

    public void NewPlayer()
    {
        for (int i = 0; i < inventorySlots.slots.Count; i++)
        {
            inventorySlots.slots[i].Clear();
        }
        equipments.Clear();
        for (int i = 0; i < quests.Count; i++)
        {
            quests[i].Clear();
        }
        playerData.maxHP = 0;
        // skillData.Clear();
        // playerData.Clear();
    }
}
