using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShop : MonoBehaviour
{
    [SerializeField] ListShopData shopData;
    public GameObject content;
    public GameObject quest;

    private void Start()
    {
        UpdateShop();
    }
    public void UpdateShop()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        int x = 0;
        for (int i = 0; i < shopData.shops.Count; i++)
        {

                GameObject newQuest = Instantiate(quest);
                newQuest.transform.SetParent(content.transform);
                newQuest.GetComponent<ItemShopDetail>().Set(shopData.shops[i]);
                x++;
        }
        UpdateContentWidth(x);
    }
    private void UpdateContentWidth(int questCount)
    {
        RectTransform contentRect = content.GetComponent<RectTransform>();
        if (contentRect != null)
        {
            float totalWidth = questCount * 420;

            contentRect.sizeDelta = new Vector2(totalWidth, contentRect.sizeDelta.y);
        }
    }
}
