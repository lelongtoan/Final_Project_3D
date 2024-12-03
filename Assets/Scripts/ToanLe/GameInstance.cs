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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (playerInfor == null)
        {
            playerInfor = FindAnyObjectByType<PlayerInfor>();
            if (playerInfor == null)
            {
                Debug.LogWarning("Không tìm thấy PlayerInfor trong Scene!");
            }
        }
        if (itemContainer == null)
        {
            itemContainer = FindAnyObjectByType<ItemContainer>();
            if (itemContainer == null)
            {
                Debug.LogWarning("Không tìm thấy ItemContainer trong Scene!");
            }
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
    }
    private void Update()
    {
        if(playerInfor == null)
        {
            playerInfor = FindAnyObjectByType<PlayerInfor>();
        }
    }
}
