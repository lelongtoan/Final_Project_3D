using UnityEngine;
using System;
using UnityEngine.UI;

public class WeeklyCheckInManager : MonoBehaviour
{
    public RewardCIData[] rewards;
    public Button[] dayButtons;

    private void OnEnable()
    {
        UpdateButtonStates();
    }
    void UpdateButtonStates()
    {
        string lastDate = PlayerPrefs.GetString("LastCheckInDate", "");
        DateTime today = DateTime.Now.Date;

        bool canEnableNext = true; // Chỉ cho phép kích hoạt nút đầu tiên không được đánh dấu

        for (int i = 0; i < dayButtons.Length; i++)
        {
            Button button = dayButtons[i];
            RewardUI rewardUI = button.GetComponent<RewardUI>();
            RewardCIData reward = rewards[i];

            if (reward.isCheck)
            {
                button.interactable = false;
                rewardUI.stateReward.color = Color.green; // Đã nhận - màu xanh lá
                rewardUI.takedReward.gameObject.SetActive(true);
            }
            else if (canEnableNext)
            {
                if (DateTime.TryParse(lastDate, out DateTime lastCheckInDate))
                {
                    TimeSpan diff = today - lastCheckInDate;
                    if (diff.Days != 0)
                    {
                        button.interactable = true;
                        rewardUI.stateReward.color = Color.green;
                        canEnableNext = false;
                    }
                }
                else if(string.IsNullOrEmpty(lastDate))
                {
                    button.interactable = true;
                    rewardUI.stateReward.color = Color.green;
                    canEnableNext = false;
                }
                rewardUI.takedReward.gameObject.SetActive(false);
            }
            else
            {
                button.interactable = false;
                rewardUI.stateReward.color = Color.white;
                rewardUI.takedReward.gameObject.SetActive(false);
            }

            // Xóa sự kiện cũ và thêm sự kiện mới
            int index = i;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnDayButtonClick(index));
        }
    }

    public void OnDayButtonClick(int index)
    {
        if (!rewards[index].isCheck)
        {
            CheckIn(index);
        }
        else
        {
            Debug.Log("Ngày này đã được điểm danh!");
        }
    }

    public void CheckIn(int index)
    {
        string lastDate = PlayerPrefs.GetString("LastCheckInDate", "");
        DateTime today = DateTime.Now.Date;

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
        PlayerPrefs.Save();

        rewards[index].isCheck = true;
        switch (rewards[index].rewardState)
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

        MainMenuInstance.instance.achieManager.achieCheck.countSignIn++;
        Debug.Log($"Điểm danh ngày {index + 1}. Nhận phần thưởng: {rewards[index].rewardAmount} {rewards[index].rewardState}");

        UpdateButtonStates();
    }
}
