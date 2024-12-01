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

    [SerializeField] GameObject useGO;
    public static ItemD Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        itemSlot = new ItemSlot();
    }
    public void SetItem(ItemSlot itemSlot,int id)
    {
        this.itemSlot = itemSlot;
        icon.sprite = itemSlot.item.icon;
        tenText.text = itemSlot.item.name;
        textQ.text = itemSlot.count.ToString();
        textDes.text = itemSlot.item.description;
    }
    public void UseItem()
    {
        if (itemSlot.item == null)
            return;
        if (itemSlot.item.itemSet == ItemSet.Equippable)
        {
            //goi ham eq
            itemSlot = null;
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
        itemSlot = null;
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
                this.itemSlot.Clear();
            }
        }
        
    }
    public void DeleteItem()
    {
        this.itemSlot.Clear();
    }
}
