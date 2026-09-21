using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.InputSystem;

public class LeaderBoard : MonoBehaviour

{
    public List<TextMeshProUGUI> playersNames;
    public List<TextMeshProUGUI> playersScores;

    void Start()
    {
        for (int i = 0; i < playersNames.Count; i++)
        {
            playersNames[i].text = "---";
            playersScores[i].text = "000000";
        }
        
    }

    Dictionary<string,int> SortLeader(Dictionary<string,int> numbers)
    {
        
        numbers.OrderBy(key => key.Value);
        
        foreach (int number in numbers.Values)
        {
            print(number);
        }
        return numbers;
    }

    public void ShowLeaderBoard(Dictionary<string,int> scores)
    {
        Dictionary<string, int> sortedScores = SortLeader(scores);
        string[] playerNames = sortedScores.Keys.ToArray();
        int[] playerScores = sortedScores.Values.ToArray();
        for (int i = 0; i < playerNames.Length; i++)
        {
            playersNames[i].text = playerNames[i];
            playersScores[i].text = playerScores[i].ToString();
        }
    }

}

    

    
 

