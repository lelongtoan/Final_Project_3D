using System.IO;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    public float maxHP=100;
    public float healthPoint = 100;
    public float manaPoint = 100;
    public float maxMP = 100;
    public int def = 5;
    public int dame = 10;
    public int level = 1;
    public float exp = 0;
    public int money = 0;
    public int point = 0;

    public void Initialaze()
    {
        maxHP = 100f;
        healthPoint = maxHP;
        maxMP = 100f;
        manaPoint = maxMP;
        def = 5;
        dame = 10;
        level = 1;
        exp = 0;
        money = 0;
        point = 0;
    }
}
