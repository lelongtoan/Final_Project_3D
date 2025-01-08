using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    public Image iconReward;
    public TextMeshProUGUI rewardAmout;
    public Image stateReward;
    public Image takedReward;
    public RewardCIData reward;

    void Start()
    {
        iconReward.sprite = reward.iconReward;
        rewardAmout.text = reward.rewardAmount.ToString();
    }
    //private void Update()
    //{
    //    if (reward.isCheck)
    //    {
    //        stateReward.color = Color.green;
    //    }
    //}
}
