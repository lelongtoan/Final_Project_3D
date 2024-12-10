using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsData : MonoBehaviour
{
    public int HP;
    public int MP;
    public int DMG;
    public int DEF;
    public ListSkillNode skillnode;

    public void Set()
    {
        HP = 0; MP = 0; DMG = 0; DEF = 0;
        HP += skillnode.hp;
        MP += skillnode.mp;
        DMG += skillnode.dmg;
        DEF += skillnode.def;
    }
}
