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
    
    private ClientOject[] clientPossible;
    private ClientOject clientBase;
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
    public Image colorSlider;
    
    private AudioManager audioManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        recettesPossible = Resources.LoadAll<Recette>("Scriptable Object\\Recettes");
        clientPossible = Resources.LoadAll<ClientOject>("Scriptable Object\\Clients");
        clientSprite = GetComponent<SpriteRenderer>();
        imageRecette = recetteObject.GetComponent<SpriteRenderer>();
        recetteAnimation = recetteObject.GetComponent<Animation>();
        potionAnimation = potionObject.GetComponent<Animation>();
        potionImage = potionObject.GetComponent<SpriteRenderer>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        timerSlider = timerSlider.GetComponent<Slider>();
        colorSlider = colorSlider.GetComponent<Image>();
        colorSlider.color = Color.mediumSeaGreen;
        
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
            ChangeSprite(clientBase.spriteEnerve);
            audioManager.JoueSfx(clientBase.audioEnerve);
            spriteChanged2 = true;
            colorSlider.color = Color.red;
        }
        else if (timer <= maxTimer/2 && !spriteChanged1)
        {
            ChangeSprite(clientBase.spriteAgace);
            audioManager.JoueSfx(clientBase.audioAgace);
            spriteChanged1 = true;
            colorSlider.color = Color.orange;
        }

        if (timer <= 0 && !commandeFini)
        {
            recetteAnimation.Play("RecetteOut");
            animation.Play("Client Part");
            audioManager.JoueSfx(clientBase.audioHeureux);
            timerSlider.gameObject.SetActive(false);
            colorSlider.color = Color.mediumSeaGreen;
        }
        else if (commandeFini)
        {
            recetteAnimation.Play("RecetteOut");
            ChangeSprite(clientBase.spriteHeureux);
            animation.Play("Client Part");
            timerSlider.gameObject.SetActive(false);
            colorSlider.color = Color.mediumSeaGreen;
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
        recetteDemander = recettesPossible[Random.Range(0, recettesPossible.Length)];
        clientBase = clientPossible[Random.Range(0, clientPossible.Length)];
        clientSprite.sprite = clientBase.spriteHeureux;
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
        ChangeSprite(clientBase.spriteBase);
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
        gameSystem.AddScore(timer*100);
    }
}