using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemD : MonoBehaviour
{
    [SerializeField] public ItemSlot itemSlot;
    [SerializeField] TextMeshProUGUI tenText;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI textQ;
    [SerializeField] TextMeshProUGUI textDes;

    [SerializeField] GameObject sellGo;
    [SerializeField] GameObject unEquipGo;
    [SerializeField] GameObject useGO;
    [SerializeField] GameObject unStackGO;
    [SerializeField] GameObject moveGO;
    [SerializeField] GameObject dropGO;
    SoundEffect sound;
    public static ItemD Instance { get; set; }

    public int idSelect;
    public bool sell;
    public bool equipAble;
    public bool isMoving;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        sound = FindObjectOfType<SoundEffect>();
        idSelect = -1;
        Clear();
    }
    private void OnEnable()
    {
        sell = GameInstance.instance.gameMenu.buyNPCPanel.activeInHierarchy;
    }
    public void Clear()
    {
        textQ.enabled = false;
        isMoving = false;
        sellGo.SetActive(false);
        unEquipGo.SetActive(false);
        useGO.SetActive(false);
        unStackGO.SetActive(false);
        moveGO.SetActive(false);
        dropGO.SetActive(false);
        itemSlot = new ItemSlot();
    }
    public void SetItem(ItemSlot itemSlot,int id, int equip = 0)
    {
        Clear();
        idSelect = id;
        this.itemSlot = itemSlot;
        icon.sprite = itemSlot.item.icon;
        tenText.text = itemSlot.item.name;
        textDes.text = itemSlot.item.description; 
        if (sell)
        {
            sellGo.SetActive(true);
            return;
        }
        else
        if (equip != 0)
        {
            unEquipGo.SetActive(true);
            return;
        }
        else
        {
            moveGO.SetActive(true);
            dropGO.SetActive(true);
        }
        if (itemSlot.item.itemSet != ItemSet.Nope)
        {
            if (SceneManager.GetActiveScene().name != "LobbyMap")
            {
                useGO.SetActive(true);
            }
            else if(itemSlot.item.itemSet == ItemSet.Equippable)
            {
                useGO.SetActive(true);
            }
        }
        if (itemSlot.item.stackable)
        {
            textQ.enabled = true;
            textQ.text = itemSlot.count.ToString();
            //unStackGO.SetActive(true);
        }

        Debug.Log("ItemD : 51 : Unstack");
        
    }
    public void UseItem(ItemSlot itemS)
    {
        if (itemS != null)
        {
            itemSlot = new ItemSlot();
            itemSlot = itemS;
        }
        UseItem();
    }
    public void UseItem()
    {
        if (itemSlot.item == null)
            return;
        if (itemSlot.item.itemSet == ItemSet.Equippable)
        {
            GameInstance.instance.itemManager.equipment.UseEquipment(itemSlot);
            if (sound == null)
            {
                sound = FindObjectOfType<SoundEffect>();
            }
            sound.PlaySound("Cloth");
            itemSlot = new ItemSlot();
            return;
            
        }
        else if (itemSlot.item.itemSet == ItemSet.Heal)
        {
            GameInstance.instance.playerInfor.HealthRecovery((int)itemSlot.item.HP);
            if (sound == null)
            {
                sound = FindObjectOfType<SoundEffect>();
            }
            sound.PlaySound("Drink");
        }
        else if (itemSlot.item.itemSet == ItemSet.Mana)
        {
            GameInstance.instance.playerInfor.ManaRecover((int)itemSlot.item.MP);
            if (sound == null)
            {
                sound = FindObjectOfType<SoundEffect>();
            }
            sound.PlaySound("Drink");
        }
        else
        {
            if (itemSlot.item.buff != Buff.Nope)
            {
                GameInstance.instance.buffManager.ActivateBuff(itemSlot.item);
                if (sound == null)
                {
                    sound = FindObjectOfType<SoundEffect>();
                }
                sound.PlaySound("Drink");
            }
            else
            {
                GameInstance.instance.playerInfor.HealthRecovery((int)itemSlot.item.HP);
                GameInstance.instance.playerInfor.ManaRecover((int)itemSlot.item.MP);

            }
        }
        itemSlot.count--;
        if (itemSlot.count <= 0)
        {
            itemSlot.Clear();
        }
        itemSlot = new ItemSlot();
    }
    public void UnEquipment()
    {
        if (GameInstance.instance.itemManager.inventory.CheckFull(itemSlot.item))
        {
            GameInstance.instance.itemManager.inventory.Add(itemSlot.item);
            GameInstance.instance.playerInfor.UpMaxHP(-itemSlot.item.HP);
            GameInstance.instance.playerInfor.UpMaxMP(-itemSlot.item.MP);
            GameInstance.instance.playerInfor.UpDame((int)-itemSlot.item.Dmg);
            if (sound == null)
            {
                sound = FindObjectOfType<SoundEffect>();
            }
            sound.PlaySound("Cloth");
            this.itemSlot.Clear();
        }
    }
    public void MoveItem(ItemSlot itemSlot)
    {
        if (ReferenceEquals(this.itemSlot, itemSlot))
        {
            GameInstance.instance.gameReport.SetReport(" ??? Không thể di chuyển vào vị trí cũ! ");
            isMoving = false;
            return;
        }
        if (this.itemSlot.item != null)
        {
            if (this.itemSlot.item == itemSlot.item && itemSlot.item.stackable == true)
            {
                int totalQuantity = this.itemSlot.count + itemSlot.count;

                if (itemSlot.count == 99)
                {
                    Item tempItem = itemSlot.item;
                    int tempDurability = itemSlot.toolDurability;

                    itemSlot.Copy(this.itemSlot);
                    this.itemSlot.Set(tempItem, tempDurability, 99);
                }
                else if (totalQuantity <= 99)
                {
                    itemSlot.count += this.itemSlot.count;
                    this.itemSlot.Clear();
                }
                else
                {
                    int remaining = totalQuantity - 99;
                    itemSlot.count = 99;
                    this.itemSlot.count = remaining;
                }
            }
            else
            {
                Item tempItem = itemSlot.item;
                int tempDurability = itemSlot.toolDurability;
                int tempCount = itemSlot.count;

                itemSlot.Copy(this.itemSlot);
                this.itemSlot.Set(tempItem, tempDurability, tempCount);
            }
        }
        else
        {
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();
        }

        isMoving = false;
    }

    public void SetMoveItem()
    {
        isMoving = true;
    }
    public void DeleteItem()
    {
        this.itemSlot.Clear();
    }
    public void SetSell()
    {
        if (sound == null)
        {
            sound = FindObjectOfType<SoundEffect>();
        }
        sound.PlaySound("Sell");
        ValueSell valueSell = GameInstance.instance.valueSell;
        GameInstance.instance.playerInfor.GetMoney((int)valueSell.price);
        GameInstance.instance.itemManager.RemoveItem(itemSlot, valueSell.quantityItem);
    }
}
