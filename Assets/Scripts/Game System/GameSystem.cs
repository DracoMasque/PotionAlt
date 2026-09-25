using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    public Canvas leaderBoardCanvas;

    public List<float> listeScore = new List<float>();
    
    public static GameSystem Instance;
    private Melange melange;
    [SerializeField] private AudioManager audioManager;
    [FormerlySerializedAs("IndiqueMelange")] public GameObject indiqueMelange;

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
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        leaderBoardCanvas = GameObject.Find("TitleAndLeaderBoard").GetComponent<Canvas>();
        FindObjectOfType<AudioManager>().JoueMusic(0,1);
       
    
      
        if (scoreData != null)
        {
            listeScore = scoreData.listeScore;
            leaderBoard.ShowLeaderBoard(listeScore,0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (client.partie && chaudron.listeIngredients.Count == 0 && currentRound < maxRound-1)
        {
            print("venir");
            client.NewClient();
        }
        else if (client.partie && chaudron.listeIngredients.Count == 0 && gameStarted)
        {
            UpdateRoundClient();
        }

        if (melange.rotationNumber > 3)
        {
            melange.rotationNumber = 0;
            chaudron.Servire();
        }

        if (melange.timerMelange > 0)
        {
            melange.timerMelange -= Time.deltaTime;
        }
        else if (melange.timerMelange < 0)
        {
            melange.rotationNumber = 0;
        }

        if (melange.rotationNumber > 0 && !gameStarted)
        {
            LancerJeu();
        }
        
        leaderBoard.ShowLeaderBoard(listeScore,score);
        
    }

    public void LancerJeu()
    {
        leaderBoardCanvas.gameObject.SetActive(false);
        gameStarted = true;
        client.NewClient();
        score = 0;
        
    }

    public void OnRecetteConfirme(Recette recetteJoueur)
    {
        if (client.recetteDemander == recetteJoueur)
        {
            indiqueMelange.SetActive(false);
            client.SetFini();
            audioManager.JoueSfx(1);
        }
        else
        {
            indiqueMelange.SetActive(false);
            score -= 500;
            audioManager.JoueSfx(2);
        }
    }

    public void AddScore(float scoreClient)
    {
        score += MathF.Round(scoreClient);
    }

    public void UpdateRoundClient()
    {
        if (numberClientRound > maxClientRound-1)
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
        if (currentRound > maxRound-1)
        {
            //écran finish
            leaderBoardCanvas.gameObject.SetActive(true);
            listeScore.Add(score);
            leaderBoard.ShowLeaderBoard(listeScore,score);
            SaveSystem.SaveGame();
            //print("saved");
            gameStarted = false;
        }
    }
    
}
