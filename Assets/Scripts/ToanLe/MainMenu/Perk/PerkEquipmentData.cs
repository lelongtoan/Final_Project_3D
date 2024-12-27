using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "MainMenu/Perk Equipment Data")]
public class PerkEquipmentData : ScriptableObject
{
    public int HP;
    public int MP;
    public int DEF;
    public int DMG;
    public List<PerkData> equippedPerks = new List<PerkData>();
    public int maxSlots = 3;
    public void Set()
    {
        HP = MP = DEF = DMG = 0;
        foreach (PerkData perk in equippedPerks)
        {
            if (perk != null) 
            {
                HP += perk.hp;
                MP += perk.mp;
                DEF += perk.def;
                DMG += perk.dmg;
            }
        }
    }
}
