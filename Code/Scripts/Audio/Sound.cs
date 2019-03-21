using UnityEngine;
using UnityEngine.Audio;


//neue Klasse die man überall verwenden kann sowas wie: AudioClip, GameObject, etc.
[System.Serializable] // Anpassbar im Inspector
public class Sound {

    //Alle Attribute in dieser Klasse.

    public AudioClip audio; //Audio File

    public string name; //Name der Audio

    [Range(0f, 1f)]
    public float volume; //Lautstärke der Audio File (0-100%)

    [Range(.1f, 3f)]
    public float pitch; //Pitch der Audio File (10-300%)

    public AudioMixerGroup mixer;

    public bool loop; //Soll die File geloopt werden


    [HideInInspector] //Nicht anschaubar im Inspector
    public AudioSource source; //Das bedeutet, das das ein Audio Object ist. 
}
