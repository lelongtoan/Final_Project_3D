using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapZoom : MonoBehaviour
{
    [SerializeField]public GameObject minimapCamera; 
    [SerializeField]public GameObject largeMapPanel;

    private bool isLargeMapActive = false;


    public void ToggleMiniMap(bool on) 
    {
        isLargeMapActive = !isLargeMapActive;

        if (minimapCamera != null) minimapCamera.SetActive(!isLargeMapActive);
        if (largeMapPanel != null) largeMapPanel.SetActive(isLargeMapActive);

        Debug.Log("Map toggled. Large map active: " + isLargeMapActive);
    }
}