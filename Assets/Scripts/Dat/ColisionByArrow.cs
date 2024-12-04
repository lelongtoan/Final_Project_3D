using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionByArrow : MonoBehaviour
{
    public PlayerInfor player;
    public EnemyInfor enemy;
    SoundEffect soundEffect;
    public int dame = 0;
    private void Start()
    {
    }
    private void Update()
    {
        if(soundEffect == null)
        {
            soundEffect = FindObjectOfType<SoundEffect>();
        }
        if (enemy == null)
        {
            enemy = GameObject.FindWithTag("RogueEnemy").GetComponent<EnemyInfor>();
        }
        if (player == null)
        {
            player = GameObject.FindObjectOfType<PlayerInfor>().GetComponent<PlayerInfor>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enemy == null)
            {
                enemy = GameObject.FindWithTag("RogueEnemy").GetComponent<EnemyInfor>();
            }
            if (player == null)
            {
                player = GameObject.FindObjectOfType<PlayerInfor>().GetComponent<PlayerInfor>();
            }
            Debug.Log("Va cham");

            if (player != null)
            {
                if (soundEffect == null)
                {
                    soundEffect = FindObjectOfType<SoundEffect>();
                }
                soundEffect.PlaySound("Arrow");
                player.PlayerTakeDame(enemy.dame);
            }
            else
            {
                Debug.LogError("Player reference is null.");
            }

            if (enemy != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Enemy reference is null.");
            }
        }
    }

}
