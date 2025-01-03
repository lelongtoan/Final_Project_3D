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

    public ListSaveData saveData;

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
        saveData.saveDatas[id].maxHP = playerData.maxHP;
        saveData.saveDatas[id].healthPoint = playerData.healthPoint;
        saveData.saveDatas[id].manaPoint = playerData.manaPoint;
        saveData.saveDatas[id].maxMP = playerData.maxMP;
        saveData.saveDatas[id].def = playerData.def;
        saveData.saveDatas[id].dame = playerData.dame;
        saveData.saveDatas[id].level = playerData.level;
        saveData.saveDatas[id].exp = playerData.exp;
        saveData.saveDatas[id].money = playerData.money;
        saveData.saveDatas[id].levelSkill1 = skillData.levelSkill1;
        saveData.saveDatas[id].levelSkill2 = skillData.levelSkill2;
        saveData.saveDatas[id].levelSkill3 = skillData.levelSkill3;

        // Lưu dữ liệu nhiệm vụ
        for (int i = 0; i < quests.Count; i++)
        {
            int state = (quests[i].stateQuest == StateQuest.Completed) ? 1 :
                        (quests[i].stateQuest == StateQuest.Taked) ? 2 : 0;
            QuestSave questSave = new QuestSave();
            questSave.Set(quests[i].questId, quests[i].isShowQuest, state);
            saveData.saveDatas[id].quests.Add(questSave);
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
            saveData.saveDatas[id].inventory.Add(invenSave);
        }

        // Lưu dữ liệu trang bị
        saveData.saveDatas[id].equipment.Clear();
        AddEquipmentSave(equipments.swordSlot, 0, saveData.saveDatas[id]);
        AddEquipmentSave(equipments.shieldSlot, 1, saveData.saveDatas[id]);
        AddEquipmentSave(equipments.helmetSlot, 2, saveData.saveDatas[id]);
        AddEquipmentSave(equipments.armourSlot, 3, saveData.saveDatas[id]);
        AddEquipmentSave(equipments.glovesSlot, 4, saveData.saveDatas[id]);
        AddEquipmentSave(equipments.bootSlot, 5, saveData.saveDatas[id]);
        // Lưu vào file
        SaveLoadJson.SaveToJson(saveData.saveDatas[id], id);
    }

    private void AddEquipmentSave(ItemSlot slot, int idSlot, SaveDatas saveD)
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
        if (idLoad >= 0 && idLoad < saveData.saveDatas.Count)
        {
            saveData.saveDatas[idLoad] = SaveLoadJson.LoadFromJson(idLoad);
            // Tải dữ liệu người chơi
            playerData.maxHP = saveData.saveDatas[idLoad].maxHP;
            playerData.healthPoint = saveData.saveDatas[idLoad].healthPoint;
            playerData.manaPoint = saveData.saveDatas[idLoad].manaPoint;
            playerData.maxMP = saveData.saveDatas[idLoad].maxMP;
            playerData.def = saveData.saveDatas[idLoad].def;
            playerData.dame = saveData.saveDatas[idLoad].dame;
            playerData.level = saveData.saveDatas[idLoad].level;
            playerData.exp = saveData.saveDatas[idLoad].exp;
            playerData.money = saveData.saveDatas[idLoad].money;

            skillData.levelSkill1 = saveData.saveDatas[idLoad].levelSkill1;
            skillData.levelSkill2 = saveData.saveDatas[idLoad].levelSkill2;
            skillData.levelSkill3 = saveData.saveDatas[idLoad].levelSkill3;

            // Tải dữ liệu nhiệm vụ
            for (int i = 0; i < saveData.saveDatas[idLoad].quests.Count; i++)
            {
                int state = saveData.saveDatas[idLoad].quests[i].state;
                quests[i].isShowQuest = saveData.saveDatas[idLoad].quests[i].isShow;
                quests[i].stateQuest = (StateQuest)state;
            }

            // Tải dữ liệu đồ vật
            for (int i = 0; i < saveData.saveDatas[idLoad].inventory.Count; i++)
            {
                InventorySave invenSave = saveData.saveDatas[idLoad].inventory[i];
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
        else
        {
            Debug.LogError("Invalid save ID");
        }
    }

    private void LoadEquipment(ItemSlot slot, int idSlot)
    {
        EquipmentSave equipSave = saveData.saveDatas[0].equipment.Find(e => e.idSlot == idSlot); // Sửa từ saveData.saveDatas[idLoad] thành saveData.saveDatas[0] khi lấy dữ liệu
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
