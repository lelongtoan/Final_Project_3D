using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeLine : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject linePrefab;

    void Start()
    {
        if (linePrefab == null)
        {
            Debug.LogError("Line Prefab chưa được gắn!");
            return;
        }

        foreach (var target in targets)
        {
            if (target != null)
            {
                CreateLine(transform, target.transform);
            }
        }
    }

    void CreateLine(Transform from, Transform to)
    {
        GameObject line = Instantiate(linePrefab, transform);
        RectTransform lineRect = line.GetComponent<RectTransform>();

        if (lineRect == null)
        {
            Debug.LogError("Line Prefab phải có thành phần RectTransform!");
            return;
        }

        Vector3 startPosition = from.position;
        Vector3 endPosition = to.position;
        Vector3 middlePosition = (startPosition + endPosition) / 2;

        lineRect.position = middlePosition;

        Vector3 direction = endPosition - startPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineRect.rotation = Quaternion.Euler(0, 0, angle);

        float distance = Vector3.Distance(startPosition, endPosition);
        lineRect.sizeDelta = new Vector2(distance, lineRect.sizeDelta.y); // Đặt chiều dài
    }
}
