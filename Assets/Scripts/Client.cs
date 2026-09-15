using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Recette[] recettesPossible;
    public Recette RecetteDemander;
    
    private Sprite[] spritesPossible;
    private Sprite spriteActuel;
    
    public float timer;
    public float maxTimer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recettesPossible = Resources.LoadAll<Recette>("ScriptableObjects/Recettes");
        spritesPossible = Resources.LoadAll<Sprite>("Sprite/Client");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeState()
    {
        if (timer < maxTimer / 2)
        {
            if ()
        }
    }
}
