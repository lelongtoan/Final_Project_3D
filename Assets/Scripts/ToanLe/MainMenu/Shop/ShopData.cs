using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemBuy
{
    Nope, IronKey, SilverKey, Diamond
}

public enum StatePrice
{
    Nope, Money, Diamond
}

[CreateAssetMenu(menuName ="MainMenu/Item In Shop")]
public class ShopData : ScriptableObject
{
    public string itemName;
    public Sprite imageItemBuy;
    public Sprite imagePrice;
    public int quanlity;
    public int price;
    public StatePrice priceType;
    public ItemBuy buyType;
    public PerkData perkData;
}
