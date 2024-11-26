using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScnen : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LoadScene"))
        {
            gameObject.GetComponent<PlayerInfor>().SaveData();
            SceneManager.LoadScene(1);
        }
    }
}
