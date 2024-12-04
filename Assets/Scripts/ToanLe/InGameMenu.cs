using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] public GameObject characterPanel;
    [SerializeField] public GameObject questPanel;
    [SerializeField] public GameObject gameOver;
    [Header("NPC")]
    [SerializeField] public GameObject npcPanel;
    [SerializeField] public GameObject shopNPCPanel;
    [SerializeField] public GameObject buyNPCPanel;
    [SerializeField] public GameObject craftNPCPanel;
    [SerializeField] public GameObject npcChatPanel;

    [Header("Iventory")]
    [SerializeField] public GameObject infoItem;
    [SerializeField] public GameObject dragAndDrop;
    [Header("Game Report")]
    [SerializeField] public GameObject gameReportPanel;
    public void SetGameReport()
    {
        gameReportPanel.SetActive(!gameReportPanel.activeInHierarchy);
    }
    public void SetCharPanel()
    {
        characterPanel.SetActive(!characterPanel.activeInHierarchy);
    }
    public void SetShopPanel()
    {
        shopNPCPanel.SetActive(!shopNPCPanel.activeInHierarchy);
    }
    public void SetCraftPanel()
    {
        SetShopPanel();
        buyNPCPanel.SetActive(false);
        craftNPCPanel.SetActive(true);
    }
    public void SetBuyPanel()
    {
        SetShopPanel();
        buyNPCPanel.SetActive(true);
        craftNPCPanel.SetActive(false);
    }
    public void SetQuestPanel()
    {
        questPanel.SetActive(!questPanel.activeInHierarchy);
    }
    public void SetSettingPanel()
    {
        //settingPanel.SetActive(!settingPanel.activeInHierarchy);
    }
    public void SetInfoItem()
    {
        infoItem.SetActive(!infoItem.activeInHierarchy);
    }
    public void SetDragAndDrop(bool i)
    {
        dragAndDrop.SetActive(i);
        ItemD.Instance.isMoving = i;
    }
    public void SetGameOver()
    {
        gameOver.SetActive(!gameOver.activeInHierarchy);
    }
    public void SetNPC()
    {
        npcPanel.SetActive(!npcPanel.activeInHierarchy);
    }
    public void SetNPCChat()
    {
        npcChatPanel.SetActive(!npcChatPanel.activeInHierarchy);
    }
    
}
