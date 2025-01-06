using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingP : MonoBehaviour
{

    [SerializeField] public GameObject saveGame;
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "LobbyMap")
        {
            saveGame.SetActive(true);
        }
        else
        {
            saveGame.SetActive(false);
        }
    }
    public void SaveGame()
    {
        SaveInGame.instance.SaveGame();
        Debug.Log("Saved");
    }
    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
