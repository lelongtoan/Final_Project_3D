using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MainMenu/List Perk")]
public class ListPerkData : ScriptableObject
{
    public List<PerkData> listPerk;
    public void Set()
    {
        for (int i = 0; i < listPerk.Count; i++)
        {
            listPerk[i].id = i;
        }
    }
}
