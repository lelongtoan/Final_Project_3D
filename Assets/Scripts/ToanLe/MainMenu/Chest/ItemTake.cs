using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTake : MonoBehaviour
{
    [SerializeField] Image iconTake;
    [SerializeField] TextMeshProUGUI quanText;
    public int gold;
    public int diamond;
    public void Set(Sprite image,int quan,int gold = 0,int diamond = 0)
    {
        this.gold = gold;
        this.diamond = diamond;
        iconTake.sprite = image;
        quanText.text = quan.ToString();
    }
}
