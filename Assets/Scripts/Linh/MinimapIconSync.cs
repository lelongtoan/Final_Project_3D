using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconSync : MonoBehaviour
{
    private RectTransform icon; // Icon RectTransform đã gắn sẵn trong object

    void Start()
    {
        // Tìm icon trong các con của object
        icon = GetComponentInChildren<RectTransform>();
        if (icon == null)
        {
            Debug.LogError($"Icon not found in {gameObject.name}. Make sure it has a child with RectTransform.");
        }
    }

    // Phương thức để cập nhật vị trí icon, được gọi từ MinimapManager
    public void UpdateIconPosition(Camera minimapCamera, RectTransform minimapBounds)
    {
        if (icon != null && minimapCamera != null && minimapBounds != null)
        {
            // Chuyển vị trí thế giới sang viewport
            Vector3 viewportPos = minimapCamera.WorldToViewportPoint(transform.position);

            // Tính toán vị trí trong minimap
            float x = viewportPos.x * minimapBounds.rect.width - (minimapBounds.rect.width / 2);
            float y = viewportPos.y * minimapBounds.rect.height - (minimapBounds.rect.height / 2);

            // Giới hạn trong biên minimap
            float clampedX = Mathf.Clamp(x, -minimapBounds.rect.width / 2, minimapBounds.rect.width / 2);
            float clampedY = Mathf.Clamp(y, -minimapBounds.rect.height / 2, minimapBounds.rect.height / 2);

            // Cập nhật vị trí icon
            icon.localPosition = new Vector3(clampedX, clampedY, 0);
        }
    }
}
