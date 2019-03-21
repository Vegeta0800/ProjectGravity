using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    //Die Public Klasse Sound die angelegt wurde im Sound Script
    //Ein Array(Liste) von Sounds wird festgelegt
    public Sound[] sounds;

    //Bevor das Spiel startet
    void Awake()
    {
        //für jeden Sound (mit Namen s) in dem Array(Liste) sounds
        foreach (Sound s in sounds)
        {
            //ist die Source ein Audio
            s.source = gameObject.AddComponent<AudioSource>();

            //Der Name wird festgelegt im Inspector
            s.source.name = s.name;

            //ist der clip vom Sound der ausgewählte Sound im Inspector
            s.source.clip = s.audio;

            //ist der Pitch und die Lautstärke vom Sound die ausgewählten Werte im Inspector
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            //ist festgelegt ob der Sound  sich loopt oder nicht im Inspector
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.mixer;

        }
    }


    //Neue Funktion die man überall aufrufen kann mit Namen Play()
    //Dazu muss man in die Klammern den Namen vom Sound eingeben.
    public void Play(string name)
    {
        //Der Sound s = das Objekt aus dem Array Sounds[] mit dem Namen in Play()
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //Der Sound wird abgespielt mit den Attributen die festgelegt wurden.
        s.source.Play();
	}

    public void Stop(string name)
    {
        //Der Sound s = das Objekt aus dem Array Sounds[] mit dem Namen in Play()
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //Der Sound wird abgespielt mit den Attributen die festgelegt wurden.
        s.source.Stop();
    }
}
