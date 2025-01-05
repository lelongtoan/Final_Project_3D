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

        // Lưu dữ liệu nhiệm vụ
        for (int i = 0; i < quests.Count; i++)
        {
            int state = (quests[i].stateQuest == StateQuest.Completed) ? 1 :
                        (quests[i].stateQuest == StateQuest.Taked) ? 2 : 0;
            QuestSave questSave = new QuestSave();
            questSave.Set(quests[i].questId, quests[i].isShowQuest, state);
            saveData.quests.Add(questSave);
        }

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
            saveData.inventory.Add(invenSave);
        }

        // Lưu dữ liệu trang bị
        saveData.equipment.Clear();
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
            saveD.equipment.Add(equipSave);
        }
        else
        {
            equipSave.Set(idSlot);
            saveD.equipment.Add(equipSave);
        }
    }

    public void SetLoadData(int idLoad)
    {
        temp = idLoad;
        saveData = SaveLoadJson.LoadFromJson(idLoad);
        // Tải dữ liệu người chơi
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

        // Tải dữ liệu nhiệm vụ
        for (int i = 0; i < saveData.quests.Count; i++)
        {
            int state = saveData.quests[i].state;
            quests[i].isShowQuest = saveData.quests[i].isShow;
            quests[i].stateQuest = (StateQuest)state;
        }

        // Tải dữ liệu đồ vật
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
            else
            {
                inventorySlots.slots[invenSave.idSlot].Clear();
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
        EquipmentSave equipSave = saveData.equipment.Find(e => e.idSlot == idSlot); // Sửa từ saveData.saveDatas[idLoad] thành saveData.saveDatas[0] khi lấy dữ liệu
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
        skillData.Initialize();
    }
}
