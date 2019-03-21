using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LastLevel : MonoBehaviour
{

    public MovieTexture cutscene;
    public GameObject aud;
    public MovieTexture Cutscene;

    bool handler;
    bool handler2;
    private void Start()
    {
        aud.SetActive(true);
        handler2 = false;
        handler = true;
        GetComponent<RawImage>().texture = cutscene;
        cutscene.Play();

    }
    // Update is called once per frame
    void Update()
    {

        if (!cutscene.isPlaying && handler)
        {
            handler = false;
            Credit();
        }
        if (!Cutscene.isPlaying && handler2)
        {
            handler2 = false;
            SceneManager.LoadSceneAsync("MainMenu");
        }

    }
    void Credit()
    {
        aud.SetActive(false);
        GetComponent<RawImage>().texture = Cutscene;
        handler2 = true;
        Cutscene.Play();
    }
}

