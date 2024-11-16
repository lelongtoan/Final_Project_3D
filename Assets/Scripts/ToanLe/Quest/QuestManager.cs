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
            if (questContainer.questList[i].isShowQuest)
            {
                GameObject newQuest = Instantiate(quest);
                newQuest.transform.SetParent(content.transform);
                newQuest.GetComponent<GOQuest>().SetQuest(questContainer.questList[i]);
                quests.Add(newQuest);
            }
        }
    }
}
