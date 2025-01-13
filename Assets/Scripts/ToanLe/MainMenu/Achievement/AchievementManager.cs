using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public ListAchievement listAchievement;
    public GameObject content;
    public GameObject quest;
    public AchievementCheck achieCheck;
    SoundEffect sound;
    private void Start()
    {
        sound = FindObjectOfType<SoundEffect>();
    }
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
        RectTransform contentRect = content.GetComponent<RectTransform>();
        float contentWidth = contentRect.rect.width;
        Debug.Log(contentWidth);
        float scaleFactor = contentWidth / 1120f;
        int x = 0;
        for (int i = 0; i < listAchievement.listAchievement.Count; i++)
        {
            GameObject newQuest = Instantiate(quest);
            newQuest.transform.SetParent(content.transform);
            newQuest.GetComponent<AchievementGO>().SetAchievement(listAchievement.listAchievement[i], contentWidth, 200 * scaleFactor);
            x++;
        }
        UpdateContentHeight(x, scaleFactor);
    }

    private void UpdateContentHeight(int questCount,float scaleFactor)
    {
        RectTransform contentRect = content.GetComponent<RectTransform>();
        if (contentRect != null)
        {
            float totalHeight = questCount * (200 * scaleFactor) + 20; // Chiều cao từng item theo tỷ lệ

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
                        sound.PlaySound("Complete");
                    }
                    break;

                case CheckAchievement.Level:
                    if (ac.completeAchivement >= achieCheck.countLevel)
                    {
                        ac.stateAchievement = StateAchie.Completed;
                        sound.PlaySound("Complete");
                    }
                    break;

                case CheckAchievement.Boss:
                    if (ac.completeAchivement >= achieCheck.countBoss)
                    {
                        ac.stateAchievement = StateAchie.Completed;
                        sound.PlaySound("Complete");
                    }
                    break;

                case CheckAchievement.Enemy:
                    if (ac.completeAchivement >= achieCheck.countEnemy)
                    {
                        ac.stateAchievement = StateAchie.Completed;
                        sound.PlaySound("Complete");
                    }
                    break;

                case CheckAchievement.SignIn:
                    if (ac.completeAchivement >= achieCheck.countSignIn)
                    {
                        ac.stateAchievement = StateAchie.Completed;
                        sound.PlaySound("Complete");
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
