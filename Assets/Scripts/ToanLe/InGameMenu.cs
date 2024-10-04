using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] GameObject allPanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject characterPanel;
    [SerializeField] GameObject craftPanel;
    [SerializeField] GameObject mapPanel;
    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject settingPanel;
    // Start is called before the first frame update
    public void SetAllPanel()
    {
        allPanel.SetActive(!allPanel.activeInHierarchy);
    }
    public void SetInventoryPanel()
    {
        CloseAllPanel();
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }
    public void SetCharPanel()
    {
        CloseAllPanel();
        characterPanel.SetActive(!characterPanel.activeInHierarchy);
    }
    public void SetCraftPanel()
    {
        CloseAllPanel();
        craftPanel.SetActive(!craftPanel.activeInHierarchy);
    }
    public void SetMapPanel()
    {
        CloseAllPanel();
        mapPanel.SetActive(!mapPanel.activeInHierarchy);
    }
    public void SetQuestPanel()
    {
        CloseAllPanel();
        questPanel.SetActive(!questPanel.activeInHierarchy);
    }
    public void SetSettingPanel()
    {
        CloseAllPanel();
        settingPanel.SetActive(!settingPanel.activeInHierarchy);
    }
    public void CloseAllPanel()
    {
        inventoryPanel.SetActive(false);
        characterPanel.SetActive(false);
        craftPanel.SetActive(false);
        mapPanel.SetActive(false);
        questPanel.SetActive(false);
        settingPanel.SetActive(false);
    }
}
