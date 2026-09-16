using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Recette[] recettesPossible;
    public Recette recetteDemander;
    [SerializeField] private GameObject imagePotionObject;
    private SpriteRenderer imagePotion;
    [SerializeField] private GameObject commende;
    
    private Sprite[] spritesPossible;
    private Sprite spriteActuel;
    
    public float timer;
    public float maxTimer;
    
    public bool commendeFaite = false;
    public bool commendeFini = false;

    public Animation animation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recettesPossible = Resources.LoadAll<Recette>("Scriptable Object\\Recettes");
        spritesPossible = Resources.LoadAll<Sprite>("Sprite\\Client");
        imagePotion = imagePotionObject.GetComponent<SpriteRenderer>();
        NewClient();
        timer = maxTimer;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (commendeFaite && !commendeFini)
        {
            timer -= Time.deltaTime;
        }
        
    }

    private void NewClient()
    {
        recetteDemander = recettesPossible[Random.Range(0, recettesPossible.Length)];
        spriteActuel = spritesPossible[Random.Range(0, spritesPossible.Length)];
        imagePotion.sprite = recetteDemander.Sprite;
        timer = maxTimer;
        animation.Play("Client Arriver");
        commendeFaite = false;
    }
    
    private void FacePlayer()
    {
        animation.Play("Donne Commande");
    }
    
    private void SetCommende()
    {
        commendeFaite = true;
        BroadcastMessage("OnOrderStarted");
    }
    
    private void SetFini()
    {
        commendeFini = true;
        BroadcastMessage("AddScore", timer*10);
    }
}
