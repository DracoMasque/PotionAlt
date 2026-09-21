using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    private Recette[] recettesPossible;
    public Recette recetteDemander;
    [FormerlySerializedAs("imageRecetteObject")] [FormerlySerializedAs("imagePotionObject")] [SerializeField] private GameObject recetteObject;
    private SpriteRenderer imageRecette;
    private Animation recetteAnimation;
    [FormerlySerializedAs("commende")] [SerializeField] private GameObject potionObject;
    private SpriteRenderer potionImage;
    private Animation potionAnimation;
    
    private Sprite[] spritesPossible;
    private Sprite spriteBase;
    private Sprite spriteAgace;
    private Sprite spriteEnerve;
    private Sprite spriteHeureux;
    public SpriteRenderer clientSprite;
    
    public float timer;
    public float maxTimer = 30;
    
    [FormerlySerializedAs("commendeFaite")] public bool commandeFaite = false;
    [FormerlySerializedAs("commendeFini")] public bool commandeFini = false;

    public Animation animation;
    
    private bool spriteChanged1 = false;
    private bool spriteChanged2 = false;
    
    public Chaudron chaudron;
    private GameSystem gameSystem;
    public bool partie=false;
    
    [FormerlySerializedAs("TimerSlider")] public Slider timerSlider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        recettesPossible = Resources.LoadAll<Recette>("Scriptable Object\\Recettes");
        spritesPossible = Resources.LoadAll<Sprite>("Visual\\Sprites\\Client\\Normal");
        clientSprite = GetComponent<SpriteRenderer>();
        imageRecette = recetteObject.GetComponent<SpriteRenderer>();
        recetteAnimation = recetteObject.GetComponent<Animation>();
        potionAnimation = potionObject.GetComponent<Animation>();
        potionImage = potionObject.GetComponent<SpriteRenderer>();
        timerSlider = timerSlider.GetComponent<Slider>();
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timerSlider.value = timer;
        if (commandeFaite && !commandeFini)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= maxTimer/4 && !spriteChanged2)
        {
            ChangeSprite(spriteEnerve);
            spriteChanged2 = true;
        }
        else if (timer <= maxTimer/2 && !spriteChanged1)
        {
            ChangeSprite(spriteAgace);
            spriteChanged1 = true;
        }

        if (timer <= 0 && !commandeFini)
        {
            animation.Play("Client Part");
            timerSlider.gameObject.SetActive(false);
        }
        else if (commandeFini)
        {
            ChangeSprite(spriteHeureux);
            animation.Play("Client Part");
            timerSlider.gameObject.SetActive(false);
        }
    }

    public void SetClientPartie()
    {
        partie = true;
    }

    public void NewClient()
    {
        
        gameSystem.UpdateRoundClient();
        commandeFaite = false;
        commandeFini = false;
        spriteChanged1 = false;
        spriteChanged2 = false;
        partie = false;
        Resources.UnloadAsset(spriteAgace);
        Resources.UnloadAsset(spriteEnerve);
        Resources.UnloadAsset(spriteHeureux);
        recetteDemander = recettesPossible[Random.Range(0, recettesPossible.Length)];
        spriteBase = spritesPossible[Random.Range(0, spritesPossible.Length)];
        spriteHeureux = Resources.Load<Sprite>("Visual\\Sprites\\Client\\SuperHeureux\\" + spriteBase.name + "_SuperHeureux");
        spriteAgace = Resources.Load<Sprite>("Visual\\Sprites\\Client\\Agace\\" + spriteBase.name + "_Agace");
        spriteEnerve = Resources.Load<Sprite>("Visual\\Sprites\\Client\\Enerve\\" + spriteBase.name + "_Enerve");
        clientSprite.sprite = spriteHeureux;
        imageRecette.sprite = recetteDemander.spriteIngredient;
        potionImage.sprite = recetteDemander.spritePotion;
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
        ChangeSprite(spriteBase);
        recetteAnimation.Play("Donne Commande");
        potionAnimation.Play();
        SetCommande();
    }
    
    private void SetCommande()
    {
        commandeFaite = true;
        timerSlider.gameObject.SetActive(true);
        //chaudron.OnOrderStarted();
    }
    
    public void SetFini()
    {
        commandeFini = true;
        gameSystem.AddScore(timer*10);
    }
}
