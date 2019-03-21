using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.UI;
public class UIHandler : MonoBehaviour {
    #region Declarations
    public Inputs inp;
    public Movement mov;
    public PowerupControllers pow;
    public Animations anim;
    public AudioManager aud;
    public Starting str;
	public Timer tim;

    [Header("Menus")]
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject ControlsMenu;
    [SerializeField] private GameObject OptionMenu;

    [SerializeField] private GameObject VideoSettingsMenu;
    [SerializeField] private GameObject SoundSettingsMenu;

    [SerializeField] private GameObject win;
    [SerializeField] private GameObject black;

    [SerializeField] private GameObject LoadScene;
    [SerializeField] private GameObject MainMenu;

    [SerializeField] private Dropdown resolutionDropdown;

    [Header("GameObject Data")]
    [SerializeField] private GameObject Player;

    [SerializeField] private Transform Spawnpoint;
    [SerializeField] private Transform player;


    [Header("Other")]
    [SerializeField] private EventSystem ES;
    [SerializeField] private Text prc;

    [SerializeField] private Image load;
    [SerializeField] private string Level1; //Level1 imported

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Animator ani;

    Resolution[] resolutions;
    List<string> options = new List<string>(); //List of options for the dropdown Menu
    private int currentResolution;
    #endregion
    // Use this for initialization
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if(currentScene.name == "MainMenu")
        {
            load.enabled = false;

            aud = FindObjectOfType<AudioManager>(); //Importiere den AudioManager

            aud.Play("Backround"); //Play Backround Music

            ES.SetSelectedGameObject(GameObject.Find("Play"));  //Select the Play Button as first Selected Button
        }
        else if(currentScene.name == "Credits")
        {
			return;
        }
        else
        {
            anim.spawn = true;

            if (currentScene.name == "Championship")
            {
                str.champ = false;
            }

        }

        #region Resolution

        if (currentScene.name == "Credits")
        {
            return;
        }
        else
        {
            resolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolution = i;
                }

            }

            resolutionDropdown.AddOptions(options);

            resolutionDropdown.value = currentResolution;

            resolutionDropdown.RefreshShownValue();
        }

        #endregion


    }

    #region ButtonScripts

    #region Menu
    //Button Script for the Buttton Start from Beginning/Play Again

    public void Play()
    {
        MainMenu.SetActive(false);
        LoadScene.SetActive(true);
        ES.SetSelectedGameObject(GameObject.Find("Normal"));
    }

    public void NormalMode()
    {
        load.enabled = true;
        PlayerPrefs.DeleteKey("playerX");
        PlayerPrefs.DeleteKey("playerY");
        PlayerPrefs.DeleteKey("playerZ");
        SceneManager.LoadSceneAsync(Level1);
    }
    public void ChampionMode()
    {
        load.enabled = true;
        PlayerPrefs.DeleteKey("playerX");
        PlayerPrefs.DeleteKey("playerY");
        PlayerPrefs.DeleteKey("playerZ");
        SceneManager.LoadSceneAsync("CutScene 1");
    }

    public void Resets()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        aud.Play("spawn");

        pow.emptyBig = true;
        pow.emptySmall = true;

        pow.slowSmall.enabled = false;
        pow.shieldSmall.enabled = false;
        pow.downSmall.enabled = false;

        pow.activePowerup = null;
        pow.firstPowerup = null;
        pow.secondPowerup = null;

        inp.slow.enabled = false;
        inp.shield.enabled = false;
        inp.down.enabled = false;
        inp.powerupFilling.fillAmount = 0f;
        inp.powerupActive = false;

        inp.tutorialScreenShowOn = true;

        //Delete any Data from Checkpoints
        PlayerPrefs.DeleteKey("playerX");
        PlayerPrefs.DeleteKey("playerY");
        PlayerPrefs.DeleteKey("playerZ");

        if (currentScene.name == "Championship")
        {
            str.champ = true;
			tim.time = 0.0f;
        }

        //Unfreeze the Game
        Time.timeScale = 1;

        mov.rotationZ = 0f;
        mov.rotationY = 0f;


        inp.enabled = true;

        PauseMenu.SetActive(false);
        anim.spawn = true;


    }

    //Button Script for the Button Back To Main Menu
    public void BackToMainMenu()
    {
        //Load the Scene Main Menu (Which is the Main Menu)
        SceneManager.LoadSceneAsync("MainMenu");

        ES.SetSelectedGameObject(GameObject.Find("Play"));  //Select the Play Button as first Selected Button
    }

	public void Winning(){
		SceneManager.LoadSceneAsync("Credits");
	}

    //Button Script for the Button Options
    public void Options()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "MainMenu")
        {
            MainMenu.SetActive(false);
        }

        else
        {
            PauseMenu.SetActive(false);

        }
        OptionMenu.SetActive(true);
        ES.SetSelectedGameObject(GameObject.Find("Video Settings"));
    }

    //Button Script for the Button Controls
    public void Controls()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "MainMenu")
        {
            MainMenu.SetActive(false);
        }

        else
        {
            PauseMenu.SetActive(false);

        }

        ControlsMenu.SetActive(true);
        ES.SetSelectedGameObject(GameObject.Find("Back"));
    }

    public void Exit()
    {
        //If the Game is only opened in the UnityEditor
        //if (UnityEditor.EditorApplication.isPlaying == true)
        //{
         //   //Deactivate the Play Mode
        //    UnityEditor.EditorApplication.isPlaying = false;
       // }

        //If it is the finished Game
       // else
       // {
            //Close the Game
            Application.Quit();
        //}
    }

    //Button Script for the Button Resume
    public void Resume()
    {
        //Enable the Input Script again
        inp.enabled = true;

        //Set the Pause Menu to not active
        PauseMenu.SetActive(false);

        //Unfreeze the Game
        Time.timeScale = 1;
    }
    #endregion

    #region SettingsMenu
    public void MasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);

        float y = volume * -1;
        float x = (y * 100) / 80;
        float txtVolume = 100 - x;
        string text = string.Format("{0:0\\%}", txtVolume);

        prc.text = text;
    }

    public void Quality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void Fullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void Resolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void BackToOptions()
    {

        OptionMenu.SetActive(true);

        //Deactivate the other two
        VideoSettingsMenu.SetActive(false);
        SoundSettingsMenu.SetActive(false);

        //Select the Button "Resume" first
        ES.SetSelectedGameObject(GameObject.Find("Video Settings"));

    }
    #endregion

    #region Other
    //Button Script for the Button Back to Menu
    public void BackToMenu()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "MainMenu")
        {
            MainMenu.SetActive(true);
            LoadScene.SetActive(false);
            ES.SetSelectedGameObject(GameObject.Find("Play"));
        }
        else
        {
            PauseMenu.SetActive(true);
            ES.SetSelectedGameObject(GameObject.Find("Resume"));
        }
        //Deactivate the other two
        OptionMenu.SetActive(false);
        ControlsMenu.SetActive(false);



    }
    public void VideoSettings()
    {
        OptionMenu.SetActive(false);

        //Activate the Video Settings Menu
        VideoSettingsMenu.SetActive(true);

        //Select the Button Back to Menu first
        ES.SetSelectedGameObject(GameObject.Find("Back"));

    }

    public void SoundSettings()
    {
        OptionMenu.SetActive(false);

        //Activate the Sound Settings Menu
        SoundSettingsMenu.SetActive(true);

        //Select the Button Back to Menu first
        ES.SetSelectedGameObject(GameObject.Find("Back"));

    }
    #endregion

    #endregion
}
