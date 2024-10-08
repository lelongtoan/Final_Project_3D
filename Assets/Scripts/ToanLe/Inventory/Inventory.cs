using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : ItemManager
{
    public static Inventory inventoryInstance { get; set; }

    int idTemp;
    private void Awake()
    {
        inventoryInstance = this;
    }
    private void Update()
    {
        SetIndex();
        ShowInventory();
    }
    public override void OnClick(int id)
    {
        idTemp = id;
        DragAndDrop.dragAndDrop.OnClick(inventory.slots[id]);
        InGameMenu.inGameMenu.SetDragAndDrop();

    }
}
