using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recette", menuName = "Scriptable Objects/Recette")]
public class Recette : ScriptableObject
{
    public string Name;
    public List<Ingrediant> Ingrediants;
    public Sprite Sprite;
}
