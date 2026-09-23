using System;
using System.Collections.Generic;
using System.Linq;
using AltControllerSettings;
using UnityEngine;
using UnityEngine.UIElements;

public class Chaudron : MonoBehaviour
{
    [SerializeField] private Recette[] recettes;
    [SerializeField] public GameObject sceneIngredientsParent;
    [SerializeField] private string quitId = "C4-A7-CC-F1";
    public List<Ingrediant> listeIngredients = new List<Ingrediant>();
    
    private GameSystem gameSystem;
    private Recette recetteActuel = null;

    private IngredientsAnimationEvents[] a;
    private void Start()
    {
        recettes = Resources.LoadAll<Recette>("Scriptable Object\\Recettes");
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        GetComponentInChildren<NfcReaderManager>()._chaudronComponent = GetComponent<Chaudron>();
        a = GetComponentsInChildren<IngredientsAnimationEvents>();
    }
    
    //----------------UPDATE INGREDIENTS---------------
    //Lancer cette fonction quand on reçois gameObject.BroadcastMessage("UpdateIngredients", [la liste des id]);
    void UpdateIngredients(string[] newIngredientsId)
    {
        List<GameObject> SceneObjectsList = RecupObjectsScene();
        List<string> idList = RecupIdObjectsScene();
        
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
        for (int i = 0; i < idList.Count; i++ )
        {
            if (!newIngredientsId.Contains(idList[i]))
            {
                RetireObjects(idList[i]);
            }
        }
    }
    public void RetireObjects(string id)
    {
        if (id != quitId)
        {
            Debug.Log(id +" Retire objets");
            TrouveObjetScene(id).GetComponent<IngredientsAnimationEvents>().Disappear();
            listeIngredients.Remove(TrouveObjetScene(id).GetComponent<IngredientsAnimationEvents>().ingrediant);
        }
    }
    public void AjouteObjects(string id) //Ingrediant objet
    {
        if (id == quitId)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
        else if (id != quitId)
        {
            Debug.Log(id + " Ajoute Objet");
            TrouveObjetScene(id).GetComponent<IngredientsAnimationEvents>().Appear();
            listeIngredients.Add(TrouveObjetScene(id).GetComponent<IngredientsAnimationEvents>().ingrediant);
        }
    }
    
    //Recup tous les id des ingredients de la scene
    private List<string> RecupIdObjectsScene()
    {
        List<string> idList = new List<string>();
        
        for (int i = 0; i < a.Length; i++)
        {
            idList.Add(a[i].id);
            
        }
            
        return idList;
    }
    //Recup tous les ingredients de la scene
    private List<GameObject> RecupObjectsScene()
    {
        List<GameObject> objectList = new List<GameObject>();
        
        for (int i = 0; i < a.Length; i++)
        {
            objectList.Add(a[i].gameObject);
        }
        return objectList;
    }
   
    //Trouve un objet a partir d'une id
    private GameObject TrouveObjetScene(string id)
    {
        GameObject objectScene = null;
        
        List<GameObject> objectSceneList = RecupObjectsScene();
        for (int i = 0; i < objectSceneList.Count; i++)
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
    public Recette ConfirmeRecette()
    {
        //Regarde tous les ingredients et si ça match
        //Si il y a un truc qui match 
        //Broadcast un message avec l'id ou le nom de la potion
        //OnRecetteConfirme(Recette la_recette)
        
        //listeIngredients.Sort();
        for (int i = 0; i < recettes.Length; i++)
        {
            int similaire = 0;
            int similaireObjectif = recettes[i].ingredients.Count;
            //recettes[i].ingredients.Sort();
            foreach (Ingrediant ingrediant in listeIngredients)
            {
                if (recettes[i].ingredients.Contains(ingrediant))
                {
                    similaire++;
                }
            }
            if (similaire ==  similaireObjectif)
            {
                return recettes[i];
            }
        }
        return null;
    }

    public void Servire()
    {
        recetteActuel = ConfirmeRecette();
        gameSystem.OnRecetteConfirme(recetteActuel);
        recetteActuel = null;
    }

    public void OnOrderStarted()
    {
        Debug.Log("J'ai tellement oublié ce que cette fonction est sensée faire");
    }
 
}
