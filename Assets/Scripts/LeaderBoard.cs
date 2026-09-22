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
        numbers.Sort((a, b) => a.CompareTo(b));
        return numbers;
    }

    public void ShowLeaderBoard(List<float> scores, float actualScore)
    {
        List<float> sortedScores = SortLeader(scores);
        float[] playerScores = sortedScores.ToArray();
        for (int i = 0; i < playersScores.Count; i++)
        {
            playersScores[i].text = playerScores[i].ToString();
        }

        yourScore.text = actualScore.ToString();
    }

}

    

    
 

