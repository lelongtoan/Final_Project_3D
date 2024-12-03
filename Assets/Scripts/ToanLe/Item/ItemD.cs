using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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
    public static ItemD Instance { get; set; }

    public bool equipAble;
    public bool isMoving;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Clear();
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
    public void SetItem(ItemSlot itemSlot, int equip = 0, int sell = 0)
    {
        Clear();
        this.itemSlot = itemSlot;
        icon.sprite = itemSlot.item.icon;
        tenText.text = itemSlot.item.name;
        textDes.text = itemSlot.item.description;
        if (equip != 0)
        {
            unEquipGo.SetActive(true);
            return;
        }
        else
        if (sell != 0)
        {
            sellGo.SetActive(true);
            return;
        }
        else
        {
            moveGO.SetActive(true);
            dropGO.SetActive(true);
        }
        if (itemSlot.item.itemSet != ItemSet.Nope)
        {
            useGO.SetActive(true);
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
            ItemManager.intance.equipment.UseEquipment(itemSlot);
            itemSlot = new ItemSlot();
            return;
            
        }
        else if (itemSlot.item.itemSet == ItemSet.Heal)
        {
            //goi ham HealthR
        }
        else if (itemSlot.item.itemSet == ItemSet.Mana)
        {
            //goi ham ManaR
        }
        else
        {
            //goi ham use
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
        if (ItemManager.intance.inventory.CheckFull())
        {
            ItemManager.intance.inventory.Add(itemSlot.item);
            this.itemSlot.Clear();
        }
    }
    public void MoveItem(ItemSlot itemSlot)
    {
        if (this.itemSlot.item != null)
        {
            if (this.itemSlot.item == itemSlot.item && itemSlot.item.stackable == true)
            {
                itemSlot.toolDurability = (itemSlot.toolDurability + this.itemSlot.toolDurability) / 2;
                itemSlot.count += this.itemSlot.count;
                this.itemSlot.Clear();
            }
            else
            {
                Item item = itemSlot.item;
                int toolDurability = itemSlot.toolDurability;
                int count = itemSlot.count;
                itemSlot.Copy(this.itemSlot);
                this.itemSlot.Set(item, toolDurability, count);
                this.itemSlot = new ItemSlot();
            }
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
}
