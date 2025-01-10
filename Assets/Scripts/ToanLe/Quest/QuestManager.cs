using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestContainer questContainer;
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
        RectTransform contentRect = content.GetComponent<RectTransform>();
        float contentWidth = contentRect.rect.width;
        Debug.Log(contentWidth);
        float scaleFactor = contentWidth / 1120f;
        int x = 0;
        for (int i = 0; i < questContainer.questList.Count; i++)
        {
            if (questContainer.questList[i] != null && questContainer.questList[i].isShowQuest)
            {
                GameObject newQuest = Instantiate(quest);
                newQuest.transform.SetParent(content.transform);
                newQuest.GetComponent<GOQuest>().SetQuest(questContainer.questList[i], contentWidth, 200 * scaleFactor);
                x++;
            }
        }
        UpdateContentHeight(x, scaleFactor);
    }
    private void UpdateContentHeight(int questCount, float scaleFactor)
    {
        RectTransform contentRect = content.GetComponent<RectTransform>();
        if (contentRect != null)
        {
            float totalHeight = questCount * (200 * scaleFactor) + 20; // Chiều cao từng item theo tỷ lệ

            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, totalHeight);
        }
    }
    public bool CheckQuest(Quest quest)
    {
        ItemSlot itemSlot = itemContainer.inventory.slots.Find(c => c.item == quest.itemCheck);
        if (itemSlot == null)
        {
            return false;
        }
        if (itemSlot.count >= quest.numberComplete)
        {
            return true;
        }
        return false;
    }
    public void RemoveItemQuest(Quest quest)
    {
        ItemSlot itemSlot = itemContainer.inventory.slots.Find(c => c.item == quest.itemCheck);
        itemContainer.RemoveItem(itemSlot, quest.numberComplete);
    }
}