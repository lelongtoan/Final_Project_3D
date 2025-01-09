using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopDetail : MonoBehaviour
{
    ShopData shopData;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI quanTxt;
    [SerializeField] Image iconImg;
    [SerializeField] Image iconPriceImg;
    [SerializeField] TextMeshProUGUI quanPriceTxt;
    [SerializeField] Button buttonBuy;
    //[SerializeField] Button buttonWatch;

    private void Awake()
    {
        buttonBuy.onClick.AddListener(Buy);
        //buttonWatch.onClick.AddListener(WatchVideoAds);
    }
    public void Set(ShopData data)
    {
        shopData = data;
        nameTxt.text = data.itemName;
        quanTxt.text = data.quanlity.ToString();
        iconImg.sprite = data.imageItemBuy;
        iconPriceImg.sprite = data.imagePrice;
        quanPriceTxt.text = data.price.ToString();
        if (shopData.buyType == ItemBuy.Nope)
        {
            iconImg.sprite = data.perkData.image;
        }
        if(data.priceType == StatePrice.Nope)
        {
            buttonBuy.gameObject.SetActive(false);
            //buttonWatch.gameObject.SetActive(true);
        }
    }
    public void WatchVideoAds()
    {

    }
    public void Buy()
    {
        if(shopData.priceType == StatePrice.Money)
        {
            if (MainMenuInstance.instance.inforMenu.money < shopData.price)
                return;
            MainMenuInstance.instance.inforMenu.money -= shopData.price;
        }
        else if(shopData.priceType == StatePrice.Diamond)
        {
            if (MainMenuInstance.instance.inforMenu.diamond < shopData.price)
                return;
            MainMenuInstance.instance.inforMenu.diamond -= shopData.price;
        }

        if (shopData.buyType == ItemBuy.Nope)
        {
            if(shopData.perkData == null)
            {
                Debug.LogError("Chua Them Perk hoac chua chinh buyType!!!");
            }
            if(shopData.perkData.perkState == PerkState.Lock)
            {
                shopData.perkData.perkState = PerkState.Unlock;
            }
            shopData.perkData.quantity += shopData.quanlity;
        }
        else 
        {
            if(shopData.buyType == ItemBuy.IronKey)
            {
                MainMenuInstance.instance.inforMenu.ironKey += shopData.quanlity;
            }
            else if(shopData.buyType == ItemBuy.SilverKey)
            {
                MainMenuInstance.instance.inforMenu.silverKey += shopData.quanlity;
            }
            else
            {
                MainMenuInstance.instance.inforMenu.diamond += shopData.quanlity;
            }
        }
    }
}
