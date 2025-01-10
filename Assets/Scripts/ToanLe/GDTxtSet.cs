using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GDTxtSet : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI diamondText;
    void Update()
    {
        moneyText.text = MainMenuInstance.instance.inforMenu.money.ToString();
        diamondText.text = MainMenuInstance.instance.inforMenu.diamond.ToString();
    }
}
