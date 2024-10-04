using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController inventoryController { get; set; }

    private void Awake()
    {
        inventoryController = this;
    }
    private void Start()
    {

    }
    private void Update()
    {

    }
}
