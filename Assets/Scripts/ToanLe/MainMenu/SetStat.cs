using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStat : MonoBehaviour
{
    public int HP;
    public int MP;
    public int DMG;
    public int DEF;
    public StatsData statData;
    public bool isSet = false;
    private void Start()
    {
        SetPlayer();
    }
    public void SetPlayer()
    {
        PlayerInfor player = GameInstance.instance.playerInfor;
        if (player != null && !isSet)
        {
            player.UpMaxHP(statData.HP);
            player.UpMaxMP(statData.MP);
            player.UpDame(statData.DMG);
            player.UpDef(statData.DEF);
            isSet = true;
            Debug.Log("Player stats updated.");
        }
        else
        {
            Debug.LogError("Player information is missing!");
        }
    }
}
