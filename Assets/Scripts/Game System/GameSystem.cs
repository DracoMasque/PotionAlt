using System;
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
    public Chaudron chaudron;
    
    public LeaderBoard leaderBoard;

    public List<float> listeScore = new List<float>();
    
    public static GameSystem Instance;
    private Melange melange;
    private float timerMelange;

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
        chaudron = GameObject.Find("Chaudron").GetComponent<Chaudron>();
        ScoreData scoreData = LoadSystem.LoadScore();
        melange = GetComponent<Melange>();
        if (scoreData != null)
        {
            listeScore = scoreData.listeScore;
            leaderBoard.ShowLeaderBoard(listeScore,0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (client.partie && chaudron.listeIngredients.Count == 0)
        {
            client.NewClient();
        }

        if (melange.rotationNumber == 3 && timerMelange != 0)
        {
            chaudron.Servire();
        }
        else if (melange.rotationNumber <= 2)
        {
            timerMelange = 0.5f;
        }

        if (timerMelange > 0)
        {
            timerMelange -= Time.deltaTime;
        }

        if (melange.rotationNumber > 0 && !gameStarted)
        {
            LancerJeu();
        }
        
    }

    public void LancerJeu()
    {
        
        leaderBoard.gameObject.SetActive(false);
        client.NewClient();
        gameStarted = true;
        score = 0;
        
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
        score += MathF.Round(scoreClient);
    }

    public void UpdateRoundClient()
    {
        if (numberClientRound > maxClientRound)
        {
            numberClientRound = 1;
            UpdateRoundNumber();
            
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
