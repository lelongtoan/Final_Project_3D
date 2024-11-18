using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemContainer inventory;
    public List<InventoryButton> buttons;
    public static ItemManager intance { get; set; }

    private void Awake()
    {
        intance = this;
    }
    private void Start()
    {
        SetIndex();
        ShowInventory();
    }
    private void OnEnable()
    {
        ShowInventory();
    }
    public virtual void ShowInventory()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            if (inventory.slots[i].item == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }
    public void SetIndex()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }
    public virtual void OnClick(int id)
    {

    }
}
