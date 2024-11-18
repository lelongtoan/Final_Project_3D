using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform destination;
    public float teleportDelay = .1f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<PlayerController>(out var player))
        {
            
            StartCoroutine(TeleportWithDelay(player));
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(destination.position, -.6f);
        var direction = destination.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(destination.position, direction);
    }

    IEnumerator TeleportWithDelay(PlayerController player)
    {
        Debug.Log("Teleport will happen after delay...");

        // Tắt điều khiển của người chơi
        player.enabled = false;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;  // Làm cho Rigidbody không bị ảnh hưởng bởi vật lý
        }

        // Tạm dừng tất cả âm thanh trong game
        AudioListener.pause = true;  // Dừng tất cả âm thanh (toàn cục)

        // Đợi một khoảng thời gian trước khi dịch chuyển
        yield return new WaitForSeconds(teleportDelay);

        // Sau khi thời gian chờ kết thúc, thực hiện dịch chuyển
        player.transform.position = destination.position;
        player.transform.rotation = destination.rotation;

        // Bật lại điều khiển người chơi sau khi dịch chuyển
        player.enabled = true;

        // Nếu có Rigidbody, bật lại tương tác vật lý
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // Bật lại âm thanh sau khi dịch chuyển
        AudioListener.pause = false;  // Bật lại tất cả âm thanh

        // In thông báo dịch chuyển thành công (tùy chọn)
        Debug.Log("Player teleported and controls restored!");
    }
}

