using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public ListSkillNode list;
    public PerkEquipmentData perkEquipment;
    private void OnEnable()
    {
        Set();
    }
    public void Set()
    {
        list.hp = 0; list.mp = 0; list.dmg = 0; list.def = 0;
        foreach (SkillNodeData node in list.dataSkillNode)
        {
            if (node.state == SkillNodeState.Taked)
            {
                list.hp += node.hp;
                list.mp += node.mp;
                list.dmg += node.dmg;
                list.def += node.def;
            }
        }
        perkEquipment.HP = perkEquipment.MP = perkEquipment.DEF = perkEquipment.DMG = 0;
        foreach (PerkData data in perkEquipment.equippedPerks)
        {
            perkEquipment.HP += data.hp;
            perkEquipment.MP += data.mp;
            perkEquipment.DEF += data.def;
            perkEquipment.DMG += data.dmg;
        }
    }

}
