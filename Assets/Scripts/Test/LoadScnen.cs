using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScnen : MonoBehaviour
{
    PlayerSkill player;
    PlayerInfor inf;
    PlayerData playerData;
    SceneTransition gameManager;
    private void Start()
    {
        player = GetComponent<PlayerSkill>();
        inf = GetComponent<PlayerInfor>();
        gameManager = GameObject.FindWithTag("GameMNG").GetComponent<SceneTransition>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LoadScene"))
        {
            string name = other.gameObject.GetComponent<SceneInf>().nameScene;
            if (player.buff)
            {
                player.EndBuff();
            }
            gameObject.GetComponent<PlayerInfor>().SaveData();
            gameManager.LoadScene(name);
        }
    }
}
