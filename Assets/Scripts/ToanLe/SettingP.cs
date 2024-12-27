using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingP : MonoBehaviour
{

    [SerializeField] public GameObject soundPanel;
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
    public void SetSoundPanel()
    {
        soundPanel.SetActive(!soundPanel.activeInHierarchy);
    }
    public void SaveGame()
    {
        SaveInGame.instance.SaveGame();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
