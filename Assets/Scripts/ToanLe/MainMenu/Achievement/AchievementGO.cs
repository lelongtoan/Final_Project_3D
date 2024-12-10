using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementGO : MonoBehaviour
{
    public int idGoAchiement;
    [SerializeField] public Image itemRewardImage;
    [SerializeField] public GameObject completeGO;
    [SerializeField] public GameObject takeGO;
    [SerializeField] public TextMeshProUGUI detailQuestGO;
    [SerializeField] public TextMeshProUGUI quaCountText;
    [SerializeField] public TextMeshProUGUI qualityRewardText;
    [SerializeField] public AchievementData data;
    private void Start()
    {
        SetClose();
    }
    void SetClose()
    {
        completeGO.SetActive(false);
        takeGO.SetActive(false);
        quaCountText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (data.stateAchievement == StateAchie.Nope)
        {
            SetClose();
            quaCountText.gameObject.SetActive(true);
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
    public void SetAchievement(AchievementData qc)
    {
        data = qc;
        //qc.numberInt = numberInt;
        idGoAchiement = qc.questId;
        itemRewardImage.sprite = qc.icon;
        detailQuestGO.text = qc.nameAchivement.ToString();
        qualityRewardText.text = qc.rewardAchievement.ToString();
        quaCountText.text = data.countAchievement + " / " + data.completeAchivement;
        //Debug.Log(idGOQuest.ToString() + " + " + completeGO.activeInHierarchy + " + "
        //    + detailQuestGO.text + " + "
        //    + quaCountText.text + " + "
        //    + qualityRewardText.text + " + ");
    }
    public void TakeReward()
    {
        AchievementData data = MainMenuInstance.instance.achieManager.listAchievement.listAchievement.Find(c => c.questId == idGoAchiement);
        MainMenuInstance.instance.achieManager.SetTake(data);
    }
}