using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapZoom : MonoBehaviour
{
    public Camera minimapCamera;
    public float zoomedInSize = 10f; 
    public float defaultSize = 30f; 
    private bool isZoomedIn = false; 
    public Button zoomInOut;

    public void Start()
    {
        zoomInOut = GameObject.Find("Mapmini").GetComponent<Button>();
    }
    public void ToggleZoom()
    {
        if (isZoomedIn)
        {
            
            minimapCamera.orthographicSize = defaultSize;
        }
        else
        {
            
            minimapCamera.orthographicSize = zoomedInSize;
        }

        isZoomedIn = !isZoomedIn;
    }
}