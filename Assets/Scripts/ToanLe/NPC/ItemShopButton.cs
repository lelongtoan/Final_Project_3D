using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopButton : MonoBehaviour
{
    public TextMeshProUGUI itemNameText; 
    public TextMeshProUGUI itemPriceText; 
    public Image itemImage;
    public TextMeshProUGUI buyText;
    private int index;
    private bool locked;

    public void Set(ItemInShop item)
    {
        gameObject.SetActive(true);
        itemNameText.text = item.nameItem;
        itemPriceText.text = $"{item.priceItem}";

        if (item.itemShop != null && item.itemShop.icon != null)
        {
            itemImage.sprite = item.itemShop.icon;
            itemImage.enabled = true;
        }
        else
        {
            itemImage.enabled = false;
        }

        if (item.locked)
        {
            buyText.text = "Lock";
        }
        else
        {
            
            buyText.text = "Buy";
        }
        locked = item.locked;
    }
    public void Clean()
    {
        buyText.text = "";
        itemNameText.text = "";
        itemPriceText.text = "";
        itemImage.sprite = null;
        itemImage.enabled = false;
    }
    public void SetIndex(int newIndex)
    {
        index = newIndex;
    }
    public void BuyButton()
    {
        if(!locked)
        {
            GameInstance.instance.npcShop.OnClick(index);
        }
    }
}
