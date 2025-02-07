using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public ListSkillNode list;
    private void OnEnable()
    {
        SetListSkillNode();
    }
    public void SetListSkillNode()
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
    }

}
