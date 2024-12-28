using UnityEngine;
using System.Collections.Generic;

public class MinimapManager : MonoBehaviour
{
    //[SerializeField] private Transform player;
    public Camera minimapCamera;        // Minimap camera
    public RectTransform minimapBounds; // RectTransform của minimap

    // Danh sách các đối tượng cần theo dõi
    private List<MinimapIconSync> trackedObjects = new List<MinimapIconSync>();

    void Start()
    {
        minimapBounds = GameObject.Find("BackgroundMinimap").GetComponent<RectTransform>();
    }
    void Update()
    {
        // Cập nhật vị trí của tất cả các icon
        foreach (var tracker in trackedObjects)
        {
            tracker.UpdateIconPosition(minimapCamera, minimapBounds);
        }
    }

    public void RegisterObject(MinimapIconSync tracker)
    {
        if (!trackedObjects.Contains(tracker))
        {
            trackedObjects.Add(tracker);
        }
    }

    public void UnregisterObject(MinimapIconSync tracker)
    {
        if (trackedObjects.Contains(tracker))
        {
            trackedObjects.Remove(tracker);
        }
    }
}
//void Update()
//    {
//        Vector3 newPosition = player.position;
//        newPosition.y = transform.position.y;
//        transform.position = newPosition;
//    }
//}
