using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveLoadJson
{
    public static void SaveToJson(SaveData saveData, int id)
    {
        string filePath = GetSaveFilePath(id);
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(filePath, json);
    }

    public static SaveData LoadFromJson(int id)
    {
        string filePath = GetSaveFilePath(id);  // Lấy đường dẫn tệp dựa trên id
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;  // Nếu không tìm thấy tệp, trả về null
    }

    public static void DeleteSaveFile(int id)
    {
        string filePath = GetSaveFilePath(id);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    private static string GetSaveFilePath(int id)
    {
        return Application.persistentDataPath + $"/saveData{id}.json";
    }
}
