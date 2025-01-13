using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextAutoSizer : MonoBehaviour
{
    [SerializeField] Text nameMap;
    public int minFontSize = 6; // Kích thước font nhỏ nhất
    public int maxFontSize = 72; // Kích thước font lớn nhất
    public CanvasScaler canvasScaler; // CanvasScaler cần kiểm soát
    public List<Text> excludedTexts = new List<Text>(); // Danh sách các Text không bị thay đổi

    private void Update()
    {
        if (nameMap != null) 
        {
            nameMap.text = SceneManager.GetActiveScene().name;
        }
        if (canvasScaler == null) return;

        // Lấy tất cả các Text trong Canvas được quản lý bởi CanvasScaler
        Text[] allTexts = canvasScaler.GetComponentInParent<Canvas>().GetComponentsInChildren<Text>(true);

        // Áp dụng AutoSizeText cho từng Text
        foreach (Text text in allTexts)
        {
            // Bỏ qua các Text trong danh sách excludedTexts
            if (excludedTexts.Contains(text)) continue;

            AutoSizeText(text);
        }
    }

    private void AutoSizeText(Text text)
    {
        if (text == null) return;

        RectTransform rect = text.GetComponent<RectTransform>();
        if (rect == null) return;

        // Bật chế độ xuống dòng tự động nếu chưa bật
        if (text.horizontalOverflow != HorizontalWrapMode.Wrap)
        {
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
        }

        if (text.verticalOverflow != VerticalWrapMode.Truncate)
        {
            text.verticalOverflow = VerticalWrapMode.Truncate;
        }

        // Đặt kích thước font bắt đầu từ maxFontSize
        text.fontSize = maxFontSize;

        // Điều chỉnh kích thước font cho đến khi nó vừa với khung hoặc đạt minFontSize
        while (text.preferredHeight > rect.rect.height && text.fontSize > minFontSize)
        {
            text.fontSize--;
        }

        // Đảm bảo fontSize không vượt quá maxFontSize nếu không cần giảm
        if (text.fontSize > maxFontSize)
        {
            text.fontSize = maxFontSize;
        }
    }
}
