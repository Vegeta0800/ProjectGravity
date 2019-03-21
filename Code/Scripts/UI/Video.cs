using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Video : MonoBehaviour {

    public string SceneName;
    public GameObject load;
    public bool switching;
    public MovieTexture cutscene;
    public Image black;
    public GameObject aud;

    private void Start()
    {
        black.canvasRenderer.SetAlpha(0.0f);

		GetComponent<RawImage>().texture = cutscene;
        cutscene.Play();
        switching = true;
        load.SetActive(false);
        aud.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("ButtonA") || Input.GetKeyDown(KeyCode.Return))
        {
			if (cutscene.isPlaying)
            {
                aud.SetActive(false);
                switching = false;
                load.SetActive(true);
				cutscene.Stop();
				black.CrossFadeAlpha(1.0f, 2.0f, false);
				SceneManager.LoadSceneAsync(SceneName);

            }
        }

		else if(!cutscene.isPlaying && switching)
        {
            aud.SetActive(false);
            load.SetActive(true);
            switching = false;
			black.CrossFadeAlpha(1.0f, 2.0f, false);
			SceneManager.LoadSceneAsync(SceneName);

        }

    }

}
