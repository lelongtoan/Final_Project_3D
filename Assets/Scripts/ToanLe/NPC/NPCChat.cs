using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCChat : MonoBehaviour
{
    public List<NPCChatData> npcChats;
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
        if (npcChats != null && npcChats.Count > currentChatIndex && npcChats[currentChatIndex] != null)
        {
            nameText.text = npcChats[currentChatIndex].npcName;
            avatarImage.sprite = npcChats[currentChatIndex].npcImage;
        }
    }

    private void ShowDialogueLine()
    {
        if (npcChats != null && npcChats.Count > currentChatIndex)
        {
            NPCChatData currentChatData = npcChats[currentChatIndex];
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

        if (npcChats[currentChatIndex] != null && npcChats[currentChatIndex].quest != null)
        {
            npcChats[currentChatIndex].quest.isShowQuest = true;
        }

        // đổi sang chat lần sau
        currentLine = 0;
        if (currentChatIndex < npcChats.Count - 1)
        {
            currentChatIndex++;
        }
    }
}
