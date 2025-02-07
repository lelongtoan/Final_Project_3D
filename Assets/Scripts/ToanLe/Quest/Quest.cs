using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum StateQuest
{
    Nope, Completed, Taked
}
[System.Serializable]
[CreateAssetMenu(menuName = "Data/Quest")]
public class Quest : ScriptableObject
{
    public int questId;
    public string questName;
    public string questDes;
    public int numberInt;
    public int numberComplete;
    public bool isShowQuest;
    public StateQuest stateQuest;
    public int isCoin;
    public int exp;
    public bool isStack;
    public Item itemCheck;
    public void Clear()
    {
        stateQuest = StateQuest.Nope;
        isShowQuest = false;
    }
}


