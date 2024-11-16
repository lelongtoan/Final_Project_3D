using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotionButton : MonoBehaviour
{
    public ItemContainer inventory;
    public bool healingPotion;
    public TextMeshProUGUI countText;
    public int countItem;
    void Start()
    {
        countItem = 0;
        SetItemCount();
    }

    // Update is called once per frame
    void Update()
    {
        countText.text = countItem.ToString();
        //countItem = 0;
        //SetItemCount();
    }
    public void UseItem()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (healingPotion && inventory.slots[i].item.itemSet == ItemSet.Heal)
            {
                ItemD.Instance.itemSlot = inventory.slots[i];
                ItemD.Instance.UseItem();
                inventory.slots[i].count--;
                SetItemCount();
                return;
            }
            else if (!healingPotion && inventory.slots[i].item.itemSet == ItemSet.Mana)
            {
                ItemD.Instance.itemSlot = inventory.slots[i];
                ItemD.Instance.UseItem();
                inventory.slots[i].count--;
                SetItemCount();
                return;
            }
        }
    }
    public void SetItemCount()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item != null)
            {
                if (healingPotion && inventory.slots[i].item.itemSet == ItemSet.Heal)
                {
                    countItem++;
                }
                else if (!healingPotion && inventory.slots[i].item.itemSet == ItemSet.Mana)
                {
                    countItem++;
                }
            }
        }
    }
}
