using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour {

    public GameObject TutorialScreen;
    public Inputs inp;
    public float waitTime;

    public bool tutorialScreenShowOn = true;
    bool screenOn;

    private void Update()
    {
        if(Input.GetButtonDown("ButtonB") && screenOn || Input.GetKeyDown(KeyCode.Return) && screenOn || Input.GetMouseButtonDown(0) && screenOn)
        {
            screenOn = false;
            inp.enabled = true;
            inp.ActivateScripts();
            TutorialScreen.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tutorialScreenShowOn)
            {
                tutorialScreenShowOn = false;
                Tutorial();
            }
        }
    }


    void Tutorial()
    {
        screenOn = true;
        inp.speed = 0f;
        inp.DeactivateScripts();
        inp.enabled = false;
        TutorialScreen.SetActive(true);
    }
}
