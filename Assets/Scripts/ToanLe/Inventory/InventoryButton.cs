using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] GameObject quantity;
    [SerializeField] TextMeshProUGUI textQ;
    [SerializeField] Image select;

    float NumberToClamp;
    public int myIndex;
    private void Start()
    {
        select.enabled = false;
        quantity.SetActive(false);
    }
    private void Update()
    {
        //if (GameInstance.instance.itemD.idSelect == myIndex && GameInstance.instance.gameMenu.dragAndDrop.activeInHierarchy)
        //{
        //   select.gameObject.SetActive(true);
        //}
        //else
        //{
        //    select.gameObject.SetActive(false);
        //}
    }
    public void SetIndex(int index)
    {
        myIndex = index;
    }
    public void Set(ItemSlot slot)
    {
        quantity.SetActive(false);
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
        textQ.text = slot.count + "";
        if (slot.item.stackable == true)
        {
            quantity.SetActive(true);
        }
    }
    public void Clean()
    {
        icon.sprite = null;

        icon.gameObject.SetActive(false);

        quantity.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemManager itemManager = transform.parent.GetComponent<ItemManager>();
        itemManager.OnClick(myIndex);
        Debug.Log(myIndex);
    }

}
