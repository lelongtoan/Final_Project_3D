using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyChestPanel : MonoBehaviour
{
    public InforMainMenu inforMenu;
    public TextMeshProUGUI silverKeyText;
    public TextMeshProUGUI ironKeyText;

    private void Update()
    {
        silverKeyText.text = inforMenu.silverKey.ToString();
        ironKeyText.text = inforMenu.ironKey.ToString();
    }
}
