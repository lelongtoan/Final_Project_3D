using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public string npcName;
    [SerializeField] TextMeshProUGUI namePanel;
    [Header("Đang Xài")]
    [SerializeField] NPCChat chat;
    [SerializeField] NPCCraft craft;
    [SerializeField] NPCShop shop;
    [Header("Sử Dụng")]
    [SerializeField] ListCraftData listCraft;
    [SerializeField] ListNPCChat listChat;
    [SerializeField] ListItemShop listItemShop;
    [Header("Nút")]
    [SerializeField] GameObject shopGO;
    [SerializeField] GameObject craftGO;
    [SerializeField] GameObject questGO;
    [Header("Tương Tác")]
    [SerializeField] GameObject attack;
    [SerializeField] GameObject interact;
    [SerializeField] Button interactButton;
    private bool isPlayerInRange = false;

    private void Start()
    {
        attack.SetActive(true);
        interact.SetActive(false);
        interactButton.onClick.AddListener(OnInteractButtonClicked);

    }

    private void OnInteractButtonClicked()
    {
        if (isPlayerInRange)
        {
            SetNPCInteract();
        }
    }
    public void CloseButtonNPC()
    {
        shopGO.SetActive(false);
        craftGO.SetActive(false);
        questGO.SetActive(false);
    }
    public void SetNPCInteract()
    {
        shopGO.SetActive(false);
        craftGO.SetActive(false);
        questGO.SetActive(false);
        if (listChat!=null)
        {
            chat.npcChats = listChat;
            questGO.SetActive(true);
        }
        else
        {
            chat.npcChats = null;
        }
        if(listCraft != null)
        {
            craft.craftInventory = listCraft;
            craftGO.SetActive(true);
        }
        else
        {
            craft.craftInventory = null;
        }
        if(listItemShop != null)
        {
            shop.shopInventory = listItemShop;
            shopGO.SetActive(true);
        }
        else
        {
            shop.shopInventory = null;
        }
        namePanel.text = npcName;
        Debug.Log($"Tương tác với NPC: {gameObject.name}"); 
        InGameMenu.Instance.SetNPC();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            attack.SetActive(false);
            interact.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            attack.SetActive(true);
            interact.SetActive(false);
        }
    }
}
