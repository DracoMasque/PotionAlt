using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
    public float maxTimer = 30;
    
    public bool commendeFaite = false;
    public bool commendeFini = false;

    public Animation animation;
    
    private bool spriteChanged1 = false;
    private bool spriteChanged2 = false;
    
    public Chaudron chaudron;
    private GameSystem gameSystem;
    private float timeClientAgace;
    private float timeClientEnerve;
    
    [FormerlySerializedAs("TimerSlider")] public Slider timerSlider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        recettesPossible = Resources.LoadAll<Recette>("Scriptable Object\\Recettes");
        spritesPossible = Resources.LoadAll<Sprite>("Visual\\Sprites\\Client\\Normal");
        clientSprite = GetComponent<SpriteRenderer>();
        imagePotion = imagePotionObject.GetComponent<SpriteRenderer>();
        timerSlider = timerSlider.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        timerSlider.value = timer;
        if (commendeFaite && !commendeFini)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= maxTimer/4 && !spriteChanged2)
        {
            print("change2");
            ChangeSprite(spriteEnerve);
            spriteChanged2 = true;
        }
        else if (timer <= maxTimer/2 && !spriteChanged1)
        {
            print("change1");
            ChangeSprite(spriteAgace);
            spriteChanged1 = true;
        }

        if (timer <= 0 && !commendeFini)
        {
            animation.Play("Client Part");
            timerSlider.gameObject.SetActive(false);
        }
        else if (commendeFini)
        {
            ChangeSprite(spriteHeureu);
            animation.Play("Client Part");
            timerSlider.gameObject.SetActive(false);
        }
    }

    public void NewClient()
    {
        print("WHYYYY");
        gameSystem.UpdateRoundClient();
        commendeFaite = false;
        commendeFini = false;
        spriteChanged1 = false;
        spriteChanged2 = false;
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
        ChangeSprite(spriteBase);
        animation.Play("Donne Commande");
    }
    
    private void SetCommende()
    {
        commendeFaite = true;
        timerSlider.gameObject.SetActive(true);
        //chaudron.OnOrderStarted();
    }
    
    public void SetFini()
    {
        commendeFini = true;
        gameSystem.AddScore(timer*10);
    }
}
