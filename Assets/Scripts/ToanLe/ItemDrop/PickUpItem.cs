using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    public GameObject pickUpPrefab;

    [Header("Time Fade")]
    public float fadeDuration = 1.5f;

    [Header("Max Visible Items")]
    public int maxVisibleItems = 4;

    private Queue<GameObject> activePickUps = new Queue<GameObject>();

    public void ShowPickUp(ItemSlot itemSlot)
    {
        GameObject pickUpGO = Instantiate(pickUpPrefab, transform);
        pickUpGO.GetComponent<ItemPickUp>().Set(itemSlot);

        activePickUps.Enqueue(pickUpGO);

        if (activePickUps.Count > maxVisibleItems)
        {
            GameObject oldestPickUp = activePickUps.Dequeue(); // Lấy go 0
            Destroy(oldestPickUp);
        }

        StartCoroutine(FadeOutEffect(pickUpGO));
    }

    private IEnumerator FadeOutEffect(GameObject pickUpGO)
    {
        CanvasGroup canvasGroup = pickUpGO.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = pickUpGO.AddComponent<CanvasGroup>();
        }

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;

            canvasGroup.alpha = 1 - t;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (activePickUps.Contains(pickUpGO))
        {
            activePickUps.Dequeue();
        }

        Destroy(pickUpGO);
    }
}
