using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RewardState
{
    Money,
    Diamond,
    SilverKey,
    GoldKey
}
[CreateAssetMenu(menuName = "Reward System/RewardCheckIn")]
public class RewardCIData : ScriptableObject
{
    public Sprite iconReward;
    public RewardState rewardState;
    public int rewardAmount;
    public bool isCheck;
}
