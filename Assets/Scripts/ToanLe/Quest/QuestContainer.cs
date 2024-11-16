using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Quest Container")]
public class QuestContainer : ScriptableObject
{
    public List<Quest> questList;
}