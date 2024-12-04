using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ListChat")]
public class ListNPCChat : ScriptableObject
{
    public string npcName;
    public Sprite npcImage;
    public List<NPCChatData> list;
}
