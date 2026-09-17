using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = System.Diagnostics.Debug;

public class Chaudron : MonoBehaviour
{
    [SerializeField] private Recette[] recettes;
    [SerializeField] public GameObject sceneIngredientsParent;
    private List<Ingrediant> listeIngredients;
    private GameSystem gameSystem;

    void Start()
    {
        recettes = Resources.LoadAll<Recette>("Scriptable Object\\Recettes");
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
    }
    
    //----------------UPDATE INGREDIENTS---------------
    //Lancer cette fonction quand on reçois gameObject.BroadcastMessage("UpdateIngredients", [la liste des id]);
    void UpdateIngredients(string[] newIngredientsId)
    {
        GameObject[] SceneObjectsList = RecupObjectsScene();
        string[] idList = RecupIdObjectsScene();
        
        //regarde chacun des newIngredientsId
        //si l'ingredient existe pas le rajouter 
        for (int i = 0; i < newIngredientsId.Length; i++)
        {
            if (idList.Contains(newIngredientsId[i]))
            {
                AjouteObjects(newIngredientsId[i]);
            }
        }
        
        //regarde chacun des idList
        //si ils sont pas sur les newIngredientsId les enlever
        for (int i = 0; i < idList.Length; i++ )
        {
            if (!newIngredientsId.Contains(idList[i]))
            {
                retireObjects(idList[i]);
            }
        }
    }
    private void retireObjects(string id)
    {
        TrouveObjetScene(id).GetComponent<IngredientsAnimationEvents>().Disappear();
    }
    private  void AjouteObjects(string id) //Ingrediant objet
    {
        TrouveObjetScene(id).GetComponent<IngredientsAnimationEvents>().Appear();
    }
    
    //Recup tous les id des ingredients de la scene
    private string[] RecupIdObjectsScene()
    {
        string[] idList = { };
        
        IngredientsAnimationEvents[] a = GetComponentsInChildren<IngredientsAnimationEvents>();
        for (int i = 0; i < a.Length; i++)
        {
            idList.Append(a[i].id);
        }
            
        return idList;
    }
    //Recup tous les ingredients de la scene
    private GameObject[] RecupObjectsScene()
    {
        GameObject[] objectList = { };
        
        IngredientsAnimationEvents[] a = GetComponentsInChildren<IngredientsAnimationEvents>();
        for (int i = 0; i < a.Length; i++)
        {
            objectList.Append(a[i].gameObject);
        }
            
        return objectList;
    }
   
    //Trouve un objet a partir d'une id
    private GameObject TrouveObjetScene(string id)
    {
        GameObject objectScene = null;
        
        GameObject[] objectSceneList = RecupObjectsScene();
        for (int i = 0; i < objectSceneList.Length; i++)
        {
            if (objectSceneList[i].GetComponent<IngredientsAnimationEvents>().id == id)
            {
                objectScene = objectSceneList[i];
            }
        }

        return objectScene;
    }
    //----------------UPDATE INGREDIENTS---------------
    //Quand le joueur Appuis sur un bouton pour confirmer)
    void ConfirmeRecette()
    {
        //Regarde tous les ingredients et si ça match
        //Si il y a un truc qui match 
        //Broadcast un message avec l'id ou le nom de la potion
        //OnRecetteConfirme(Recette la_recette)
        listeIngredients.Sort();
        foreach (Recette recette in recettes)
        {
            recette.ingredients.Sort();
            if (recette.ingredients == listeIngredients)
            {
                gameSystem.OnRecetteConfirme(recette);
            }
        }
    }

    void GetIngredients()
    {
        
    }
}
