using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopButton : MonoBehaviour
{
    public Text itemNameText; 
    public Text itemDes; 
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
        AutoSizeText(itemDes);
        AutoSizeText(itemNameText);
        if (item.itemShop != null && item.itemShop.icon != null)
        {
            itemImage.sprite = item.itemShop.icon;
            itemImage.enabled = true;
        }
        else
        {
            itemImage.enabled = false;
        }
        if (item.questShop != null && item.questShop.stateQuest == StateQuest.Taked || item.questShop == null)
        {
            item.locked = false;
        }
        else
        {
            item.locked = true;
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
    private void AutoSizeText(Text text)
    {
        RectTransform rect = text.GetComponent<RectTransform>();
        int minFontSize = 28;

        while ((text.preferredWidth > rect.rect.width || text.preferredHeight > rect.rect.height) && text.fontSize > minFontSize)
        {
            text.fontSize--;
        }
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
