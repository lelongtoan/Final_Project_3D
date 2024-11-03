using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum StateQuest
{
    Nope, Completed, Taked
}
[CreateAssetMenu(menuName = "Data/Quest")]
public class Quest : ScriptableObject
{
    public int questId;
    public int numberInt;
    public int numberComplete;
    public string questString;
    public bool isShowQuest;
    public StateQuest stateQuest;
    public int isCoin;
    public bool isStack;
    public Item itemOpen;
    public Item itemCheck;
    
}


