using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagePoision : MonoBehaviour
{
    public int dame;
    private float time=1;
    public bool inside = false;
    public PlayerInfor player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerInfor>();
            dame = GameObject.FindWithTag("MageEnemy").GetComponent<EnemyInfor>().dame / 3;
            inside = true;
            StartCoroutine(ApplyDamge());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inside = false;
        player = null;
    }
    private IEnumerator ApplyDamge()
    {
        while(inside && player != null && gameObject != null)
        {
            Debug.Log("Trung doc");
            yield return new WaitForSeconds(time);
            player.PlayerTakeStandanDame(dame);
        }
    }
}
