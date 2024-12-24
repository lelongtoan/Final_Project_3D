using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PerkQuality
{
    S, A, B
}
public enum PerkState
{
    Lock, Unlock
}

[CreateAssetMenu(menuName ="MainMenu/Perk")]
public class PerkData : ScriptableObject
{
    public int id;
    public int levelPerk;
    public string perkName;
    public Sprite image;
    public int quantity;
    public int rate;
    public int hp;
    public int mp;
    public int dmg;
    public int def;
    public PerkState perkState;
    public PerkQuality quality;
}
