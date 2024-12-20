using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInGame : MonoBehaviour
{
    public static SaveInGame instance;
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
    }
    public SaveData saveData;

    public void SaveGame()
    {
        SaveLoadJson.SaveToJson(saveData, saveData.idSave);
    }

    public void LoadGame(int id)
    {
        saveData = SaveLoadJson.LoadFromJson(id);
        if (saveData != null)
        {
            //
        }
    }
}
