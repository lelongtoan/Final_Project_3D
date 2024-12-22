using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MainMenu/Start Info")]
public class StartInfo : ScriptableObject
{
    public Sprite avatar;
    public string nameChar;
    public int level;
    public int point;
    public bool isSave;
}
