using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inputs : MonoBehaviour
{
    #region Declarations

    public GravityMechanic grv;
    public Movement mov;
    public PowerupControllers pow;

    public ParticleSystem shieldOnPS;
    public ParticleSystem shieldHitPS;
    public ParticleSystem shieldDestroyedPS;
    public ParticleSystem superheroLandingUpPS;
    public ParticleSystem superheroLandingPS;
    public ParticleSystem shrinkingPS;
    public ParticleSystem WaterCollisonPS;

    public GameObject shieldOnGB;
    public GameObject shrinkingOnGB;
    public GameObject slowmoOnGB;
    public GameObject superheroOnGB;
    public GameObject waterCollisonGB;
    public GameObject superheroUpOnGB;

    public bool shieldHit;
    public bool shieldDestroyed;

    public bool downOn;
    public bool downOnImage;

    public bool noSwitch;
    public bool powerupActive;

    public bool downTrue;

    public bool waterCollision;

    public bool tutorialScreenShowOn;

    public Image slow;
    public Image shield;
    public Image down;

    public Image powerupFilling;


    public AudioManager aud;

    public float cooldown;
    public float timer;

    public GameObject PauseMenu; //Pausen Menu wird importiert

    public Animator ani; //Spieler Animationen werden importiert

    public EventSystem ES; //Eventsystem wird importiert

    public float speed; //Speed von der Animation beim laufen.

    public bool powerup;

    public bool shieldTrue;

    public bool dashTrue;

    public bool timeTrue;

    public bool slowTrue;

    public bool shieldOn;

    public bool gravityAniTop;

    public bool gravityAniBot;

    public bool onWall;

    public int shieldActivations;

    public bool deathOn;


    public bool shieldImageactive;
    public bool slowImageactive;
    #endregion
   
    void Start()
    {
        aud.Play("main");   

        slow.enabled = false;
        noSwitch = false;
        shield.enabled = false;
        down.enabled = false;
        powerupFilling.fillAmount = 0f;
        powerupActive = false;
    }

    //Update each Frame
    void Update() 
    {
        if (waterCollisonGB.activeInHierarchy == false)
        {
            waterCollisonGB.SetActive(true);
        }

        if (!deathOn)
        {
            InputsGravity();

            InputsMovement();

            InputsMenu();

            InputPowerups();

            InputDash();
        }
        //All of the Functions will constantly be used.


        ParticleShield();

        Images();
    }

    void InputsGravity()
    {

        if (Input.GetButtonDown("ButtonA")) // ButtonA means XboxController ButtonA and Space
        {
            if (onWall)
            {
                aud.Play("gravity");

                //if gravity is not active
                if (grv.gravityActive == false)
                {
                    gravityAniTop = true;
                    gravityAniBot = false;

                    grv.gravityActive = true; //set to active

                    mov.rotationZ = 180f; //Der Spieler dreht sich richtig rum.


                }
                //if gravity is active
                else
                {
                    gravityAniBot = true;
                    gravityAniTop = false;

                    mov.rotationZ = 0f; //Der Spieler dreht sich richtig rum.

                    grv.gravityActive = false; //set to not active
                }
            }
        }

    }

    void InputsMovement()
    {
        //LeftJoystick and A, D, Left_Arrow and Right_Arrow 

        mov.move = new Vector3(0, 0, Input.GetAxis("Horizontal"));
        //Look Right if going right
        if (Input.GetKeyDown(KeyCode.D) || Input.GetAxis("Horizontal") > 0)
        {

            //Player gets no rotation so he looks right
            mov.rotationY = 0;
            //Set the speed to 100%  
            speed += 0.085f;

            //he looks to the front
            mov.frontActive = true;
        }
        //Look Left if going left
        if (Input.GetKeyDown(KeyCode.A) || Input.GetAxis("Horizontal") < 0)
        {

            //Player gets no rotation so he looks right
            mov.rotationY = 180;

            //Set the speed to 100%  
            speed += 0.085f;

            //he looks not to the front
            mov.frontActive = false;
        }

        if (Input.GetAxis("Horizontal") == 0f)
        {
            //Set the speed to 0%  
            speed = 0f;
        }
    }

    void InputsMenu()
    {
        //If the Start Button on the Xbox Controller
        //or the Escape Button is pressed
        if (Input.GetButtonDown("ButtonStart"))
        {
            //activate the Pause Menu
            if (PauseMenu.activeInHierarchy == false)
            {
                Time.timeScale = 0;

                //Disable this script
                this.enabled = false;

                //Show the Pause Menu
                PauseMenu.SetActive(true);

                //Select the first Button which is Resume
                ES.SetSelectedGameObject(GameObject.Find("Resume"));
            }

            else if (PauseMenu.activeInHierarchy == true)
            {
                Time.timeScale = 1;

                //Disable this script
                this.enabled = true;

                //Closes the Pause Menu
                PauseMenu.SetActive(false);
            }

        }
    }

    void InputPowerups()
    {
        if (Input.GetButtonDown("ButtonB") && powerup && !powerupActive)
        {
            aud.Play("powerupUse");
            powerup = false;

            if (timeTrue)
            {
                slowTrue = true;

                if (!slowmoOnGB.activeInHierarchy)
                {
                    slowmoOnGB.SetActive(true);
                }
            }

            if (downOn)
            {
                downTrue = true;

                if (!shrinkingOnGB.activeInHierarchy)
                {
                    shrinkingOnGB.SetActive(true);
                }
            }

            if (shieldOn)
            {
                shieldTrue = true;

                aud.Play("Shield");

                timer = cooldown;

                powerupFilling.fillAmount = 1f;

                if (!shieldOnGB.activeInHierarchy)
                {
                    shieldOnGB.SetActive(true);
                }
            }

        }

        if (Input.GetButtonDown("ButtonRB") && !noSwitch)
        {
            pow.Switch();
        }
    }

    void InputDash()
    {
        if (Input.GetButtonDown("ButtonX"))
        {
            pow.DoDash();
        }

    }


    void ParticleShield()
    {
        if (shieldTrue)
        {
            shieldOnPS.Play();
        }

        if (shieldHit)
        {
            shieldHit = false;
            shieldHitPS.Play();
        }

        if (shieldDestroyed)
        {
            aud.Stop("Shield");
            aud.Play("ShieldHit");

            noSwitch = false;
            powerupActive = false;

            pow.activePowerup = null;
            shieldDestroyed = false;
            shieldTrue = false;
            shieldImageactive = false;
            slowImageactive = false;
            downOn = false;
            downOnImage = false;

            shield.enabled = false;
            slow.enabled = false;
            down.enabled = false;

            powerupFilling.fillAmount = 0f;


            shieldOnPS.Stop();

            shieldDestroyedPS.Play();
        }

    }

    void Images()
    {
        if (shieldImageactive)
        {
            shield.enabled = true;
            slow.enabled = false;
            down.enabled = false;
        }

        else if (slowImageactive)
        {
            slow.enabled = true;
            shield.enabled = false;
            down.enabled = false;
        }

        else if (downOnImage)
        {
            slow.enabled = false;
            shield.enabled = false;
            down.enabled = true;
        }

        if (timer > 0f && shieldTrue)
        {
            noSwitch = true;
            powerupActive = true;
            timer -= Time.deltaTime;

            powerupFilling.fillAmount -= 1.0f / cooldown * Time.deltaTime;

        }
        if (timer <= 0f && shieldTrue)
        {
            shieldDestroyed = true;
            timer = 0f;
        }



    }

    public void DeactivateScripts()
    {
        mov.enabled = false;
    }

    public void ActivateScripts()
    {
        mov.enabled = true;
    }

}

