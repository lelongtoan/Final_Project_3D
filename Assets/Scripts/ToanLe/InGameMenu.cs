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
    [SerializeField] GameObject allPanel;
    //[SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject characterPanel;
    //[SerializeField] GameObject mapPanel;
    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject gameOver;
    [Header("NPC")]
    [SerializeField] GameObject npcPanel;
    [SerializeField] GameObject buyNPCPanel;
    [SerializeField] GameObject craftNPCPanel;
    [SerializeField] GameObject questNPCPanel;

    [Header("Iventory")]
    [SerializeField] GameObject infoItem;
    [SerializeField] GameObject dragAndDrop;
    //[SerializeField] GameObject settingPanel;
    // Start is called before the first frame update
    //public void SetInventoryPanel()
    //{
    //    CloseAllPanel();
    //    inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    //}
    public void SetCharPanel()
    {
        characterPanel.SetActive(!characterPanel.activeInHierarchy);
    }
    public void SetCraftPanel()
    {

    }
    public void SetMapPanel()
    {
        //mapPanel.SetActive(!mapPanel.activeInHierarchy);
    }
    public void SetQuestPanel()
    {
        questPanel.SetActive(!questPanel.activeInHierarchy);
    }
    public void SetSettingPanel()
    {
        //settingPanel.SetActive(!settingPanel.activeInHierarchy);
    }
    public void CloseAllPanel()
    {
        //inventoryPanel.SetActive(false);
        characterPanel.SetActive(false);
        //mapPanel.SetActive(false);
        questPanel.SetActive(false);
        //settingPanel.SetActive(false);
    }

    //Inventory

    public void SetInfoItem()
    {
        infoItem.SetActive(!infoItem.activeInHierarchy);
    }
    public void SetDragAndDrop(bool i)
    {
        dragAndDrop.SetActive(i);
    }
    public void SetGameOver()
    {
        gameOver.SetActive(!gameOver.activeInHierarchy);
    }
    public void SetNPC()
    {
        npcPanel.SetActive(!npcPanel.activeInHierarchy);
    }
    
}
