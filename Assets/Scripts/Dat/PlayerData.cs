using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data", order = 1)]
public class PlayerData : ScriptableObject
{
    public float maxHP = 100f;
    public float healthPoint = 100f;
    public float manaPoint = 100f;
    public float maxMP = 100f;
    public int def = 10;
    public int dame = 10;
    public int exp = 0;
    public int money = 0;
    


    public void ResetData()
    {
        healthPoint = maxHP;
        manaPoint = maxMP;
        dame = 10;
        def = 5;
        exp = 0;
        money = 0;
    }


}
