using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
   public const string FILENAME = "/leaderBoard.save";

   public static void SaveGame()
   {
       string path = Application.persistentDataPath + FILENAME;
       ScoreData saveData = new ScoreData(GameSystem.Instance.listeScore);
       string txt = JsonUtility.ToJson(saveData);
       File.WriteAllText(path, txt);
   }
}

[System.Serializable]
public class ScoreData
{
   [SerializeField] public List<float> listeScore;
   
   public ScoreData(List<float> listeScoreActual)
   {
       listeScore = listeScoreActual;
   }
}
