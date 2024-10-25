using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int skipQuest;//List Id Quest Complete
    public int listQuest;//List Id Quest Show
    public List<GameObject> quests;
    public RectTransform scrollArea;
    public float heightSize;
    private void Start()
    {
        //heightSize = 50f;
        //foreach (GameObject quest in quests)
        //{
        //    quest.SetActive(false);
        //}
        //for (int i = 0; i < skipQuest; i++)
        //{
        //    quests[i].SetActive(true);
        //    if (quests[i].GetComponent<Quest>().itemOpen != null)
        //    {
        //        quests[i].GetComponent<Quest>().itemOpen.canShow = true;
        //    }
        //}
        UpdateQuest();
    }
    public void UpdateQuest()
    {
        for (int i = 0; i < quests.Count; i++)
        {
            //thisQuest = quests[i];
            ////Debug.Log("Q1");
            //quests[i].SetActive(true);
            //if (!quests[i].GetComponent<Quest>().completedBool)
            //{
            //    scrollArea.anchoredPosition = new Vector2(0f, -(heightSize / 2));
            //    scrollArea.sizeDelta = new Vector2(1000f, heightSize);
            //}
            //else
            //{
            //    if (quests[i].GetComponent<Quest>().itemOpen != null)
            //    {
            //        quests[i].GetComponent<Quest>().itemOpen.canShow = true;
            //    }
            //}
        }
    }
}
