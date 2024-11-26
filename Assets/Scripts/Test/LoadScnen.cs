using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScnen : MonoBehaviour
{
    PlayerSkill player;
    PlayerInfor inf;
    private void Start()
    {
        player = GetComponent<PlayerSkill>();
        inf = GetComponent<PlayerInfor>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LoadScene"))
        {
            if (player.buff)
            {
                player.buff = false;
                inf.dame = player.dame;
            }
            gameObject.GetComponent<PlayerInfor>().SaveData();
            SceneManager.LoadScene(1);
        }
    }
}
