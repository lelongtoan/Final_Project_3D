using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class QuestCheck : MonoBehaviour
{
    private void OnEnable()
    {
        GameInstance.instance.chat.currentLine = 0;
        GameInstance.instance.chat.SetInfo();
        int id = GameInstance.instance.chat.currentChatIndex;
        if(id < GameInstance.instance.chat.npcChats.list.Count)
        {
            NPCChatData npcData = GameInstance.instance.chat.npcChats.list[id];
            if (GameInstance.instance.questManager.CheckQuest(npcData.quest)
                    && npcData.quest.isShowQuest == true
                    && npcData.quest.stateQuest == StateQuest.Nope)
            {
                GameInstance.instance.questManager.RemoveItemQuest(npcData.quest);
                npcData.quest.stateQuest = StateQuest.Completed;
                Debug.Log("X");
            }
        }
    }
}
