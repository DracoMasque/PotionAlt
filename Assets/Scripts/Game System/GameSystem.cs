using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public int maxRound;
    public int currentRound;
    public float score;

    public int maxClientRound = 3; 
    public int numberClientRound;
    
    public bool  gameStarted = false;

    public Client client;
    
    public LeaderBoard leaderBoard;

    [SerializeField] public Dictionary<string, int> listeScore = new Dictionary<string, int>();
    
    public static GameSystem Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        client = GameObject.Find("Client").GetComponent<Client>();
        ScoreData scoreData = LoadSystem.LoadScore();
        if (scoreData != null)
        {
            listeScore = scoreData.listeScore;
            leaderBoard.ShowLeaderBoard(listeScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LancerJeu()
    {
        if (!gameStarted)
        {
            leaderBoard.gameObject.SetActive(false);
            client.NewClient();
            gameStarted = true;
        }
    }

    public void OnRecetteConfirme(Recette recetteJoueur)
    {
        if (client.recetteDemander == recetteJoueur)
        {
            client.SetFini();
        }
        
    }

    public void AddScore(float scoreClient)
    {
        score += scoreClient;
    }

    public void UpdateRoundClient()
    {
        if (numberClientRound > maxClientRound)
        {
            numberClientRound = 1;
            
        }
        else
        {
            numberClientRound++;
        }
    }
    
    void UpdateRoundNumber()
    {
        currentRound++;
        if (currentRound > maxRound)
        {
            //écran finish
            leaderBoard.gameObject.SetActive(true);
            Time.timeScale = 0f;
            SaveSystem.SaveGame();
            gameStarted = false;
        }
    }
    
}
