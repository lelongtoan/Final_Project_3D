using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour  , IPointerClickHandler

{
    [SerializeField] Image _sprite;
    Button x;
    public int myIndex;
    private void Awake()
    {
        x = _sprite.gameObject.GetComponent<Button>();
        //x.onClick.AddListener(OnEquip);
    }
    public void SetIndex(int index)
    {
        myIndex = index;
    }

    public void Set(ItemSlot slot)
    {
        _sprite.gameObject.SetActive(true);
        _sprite.sprite = slot.item.icon;
    }
    public void Clean()
    {
        _sprite.sprite = null;

        _sprite.gameObject.SetActive(false);
    }
    public void OnEquip()
    {
        ItemManager itemManager = FindObjectOfType<ItemManager>();
        itemManager.OnClick(myIndex);
        Debug.Log(myIndex);
    } 
    public void OnPointerClick(PointerEventData eventData)
    {
        ItemManager itemManager = FindObjectOfType<ItemManager>();
        itemManager.OnClick(myIndex);
        Debug.Log(myIndex);
    }
}
