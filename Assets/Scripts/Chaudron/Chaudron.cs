using UnityEngine;

public class Chaudron : MonoBehaviour
{
    [SerializeField] public GameObject sceneIngredientsParent;
    Ingrediant[] listeIngredients;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void UpdateIngredients(string[] newIngredientsId)
    {
        //recup tt les ingredients dans la scene
        //recup tt les id des ingredients de la scene
        
        //regarde tous les newIngredientsId dans for
        //si l'ingredient existe pas le rajouter 
        
        //regarde tous les ingredientsSceneId
        //si ils sont pas sur les newIngredientsId les enlever
    }
    void retireObjets(Ingrediant objet)
    {
        //animation
        //desactive l'objet entièrement
    }

    void AjouteObjets(Ingrediant objet)
    {
        //animation
    }
}
