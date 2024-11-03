using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemSet
{
    Nope,Equippable,Food
}
[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public bool stackable;

    public float HP;
    public int water;
    public int food;

    public bool canShow;
    public string description;
    public ItemSet itemSet;
    public Sprite icon;
}
