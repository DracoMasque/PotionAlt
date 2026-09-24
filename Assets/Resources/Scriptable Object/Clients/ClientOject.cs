using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Client", menuName = "Scriptable Objects/Client")]
public class ClientOject : ScriptableObject
{
    public string nom;
    public Sprite spriteBase;
    public Sprite spriteAgace;
    public Sprite spriteEnerve;
    public Sprite spriteHeureux;
    
    public AudioClip audioAgace;
    public AudioClip audioEnerve;
    public AudioClip audioHeureux;
    public AudioClip audioMarche;
}
