using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Perks
{
    public int _01_id;
    public int _02_idPerk;
    public int _03_statePerk;
    public int _04_levelPerk;
    public int _05_quantityPerk;
    public void Set(int id, int idPerk,int statePerk,int levelPerk,int quantityPerk)
    {
        this._01_id = id;
        this._02_idPerk = idPerk;
        this._03_statePerk = statePerk;
        this._04_levelPerk = levelPerk;
        this._05_quantityPerk = quantityPerk;
    }
}
[System.Serializable]
public class Achies
{
    public int _01_id;
    public int _02_state;
    public void Set(int id, int state)
    {
        this._01_id = id;
        this._02_state = state;
    }
}
[System.Serializable]
public class SkillNodes
{
    public int _01_id;
    public int _02_state;
    public void Set(int id, int state)
    {
        this._01_id = id;
        this._02_state = state;
    }
}
[System.Serializable]
public class EquipPerk
{
    public int _01_id;
    public int _02_idPerk;
    public void Set(int id, int idPerk)
    {
        this._01_id = id;
        this._02_idPerk = idPerk;
    }
}
[System.Serializable]
public class SaveAllData
{
    public int _01_countBoss;
    public int _02_countEnemy;
    public int _03_countGold;
    public int _04_countSignIn;
    public int _05_maxLevel;


    public int _06_money;
    public int _07_diamond;
    public int _08_ironKey;
    public int _09_silverKey;

    public List<EquipPerk> _10_perks = new List<EquipPerk>();//max 3
    public List<Perks> _11_listPerk = new List<Perks>();
    public List<SkillNodes> _12_skillNode = new List<SkillNodes>();
    public List<Achies> _13_achies = new List<Achies>();
}
