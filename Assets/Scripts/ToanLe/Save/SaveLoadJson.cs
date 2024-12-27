using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class SaveLoadJson
{
    public static void SaveToJson(SaveDatas saveData, int id)
    {
        saveData.idSave = id;
        saveData.isSave = true;
        string filePath = GetSaveFilePath(id);
        string json = JsonUtility.ToJson(saveData, true); 
        File.WriteAllText(filePath, json);
    }

    public static SaveDatas LoadFromJson(int id)
    {
        string filePath = GetSaveFilePath(id);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    SaveDatas saveData = new SaveDatas();
                    JsonUtility.FromJsonOverwrite(json, saveData);
                    return saveData;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error deserializing JSON: {e.Message}");
                }
            }
            else
            {
                Debug.LogWarning("JSON file is empty or null.");
            }
        }
        return null;
    }

    public static void DeleteSaveFile(int id)
    {
        string filePath = GetSaveFilePath(id);

        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                Debug.Log($"Save file {filePath} deleted successfully.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error deleting save file {filePath}: {e.Message}");
            }
        }
        else
        {
            Debug.LogWarning($"Save file {filePath} does not exist.");
        }
    }

    private static string GetSaveFilePath(int id)
    {
        return Application.persistentDataPath + $"/saveData{id}.json";
    }
}
