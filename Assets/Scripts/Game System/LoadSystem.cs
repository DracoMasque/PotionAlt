using System.IO;
using UnityEngine;

public static class LoadSystem
{
    public static ScoreData LoadScore()
    {
        try
        {
            string filepath = Application.persistentDataPath + SaveSystem.FILENAME;
            string fileContent = File.ReadAllText(filepath);
            ScoreData saveData = JsonUtility.FromJson<ScoreData>(fileContent);
            return saveData;
        }
        catch
        {
            return null;
        }
    }
}
