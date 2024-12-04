using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionWithEnenemy : MonoBehaviour
{
    public SoundEffect soundEffect;
    public PlayerInfor infor;
    public PlayerSkill skill;
    bool isAttack = false;
    void Start()
    {
        soundEffect = FindObjectOfType<SoundEffect>();
        infor = FindObjectOfType<PlayerInfor>().GetComponent<PlayerInfor>();
        skill=FindObjectOfType<PlayerSkill>().GetComponent<PlayerSkill>();
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyInfor enemy = other.gameObject.GetComponent<EnemyInfor>();
        if ((other.CompareTag("MinionEnemy") || other.CompareTag("RogueEnemy") || other.CompareTag("WariorBoss")
            || other.CompareTag("MageEnemy"))&& !isAttack)
        {
            if (gameObject.CompareTag("Skill1"))
            {
                isAttack = true;
                enemy.EnemyTakeDame(skill.dame1);
                soundEffect.PlaySound("Attack");
            }
            if (gameObject.CompareTag("Skill3"))
            {
                isAttack = true;
                enemy.EnemyTakeDame(skill.dame3);
                soundEffect.PlaySound("EndAttack");
            }
            if (gameObject.CompareTag("AttackNormal"))
            {
                isAttack = true;
                enemy.EnemyTakeDame(infor.dame);
                Debug.Log("Dame");
            }
            isAttack = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MinionEnemy") || other.CompareTag("WariorBoss") || other.CompareTag("RogueEnemy") || other.CompareTag("MageEnemy"))
        {
            isAttack = false;
        }
    }
}
