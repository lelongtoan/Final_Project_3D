using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] ListTakeData takeData;
    [SerializeField] PlayerData playerData;
    public void SetGameOver()
    {
        takeData.point = playerData.point;
        takeData.isTake = true;
        SaveInGame.instance.DetelePlayer(-1);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
