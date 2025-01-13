using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapZoom : MonoBehaviour
{
    public GameObject minimapCamera; // Camera hoặc UI của minimap
    public GameObject largeMapPanel; // UI panel chứa bản đồ lớn

    private bool isLargeMapActive = false; // Trạng thái bản đồ lớn

    public void ToggleMap() // Đảm bảo hàm là public
    {
        isLargeMapActive = !isLargeMapActive;

        if (minimapCamera != null) minimapCamera.SetActive(!isLargeMapActive);
        if (largeMapPanel != null) largeMapPanel.SetActive(isLargeMapActive);

        Debug.Log("Map toggled. Large map active: " + isLargeMapActive);
    }
}