using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Starting : MonoBehaviour {

    #region Declarations

    public Transform player; //Spieler wird importiert
    public CharacterController playerC;
    public Transform Spawnpoint; //Spawnpoint wird importiert.
    public Transform Spawnpoint2; //Spawnpoint wird importiert.

    //Lost, Win und Pause Menu werden importiert.
    public GameObject pause;

    public bool champ;

    [System.Serializable]
    public class PowerUp
    {
        public string name;
        public GameObject Powerup;
        public Transform Position;
        public string tag;
    }

    public PowerUp[] powerups;

    public List<GameObject> activePowerups;

    public EventSystem ES; //EventSystem wird importiert

    public Movement mov;
    public Inputs inp;
    public PowerupControllers pow;

    #endregion

    //Vor dem Programm
    public void StartofGame()
    {

        DeletePowerups();

        if (pow.effectOnD)
        {
            pow.EndofDown();
        }

        if (pow.effectOnS)
        {
            pow.EndofSlow();
        }

        //Wenn Spieler Positionen gespeichert wurden
        if (PlayerPrefs.HasKey("playerX"))
        {
            //Wird der Spieler an die entsprechenden Positionen gestellt.
            player.position = new Vector3(PlayerPrefs.GetFloat("playerX"), PlayerPrefs.GetFloat("playerY"), PlayerPrefs.GetFloat("playerZ"));
        }
        //Wenn es keine Spieler Positionen gibt
        else
        {
            if (champ)
            {
                player.position = Spawnpoint2.position;
            }
            else
            {
                //Wird die Spieler Position auf die Spawnpoint Position gestellt
                player.position = Spawnpoint.position;
            }

        }

        playerC.enabled = true;
        //Alle Menus werden deaktiviert

        pause.SetActive(false);

        inp.timer = 0f;
        pow.timerDash = 0f;

        inp.noSwitch = false;

        SpawnPowerups();
    }

    void SpawnPowerups()
    {
        foreach(PowerUp p in powerups)
        {
            activePowerups.Add(Instantiate(p.Powerup, p.Position.position, p.Position.rotation));
            p.Powerup.name = p.name;
            p.Powerup.tag = p.tag;
        }
    }

    public void DeletePowerups()
    {
        for(int i = 0; i < activePowerups.Count; i++)
        {
            Destroy(activePowerups[i]);
        }
        activePowerups.Clear();
    }

}
