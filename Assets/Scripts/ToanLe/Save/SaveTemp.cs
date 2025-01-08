using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTemp : MonoBehaviour
{
    public int temp;
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
        if (id == -1)
        {
            id = temp;
        }
        // Lưu dữ liệu người chơi
        saveData._01_maxHP = playerData.maxHP;
        saveData._02_healthPoint = playerData.healthPoint;
        saveData._03_manaPoint = playerData.manaPoint;
        saveData._04_maxMP = playerData.maxMP;
        saveData._05_def = playerData.def;
        saveData._06_dame = playerData.dame;
        saveData._07_level = playerData.level;
        saveData._08_exp = playerData.exp;
        saveData._09_money = playerData.money;
        saveData._10_levelSkill1 = skillData.levelSkill1;
        saveData._11_levelSkill2 = skillData.levelSkill2;
        saveData._12_levelSkill3 = skillData.levelSkill3;

        saveData._13_quests.Clear();
        // Lưu dữ liệu nhiệm vụ
        for (int i = 0; i < quests.Count; i++)
        {
            int state = (quests[i].stateQuest == StateQuest.Completed) ? 1 :
                        (quests[i].stateQuest == StateQuest.Taked) ? 2 : 0;
            QuestSave questSave = new QuestSave();
            questSave.Set(i,quests[i].questId, quests[i].isShowQuest, state);
            saveData._13_quests.Add(questSave);
        }

        saveData._14_inventory.Clear();
        // Lưu dữ liệu đồ vật
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
            saveData._14_inventory.Add(invenSave);
        }

        // Lưu dữ liệu trang bị
        saveData._15_equipment.Clear();
        AddEquipmentSave(equipments.swordSlot, 0, saveData);
        AddEquipmentSave(equipments.shieldSlot, 1, saveData);
        AddEquipmentSave(equipments.helmetSlot, 2, saveData);
        AddEquipmentSave(equipments.armourSlot, 3, saveData);
        AddEquipmentSave(equipments.glovesSlot, 4, saveData);
        AddEquipmentSave(equipments.bootSlot, 5, saveData);
        // Lưu vào file
        SaveLoadJson.SaveToJson(saveData, id);
    }

    private void AddEquipmentSave(ItemSlot slot, int idSlot, SaveData saveD)
    {
        EquipmentSave equipSave = new EquipmentSave();
        if (slot != null && slot.item != null)
        {
            equipSave.Set(idSlot, slot.item.id, slot.count, slot.toolDurability);
        }
        else
        {
            equipSave.Set(idSlot);
        }
        saveD._15_equipment.Add(equipSave);
    }

    public void SetLoadData(int idLoad)
    {
        temp = idLoad;
        saveData = SaveLoadJson.LoadFromJson(idLoad);
        // Tải dữ liệu người chơi
        playerData.maxHP = saveData._01_maxHP;
        playerData.healthPoint = saveData._02_healthPoint;
        playerData.manaPoint = saveData._03_manaPoint;
        playerData.maxMP = saveData._04_maxMP;
        playerData.def = saveData._05_def;
        playerData.dame = saveData._06_dame;
        playerData.level = saveData._07_level;
        playerData.exp = saveData._08_exp;
        playerData.money = saveData._09_money;

        skillData.levelSkill1 = saveData._10_levelSkill1;
        skillData.levelSkill2 = saveData._11_levelSkill2;
        skillData.levelSkill3 = saveData._12_levelSkill3;

        // Tải dữ liệu nhiệm vụ
        for (int i = 0; i < saveData._13_quests.Count; i++)
        {
            int state = saveData._13_quests[i]._04_state;
            quests[i].isShowQuest = saveData._13_quests[i]._03_isShow;
            quests[i].stateQuest = (StateQuest)state;
        }

        // Tải dữ liệu đồ vật
        for (int i = 0; i < saveData._14_inventory.Count; i++)
        {
            InventorySave invenSave = saveData._14_inventory[i];
            Item inventoryItem = setIdItem.idItemSlots.Find(s => s.id == invenSave._02_idItem);
            if (inventoryItem != null)
            {
                inventorySlots.slots[invenSave._01_idSlot].item = inventoryItem;
                inventorySlots.slots[invenSave._01_idSlot].count = invenSave._03_quantity;
                inventorySlots.slots[invenSave._01_idSlot].toolDurability = invenSave._04_durality;
            }
            else
            {
                inventorySlots.slots[invenSave._01_idSlot].Clear();
            }
        }

        // Tải dữ liệu trang bị
        LoadEquipment(equipments.swordSlot, 0);
        LoadEquipment(equipments.shieldSlot, 1);
        LoadEquipment(equipments.helmetSlot, 2);
        LoadEquipment(equipments.armourSlot, 3);
        LoadEquipment(equipments.glovesSlot, 4);
        LoadEquipment(equipments.bootSlot, 5);
    }

    private void LoadEquipment(ItemSlot slot, int idSlot)
    {
        EquipmentSave equipSave = saveData._15_equipment.Find(e => e._01_idSlot == idSlot); // Sửa từ saveData.saveDatas[idLoad] thành saveData.saveDatas[0] khi lấy dữ liệu
        if (equipSave != null)
        {
            Item equipmentItem = setIdItem.idItemSlots.Find(s => s.id == equipSave._02_idItem);
            if (equipmentItem != null)
            {
                slot.item = equipmentItem;
                slot.count = equipSave._03_quantity;
                slot.toolDurability = equipSave._04_durality;
            }
        }
    }

    public void NewPlayer()
    {
        for (int i = 0; i < inventorySlots.slots.Count; i++)
        {
            inventorySlots.slots[i].Clear();
        }
        equipments.helmetSlot.Clear();
        equipments.armourSlot.Clear();
        equipments.glovesSlot.Clear();
        equipments.bootSlot.Clear();
        equipments.swordSlot.Clear();
        equipments.shieldSlot.Clear();
        for (int i = 0; i < quests.Count; i++)
        {
            quests[i].isShowQuest = false;
            quests[i].stateQuest = StateQuest.Nope;
        }
        skillData.Initialize();
    }
}
