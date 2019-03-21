using UnityEngine;
using UnityEngine.EventSystems;

public class SafeZone : MonoBehaviour {
    public GameObject win;

	public Timer tim;
    public AudioManager aud;

    public EventSystem ES; //Eventsystem wird importiert

    private void Start()
    {
        win.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0; //Pausiere das Spiel

            aud.Stop("main");

            //Aktiviere den Win Screen
            win.SetActive(true);


            ES.SetSelectedGameObject(GameObject.Find("Back")); //Wähle den ersten Button im Menu automatisch aus.

            tim.end.text = "Your time was: " + tim.text + " seconds";

            PlayerPrefs.DeleteKey("playerX");
            PlayerPrefs.DeleteKey("playerY");
            PlayerPrefs.DeleteKey("playerZ");


            if (PlayerPrefs.HasKey("Highscore"))
            {
               if(tim.time < PlayerPrefs.GetFloat("Highscore")){

                    PlayerPrefs.SetFloat("Highscore", tim.time);
                }
            }
            else
            {
                PlayerPrefs.SetFloat("Highscore", tim.time);
            }
                tim.highscore = PlayerPrefs.GetFloat("Highscore");

            tim.highscoreText.text = "Your best time was: " + tim.highscore.ToString("0.00") + " seconds";
        }
    }
}
