using UnityEngine;

//  djeydki"_èaé&zhyutrkoiçe'_"dofr"'ièhu" +
//   "" +
//   normazleme,t je peux rire sza,d trgztfdrt yu boid <;
//:   en gros ce que j'esszire fr yr fitr 
// a pztrtir dfu momr,ny ou jr tzyr un,dofr'
                
// print( de mon esprit   je pense qu'on a bien plus le temps que ce qu'on se rend cpompte
//parce que en fait je t'ai déja erxpliqué le théoreme de la bite moulue ?'
//  en gros je t'explique fermeeee ta gueuuleeeeeee
//  c'est ^pas ùmpo c'est mmes ùao,s 
//    c'est mles mùains qui écrivent '*
//    clemm a l'aode ke sios pprisonnier' );


public class AudioManager : MonoBehaviour
{
    
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("AudioManager vide");
            }
            return _instance;
        }
    }
    [Header("ClipsAudio")]
    [SerializeField] private AudioClip[] SfxFichier;
    [SerializeField] private AudioClip[] MusicFichier;

    [SerializeField] private AudioSource SfxAudioSource;
    [SerializeField] private AudioSource MusicAudioSource1;
    [SerializeField] private AudioSource MusicAudioSource2;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void JoueSfx(int valeur)
    {
        if (valeur < 0 || valeur >= SfxFichier.Length)
        {
            SfxAudioSource.Pause();
            return;
        }
        SfxAudioSource.clip =  SfxFichier[valeur];
        SfxAudioSource.Play();
    }

    public void JoueSfx(AudioClip clip)
    {
        if (clip != null)
        {
            SfxAudioSource.clip = clip;
            SfxAudioSource.Play();
        }
    }

    public void JoueMusic(int valeur1, int valeur2)
    {
        
        MusicAudioSource1.clip = MusicFichier[valeur1];
        MusicAudioSource1.Play();
        
        MusicAudioSource2.clip = MusicFichier[valeur2];
        MusicAudioSource2.Play();
    }
}
