using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    public float maxHP = 100f;
    public float healthPoint = 100f;
    public float manaPoint = 100f;
    public float maxMP = 100f;  
    public int def = 5;
    public int dame = 10;
    public int level = 1;
    public float exp = 0;
    public int money = 0;
}
