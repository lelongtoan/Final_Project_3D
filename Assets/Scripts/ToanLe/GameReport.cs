using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameReport : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI reportText;
    public Image reportImage;

    public void SetReport(string reportContent, string playerName = "", Sprite reportImageSprite = null)
    {
        GameInstance.instance.gameMenu.SetGameReport();
        playerNameText.text = playerName;

        reportText.text = reportContent;

        if (reportImageSprite != null)
        {
            reportImage.sprite = reportImageSprite;
        }
        else
        {
            Debug.LogWarning("No image assigned to the report.");
        }
    }
}
