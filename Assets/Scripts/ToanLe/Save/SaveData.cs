using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestSave
{
    public int _01_idSlot;
    public int _02_id;
    public bool _03_isShow;
    public int _04_state;
    public void Set(int idSlot, int id, bool isShow, int state)
    {
        this._01_idSlot = idSlot;
        this._02_id = id;
        this._03_isShow = isShow;
        this._04_state = state;
    }
}
[Serializable]
public class EquipmentSave
{
    public int _01_idSlot;
    public string _02_idItem;
    public int _03_quantity;
    public int _04_durality;
    public void Set(int idSlot, string idItem = "", int quantity = 0, int durality = 0)
    {
        this._01_idSlot = idSlot;
        this._02_idItem = idItem;
        this._03_quantity = quantity;
        this._04_durality = durality;
    }
}
[Serializable]
public class InventorySave
{
    public int _01_idSlot;
    public string _02_idItem;
    public int _03_quantity;
    public int _04_durality;
    public void Set(int idSlot, string idItem = "", int quantity = 0, int durality = 0)
    {
        this._01_idSlot = idSlot;
        this._02_idItem = idItem;
        this._03_quantity = quantity;
        this._04_durality = durality;
    }
}
[System.Serializable]
public class SaveData
{
    [Header("Player Stats")]
    public float _01_maxHP;
    public float _02_healthPoint;
    public float _03_manaPoint;
    public float _04_maxMP;
    public int _05_def;
    public int _06_dame;
    public int _07_level;
    public float _08_exp;
    public int _09_money;

    [Header("Skill Levels")]
    public int _10_levelSkill1;
    public int _11_levelSkill2;
    public int _12_levelSkill3;

    [Header("Quests")]
    public List<QuestSave> _13_quests = new List<QuestSave>();

    [Header("Inventory Slots")]
    public List<InventorySave> _14_inventory = new List<InventorySave>();
    public List<EquipmentSave> _15_equipment = new List<EquipmentSave>();
}
