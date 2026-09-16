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
    private Sprite spriteBase;
    private Sprite spriteAgace;
    private Sprite spriteEnerve;
    private Sprite spriteHeureu;
    public SpriteRenderer clientSprite;
    
    public float timer;
    public float maxTimer;
    
    public bool commendeFaite = false;
    public bool commendeFini = false;

    public Animation animation;
    
    private bool spriteChanged1 = false;
    private bool spriteChanged2 = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recettesPossible = Resources.LoadAll<Recette>("Scriptable Object\\Recettes");
        spritesPossible = Resources.LoadAll<Sprite>("Visual\\Sprites\\Client\\Normal");
        clientSprite = GetComponent<SpriteRenderer>();
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

        if (timer <= maxTimer / 4 && !spriteChanged2)
        {
            ChangeSprite(spriteEnerve);
            spriteChanged2 = true;
        }
        else if (timer <= maxTimer / 2 && !spriteChanged1)
        {
            ChangeSprite(spriteAgace);
            spriteChanged1 = true;
        }

        if (timer <= 0 && !commendeFini)
        {
            animation.Play("Client Part");
        }
        else if (commendeFini)
        {
            ChangeSprite(spriteHeureu);
            
        }
    }

    private void NewClient()
    {
        commendeFaite = false;
        commendeFini = false;
        Resources.UnloadAsset(spriteAgace);
        Resources.UnloadAsset(spriteEnerve);
        Resources.UnloadAsset(spriteHeureu);
        recetteDemander = recettesPossible[Random.Range(0, recettesPossible.Length)];
        spriteBase = spritesPossible[Random.Range(0, spritesPossible.Length)];
        spriteHeureu = Resources.Load<Sprite>("Visual\\Sprites\\Client\\Super Heureux\\" + spriteBase.name + "_Super Heureux");
        spriteAgace = Resources.Load<Sprite>("Visual\\Sprites\\Client\\Agace\\" + spriteBase.name + "_Agace");
        spriteEnerve = Resources.Load<Sprite>("Visual\\Sprites\\Client\\Enerve\\" + spriteBase.name + "_Enerve");
        clientSprite.sprite = spriteHeureu;
        imagePotion.sprite = recetteDemander.sprite;
        timer = maxTimer;
        animation.Play("Client Arriver");
    }

    private void ChangeSprite(Sprite newSprite)
    {
        clientSprite.sprite = newSprite;
    }
    
    private void FacePlayer()
    {
        clientSprite.sprite = spriteBase;
        animation.Play("Donne Commande");
    }
    
    private void SetCommende()
    {
        commendeFaite = true;
        //BroadcastMessage("OnOrderStarted");
    }
    
    private void SetFini()
    {
        commendeFini = true;
        //BroadcastMessage("AddScore", timer*10);
    }
}
