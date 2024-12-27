using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPerk : MonoBehaviour
{
    public static ButtonPerk instance;
    public int id;
    [SerializeField] Image image;
    [SerializeField] Button buttonPerk;
    [SerializeField] PerkEquipmentData PerkEquipmentData;
    private void Awake()
    {
        instance = this;
        buttonPerk.onClick.AddListener(EquipmentPerk);
    }
    private void OnEnable()
    {
        Set();
    }
    public void Set()
    {
        if (PerkEquipmentData.equippedPerks[id] != null)
        {
            image.sprite = PerkEquipmentData.equippedPerks[id].image;
            image.gameObject.SetActive(true);

            Debug.Log($"111."); 
        }
        else
        {
            image.gameObject.SetActive(false);
            Debug.Log($"222.");
        }
    }
    public void EquipmentPerk()
    {
        MainMenuManager.Instance.SetPerkMenu(true);
        ShowListPerk.instance.DisplayUnlockedPerks(id, true);
    }
}
