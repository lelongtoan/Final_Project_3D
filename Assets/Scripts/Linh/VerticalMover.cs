using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover : MonoBehaviour
{
    public float moveSpeed = 2f; // Tốc độ di chuyển
    public float moveRange = 3f; // Khoảng cách di chuyển

    private Vector3 startPosition;
    private bool movingUp = true; 

    void Start()
    {
        startPosition = transform.position; 
    }

    void Update()
    {
        float newY = transform.position.y;

        if (movingUp)
        {
            newY += moveSpeed * Time.deltaTime;
            if (newY >= startPosition.y + moveRange)
            {
                newY = startPosition.y + moveRange;
                movingUp = false; 
            }
        }
        else
        {
            newY -= moveSpeed * Time.deltaTime;
            if (newY <= startPosition.y - moveRange)
            {
                newY = startPosition.y - moveRange;
                movingUp = true; 
            }
        }

        // Cập nhật vị trí
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        
    }
}

