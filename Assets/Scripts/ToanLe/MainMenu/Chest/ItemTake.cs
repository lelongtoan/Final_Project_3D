using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTake : MonoBehaviour
{
    [SerializeField] Image iconTake;
    [SerializeField] TextMeshProUGUI quanText;

    public void Set(Sprite image,int quan)
    {
        iconTake.sprite = image;
        quanText.text = quan.ToString();
    }
}
