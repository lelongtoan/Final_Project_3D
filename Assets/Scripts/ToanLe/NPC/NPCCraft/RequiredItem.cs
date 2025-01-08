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
    public void Set(int sl,Sprite img = null)
    {
        quantity.text = count +"/"+ sl;
        if(count < sl)
        {
            quantity.color = Color.red;
        }
        else
        {
            quantity.color= Color.green;
        }
        if (img != null )
            icon.sprite = img;
    }
}
