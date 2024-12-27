using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveInGame : MonoBehaviour
{
    public static SaveInGame instance;
    public ListSaveData saveData;
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
        if (saveData.saveDatas[id] != null)
        {
            Debug.Log("Load Player Success");
            SceneManager.LoadScene("LobbyMap");
        }
    }
    public void SetCharSave(int id)
    {
        idSelect = id;
        if (saveData.saveDatas[id] != null 
            && saveData.saveDatas[id].isSave)
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
        Debug.Log("New Player Success");
        SceneManager.LoadScene("LobbyMap");
    }
    public void DetelePlayer(int id)
    {
        saveData.saveDatas[id] = new SaveDatas();
        saveData.saveDatas[id].isSave = false;
        saveTemp.NewPlayer();
        saveTemp.SetSaveData(id);
        Debug.Log("Del Player Success");
    }
    public void LoadPanelChar()
    {
        for (int i = 0; i < saveData.saveDatas.Count; i++)
        {
            saveData.saveDatas[i] = SaveLoadJson.LoadFromJson(i);
            if (saveData.saveDatas[i] == null)
            {
                Debug.Log($"Data : {i} Khong co!");
            }
            //Debug.Log(saveData.saveDatas[i].level.ToString());
        }
    }
}
