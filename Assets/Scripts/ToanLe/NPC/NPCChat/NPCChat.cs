using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCChat : MonoBehaviour
{
    public ListNPCChat npcChats;
    public TextMeshProUGUI nameText;
    public Image avatarImage;
    public TextMeshProUGUI dialogueText;

    public int currentChatIndex = 0;
    public int currentLine = 0;
    private void Start()
    {
        SetCurrentChat();
    }
    public void SetCurrentChat()
    {
        for (int i = 0; i < npcChats.list.Count; i++)
        {
            if (npcChats.list[i].quest.stateQuest != StateQuest.Taked)
            {
                currentChatIndex = i;
                break;
            }
            if (i + 1 == npcChats.list.Count)
            {
                currentChatIndex = i + 1;
            }
        }
    }
    public void SetInfo()
    {

        nameText.text = npcChats.npcName;
        if(npcChats.npcImage != null)
        {
            avatarImage.sprite = npcChats.npcImage;
        }
    }
    public void ShowDialogueLine()
    {
        Debug.Log("Chat Index :"+currentChatIndex
            +" Line : "+currentLine);
        if (currentChatIndex >= npcChats.list.Count)
        {
            if (currentLine == 0)
            {
                Debug.Log("xx");
                dialogueText.text = "Xin lỗi! Tôi không còn nhiệm vụ cho bạn nữa.";
            }
            else
            {
                EndChat();
            }
        }
        else
        {
            NPCChatData npcData = npcChats.list[currentChatIndex];
            if (!npcData.quest.isShowQuest)
            {
                if (npcData != null && currentLine < npcData.npcChat.Length)
                {
                    dialogueText.text = npcData.npcChat[currentLine];
                }
                else
                {
                    EndChat();
                }
            }
            else if (npcData.quest.stateQuest == StateQuest.Nope)
            {
                if (npcData != null && currentLine < npcData.questNope.Length)
                {
                    dialogueText.text = npcData.questNope[currentLine];
                }
                else
                {
                    EndChat();
                }
            }
            else
            {
                if (npcData != null && currentLine < npcData.questNope.Length)
                {
                    dialogueText.text = npcData.questComplete[currentLine];
                }
                else
                {
                    GameInstance.instance.playerInfor.GetExp(npcData.quest.exp);
                    GameInstance.instance.playerInfor.GetMoney(npcData.quest.isCoin);

                    npcData.quest.stateQuest = StateQuest.Taked;
                    EndChat();
                }
            }
        }
        
    }

    public void NextLine()
    {
        currentLine++;
        ShowDialogueLine();
    }

    private void EndChat()
    {
        dialogueText.text = "";
        nameText.text = "";
        avatarImage.sprite = null;
        if(npcChats.list.Count > currentChatIndex)
        {
            if (npcChats.list[currentChatIndex] != null && npcChats.list[currentChatIndex].quest != null)
            {
                npcChats.list[currentChatIndex].quest.isShowQuest = true;
            }
            if (npcChats.list[currentChatIndex].quest.stateQuest == StateQuest.Taked)
            {
                if (currentChatIndex < npcChats.list.Count)
                {
                    currentChatIndex++;
                }
            }
        }
        currentLine = 0;
        GameInstance.instance.gameMenu.SetNPCChat();

    }
}
