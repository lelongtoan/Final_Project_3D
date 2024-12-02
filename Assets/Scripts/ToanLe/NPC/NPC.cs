using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] NPCChat chat;
    [SerializeField] NPCCraft craft;
    [SerializeField] NPCShop shop;

    [SerializeField] ListCraftData listCraft;
    [SerializeField] ListNPCChat listChat;
    [SerializeField] ListItemShop listItemShop;

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

    public void SetNPCInteract()
    {
        chat.npcChats = listChat;
        craft.craftInventory = listCraft;
        shop.shopInventory = listItemShop;

        Debug.Log($"Tương tác với NPC: {gameObject.name}");
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
