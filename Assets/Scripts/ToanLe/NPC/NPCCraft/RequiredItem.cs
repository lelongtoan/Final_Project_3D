using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequiredItem : MonoBehaviour
{
    public TextMeshProUGUI quantity;
    public Image icon;
    public int count; 
    public void Set(int sl,Sprite img)
    {
        quantity.text = count +"/"+ sl;
        if (img != null )
            icon.sprite = img;
    }
}
