using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StartD
{
    public Sprite image;
    public string nameChar;
    public int level;
    public int point;
    public bool isSave;
}

[System.Serializable]
[CreateAssetMenu(menuName ="MainMenu/StartD")]
public class StartData : ScriptableObject
{
    public List<StartD> data;
}
