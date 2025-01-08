using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShop : MonoBehaviour
{
    public ListItemShop shopInventory;
    public List<ItemShopButton> buttons = new();
    public GameObject buttonItemShop;
    public GameObject content;
    SoundEffect sound;
    private void OnEnable()
    {
        CreateButtons();
        ShowShopInventory();
    }
    private void CreateButtons()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        buttons.Clear();
        int x = 0;
        foreach (var item in shopInventory.itemsInShop)
        {
            GameObject newButton = Instantiate(buttonItemShop, content.transform);
            ItemShopButton buttonComponent = newButton.GetComponent<ItemShopButton>();
            if (buttonComponent != null)
            {
                buttons.Add(buttonComponent);
                x++;
            }
        }
        UpdateContentHeight(x);
    }
    private void UpdateContentHeight(int questCount)
    {
        RectTransform contentRect = content.GetComponent<RectTransform>();
        if (contentRect != null)
        {
            float totalHeight = questCount * 135;

            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, totalHeight);
        }
    }
    public virtual void ShowShopInventory()
    {
        for (int i = 0; i < shopInventory.itemsInShop.Count && i < buttons.Count; i++)
        {
            if (shopInventory.itemsInShop[i] == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(shopInventory.itemsInShop[i]);
                buttons[i].SetIndex(i);
            }
        }
    }
    public virtual void OnClick(int id)
    {
        if (sound == null)
        {
            sound = FindObjectOfType<SoundEffect>();
        }
        ItemInShop selectedItem = shopInventory.itemsInShop[id];
        if (id >= 0 && id < shopInventory.itemsInShop.Count)
        {
            if (PlayerInfor.Instance.money >= selectedItem.priceItem)
            {
                if (GameInstance.instance.itemManager.inventory.CheckFull(selectedItem.itemShop))
                {
                    sound.PlaySound("Buy");  
                    GameInstance.instance.itemManager.inventory.Add(selectedItem.itemShop, 1);
                    PlayerInfor.Instance.GetMoney(-selectedItem.priceItem);
                    GameInstance.instance.gameReport.SetReport($"Bạn đã mua {selectedItem.itemShop.itemName} với giá {selectedItem.priceItem} vàng!");
                }
            }
            else
            {
                GameInstance.instance.gameReport.SetReport("Bạn Không Đủ Tiền");
            }
        }
    }
}
