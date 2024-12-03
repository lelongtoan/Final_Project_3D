using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestContainer questContainer;
    public List<GameObject> quests;
    public GameObject content;
    public GameObject quest;
    public ItemManager itemContainer;
    private void Start()
    {
        UpdateQuest();
    }
    public void UpdateQuest()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < questContainer.questList.Count; i++)
        {
            if (questContainer.questList[i].isShowQuest)
            {
                GameObject newQuest = Instantiate(quest);
                newQuest.transform.SetParent(content.transform);
                newQuest.GetComponent<GOQuest>().SetQuest(questContainer.questList[i]);
                quests.Add(newQuest);
            }
        }
    }
    public bool CheckQuest(Quest quest)
    {
        ItemSlot itemSlot = itemContainer.inventory.slots.Find(c => c.item == quest.itemCheck);
        if (itemSlot == null)
        {
            return false;
        }
        if(itemSlot.count >= quest.numberComplete)
        {
            itemContainer.RemoveItem(itemSlot, quest.numberComplete);
            return true;
        }
        return false;
    }
}
