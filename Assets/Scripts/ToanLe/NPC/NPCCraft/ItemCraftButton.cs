using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCraftButton : MonoBehaviour
{
    public TextMeshProUGUI craftNameText;
    public Image resultImage;
    public TextMeshProUGUI craftButtonText;
    public GameObject content;
    public GameObject reqItem;
    private int index;

    public void Set(CraftData craft)
    {
        gameObject.SetActive(true);
        craftNameText.text = craft.nameCraft;
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemSlot itemSlot in craft.requiredItems)
        {
            List<ItemSlot> listItemSlot = GameInstance.instance.itemManager.inventory.slots.Where(c => c.item == itemSlot.item).ToList();
            int count = 0;
            foreach (var item in listItemSlot)
            {
                count += item.count;
            }
            GameObject newButton = Instantiate(reqItem, content.transform);
            RequiredItem buttonComponent = newButton.GetComponent<RequiredItem>();
            buttonComponent.count = count;
            buttonComponent.Set(itemSlot.count,itemSlot.item.icon);
        }

        if (craft.resultItem != null && craft.resultItem.icon != null)
        {
            resultImage.sprite = craft.resultItem.icon;
            resultImage.enabled = true;
        }
        else
        {
            resultImage.enabled = false;
        }
        craftButtonText.text = "Craft";
    }
    public void Clean()
    {
        craftNameText.text = "";
        resultImage.sprite = null;
        resultImage.enabled = false;
        craftButtonText.text = "";
    }

    public void SetIndex(int newIndex)
    {
        index = newIndex;
    }

    public void CraftButton()
    {
        GameInstance.instance.npcCraft.OnClick(index);
        InGameMenu.Instance.SetCraftPanel();
        InGameMenu.Instance.SetCraftPanel();
    }
}
