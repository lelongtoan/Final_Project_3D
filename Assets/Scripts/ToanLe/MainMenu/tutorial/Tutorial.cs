using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Text nameTxt;
    [SerializeField] public List<TutorialData> dataList;
    [SerializeField] Image image;
    [SerializeField] GameObject content;
    [SerializeField] GameObject tutorialP;
    public int currentIndex = 0;

    private void OnEnable()
    {
        TutorialSet();
    }

    public void Set()
    {
        if (currentIndex < dataList.Count)
        {
            var currentData = dataList[currentIndex];
            nameTxt.text = currentData.description;
            image.sprite = currentData.image;

            RectTransform imageRect = image.GetComponent<RectTransform>();
            if (currentData.image != null)
            {
                Vector2 spriteSize = new Vector2(currentData.image.texture.width, currentData.image.texture.height);

                float widthRatio = 1300f / spriteSize.x;
                float heightRatio = 650f / spriteSize.y;
                float scaleRatio = Mathf.Min(widthRatio, heightRatio, 1f);

                Vector2 adjustedSize = new Vector2(spriteSize.x * scaleRatio, spriteSize.y * scaleRatio);

                imageRect.sizeDelta = adjustedSize;
            }
            EnableWordWrap(nameTxt);
            AutoSizeText(nameTxt);
        }
    }

    private void AutoSizeText(Text text)
    {
        RectTransform rect = text.GetComponent<RectTransform>();
        int minFontSize = 48;

        while ((text.preferredWidth > rect.rect.width || text.preferredHeight > rect.rect.height) && text.fontSize > minFontSize)
        {
            text.fontSize--;
        }
    }

    private void EnableWordWrap(Text text)
    {
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Truncate;
    }
    public void TutorialSet()
    {
        if (currentIndex >= dataList.Count)
        {
            CloseButton();
            return;
        }
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        TutorialData currentData = dataList[currentIndex];

        for (int i = 0; i < currentData.data.Count; i++)
        {
            GameObject tutorialPInstance = Instantiate(tutorialP);
            tutorialPInstance.transform.SetParent(content.transform);
            tutorialPInstance.GetComponent<TutorialP>().Set(currentData.data[i], (i + 1).ToString());
        }

        Set();
    }

    public void NextTutorial()
    {
        currentIndex++;

        if (currentIndex >= dataList.Count)
        {
            currentIndex = 0;
            CloseButton();
        }
        else
        {
            TutorialSet();
        }
    }

    public void CloseButton()
    {
        gameObject.SetActive(false);
    }
}
