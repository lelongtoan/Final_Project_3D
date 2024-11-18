using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform character; // Tham chiếu đến nhân vật
    public float defaultDistance = 5f; // Khoảng cách mặc định từ camera đến nhân vật
    public float minDistance = 1.5f; // Khoảng cách gần nhất cho phép camera với nhân vật
    public float smoothing = 5f; // Tốc độ di chuyển camera khi khoảng cách thay đổi
    public LayerMask wallLayer; // Layer của tường

    private float currentDistance;

    void Start()
    {
        currentDistance = defaultDistance;
    }

    void LateUpdate()
    {
        // Vị trí mong muốn của camera
        Vector3 desiredPosition = character.position - transform.forward * currentDistance;

        // Raycast từ nhân vật đến vị trí mong muốn của camera
        RaycastHit hit;
        if (Physics.Raycast(character.position, -transform.forward, out hit, defaultDistance, wallLayer))
        {
            // Nếu gặp tường, giảm khoảng cách camera sao cho không vượt quá tường
            currentDistance = Mathf.Clamp(hit.distance, minDistance, defaultDistance);
        }
        else
        {
            // Nếu không có vật cản, trả về khoảng cách mặc định
            currentDistance = Mathf.Lerp(currentDistance, defaultDistance, Time.deltaTime * smoothing);
        }

        // Cập nhật vị trí camera theo khoảng cách tính toán
        transform.position = character.position - transform.forward * currentDistance;
        transform.LookAt(character); // Camera luôn hướng về nhân vật
    }
}

