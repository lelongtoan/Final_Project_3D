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
    private void Start()
    {
        UpdateQuest();
    }
    public void UpdateQuest()
    {
        for (int i = 0; i < questContainer.questList.Count; i++)
        {
            GameObject newQuest = Instantiate(quest);
            newQuest.transform.SetParent(content.transform);
            newQuest.GetComponent<GOQuest>().SetQuest(questContainer.questList[i]);
            quests.Add(newQuest);
        }
    }
    public void SetShowQuest()
    {
        for (int i = 0; i < quests.Count; i++) 
        {
            quests[i].SetActive(questContainer.questList[i] ? true : false);
        }
    }
}
