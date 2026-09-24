using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class IngredientsAnimationEvents : MonoBehaviour
{
    public Ingrediant ingrediant;
    public List<string> id;
    private AudioManager audioManager;
    
   
    
    private void Start()
    {
        //From ingredient scriptable, take the id and set it
        
        //Test
        {
            id = ingrediant.uid;
            if (ingrediant.sprite)
            {
                GetComponent<SpriteRenderer>().sprite = ingrediant.sprite;
            }
        }
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

    }
    public void Appear()
    {
        GetComponent<Animation>().Play("AddIngredient");
    }
    public void Disappear()
    {
        //GetComponentInChildren<ParticleSystem>().Play();
        GetComponent<Animation>().Play("RemoveIngredient");
    }
    //FONCTION POUR ANIMATION
    private void DisableRigidBodyComponents()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
        //Debug.Log("Disappear");
    }
    private void EnableRigidBodyComponents()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<BoxCollider2D>().enabled = true;
        //Debug.Log("Appear??");
    }

    private void JouePlouf()
    {
        audioManager.JoueSfx(0);
    }
}
