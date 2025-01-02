using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;
    //------------------------------------------
    public ItemContainer itemContainer;
    public ItemManager itemManager;
    public BuffManager buffManager;
    public EquipmentGenerator equipmentGenerator;
    public NPCShop npcShop;
    public QuestManager questManager;
    public NPCCraft npcCraft;
    public PlayerInfor playerInfor;
    public GameReport gameReport;
    public InGameMenu gameMenu;
    public NPCChat chat;
    public ItemD itemD;
    public ValueSell valueSell;
    public PickUpItem pickUpItem;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (playerInfor == null)
        {
            playerInfor = FindAnyObjectByType<PlayerInfor>();
        }
        if (itemContainer == null)
        {
            itemContainer = FindAnyObjectByType<ItemContainer>();
        }
        if (itemManager == null)
        {
            itemManager = FindAnyObjectByType<ItemManager>();
        }

        if (buffManager == null)
        {
            buffManager = FindAnyObjectByType<BuffManager>();
        }

        if (equipmentGenerator == null)
        {
            equipmentGenerator = FindAnyObjectByType<EquipmentGenerator>();
        }

        if (npcShop == null)
        {
            npcShop = FindAnyObjectByType<NPCShop>();
        }

        if (questManager == null)
        {
            questManager = FindAnyObjectByType<QuestManager>();
        }

        if (npcCraft == null)
        {
            npcCraft = FindAnyObjectByType<NPCCraft>();
        }

        if (gameReport == null)
        {
            gameReport = FindAnyObjectByType<GameReport>();
        }

        if (gameMenu == null)
        {
            gameMenu = FindAnyObjectByType<InGameMenu>();
        }

        if (chat == null)
        {
            chat = FindAnyObjectByType<NPCChat>();
        }

        if (itemD == null)
        {
            itemD = FindAnyObjectByType<ItemD>();
        }

        if (valueSell == null)
        {
            valueSell = FindAnyObjectByType<ValueSell>();
        }

        if (pickUpItem == null)
        {
            pickUpItem = FindAnyObjectByType<PickUpItem>();
        }
    }
}
