using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int dame = 10;
    public PlayerInfor player;
    bool isTrap = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerInfor>();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && isTrap == false)
        {
            isTrap = true;
            player.PlayerTakeDame(dame);
            player.GetComponent<SoundEffect>().PlaySound("EnemyNormalAttck");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isTrap = false;
    }
}
