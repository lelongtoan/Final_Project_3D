using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] public TutorialData data;
    [SerializeField] public GameObject image;
    [SerializeField] GameObject content;
    [SerializeField] GameObject tutorialP;
    private void OnEnable()
    {
        TutorialSet();
    }
    public void TutorialSet()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < data.data.Count; i++)
        {
            GameObject tutorialPInstance = Instantiate(tutorialP);
            tutorialPInstance.transform.SetParent(content.transform);
            tutorialPInstance.GetComponent<TutorialP>().Set(data.data[i], (i + 1).ToString());
        }
    }
    public void CloseButton()
    {
        gameObject.SetActive(false);
    }
}
