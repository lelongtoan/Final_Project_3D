using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GOQuest : MonoBehaviour
{
    public int idGOQuest;
    [SerializeField] public GameObject completeGO;
    [SerializeField] public Text nameQuestGO;
    [SerializeField] public TextMeshProUGUI detailQuestGO;
    [SerializeField] public TextMeshProUGUI quaCountText;
    [SerializeField] public Image itemRewardImage;
    [SerializeField] public TextMeshProUGUI qualityRewardText;
    Quest qcQuest;
    private void Start()
    {
        SetQuest(qcQuest);
    }
    public void SetQuest(Quest qc)
    {
        if (qc != null)
        {
            qcQuest = qc;
            List<ItemSlot> itemTemp = GameInstance.instance.itemManager.inventory.slots.Where(c => c.item == qc.itemCheck).ToList();
            int numberInt = 0;
            if (itemTemp != null)
            {
                foreach (ItemSlot itemSlot in itemTemp)
                {
                    numberInt += itemSlot.count;
                }
            }
            nameQuestGO.text = qc.questName;
            qc.numberInt = numberInt;
            idGOQuest = qc.questId;
            detailQuestGO.text = qc.questDes.ToString();
            if (qc.stateQuest == StateQuest.Nope)
            {
                completeGO.SetActive(false);
                quaCountText.gameObject.SetActive(true);
                quaCountText.text = qc.numberInt + " / " + qc.numberComplete;
            }
            else
            {
                completeGO.SetActive(true);
                quaCountText.gameObject.SetActive(false);
            }
            if (qc.isCoin != 0)
            {
                qualityRewardText.gameObject.SetActive(true);
                qualityRewardText.text = qc.isCoin.ToString();
            }
            else
            {
                itemRewardImage.sprite = null;
            }
            if (qc.isStack)
            {
                qualityRewardText.gameObject.SetActive(true);
            }
            else
            {
                qualityRewardText.gameObject.SetActive(false);
            }
        }
    }
}