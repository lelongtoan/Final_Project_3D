using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public static InGameMenu inGameMenu { get; set; }
    private void Awake()
    {
        inGameMenu = this;
    }
    [SerializeField] GameObject characterPanel;
    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject gameOver;
    [Header("NPC")]
    [SerializeField] GameObject npcPanel;
    [SerializeField] GameObject shopNPCPanel;
    [SerializeField] GameObject buyNPCPanel;
    [SerializeField] GameObject craftNPCPanel;
    [SerializeField] GameObject npcChatPanel;

    [Header("Iventory")]
    [SerializeField] GameObject infoItem;
    [SerializeField] GameObject dragAndDrop;
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
