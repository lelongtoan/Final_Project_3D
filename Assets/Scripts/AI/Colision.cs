using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    public PlayerInfor player;
    public EnemyInfor enemy;
    public int dame = 0;
    private void Start()
    {
        dame = enemy.dame;
        
    }
    private void Update()
    {
        if(player == null)
        {
            player = GameManager.FindObjectOfType<PlayerInfor>().GetComponent<PlayerInfor>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                Debug.Log("Va cham");
            gameObject.GetComponent<Collider>().enabled = false;
                player.PlayerTakeDame(enemy.dame);
        }
    }
}
