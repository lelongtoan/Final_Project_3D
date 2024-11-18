using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCraft : MonoBehaviour
{
    public ListCraftData craftInventory;
    public List<ItemCraftButton> buttons = new();
    public GameObject buttonCraftPrefab;
    public GameObject content;
    public static NPCCraft instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateButtons();
        ShowCraftInventory();
    }

    private void CreateButtons()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        buttons.Clear();

        foreach (var craft in craftInventory.craftDataList)
        {
            GameObject newButton = Instantiate(buttonCraftPrefab, content.transform);
            ItemCraftButton buttonComponent = newButton.GetComponent<ItemCraftButton>();
            if (buttonComponent != null)
            {
                buttons.Add(buttonComponent);
            }
        }
    }

    public virtual void ShowCraftInventory()
    {
        for (int i = 0; i < craftInventory.craftDataList.Count && i < buttons.Count; i++)
        {
            if (craftInventory.craftDataList[i] == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(craftInventory.craftDataList[i]);
                buttons[i].SetIndex(i);
            }
        }
    }

    public virtual void OnClick(int id)
    {
        CraftData selectedCraft = craftInventory.craftDataList[id];
        if (id >= 0 && id < craftInventory.craftDataList.Count)
        {
            //asdfasdf
        }
    }
}
