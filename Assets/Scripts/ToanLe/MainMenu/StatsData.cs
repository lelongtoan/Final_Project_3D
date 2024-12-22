using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MainMenu/Start Data")]
public class StatsData : ScriptableObject
{
    public int HP;
    public int MP;
    public int DMG;
    public int DEF;
    public ListSkillNode skillnode;
    public PlayerData playerData;
    public void Set()
    {
        HP = 100; MP = 100; DMG = 10; DEF = 5;
        HP += skillnode.hp;
        MP += skillnode.mp;
        DMG += skillnode.dmg;
        DEF += skillnode.def;

        //+ perk

        playerData.maxHP = HP;
        playerData.maxMP = MP;
        playerData.dame = DMG;
        playerData.def = DEF;

    }
}
