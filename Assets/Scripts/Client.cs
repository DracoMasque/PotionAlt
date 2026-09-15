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

    public GameObject pointArriver;
    public GameObject pointFaceAuJoueur;
    private Transform emplacement;

    public bool commendeFaite = false;
    public bool faceAuJoueur = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recettesPossible = Resources.LoadAll<Recette>("ScriptableObjects/Recettes");
        spritesPossible = Resources.LoadAll<Sprite>("Sprite/Client");
        emplacement = gameObject.GetComponent<Transform>();
        imagePotion = imagePotionObject.GetComponent<SpriteRenderer>();
        NewClient();
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (commendeFaite)
        {
            timer -= Time.deltaTime;
        }
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Exit")
        {
            NewClient();
        }
    }

    private void NewClient()
    {
        recetteDemander = recettesPossible[Random.Range(0, recettesPossible.Length)];
        spriteActuel = spritesPossible[Random.Range(0, spritesPossible.Length)];
        emplacement.position = pointArriver.transform.position;
        timer = maxTimer;
        commendeFaite = false;
    }
    
    
}
