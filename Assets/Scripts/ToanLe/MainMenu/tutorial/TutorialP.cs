using UnityEngine;
using UnityEngine.UI;

public class TutorialP : MonoBehaviour
{
    [SerializeField] Text desText;
    [SerializeField] Text numberText;

    public void Set(string des, string number)
    {
        desText.text = des;
        numberText.text = number;

        EnableWordWrap(desText);
        EnableWordWrap(numberText);

        AutoSizeText(desText);
        AutoSizeText(numberText);
    }

    private void AutoSizeText(Text text)
    {
        RectTransform rect = text.GetComponent<RectTransform>();
        int minFontSize = 36;

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
}
