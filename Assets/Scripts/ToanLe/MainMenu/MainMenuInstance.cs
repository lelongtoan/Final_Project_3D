using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInstance : MonoBehaviour
{
    public static MainMenuInstance instance;
    private void Awake()
    {
        instance = this;
    }
    public InforMainMenu inforMenu;
    public AchievementCheck check;
    public AchievementManager achieManager;
    public StatsData statsData;
    private void Update()
    {
        if (achieManager == null)
        {
            achieManager = FindAnyObjectByType<AchievementManager>();
        }
    }
}
