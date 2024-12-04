using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPCChat")]
public class NPCChatData : ScriptableObject
{
    public string[] npcChat;
    public string[] questNope;
    public string[] questComplete;
    public Quest quest;
}
