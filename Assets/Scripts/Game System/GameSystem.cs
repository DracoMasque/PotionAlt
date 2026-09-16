using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public int maxRound;
    public int currentRound;
    public float score;

    public int maxClientRound = 3; 
    public int numberClientRound;

    private Client client;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        client = GameObject.Find("Client").GetComponent<Client>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnRecetteConfirme(Recette recetteJoueur)
    {
        if (client.recetteDemander == recetteJoueur)
        {
            client.commendeFini = true;
        }
        
    }

    void AddScore(float scoreClient)
    {
        score += scoreClient;
    }

    void UpdateRoundClient()
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
        }
    }
}
