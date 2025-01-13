using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LightGuideAI : MonoBehaviour
{
    public NavMeshAgent agent;  // NavMeshAgent điều khiển di chuyển
    public Transform target;    // Điểm đích cuối cùng
    public Light guideLight;    // Ánh sáng chỉ đường
    public ParticleSystem guideParticle;  // Particle System chỉ đường
    public float moveSpeed = 5f; // Tốc độ di chuyển (mới thêm)

    private bool hasReachedDestination = false; // Kiểm tra nếu đã đến đích

    void Start()
    {
        // Đảm bảo agent di chuyển tới target
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
            agent.speed = moveSpeed; // Thiết lập tốc độ di chuyển từ moveSpeed
        }
    }

    void Update()
    {
        // Kiểm tra xem đã đến điểm đích chưa
        if (agent != null && target != null && !hasReachedDestination)
        {
            if (Vector3.Distance(agent.transform.position, target.position) < 1f) // Kiểm tra khoảng cách tới đích
            {
                hasReachedDestination = true;
                EndGuide();
            }
        }
    }

    // Hàm gọi khi tới điểm cuối
    void EndGuide()
    {
        // Ẩn ánh sáng
        if (guideLight != null)
        {
            guideLight.enabled = false;
        }

        // Dừng và ẩn Particle System
        if (guideParticle != null)
        {
            guideParticle.Stop();
            guideParticle.gameObject.SetActive(false);
        }

        // Dừng NavMeshAgent
        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        Debug.Log("Guide path ended and objects are hidden.");
    }
}