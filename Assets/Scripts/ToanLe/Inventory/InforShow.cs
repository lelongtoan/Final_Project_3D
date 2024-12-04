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
        hp.text = pl.maxHP.ToString();
        mp.text = pl.maxMP.ToString();
        dmg.text = pl.dame.ToString();
        def.text = pl.def.ToString();
    }
}
