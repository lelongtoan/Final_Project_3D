using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopButton : MonoBehaviour
{
    public TextMeshProUGUI itemNameText; 
    public TextMeshProUGUI itemDes; 
    public TextMeshProUGUI itemPriceText; 
    public Image itemImage;
    public TextMeshProUGUI buyText;
    private int index;
    private bool locked;

    public void Set(ItemInShop item)
    {
        gameObject.SetActive(true);
        itemNameText.text = item.itemShop.itemName;
        itemPriceText.text = $"{item.priceItem}";
        itemDes.text = item.itemShop.description;
        if (item.itemShop != null && item.itemShop.icon != null)
        {
            itemImage.sprite = item.itemShop.icon;
            itemImage.enabled = true;
        }
        else
        {
            itemImage.enabled = false;
        }
        if (item.questShop != null && item.questShop.stateQuest == StateQuest.Taked)
        {
            item.locked = false;
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
