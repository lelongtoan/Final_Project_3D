using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI quatityText;

    public void Set(ItemSlot itemSlot)
    {
        icon.sprite = itemSlot.item.icon;
        nameText.text = itemSlot.item.itemName;
        quatityText.text = itemSlot.count + "";
    }
}
