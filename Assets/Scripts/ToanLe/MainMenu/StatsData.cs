using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "MainMenu/Start Data")]
public class StatsData : ScriptableObject
{
    public int HP;
    public int MP;
    public int DMG;
    public int DEF;
    public ListSkillNode skillnode;
    public PerkEquipmentData perkEquipmentData;
    public PlayerData playerData;
    public void Set()
    {
        HP = 0; MP = 0; DMG = 0; DEF = 0;

        skillnode.Set();
        perkEquipmentData.Set();

        //+ Skillnode
        HP += skillnode.hp;
        MP += skillnode.mp;
        DMG += skillnode.dmg;
        DEF += skillnode.def;

        //+ perk
        HP += perkEquipmentData.HP;
        MP += perkEquipmentData.MP;
        DMG += perkEquipmentData.DMG;
        DEF += perkEquipmentData.DEF;

        //+ Player
        playerData.maxHP = HP + 100;
        playerData.maxMP = MP + 100;
        playerData.dame = DMG + 10;
        playerData.def = DEF + 5;
    }
}
