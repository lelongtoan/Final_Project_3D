using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPCChat")]
public class NPCChatData : ScriptableObject
{
    public string npcName;
    public Sprite npcImage;
    public string[] npcChat;

    public Quest quest;
}
