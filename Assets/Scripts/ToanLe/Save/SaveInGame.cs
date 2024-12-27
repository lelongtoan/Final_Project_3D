using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveInGame : MonoBehaviour
{
    public static SaveInGame instance;
    public List<StartInfo> startInfo;
    public SaveData saveData;
    public SaveTemp saveTemp;
    public int idSelect;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadPanelChar();
    }

    public void SaveGame()
    {
        saveTemp.SetSaveData();
    }

    public void LoadGame(int id)
    {
        saveTemp.SetLoadData(id);
        if (saveData != null)
        {
            Debug.Log("Load Player Success");
            SceneManager.LoadScene("LobbyMap");
        }
    }
    public void SetCharSave(int id)
    {
        idSelect = id;
        if (startInfo[id].isSave)
        {
            LoadGame(id);
        }
        else
        {
            MainMenuManager.Instance.SetNewCharMenu();
        }
    }
    public void NewPlayer()
    {
        saveTemp.NewPlayer();
        MainMenuInstance.instance.statsData.Set();
        saveTemp.SetSaveData(idSelect);
        //saveData.isSave = true;
        Debug.Log("New Player Success");
        SceneManager.LoadScene("LobbyMap");
    }
    public void DetelePlayer(int id)
    {
        saveData = new SaveData();
        saveTemp.NewPlayer();
        saveTemp.SetSaveData(id);
        Debug.Log("Del Player Success");
    }
    public void LoadPanelChar()
    {
        for (int i = 0; i < startInfo.Count; i++)
        {
            saveData = SaveLoadJson.LoadFromJson(i);
            if(saveData == null)
            {
                Debug.Log($"Data : {i} Error!");
                continue;
            }
            Debug.Log(saveData.level.ToString());
            startInfo[i].level = saveData.level;
            startInfo[i].point = saveData.level * 10;
            if (saveData.maxHP != 0)
            {
                startInfo[i].isSave = true;
            }
            else
            {
                startInfo[i].isSave = false;
            }
        }
    }
}
