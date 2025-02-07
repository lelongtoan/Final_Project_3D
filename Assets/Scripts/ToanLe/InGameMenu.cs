using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public static InGameMenu Instance;
    [SerializeField] public GameObject settingPanel;
    [SerializeField] public GameObject characterPanel;
    [SerializeField] public GameObject questPanel;
    [SerializeField] public GameObject gameOver;
    [SerializeField] public GameObject soundPanel;
    [SerializeField] public GameObject helpPanel;
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
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CloseAll();
    }
    public void CloseAll()
    {
        settingPanel.SetActive(false);
        characterPanel.SetActive(false);
        questPanel.SetActive(false);
        gameOver.SetActive(false);
        soundPanel.SetActive(false);
        npcPanel.SetActive(false);
        shopNPCPanel.SetActive(false);
        buyNPCPanel.SetActive(false);
        craftNPCPanel.SetActive(false);
        npcChatPanel.SetActive(false);
        infoItem.SetActive(false);
        dragAndDrop.SetActive(false);
        gameReportPanel.SetActive(false);
        helpPanel.SetActive(false);
    }
    public void SetSetting(bool on)
    {
        if(on)
        {
            if (sound == null)
            {
                sound = FindObjectOfType<SoundEffect>();
            }
            sound.PlaySound("Button");
            Time.timeScale = 0;
            settingPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            if (sound == null)
            {
                sound = FindObjectOfType<SoundEffect>();
            }
            sound.PlaySound("Button");
            settingPanel.SetActive(false);
        }
    }
    public void SetGameReport()
    {
        gameReportPanel.SetActive(!gameReportPanel.activeInHierarchy);
    }
    public void SetHelpPanel()
    {
        if (sound == null)
        {
            sound = FindObjectOfType<SoundEffect>();
        }
        sound.PlaySound("Button");
        helpPanel.SetActive(!helpPanel.activeInHierarchy);
    }
    public void SetSoundPanel()
    {
        if (sound == null)
        {
            sound = FindObjectOfType<SoundEffect>();
        }
        sound.PlaySound("Button");
        soundPanel.SetActive(!soundPanel.activeInHierarchy);
    }
    public void SetCharPanel()
    {
        if (sound == null)
        {
            sound = FindObjectOfType<SoundEffect>();
        }
        characterPanel.SetActive(!characterPanel.activeInHierarchy);
        sound.PlaySound("Bag");
    }
    public void SetShopPanel()
    {
        if (sound == null)
        {
            sound = FindObjectOfType<SoundEffect>();
        }
        sound.PlaySound("Button");
        shopNPCPanel.SetActive(!shopNPCPanel.activeInHierarchy);
    }
    public void SetCraftPanel()
    {
        if (sound == null)
        {
            sound = FindObjectOfType<SoundEffect>();
        }
        sound.PlaySound("Button");
        SetShopPanel();
        buyNPCPanel.SetActive(false);
        craftNPCPanel.SetActive(true);
    }
    public void SetBuyPanel()
    {
        if (sound == null)
        {
            sound = FindObjectOfType<SoundEffect>();
        }
        sound.PlaySound("Button");
        SetShopPanel();
        buyNPCPanel.SetActive(true);
        craftNPCPanel.SetActive(false);
    }
    public void SetQuestPanel()
    {
        if (sound == null)
        {
            sound = FindObjectOfType<SoundEffect>();
        }
        sound.PlaySound("Quest");
        questPanel.SetActive(!questPanel.activeInHierarchy);
    }
    public void SetInfoItem()
    {
        if (sound == null)
        {
            sound = FindObjectOfType<SoundEffect>();
        }
        sound.PlaySound("Button");
        infoItem.SetActive(!infoItem.activeInHierarchy);
    }
    public void SetDragAndDrop(bool i)
    {
        if (sound == null)
        {
            sound = FindObjectOfType<SoundEffect>();
        }
        sound.PlaySound("Button");
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
        if (sound == null)
        {
            sound = FindObjectOfType<SoundEffect>();
        }
        sound.PlaySound("Button");
    }
    public void SetNPCChat()
    {
        npcChatPanel.SetActive(!npcChatPanel.activeInHierarchy);
    }
}
