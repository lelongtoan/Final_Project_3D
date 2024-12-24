using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingP : MonoBehaviour
{

    [SerializeField] public GameObject soundPanel;
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
