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
        foreach (var itemSlot in craft.requiredItems)
        {
            GameObject newButton = Instantiate(reqItem, content.transform);
            RequiredItem buttonComponent = newButton.GetComponent<RequiredItem>();
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
        NPCCraft.instance.OnClick(index);
    }
}
