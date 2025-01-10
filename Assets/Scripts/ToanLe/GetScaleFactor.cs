using UnityEngine;
using UnityEngine.UI;

public class GetScaleFactor : MonoBehaviour
{
    public static GetScaleFactor Instance;
    public CanvasScaler canvasScaler;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (canvasScaler != null)
        {
            float scaleFactor = canvasScaler.scaleFactor;
            Debug.Log("Current Scale Factor: " + scaleFactor);

            AdjustUIScale(scaleFactor);
        }
        else
        {
            Debug.LogError("CanvasScaler component is missing!");
        }
    }

    private void Update()
    {
        if (canvasScaler != null)
        {
            AdjustUIScale(canvasScaler.scaleFactor);
        }
    }

    private void AdjustUIScale(float scaleFactor)
    {
        Canvas parentCanvas = canvasScaler.GetComponent<Canvas>();
        if (parentCanvas == null)
        {
            Debug.LogError("CanvasScaler is not attached to a Canvas!");
            return;
        }

        RectTransform[] rectTransforms = parentCanvas.GetComponentsInChildren<RectTransform>();

        foreach (RectTransform rect in rectTransforms)
        {
            rect.localScale = new Vector3(scaleFactor, scaleFactor, 1f);

            if (rect.sizeDelta != Vector2.zero)
            {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x * scaleFactor, rect.sizeDelta.y * scaleFactor);
            }
        }
    }
}
