using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MainMenu/List Achievement")]
public class ListAchievement : ScriptableObject
{
    public List<AchievementData> listAchievement;
    public void Set()
    {
        for (int i = 0; i < listAchievement.Count; i++)
        {
            AchievementData data = listAchievement[i];
            data.questId = i;
        }
    }
}
