using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI textQ;
    [SerializeField] Image select;

    float NumberToClamp;
    public int myIndex;
    public void SetIndex(int index)
    {
        myIndex = index;
    }
    public void Set(ItemSlot slot)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
        if (slot.item.stackable == true)
        {
            textQ.gameObject.SetActive(true);
            textQ.text = slot.count + "";
        }
        else
        {
            textQ.gameObject.SetActive(false);
        }
    }
    public void Clean()
    {
        icon.sprite = null;

        icon.gameObject.SetActive(false);

        textQ.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemManager itemManager = transform.parent.GetComponent<ItemManager>();
        itemManager.OnClick(myIndex);
        Debug.Log(myIndex);
    }

}
