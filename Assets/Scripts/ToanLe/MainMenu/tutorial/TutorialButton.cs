using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] TutorialData tutorialData;
    [SerializeField] Sprite tutorialImg;
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDataTuto);
    }
    public void SetDataTuto()
    {
        tutorialPanel.GetComponent<Tutorial>().data = tutorialData;
        tutorialPanel.GetComponent<Tutorial>()
            .image.gameObject.GetComponent<Image>().sprite = tutorialImg;
        tutorialPanel.SetActive(true);
    }
}
