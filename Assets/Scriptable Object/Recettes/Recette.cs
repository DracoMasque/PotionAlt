using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recette", menuName = "Scriptable Objects/Recette")]
public class Recette : ScriptableObject
{
    public List<Ingrediant> Ingrediants;
}
