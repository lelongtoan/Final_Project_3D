using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuCharInfor : MonoBehaviour
{
    [SerializeField] StatsData statsData;

    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI mpText;
    [SerializeField] TextMeshProUGUI defText;
    [SerializeField] TextMeshProUGUI dmgText;
    private void Awake()
    {
        Set();
    }
    private void Update()
    {
        Set();
    }
    public void Set()
    {
        statsData.Set();
        hpText.text = statsData.HP.ToString();
        mpText.text = statsData.MP.ToString();
        defText.text = statsData.DEF.ToString();
        dmgText.text = statsData.DMG.ToString();
    }
}
