using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyInfor : MonoBehaviour
{
    public float hp = 100f;
    public float hpcurrent;
    public PlayerSkill skill;
    private PlayerInfor playerInfor; 
    public float dame;
    public float dame1;
    public float dame3;

    bool isCollision = false;

    private void Start()
    {
        skill = GameObject.FindWithTag("Player").GetComponent<PlayerSkill>();
        playerInfor = PlayerInfor.Instance;
        dame = playerInfor.PlayerUpdateDame();
        hpcurrent = hp;
    }
    private void Update()
    {
        dame = playerInfor.PlayerUpdateDame();
        dame1 = skill.dame1;
        dame3 = skill.dame3;
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
        else if (other.CompareTag("Skill3"))
        {
            Debug.Log("Attack");
            isCollision = true;
            hpcurrent -= dame3;
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
