using System.Collections.Generic;
using UnityEngine;

public class ShowListPerk : MonoBehaviour
{
    public static ShowListPerk instance;
    public ListPerkData perkData;
    public GameObject content;
    public GameObject perkPrefab;
    public PerkEquipmentData equip;
    private void Awake()
    {
        instance = this;
        DisplayUnlockedPerks(-1, false);
    }
    public void DisplayUnlockedPerks(int idEquip,bool isEquip)
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        List<PerkData> unlockedPerks = new List<PerkData>();
        List<PerkData> lockedPerks = new List<PerkData>();
        foreach (PerkData perk in perkData.listPerk)
        {
            if (perk.perkState == PerkState.Lock)
            {
                lockedPerks.Add(perk);
            }
            else
            {
                unlockedPerks.Add(perk);
            }
        }
        InstantiatePerks(unlockedPerks, idEquip, isEquip);
        InstantiatePerks(lockedPerks, idEquip, isEquip);
        UpdateContentHeight(unlockedPerks.Count + lockedPerks.Count);
    }
    private void InstantiatePerks(List<PerkData> perks, int idEquip, bool isEquip)
    {
        foreach (PerkData perk in perks)
        {

            if (idEquip >= 0 && idEquip < equip.maxSlots && isEquip
                && equip.equippedPerks.Contains(perk) 
                && equip.equippedPerks[idEquip] != perk)
            {
                continue;
            }
            GameObject perkInstance = Instantiate(perkPrefab);
            perkInstance.transform.SetParent(content.transform, false);

            perkInstance.GetComponent<PerkDetail>().Set(perk, isEquip, idEquip);
        }
    }
    private void UpdateContentHeight(int totalPerks)
    {
        RectTransform contentRect = content.GetComponent<RectTransform>();
        if (contentRect != null)
        {
            float totalHeight = totalPerks * 155;

            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, totalHeight);
        }
    }
}
