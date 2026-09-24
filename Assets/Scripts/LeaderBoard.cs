using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.InputSystem;

public class LeaderBoard : MonoBehaviour

{
    public List<TextMeshProUGUI> playersScores;
    public TextMeshProUGUI yourScore;

    void Start()
    {
        for (int i = 0; i < playersScores.Count; i++)
        {
            playersScores[i].text = "000000";
        }
        yourScore.text = "";
        
    }

    List<float> SortLeader(List<float> numbers)
    {
        float[] sorted = numbers.ToArray();
        Array.Sort(sorted,  (x, y) => x.CompareTo(y));
        for (int i =0; i < sorted.Length; i++)
        {
            //print(i+","+sorted[i]);
        }
        return sorted.ToList();
    }

    public void ShowLeaderBoard(List<float> scores, float actualScore)
    {
        List<float> sortedScores = SortLeader(scores);
        float[] playerScoresGame = sortedScores.ToArray();
        for (int i = 0; i < playersScores.Count; i++)
        {
            if (i <= playerScoresGame.Length - 1)
            {
                playersScores[i].text = playerScoresGame[i].ToString();
            }
        }

        yourScore.text = actualScore.ToString();
    }

}

    

    
 

