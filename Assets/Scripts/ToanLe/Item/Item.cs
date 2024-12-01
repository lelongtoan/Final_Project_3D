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
[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public bool stackable;

    public float HP;
    public float MP;
    public float Dmg;
    

    public Buff buff;


    public float buffD;

    public float gold;

    public bool canShow;
    public string description;
    public ItemSet itemSet;
    public Sprite icon;
}
