using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShop : MonoBehaviour
{
    public ListItemShop shopInventory;
    public List<ItemShopButton> buttons = new();
    public GameObject buttonItemShop;
    public GameObject content;
    public static NPCShop instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
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

        foreach (var item in shopInventory.itemsInShop)
        {
            GameObject newButton = Instantiate(buttonItemShop, content.transform);
            ItemShopButton buttonComponent = newButton.GetComponent<ItemShopButton>();
            if (buttonComponent != null)
            {
                buttons.Add(buttonComponent);
            }
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
        ItemInShop selectedItem = shopInventory.itemsInShop[id];
        if (id >= 0 && id < shopInventory.itemsInShop.Count)
        {
            if (true /*player.gold >= selectedItem.priceItem*/)
            {
                if (ItemContainer.itemContainer.Add(selectedItem.itemShop, 1))
                {
                    // player.gold -= selectedItem.priceItem;
                    Debug.Log($"Bạn đã chọn mua {selectedItem.nameItem} với giá {selectedItem.priceItem} vàng!");
                }
            }
        }
    }
}
