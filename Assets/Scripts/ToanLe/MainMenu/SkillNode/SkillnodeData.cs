using System.Collections.Generic;
using UnityEngine;

public enum SkillNodeState
{
    Lock, Open ,Taked
}
[CreateAssetMenu(menuName = "Skill Tree/SkillNode")]
public class SkillNodeData : ScriptableObject
{
    public Sprite icon;
    public string nameSkillNode;
    public string description;
    public int hp;
    public int mp;
    public int dmg;
    public int def;
    public int gold;
    public int diamond;

    public List<SkillNodeData> listNextSkillNode;

    public SkillNodeState state;
}
