using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFade : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float transparencyDistance = 5f; // Distance at which the wall starts to become transparent
    public float fadeSpeed = 1f; // Speed of the transparency change
    public float minAlpha = 0.3f; // Minimum alpha for transparency

    private Material wallMaterial;
    private Color originalColor;
    private float originalAlpha;

    void Start()
    {
        // Set up material to be transparent
        wallMaterial = GetComponent<Renderer>().material;
        wallMaterial.SetFloat("_Mode", 3);
        wallMaterial.EnableKeyword("_ALPHABLEND_ON");
        wallMaterial.renderQueue = 3000;

        originalColor = wallMaterial.color;
        originalAlpha = originalColor.a;
    }

    void Update()
    {
        // Calculate horizontal distance between player and wall (ignoring height)
        Vector3 playerPos = player.position;
        Vector3 wallPos = transform.position;

        playerPos.y = wallPos.y; // Set player’s Y to the wall’s Y to ignore vertical distance
        float distance = Vector3.Distance(playerPos, wallPos);

        // Debug log to check calculated distance
        Debug.Log($"Horizontal Distance to Wall: {distance}");

        // Determine the target alpha based on proximity
        float targetAlpha = (distance < transparencyDistance) ? minAlpha : originalAlpha;

        // Smoothly interpolate alpha for the fade effect
        Color color = wallMaterial.color;
        color.a = Mathf.Lerp(color.a, targetAlpha, Time.deltaTime * fadeSpeed);
        wallMaterial.color = color;
    }
}
