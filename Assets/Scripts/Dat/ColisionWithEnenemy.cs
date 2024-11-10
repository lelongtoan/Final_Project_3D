using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionWithEnenemy : MonoBehaviour
{
    public SoundEffect soundEffect;
    public PlayerInfor infor;
    void Start()
    {
        soundEffect = FindObjectOfType<SoundEffect>();
        infor = FindObjectOfType<PlayerInfor>().GetComponent<PlayerInfor>();
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyInfor enemy = other.gameObject.GetComponent<EnemyInfor>();
        if (other.CompareTag("Enemy"))
        {
            if (gameObject.CompareTag("Skill1"))
            {

                    enemy.EnemyTakeDame(1);
                    soundEffect.PlaySound("Attack");
            }
            if (gameObject.CompareTag("Skill3"))
            {
                    enemy.EnemyTakeDame(30);
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
