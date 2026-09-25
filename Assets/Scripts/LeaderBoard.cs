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

    public void ShowLeaderBoard(List<float> scores, float actualScore)
    {
        float[] playerScoresGame = scores.ToArray().Reverse().ToArray();
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

    

    
 

