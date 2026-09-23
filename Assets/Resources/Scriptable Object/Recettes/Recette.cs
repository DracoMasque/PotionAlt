using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recette", menuName = "Scriptable Objects/Recette")]
public class Recette : ScriptableObject
{
    public string name;
    public List<Ingrediant> ingredients;
    public Sprite spriteIngredient;
    public Sprite spritePotion;
}
