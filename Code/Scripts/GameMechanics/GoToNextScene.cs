using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToNextScene : MonoBehaviour {
    public string Name;
    public Image black;

    private void Start()
    {
        black.canvasRenderer.SetAlpha(0.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadScene();

            PlayerPrefs.DeleteKey("playerX");
            PlayerPrefs.DeleteKey("playerY");
            PlayerPrefs.DeleteKey("playerZ");
        }
    }

    void LoadScene()
    {
        SceneManager.LoadSceneAsync(Name);
        black.CrossFadeAlpha(1.0f, 2.0f, false);
    }
}
