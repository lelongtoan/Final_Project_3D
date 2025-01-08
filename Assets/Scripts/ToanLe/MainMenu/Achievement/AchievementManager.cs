using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public ListAchievement listAchievement;
    public List<GameObject> quests;
    public GameObject content;
    public GameObject quest;
    public AchievementCheck achieCheck;
    private void OnEnable()
    {
        UpdateQuest();
    }
    public void UpdateQuest()
    {   
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        listAchievement.Set();
        int x = 0;
        for (int i = 0; i < listAchievement.listAchievement.Count; i++)
        {
            GameObject newQuest = Instantiate(quest);
            newQuest.transform.SetParent(content.transform);
            newQuest.GetComponent<AchievementGO>().SetAchievement(listAchievement.listAchievement[i]);
            quests.Add(newQuest);
            x++;
        }
        UpdateContentHeight(x);
    }
    private void UpdateContentHeight(int questCount)
    {
        RectTransform contentRect = content.GetComponent<RectTransform>();
        if (contentRect != null)
        {
            float totalHeight = questCount * 220;

            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, totalHeight);
        }
    }
    public void CheckAchie(AchievementData ac)
    {
        if (ac.stateAchievement == StateAchie.Nope)
        {
            switch (ac.checkAchievement)
            {
                case CheckAchievement.Gold:
                    if (ac.completeAchivement >= achieCheck.countGold)
                    {
                        ac.stateAchievement = StateAchie.Completed;
                    }
                    break;

                case CheckAchievement.Level:
                    if (ac.completeAchivement >= achieCheck.countLevel)
                    {
                        ac.stateAchievement = StateAchie.Completed;
                    }
                    break;

                case CheckAchievement.Boss:
                    if (ac.completeAchivement >= achieCheck.countBoss)
                    {
                        ac.stateAchievement = StateAchie.Completed;
                    }
                    break;

                case CheckAchievement.Enemy:
                    if (ac.completeAchivement >= achieCheck.countEnemy)
                    {
                        ac.stateAchievement = StateAchie.Completed;
                    }
                    break;

                case CheckAchievement.SignIn:
                    if (ac.completeAchivement >= achieCheck.countSignIn)
                    {
                        ac.stateAchievement = StateAchie.Completed;
                    }
                    break;

                default:
                    Debug.LogWarning("Unknown achievement type!");
                    break;
            }
        }
    }
    public void SetTake(AchievementData data)
    {
        if (data.stateAchievement == StateAchie.Completed)
        {
            data.stateAchievement = StateAchie.Taked;
            switch (data.rewardAchie)
            {
                case RewardAchie.Money:
                    MainMenuInstance.instance.inforMenu.SetMoney(data.rewardAchievement);
                    break;
                case RewardAchie.Diamond:
                    MainMenuInstance.instance.inforMenu.SetDiamond(data.rewardAchievement);
                    break;

                case RewardAchie.SilverKey:
                    MainMenuInstance.instance.inforMenu.SetSilverKey(data.rewardAchievement);
                    break;

                case RewardAchie.GoldKey:
                    MainMenuInstance.instance.inforMenu.SetGoldKey(data.rewardAchievement);
                    break;

                default:
                    Debug.LogWarning("Unknown reward type!");
                    break;

            }
        }
    }
}
