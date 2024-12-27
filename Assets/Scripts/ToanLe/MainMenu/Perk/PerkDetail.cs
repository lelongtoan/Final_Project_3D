﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerkDetail : MonoBehaviour
{
    public int idEquip;
    [SerializeField] PerkData perk;
    [SerializeField] Image perkImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI desText;
    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Button buttonPerk;

    [SerializeField] PerkEquipmentData equipData;
    public void Set(PerkData data,bool isEquip,int idEquip = -1)
    {
        this.idEquip = idEquip;
        perk = data;
        UpdateUI();
        buttonPerk.onClick.RemoveAllListeners();
        buttonPerk.interactable = false;
        if (data.perkState == PerkState.Lock)
        {
            buttonPerk.GetComponentInChildren<TextMeshProUGUI>().text = "Đã Khóa";
        }
        else
        {
            buttonPerk.GetComponentInChildren<TextMeshProUGUI>().text = "Đã Mở";
            if (isEquip)
            {
                buttonPerk.interactable = true;
                EquipButton();

            }
            else if (data.quantity >= Mathf.Pow(2, data.levelPerk))
            {
                buttonPerk.interactable = true;
                buttonPerk.onClick.AddListener(UpLevelPerk);
                buttonPerk.GetComponentInChildren<TextMeshProUGUI>().text = "Tăng Cấp";
            }
        }
        
    }
    private void EquipButton()
    {
        if (idEquip >= 0 && idEquip < equipData.maxSlots
            && equipData.equippedPerks[idEquip] == perk)
        {
            buttonPerk.onClick.AddListener(UnEquipPerk);
            buttonPerk.GetComponentInChildren<TextMeshProUGUI>().text = "Gỡ";
        }
        else
        {
            buttonPerk.onClick.AddListener(EquipPerk);
            buttonPerk.GetComponentInChildren<TextMeshProUGUI>().text = "Trang Bị";
        }
    }
    private void UpdateUI()
    {
        perkImage.sprite = perk.image;
        levelText.text = perk.levelPerk.ToString();

        nameText.text = perk.perkName;

        int totalStats = perk.mp + perk.hp + perk.def + perk.dmg;
        desText.text = $"{perk.description} : {totalStats}";

        quantityText.text = $"{perk.quantity}/{(int)Mathf.Pow(2, perk.levelPerk)}";
    }
    public void UpLevelPerk()
    {
        if(perk.quantity >= Mathf.Pow(2, perk.levelPerk))
        {
            perk.quantity -= (int)Mathf.Pow(2, perk.levelPerk);
            perk.levelPerk++;
            UpdateUI();
        }
        else
        {
            Debug.Log("up level fail");
        }
    }

    public void EquipPerk()
    {
        if (idEquip != -1) 
        {
            equipData.equippedPerks[idEquip] = perk;
            MainMenuManager.Instance.SetPerkMenu(false);
            MainMenuManager.Instance.SetCharMenu();
            MainMenuManager.Instance.SetCharMenu();
            Debug.Log($"Trang bị {perk.perkName} vào slot {idEquip}.");
        }
    }
    public void UnEquipPerk()
    {
        if (idEquip != -1 && equipData.equippedPerks[idEquip] == perk)
        {
            equipData.equippedPerks[idEquip] = null;
            MainMenuManager.Instance.SetPerkMenu(false);
            MainMenuManager.Instance.SetCharMenu();
            MainMenuManager.Instance.SetCharMenu();
            Debug.Log($"Gỡ {perk.perkName} khỏi slot {idEquip}.");
        }
    }
    
}
