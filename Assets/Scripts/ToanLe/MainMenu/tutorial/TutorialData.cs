using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial")]
public class TutorialData : ScriptableObject
{
    public string description;
    public List<string> data = new List<string>();
    public Sprite image;
}
