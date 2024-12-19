using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Set ID Item")]
public class SetIdItem : ScriptableObject
{
    public List<Item> idItemSlots;
    public void SetID()
    {
        for (int i = 0; i < idItemSlots.Count; i++)
        {
            idItemSlots[i].id = i + "";
        }
    }
    public Item GetItemById(string id)
    {
        return idItemSlots.Find(item => item.id == id);  // Trả về Item có ID trùng khớp
    }
}
