using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Tree/ListSkillNode")]
public class ListSkillNode : ScriptableObject
{
    [Header("Test")]
    public int hp;
    public int mp;
    public int dmg;
    public int def;

    public List<SkillNodeData> data;

}
