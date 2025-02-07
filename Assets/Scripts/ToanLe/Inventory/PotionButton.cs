using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SetItemCount();
    }
    public void UseItem()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (healingPotion && countItem > 0 && inventory.slots[i].item.itemSet == ItemSet.Heal )
            {
                ItemD.Instance.itemSlot = inventory.slots[i];
                ItemD.Instance.UseItem(inventory.slots[i]);
                SetItemCount();
                if (sound == null)
                {
                    sound = FindObjectOfType<SoundEffect>();
                }
                sound.PlaySound("Drink");
                return;
            }
            else if (!healingPotion && countItem > 0 && inventory.slots[i].item.itemSet == ItemSet.Mana)
            {
                ItemD.Instance.itemSlot = inventory.slots[i];
                ItemD.Instance.UseItem(inventory.slots[i]);
                SetItemCount();
                if (sound == null)
                {
                    sound = FindObjectOfType<SoundEffect>();
                }
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
