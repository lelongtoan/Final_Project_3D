using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] List<TutorialData> tutorialData;
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDataTuto);
    }
    public void SetDataTuto()
    {
        var tutorialScript = tutorialPanel.GetComponent<Tutorial>();
        if (tutorialScript != null)
        {
            tutorialScript.dataList = tutorialData;
            tutorialScript.currentIndex = 0;
            tutorialScript.TutorialSet();
            tutorialPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Tutorial script không được tìm thấy trên panel!");
        }
    }
}
