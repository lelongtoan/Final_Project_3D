using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AchievementGO : MonoBehaviour
{
    public int idGoAchiement;
    [SerializeField] public Image itemRewardImage;
    [SerializeField] public GameObject completeGO;
    [SerializeField] public GameObject takeGO;
    [SerializeField] public GameObject quaCountGO;
    [SerializeField] public Text detailQuestGO;
    [SerializeField] public Text quaCountText;
    [SerializeField] public Text qualityRewardText;
    [SerializeField] public AchievementData data;
    [SerializeField] AchievementCheck check;
    [SerializeField] float width;
    [SerializeField] float height;
    RectTransform rectTransform;
    void SetClose()
    {
        completeGO.SetActive(false);
        takeGO.SetActive(false);
        quaCountGO.SetActive(false);
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        rectTransform.sizeDelta = new Vector2(width, height);
        if (data.stateAchievement == StateAchie.Nope)
        {
            SetClose();
            quaCountGO.SetActive(true);
        }
        else if (data.stateAchievement == StateAchie.Completed)
        {
            SetClose();
            completeGO.SetActive(true);
            
        }
        else
        {
            SetClose();
            takeGO.SetActive(true);
        }
    }
    public void SetAchievement(AchievementData qc,float width, float height)
    {
        data = qc;
        this.width = width;
        this.height = height;
        idGoAchiement = qc.questId;
        itemRewardImage.sprite = qc.icon;
        detailQuestGO.text = qc.nameAchivement.ToString();
        qualityRewardText.text = qc.rewardAchievement.ToString();
        quaCountText.text = qc.countAchievement + " / " + qc.completeAchivement;
        //AutoSizeText(detailQuestGO);
        //AutoSizeText(qualityRewardText);
        //AutoSizeText(quaCountText);
        if (qc.checkAchievement == CheckAchievement.Gold)
        {
            if(data.stateAchievement == StateAchie.Nope && check.countGold >= qc.completeAchivement)
            {
                data.stateAchievement = StateAchie.Completed;
            }
        }
        else if(qc.checkAchievement == CheckAchievement.Level)
        {
            if (data.stateAchievement == StateAchie.Nope && check.countLevel >= qc.completeAchivement)
            {
                data.stateAchievement = StateAchie.Completed;
            }
        } 
        else if(qc.checkAchievement == CheckAchievement.Boss)
        {
            if (data.stateAchievement == StateAchie.Nope && check.countBoss >= qc.completeAchivement)
            {
                data.stateAchievement = StateAchie.Completed;
            }
        }
        else if(qc.checkAchievement == CheckAchievement.Enemy)
        {
            if (data.stateAchievement == StateAchie.Nope && check.countEnemy >= qc.completeAchivement)
            {
                data.stateAchievement = StateAchie.Completed;
            }
        }
        else
        {
            if (data.stateAchievement == StateAchie.Nope && check.countSignIn >= qc.completeAchivement)
            {
                data.stateAchievement = StateAchie.Completed;
            }
        }
    }
    //private void AutoSizeText(Text text)
    //{
    //    RectTransform rect = text.GetComponent<RectTransform>();
    //    int minFontSize = 8;
    //    text.fontSize = 128;
    //    while ((text.preferredWidth > rect.rect.width || text.preferredHeight > rect.rect.height) && text.fontSize > minFontSize)
    //    {
    //        text.fontSize--;
    //    }
    //}
    public void TakeReward()
    {
        AchievementData data = MainMenuInstance.instance.achieManager.listAchievement.listAchievement.Find(c => c.questId == idGoAchiement);
        MainMenuInstance.instance.achieManager.SetTake(data);
    }
}