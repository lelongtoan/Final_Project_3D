using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveInGame : MonoBehaviour
{
    public static SaveInGame instance;
    public SaveData saveData;
    public SaveTemp saveTemp;
    public int idSelect;

    public StartData startData;
    private void Awake()
    {
        instance = this;
        LoadPanelChar();
    }
    private void Start()
    {
        idSelect = PlayerPrefs.GetInt("SelectedID", -1);
        Debug.Log($"Selected ID: {idSelect}");
    }
    public void SaveGame()
    {
        saveTemp.SetSaveData(idSelect);
    }

    public void LoadGame(int id)
    {
        saveTemp.SetLoadData(id);
        if (saveData != null) 
        {
            Debug.Log("Load Player Success");
            PlayerPrefs.SetInt("SelectedID", id);
            PlayerPrefs.Save();
            LoadScene.instance.LoadSceneMenu("LobbyMap");
        }
        Debug.Log("Load fail");
    }
    public void SetCharSave(int id)
    {
        idSelect = id;
        if (startData.data[id].isSave == true)
        {
            Debug.Log("Loading Char");
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
        PlayerPrefs.SetInt("SelectedID", idSelect);
        PlayerPrefs.Save();
        Debug.Log("New Player Success");
        LoadScene.instance.LoadSceneMenu("LobbyMap");
    }
    public void DetelePlayer(int id)
    {
        if(id == -1)
        {
            SaveLoadJson.DeleteSaveFile(idSelect);
        }
        else
        {
            SaveLoadJson.DeleteSaveFile(id);
        }
        Debug.Log("Del Player Success");
    }
    public void LoadPanelChar()
    {
        for (int i = 0; i < 3; i++)
        {
            saveData = SaveLoadJson.LoadFromJson(i);
            if (saveData == null)
            {
                startData.data[i].isSave = false;
                Debug.Log($"Data : {i} Khong co!");
                continue;
            }
            startData.data[i].isSave = true;
            startData.data[i].level = saveData._07_level;
            startData.data[i].point = saveData._07_level * 10;
            //Debug.Log(saveData.saveDatas[i].level.ToString());
        }
    }
}
