using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestSave
{
    public int id;
    public bool isShow;
    public int state;
    public void Set(int id, bool isShow, int state)
    {
        this.id = id;
        this.isShow = isShow;
        this.state = state;
    }
}
[Serializable]
public class EquipmentSave
{
    public int idSlot;
    public string idItem;
    public int quantity;
    public int durality;
    public void Set(int idSlot, string idItem = "", int quantity = 0, int durality = 0)
    {
        this.idSlot = idSlot;
        this.idItem = idItem;
        this.quantity = quantity;
        this.durality = durality;
    }
}
[Serializable]
public class InventorySave
{
    public int idSlot;
    public string idItem;
    public int quantity;
    public int durality;
    public void Set(int idSlot, string idItem = "", int quantity = 0, int durality = 0)
    {
        this.idSlot = idSlot;
        this.idItem = idItem;
        this.quantity = quantity;
        this.durality = durality;
    }
}
[System.Serializable]
[CreateAssetMenu(menuName = "Game/Save Data")]
public class SaveData : ScriptableObject
{
    public bool isSave;
    [Header("Player Stats")]
    public int idSave;
    public float maxHP;
    public float healthPoint;
    public float manaPoint;
    public float maxMP;
    public int def;
    public int dame;
    public int level;
    public float exp;
    public int money;

    [Header("Skill Levels")]
    public int levelSkill1;
    public int levelSkill2;
    public int levelSkill3;

    [Header("Quests")]
    public List<QuestSave> quests = new List<QuestSave>();

    [Header("Inventory Slots")]
    public List<InventorySave> inventory = new List<InventorySave>();
    public List<EquipmentSave> equipment = new List<EquipmentSave>();
}
