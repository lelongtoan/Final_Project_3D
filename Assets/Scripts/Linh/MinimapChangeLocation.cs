using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapChangeLocation : MonoBehaviour
{
    public RectTransform minimap; // Raw Image chứa minimap
    public Vector2 expandedPosition = new Vector2(0, 0); // Vị trí khi phóng to
    public Vector2 defaultPosition = new Vector2(-200, -200); // Vị trí mặc định
    public Vector2 expandedScale = new Vector2(2, 2); // Scale khi phóng to
    public Vector2 defaultScale = new Vector2(1, 1); // Scale mặc định
    private bool isExpanded = false; // Trạng thái minimap

    protected Button mMap;

    void Start()
    {
        mMap = GameObject.Find("Mapmini").GetComponent<Button>();    
    }

    public void ToggleMinimap()
    {
        StopAllCoroutines(); // Ngừng mọi hiệu ứng trước đó
        if (isExpanded)
        {
            StartCoroutine(SmoothTransition(defaultPosition, defaultScale));
        }
        else
        {
            StartCoroutine(SmoothTransition(expandedPosition, expandedScale));
        }

        isExpanded = !isExpanded; // Đảo trạng thái
    }

    private IEnumerator SmoothTransition(Vector2 targetPosition, Vector2 targetScale)
    {
        float duration = 0.3f; // Thời gian chuyển đổi
        float elapsed = 0f;
        Vector2 startPosition = minimap.anchoredPosition;
        Vector2 startScale = minimap.localScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            minimap.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            minimap.localScale = Vector2.Lerp(startScale, targetScale, t);
            yield return null;
        }

        minimap.anchoredPosition = targetPosition;
        minimap.localScale = targetScale;
    }
}