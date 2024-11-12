using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionWithEnenemy : MonoBehaviour
{
    public SoundEffect soundEffect;
    public PlayerInfor infor;
    public PlayerSkill skill;
    void Start()
    {
        soundEffect = FindObjectOfType<SoundEffect>();
        infor = FindObjectOfType<PlayerInfor>().GetComponent<PlayerInfor>();
        skill=FindObjectOfType<PlayerSkill>().GetComponent<PlayerSkill>();
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyInfor enemy = other.gameObject.GetComponent<EnemyInfor>();
        if (other.CompareTag("MinionEnemy") || other.CompareTag("WarioBoss"))
        {
            if (gameObject.CompareTag("Skill1"))
            {

                    enemy.EnemyTakeDame(skill.dame1);
                    soundEffect.PlaySound("Attack");
            }
            if (gameObject.CompareTag("Skill3"))
            {
                    enemy.EnemyTakeDame(skill.dame3);
                    soundEffect.PlaySound("Attack");
            }
            if (gameObject.CompareTag("AttackNormal"))
            {
                    enemy.EnemyTakeDame(infor.dame);
                    soundEffect.PlaySound("Attack");
            }
        }
    }
}
