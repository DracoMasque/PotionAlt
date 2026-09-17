using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class IngredientsAnimationEvents : MonoBehaviour
{
    public Ingrediant ingrediant;
    public string id;
    private void Start()
    {
        //From ingredient scriptable, take the id and set it
        
        //Test
        id = ingrediant.uid;
        Appear();
        Invoke("Disappear", 1.5f);

    }
    public void Appear()
    {
        GetComponent<Animation>().Play("AddIngredient");
    }
    public void Disappear()
    {
        GetComponentInChildren<ParticleSystem>().Play();
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


}
