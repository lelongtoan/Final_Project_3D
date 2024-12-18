using UnityEngine;
using System;
using UnityEngine.UI;

public class WeeklyCheckInManager : MonoBehaviour
{
    public RewardCIData[] rewards;
    public Button[] dayButtons;
    private int currentStreak;

    void Start()
    {
        currentStreak = PlayerPrefs.GetInt("CurrentStreak", 0);
        UpdateButtonStates();
    }

    void UpdateButtonStates()
    {
        for (int i = 0; i < dayButtons.Length; i++)
        {
            Button button = dayButtons[i];
            RewardCIData reward = rewards[i];

            // mở interacable
            button.interactable = (i == currentStreak && !reward.isCheck);

            int index = i;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnDayButtonClick(index));
        }
    }

    public void OnDayButtonClick(int index)
    {
        if (index == currentStreak && !rewards[index].isCheck)
        {
            CheckIn(index);
        }
        else
        {
            Debug.Log("Check Sai Ngay");
        }
    }

    public void CheckIn(int index)
    {
        string lastDate = PlayerPrefs.GetString("LastCheckInDate", "");
        int streak = PlayerPrefs.GetInt("CurrentStreak", 0);
        DateTime today = System.DateTime.Now.Date;
        if (!string.IsNullOrEmpty(lastDate) && DateTime.TryParse(lastDate, out DateTime lastCheckInDate))
        {
            TimeSpan diff = today - lastCheckInDate;

            if (diff.Days == 0)
            {
                Debug.Log("Bạn đã điểm danh hôm nay!");
                return;
            }
        }
        PlayerPrefs.SetString("LastCheckInDate", today.ToString("yyyy-MM-dd"));
        PlayerPrefs.SetInt("CurrentStreak", (currentStreak + 1) % rewards.Length);
        PlayerPrefs.Save();
        rewards[index].isCheck = true;
        switch(rewards[index].rewardState)
        {
            case RewardState.Money:
                MainMenuInstance.instance.inforMenu.SetMoney(rewards[index].rewardAmount);
                break;
            case RewardState.Diamond:
                MainMenuInstance.instance.inforMenu.SetDiamond(rewards[index].rewardAmount);
                break;

            case RewardState.SilverKey:
                MainMenuInstance.instance.inforMenu.SetSilverKey(rewards[index].rewardAmount);
                break;

            case RewardState.GoldKey:
                MainMenuInstance.instance.inforMenu.SetGoldKey(rewards[index].rewardAmount);
                break;

            default:
                Debug.LogWarning("Unknown reward type!");
                break;
        }
        Debug.Log($"Điểm danh ngày {index + 1}. Nhận phần thưởng:  {rewards[index].rewardAmount} {rewards[index].rewardState}");

        UpdateButtonStates();
    }
}
