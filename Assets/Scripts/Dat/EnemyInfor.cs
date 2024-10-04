using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyInfor : MonoBehaviour
{
    public float hp = 100f;
    public float hpcurrent;

    private PlayerInfor playerInfor; // Tham chiếu đến script PlayerInfor
    public float dame;

    bool isCollision = false;

    private void Start()
    {
        // Truy cập PlayerInfor từ biến static Instance
        playerInfor = PlayerInfor.Instance;
        dame = playerInfor.PlayerUpdateDame(); // Lấy sát thương của người chơi
        hpcurrent = hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AttackNormal") && isCollision == false)
        {
            isCollision = true; // Cập nhật lại sát thương
            hpcurrent -= dame; // Giảm HP hiện tại của kẻ địch theo sát thương

            StartCoroutine(DisableCollider(other, 0.1f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AttackNormal"))
        {
        }
    }

    IEnumerator DisableCollider(Collider other,float delay)
    {
        yield return new WaitForSeconds(delay);
        isCollision = false;
        other.enabled = false;
    }
}
