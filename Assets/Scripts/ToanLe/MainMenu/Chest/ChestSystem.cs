using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSystem : MonoBehaviour
{
    public ListPerkData data;
    public InforMainMenu mainMenu;
    List<PerkData> sPerk;
    List<PerkData> aPerk;
    List<PerkData> bPerk;
    public int minGold = 50;
    public int maxGold = 100;

    float diamondDropRate;

    public bool isNew;
    [Header("Gold - Diamond")]
    public Sprite goldImg;
    public Sprite diaImg;
    [Header("Item Chest Take")]
    public GameObject itemTake;
    public GameObject itemTakePanel;
    public GameObject content;

    private void Awake()
    {
        sPerk = new List<PerkData>();
        aPerk = new List<PerkData>();
        bPerk = new List<PerkData>();
        foreach (PerkData data in data.listPerk)
        {
            if (data.quality == PerkQuality.S)
            {
                sPerk.Add(data);
            }
            else if (data.quality == PerkQuality.A)
            {
                aPerk.Add(data);
            }
            else
            {
                bPerk.Add(data);
            }
        }
    }
    public void SetItemTakePanel()
    {
        itemTakePanel.SetActive(!itemTakePanel.activeInHierarchy);
    }
    public void OpenChest()
    {
        SetItemTakePanel();
        int gold = 0;
        int selectedItem = -1;
        bool hasDiamond = false;
        if (isNew)
        {
            if (mainMenu.silverKey > 0)
            {
                diamondDropRate = 20f;
                gold = Random.Range(minGold, maxGold + 1);
                mainMenu.money = gold;
                Debug.Log($"Bạn nhận được: {gold} vàng");

                hasDiamond = Random.value <= (diamondDropRate / 100);
                if (hasDiamond)
                {
                    Debug.Log("Chúc mừng! Bạn nhận được kim cương!");
                    mainMenu.diamond++;
                }

                selectedItem = GetRandomItem();
                if (selectedItem != -1)
                {
                    PerkData dataPerk = data.listPerk.Find(c => c.id == selectedItem);
                    if (dataPerk != null)
                    {
                        if (dataPerk.perkState == PerkState.Lock)
                        {
                            dataPerk.perkState = PerkState.Unlock;
                        }
                        else
                        {
                            dataPerk.quantity++;
                        }
                    }
                    Debug.Log($"Bạn nhận được vật phẩm: {selectedItem}");
                }
                mainMenu.silverKey--;
            }
        }
        else
        {
            if (mainMenu.ironKey > 0)
            {
                diamondDropRate = 5f;
                gold = Random.Range(minGold, maxGold + 1);
                mainMenu.money += gold;
                Debug.Log($"Bạn nhận được: {gold} vàng");

                hasDiamond = Random.value <= (diamondDropRate / 100);
                if (hasDiamond)
                {
                    Debug.Log("Chúc mừng! Bạn nhận được kim cương!");
                    mainMenu.diamond++;
                }

                selectedItem = GetRandomItem();
                if (selectedItem != -1)
                {
                    PerkData dataPerk = data.listPerk.Find(c => c.id == selectedItem);
                    if (dataPerk != null)
                    {
                        if(dataPerk.perkState == PerkState.Lock)
                        {
                            dataPerk.perkState = PerkState.Unlock;
                        }
                        else
                        {
                            dataPerk.quantity++;
                        }
                    }
                    Debug.Log($"Bạn nhận được vật phẩm: {selectedItem}");
                }
                mainMenu.ironKey--;
            }
        }
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        Vector3 targetPosition = content.transform.position;
        if (gold > 0)
        {

            GameObject go = Instantiate(itemTake);
            go.transform.SetParent(content.transform);
            go.gameObject.GetComponent<ItemTake>().Set(goldImg, gold);
        }
        if(hasDiamond)
        {
            GameObject dia = Instantiate(itemTake);
            dia.transform.SetParent(content.transform);
            dia.gameObject.GetComponent<ItemTake>().Set(diaImg, 1);
        }
        if (selectedItem != -1)
        {
            PerkData perkData = data.listPerk.Find(c=>c.id == selectedItem);
            if(perkData != null)
            {

                GameObject perk = Instantiate(itemTake);
                perk.transform.SetParent(content.transform);
                perk.gameObject.GetComponent<ItemTake>().Set(perkData.image, 1);

            }
            else
            {
                Debug.Log("take perk Error!");
            }
        }

    }

    private int GetRandomItem()
    {
        float totalRate = 0;

        if (isNew)
        {
            foreach (PerkData item in sPerk)
            {
                totalRate += 1;
            }
            foreach (PerkData item in aPerk)
            {
                totalRate += 5;
            }
            foreach (PerkData item in bPerk)
            {
                totalRate += 50;
            }
        }
        else
        {
            foreach (PerkData item in aPerk)
            {
                totalRate += 5;
            }
            foreach (PerkData item in bPerk)
            {
                totalRate += 50;
            }
        }

        float randomValue = Random.Range(0, totalRate);
        float cumulativeRate = 0;

        if (isNew)
        {
            foreach (PerkData item in sPerk)
            {
                cumulativeRate += 1;
                if (randomValue <= cumulativeRate)
                {
                    return item.id;
                }
            }
        }
        foreach (PerkData item in aPerk)
        {
            cumulativeRate += 5;
            if (randomValue <= cumulativeRate)
            {
                return item.id;
            }
        }
        foreach (PerkData item in bPerk)
        {
            cumulativeRate += 50;
            if (randomValue <= cumulativeRate)
            {
                return item.id;
            }
        }

        return -1;
    }
}