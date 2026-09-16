using UnityEngine;
using System.Collections.Generic;

public class LeaderBoard : MonoBehaviour

{
    void Start()
    {}

    void caca(List<int> numbers)
    {
        numbers = new List<int> {};
        numbers.Sort((a, b) => b.CompareTo(a));

        int[] numberArray = numbers.ToArray();

        string numberString = numberArray.ToString();
        Debug.Log(numberString);

        foreach (int number in numberArray)
        {
            print(number);

        }
    }

}

    

    
 

