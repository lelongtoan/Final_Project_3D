using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MainMenu/Infor")]
public class InforMainMenu : ScriptableObject
{
    public int money = 0;
    public int diamond = 0;
    public int ironKey = 0;
    public int silverKey = 0;

    //public List<Perk> packUnclock;
    public void SetMoney(int i)
    {
        money += i;
    }
    public void SetDiamond(int i)
    {
        diamond += i;
    }
    public void SetSilverKey(int i)
    {
        ironKey += i;
    }
    public void SetGoldKey(int i)
    {
        silverKey += i;
    }
}
