using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Save Data")]
public class SaveData : ScriptableObject
{
    [Header("Player Stats")]
    public int idSave;
    public PlayerData playerData;


    [Header("Skill Levels")]
    public PlayerSkillData skillData;

    [Header("Quests")]
    public List<Quest> quests;

    [Header("Inventory Slots")]
    public ItemContainer inventorySlots;
    public EquipmentGenerator equipmentGenerator;
    public SetIdItem setIdItem;

    public void NewPlayer()
    {
        for (int i = 0; i < inventorySlots.slots.Count; i++)
        {
            inventorySlots.slots.Clear();
        }
        equipmentGenerator.Clear();
        for (int i = 0; i < quests.Count; i++)
        {
            quests[i].Clear();
        }
        // skillData.Clear();
        // playerData.Clear();
    }
}

