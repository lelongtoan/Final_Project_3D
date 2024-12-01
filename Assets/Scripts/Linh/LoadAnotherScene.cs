using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAnotherScene : MonoBehaviour
{
    [SerializeField] private string targetSceneName; // Name of the scene to load

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the portal is the player
        if (other.CompareTag("Player"))
        {
            LoadTargetScene();
        }
    }

    private void LoadTargetScene()
    {
        // Load the target scene
        SceneManager.LoadScene(targetSceneName);
    }
}

