using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCChat : MonoBehaviour
{
    public ListNPCChat npcChats;
    public TextMeshProUGUI nameText;
    public Image avatarImage;
    public TextMeshProUGUI dialogueText;

    private int currentChatIndex = 0;
    private int currentLine = 0;

    private void Start()
    {
        currentChatIndex = 0;
        currentLine = 0;
        ShowNPCInfo();
        ShowDialogueLine();
    }

    private void ShowNPCInfo()
    {
        if (npcChats != null && npcChats.list.Count > currentChatIndex && npcChats.list[currentChatIndex] != null)
        {
            nameText.text = npcChats.list[currentChatIndex].npcName;
            avatarImage.sprite = npcChats.list[currentChatIndex].npcImage;
        }
    }

    private void ShowDialogueLine()
    {
        if (npcChats != null && npcChats.list.Count > currentChatIndex)
        {
            NPCChatData currentChatData = npcChats.list[currentChatIndex];
            if (currentChatData != null && currentLine < currentChatData.npcChat.Length)
            {
                dialogueText.text = currentChatData.npcChat[currentLine];
            }
            else
            {
                EndChat();
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

        if (npcChats.list[currentChatIndex] != null && npcChats.list[currentChatIndex].quest != null)
        {
            npcChats.list[currentChatIndex].quest.isShowQuest = true;
        }

        // đổi sang chat lần sau
        currentLine = 0;
        InGameMenu.inGameMenu.SetNPCChat();
        if (npcChats.list[currentChatIndex].quest.stateQuest == StateQuest.Completed 
            || npcChats.list[currentChatIndex].quest.stateQuest == StateQuest.Taked)
        {
            if (currentChatIndex < npcChats.list.Count - 1)
            {
                currentChatIndex++;
            }
        }
    }
}
