using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] public GameObject settingPanel;
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
    SoundEffect sound;

    private void Start()
    {
        sound = FindObjectOfType<SoundEffect>();
    }
    public void SetSetting(bool on)
    {
        if(on)
        {
            Time.timeScale = 0;
            gameReportPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            gameReportPanel.SetActive(false);
        }
    }
    public void SetGameReport()
    {
        gameReportPanel.SetActive(!gameReportPanel.activeInHierarchy);
    }
    public void SetCharPanel()
    {
        characterPanel.SetActive(!characterPanel.activeInHierarchy);
        sound.PlaySound("Bag");
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
        sound.PlaySound("Quest");
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
