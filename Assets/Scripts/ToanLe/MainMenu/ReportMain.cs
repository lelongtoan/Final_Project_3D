using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReportMain : MonoBehaviour
{
    public static ReportMain instance;
    public Text reportText;
    [SerializeField] GameObject reportGameObject;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SetR();
    }
    public void SetReport(string reportContent)
    {
        reportGameObject.SetActive(true);
        reportText.text = reportContent;
    }
    public void SetR()
    {
        reportGameObject.SetActive(false);
    }
}
