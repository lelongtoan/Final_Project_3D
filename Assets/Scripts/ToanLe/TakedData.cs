using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateTake
{
    Gold,Diamond,IronKey,SilverKey
}
[System.Serializable]
[CreateAssetMenu(menuName ="Taked Data")]
public class TakedData : ScriptableObject
{
    public int point;
    public Sprite sprite;
    public int quantity;
    public StateTake stateTake;
}
