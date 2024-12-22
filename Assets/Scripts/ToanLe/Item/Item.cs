using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemSet
{
    Nope, Equippable, Food, Heal, Mana
}
public enum Buff
{
    Nope, HP, MP, Dmg, Armour, DmgAp
}
public enum EQT
{
    Nope, Sword, Shield, Helmet, Armour, Gloves, Boot
}
[System.Serializable]
[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string id;
    public string itemName;
    public bool stackable;

    public float HP;
    public float MP;
    public float Dmg;
    public float buffD;
    public float gold;
    public float rateDrop;
    public string description;
    public Buff buff;
    public ItemSet itemSet;
    public EQT itemType;
    public Sprite icon;
}
