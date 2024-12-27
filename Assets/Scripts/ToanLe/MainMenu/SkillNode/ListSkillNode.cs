using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Skill Tree/ListSkillNode")]
public class ListSkillNode : ScriptableObject
{
    [Header("Test")]
    public int hp;
    public int mp;
    public int dmg;
    public int def;

    public List<SkillNodeData> dataSkillNode;

    public void Set()
    {
        hp = 0; mp = 0; dmg = 0; def = 0;
        foreach (SkillNodeData data in dataSkillNode)
        {
            if(data != null && data.state == SkillNodeState.Taked)
            {
                hp += data.hp;
                mp += data.mp;
                dmg += data.dmg;
                def += data.def;
            }
        }
    }
}
