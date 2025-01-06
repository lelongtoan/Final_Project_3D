using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI desText;
    [SerializeField] TextMeshProUGUI numberText;

    public void Set(string des, string number)
    {
        desText.text = des;
        numberText.text = number;
    }
}
