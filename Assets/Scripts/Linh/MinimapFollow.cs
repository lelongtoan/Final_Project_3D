using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MinimapFollow : MonoBehaviour
{
    public Button zoomButton; // Assign your button here in the Inspector
    public Camera minimapCamera;
    public float zoomedSize = 10f;
    public float defaultSize = 20f;
    private bool isZoomed = false;

    void Start()
    {
        if (zoomButton != null)
        {
            // Subscribe to the button click event
            zoomButton.onClick.AddListener(OnZoomButtonClicked);
        }
        else
        {
            Debug.LogError("Zoom Button is not assigned!");
        }
    }

    void OnZoomButtonClicked()
    {
        if (minimapCamera != null && minimapCamera.orthographic)
        {
            if (isZoomed)
            {
                minimapCamera.orthographicSize = defaultSize;
            }
            else
            {
                minimapCamera.orthographicSize = zoomedSize;
            }

            isZoomed = !isZoomed;
            Debug.Log("Zoom toggled! Current size: " + minimapCamera.orthographicSize);
        }
        else
        {
            Debug.LogError("Minimap Camera is not set or not orthographic!");
        }
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (zoomButton != null)
        {
            zoomButton.onClick.RemoveListener(OnZoomButtonClicked);
        }
    }
}
