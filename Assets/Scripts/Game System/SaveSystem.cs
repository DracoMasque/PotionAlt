using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
   public const string FILENAME = "/leaderBoard.save";

   public static void SaveGame()
   {
       string path = Application.persistentDataPath + FILENAME;
       ScoreData saveData = new ScoreData();
       string txt = JsonUtility.ToJson(saveData);
       File.WriteAllText(path, txt);
   }
}

[System.Serializable]
public class ScoreData
{
   [SerializeField] public Dictionary<string, int> listeScore;
   
   public ScoreData()
   {
       listeScore = new Dictionary<string, int>();
   }
}
