using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPerk : MonoBehaviour
{
    public int id;
    [SerializeField] Image image;
    [SerializeField] PerkEquipmentData PerkEquipmentData;
    [SerializeField] GameObject gameObjectPerk;
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
        gameObjectPerk.SetActive(true);
        ShowListPerk.instance.DisplayUnlockedPerks(id, true);
    }
}
