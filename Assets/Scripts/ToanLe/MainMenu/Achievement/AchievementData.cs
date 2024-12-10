using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardAchie
{
    Money, Diamond, SilverKey, GoldKey
}
public enum CheckAchievement
{
    Gold, Level, Boss, Enemy, SignIn 
}
public enum StateAchie
{
    Nope, Completed, Taked
}
[CreateAssetMenu(menuName = "MainMenu/Achievement")]
public class AchievementData : ScriptableObject
{
    public int questId;
    public Sprite icon;
    public string nameAchivement;
    public int countAchievement;
    public int completeAchivement;
    public int rewardAchievement;
    public StateAchie stateAchievement;
    public CheckAchievement checkAchievement;
    public RewardAchie rewardAchie;
}
