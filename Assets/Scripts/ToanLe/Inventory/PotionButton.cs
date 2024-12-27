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
    SoundEffect sound;
    void Start()
    {
        countItem = 0;
        SetItemCount();
        sound = FindObjectOfType<SoundEffect>();
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
            if (healingPotion && inventory.slots[i].item.itemSet == ItemSet.Heal && countItem > 0)
            {
                ItemD.Instance.itemSlot = inventory.slots[i];
                ItemD.Instance.UseItem(inventory.slots[i]);
                SetItemCount();
                sound.PlaySound("Drink");
                return;
            }
            else if (!healingPotion && inventory.slots[i].item.itemSet == ItemSet.Mana && countItem > 0)
            {
                ItemD.Instance.itemSlot = inventory.slots[i];
                ItemD.Instance.UseItem(inventory.slots[i]);
                SetItemCount();
                sound.PlaySound("Drink");
                return;
            }
        }
    }
    public void SetItemCount()
    {
        countItem = 0;
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item != null)
            {
                if (healingPotion && inventory.slots[i].item.itemSet == ItemSet.Heal)
                {
                    countItem += inventory.slots[i].count;
                }
                else if (!healingPotion && inventory.slots[i].item.itemSet == ItemSet.Mana)
                {
                    countItem += inventory.slots[i].count;
                }
            }
        }
    }
}
