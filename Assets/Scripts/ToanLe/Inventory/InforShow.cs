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

    private void Update()
    {
        Set();
    }
    public void Set()
    {
        PlayerInfor pl = GameInstance.instance.playerInfor;
        if (pl != null) 
        {
            hp.text = "HP : " + pl.maxHP;
            mp.text = "MP : " + pl.maxMP;
            dmg.text = "DMG : " + pl.dame;
            def.text = "DEF : " + pl.def;
        }
    }
}
