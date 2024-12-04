using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InforShow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI mp;
    [SerializeField] TextMeshProUGUI dmg;
    [SerializeField] TextMeshProUGUI def;

    private void OnEnable()
    {
        PlayerInfor pl = GameInstance.instance.playerInfor;
        hp.text = "HP : "+ pl.maxHP;
        mp.text = "MP : " + pl.maxMP;
        dmg.text = "DMG : " + pl.dame;
        def.text = "DEF : " + pl.def;
    }
}
