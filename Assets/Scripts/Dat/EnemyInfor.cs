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
    private void Update()
    {
        dame = playerInfor.PlayerUpdateDame();
        if (hpcurrent <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AttackNormal") && isCollision == false)
        {
            Debug.Log("Attack");
            isCollision = true;
            hpcurrent -= dame;
            
            StartCoroutine(DisableCollider(other, 0.1f));
        }
        else if (other.CompareTag("Skill1"))
        {
            Debug.Log("Attack");
            isCollision = true;
            hpcurrent -= 1;
        }
        else if (other.CompareTag("Skill"))
        {
            Debug.Log("Attack");
            isCollision = true;
            hpcurrent -= dame;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Skill") || other.gameObject.CompareTag("Skill1"))
        {
            isCollision = false;
        }
    }

    IEnumerator DisableCollider(Collider other,float delay)
    {
        yield return new WaitForSeconds(delay);
        isCollision = false;
        other.enabled = false;
    }
}
