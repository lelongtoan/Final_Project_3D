using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemManager
{
    public static Inventory inventoryInstance { get; set; }

    private void Awake()
    {
        inventoryInstance = this;
    }
    private void Start()
    {

    }
    private void Update()
    {
        SetIndex();
        ShowInventory();
        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (Input.GetMouseButtonDown(1))
        {

        }
    }
    public override void OnClick(int id)
    {

    }
}
