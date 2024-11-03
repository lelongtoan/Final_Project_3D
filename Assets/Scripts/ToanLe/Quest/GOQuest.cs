using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GOQuest : MonoBehaviour
{
    public int idGOQuest;
    [SerializeField] public GameObject takeGO;
    [SerializeField] public GameObject completeGO;
    [SerializeField] public TextMeshProUGUI detailQuestGO;
    [SerializeField] public TextMeshProUGUI quaCountText;
    [SerializeField] public Image itemRewardImage;
    [SerializeField] public TextMeshProUGUI qualityRewardText;
    public void SetQuest(Quest qc)
    {
        idGOQuest = qc.questId;
        detailQuestGO.text = qc.questString.ToString();
        if(qc.stateQuest == StateQuest.Nope)
        {
            takeGO.SetActive(false);
            completeGO.SetActive(false);
            quaCountText.gameObject.SetActive(true);
            quaCountText.text = qc.numberInt + " / " + qc.numberComplete;
        }
        else if(qc.stateQuest == StateQuest.Completed)
        {
            takeGO.SetActive(false);
            completeGO.SetActive(true);
            quaCountText.gameObject.SetActive(false);
        }
        else
        {
            takeGO.SetActive(true);
            completeGO.SetActive(false);
            quaCountText.gameObject.SetActive(false);
        }
        if (qc.isCoin != 0) 
        {
            qualityRewardText.gameObject.SetActive(true);
            qualityRewardText.text = qc.isCoin.ToString();
        }
        else
        {
            itemRewardImage.sprite = qc.itemOpen.icon;
        }
        if(qc.isStack)
        {
            qualityRewardText.gameObject.SetActive(true);
        }
        else
        {
            qualityRewardText.gameObject.SetActive(false);
        }
        Debug.Log(idGOQuest.ToString() + " + " + completeGO.activeInHierarchy + " + "
            + takeGO.activeInHierarchy + " + "
            + detailQuestGO.text + " + "
            + quaCountText.text + " + "
            + qualityRewardText.text + " + ");
    }
}
