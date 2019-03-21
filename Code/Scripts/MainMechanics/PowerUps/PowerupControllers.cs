using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupControllers : MonoBehaviour {
    public Movement mov;
    public Inputs inp;
    public GravityMechanic grv;

    public AudioManager aud;

    [Header("Downsize")]
    public Transform player;

    public float durationDown;
    public float timer;

    public bool downOn;

    [Header("Slowmotion")]
    public float slowdownFactor;
    public float slowdownTime;
    public float durationTime;

    public int multiSpeed;

    public ParticleSystem slowmo;

    [Header("Dash")]
    public ParticleSystem dashing;

    public ParticleSystem DashPS;

    public Image dashImage;

    bool dashOn = false;

    public float dashPower;

    private Vector3 dash;

    public CharacterController playerC;

    public float time;
    public float multi;
    private Vector3 EndPos;

    [SerializeField]
    private float cooldown;

    public float timerDash = 0f;

    [Header("UI & Control")]
    public Image slowFilling;
    public Image downFilling;

    public bool emptyBig;
    public bool emptySmall;

    public Image slowSmall;
    public Image shieldSmall;
    public Image downSmall;

    public bool down;
    public bool slow;
    public bool shield;

    public bool effectOnD;
    public bool effectOnS;

    [HideInInspector] public Image activePowerup;
    [HideInInspector] public Image firstPowerup;
    [HideInInspector] public Image secondPowerup;

    private void Start()
    {
        emptyBig = true;
        emptySmall = true;

        slowSmall.enabled = false;
        shieldSmall.enabled = false;
        downSmall.enabled = false;

        activePowerup = null;
        firstPowerup = null;
        secondPowerup = null;

        dashing.Stop();
        dashImage.gameObject.SetActive(true);
    }

    void Update()
    {
        CheckActives();

        #region TimeControllerDownsizing
        if (inp.downTrue)
        {
            inp.downTrue = false;
            StartCoroutine(Down());
        }

        if (timer >= 0f)
        {
            timer -= Time.deltaTime;
            downFilling.fillAmount -= 1.0f / durationDown * Time.deltaTime;
        }
        #endregion

        #region TimeControllerSlow
        if (inp.slowTrue)
        {
            inp.slowTrue = false;
            StartCoroutine(Slowdown());
        }

        if (durationTime >= 0f)
        {
            durationTime -= Time.deltaTime;

            slowFilling.fillAmount -= 1.0f / slowdownTime * Time.deltaTime;
        }
        #endregion

        #region TimeControllerDash
        if (timerDash >= 0f)
        {
            timerDash -= Time.deltaTime;
            dashImage.gameObject.SetActive(false);
        }

        if (timerDash <= 0f)
        {
            dashImage.gameObject.SetActive(true);

            DashPS.Stop();
        }

        if (timerDash <= 0.1f && timerDash >= 0f)
        {
            aud.Play("dashOn");
            DashPS.Play();
        }
        #endregion
    }

    #region Dash
    public void DoDash()
    {
        if (!dashOn && timerDash <= 0)
        {
            aud.Play("dash");
            timerDash = cooldown;
            dashOn = true;
            dashImage.gameObject.SetActive(false);
            if (mov.rotationY == 180)
            {
                EndPos = new Vector3(0, 0, dashPower);
                StartCoroutine(Dashing());
            }
            else
            {
                EndPos = new Vector3(0, 0, -dashPower);
                StartCoroutine(Dashing());
            }
        }
    }

    private IEnumerator Dashing()
    {
        dashing.Play();
        while (time < 1f)
        {
            playerC.Move(EndPos * time);
            time += multi;
            yield return null;
        }
        dashing.Stop();
        time = 0f;
        dashOn = false;
        yield return null;
    }
    #endregion

    #region PowerupEffects
    IEnumerator Down()
    {
        player.SetParent(null);
        effectOnD = true;
        aud.Play("shrinkDown");

        inp.shrinkingPS.Play();

        inp.powerupActive = true;
        inp.noSwitch = true;

        timer = durationDown;
        downFilling.fillAmount = 1f;

        player.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        yield return new WaitForSeconds(durationDown);

        if (effectOnD)
        {

            effectOnD = false;
            aud.Stop("shrinkDown");
            aud.Play("shrinkUp");

            player.SetParent(null);
            player.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            inp.shrinkingPS.Stop();

            downFilling.fillAmount = 0f;

            inp.powerup = false;
            inp.shield.enabled = false;
            inp.slow.enabled = false;
            inp.shieldImageactive = false;
            inp.slowImageactive = false;
            inp.down.enabled = false;
            inp.downOnImage = false;
            inp.noSwitch = false;
            inp.powerupActive = false;

            activePowerup = null;
        }
    }

    IEnumerator Slowdown()
    {
        effectOnS = true;
        aud.Play("slow");
        inp.noSwitch = true;
        inp.powerupActive = true;

        durationTime = slowdownTime;

        slowFilling.fillAmount = 1f;

        slowmo.Play();

        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;

        mov.movementSpeed = mov.movementSpeed * multiSpeed;

        grv.veloSlowmotion = true;

        yield return new WaitForSeconds(slowdownTime);

        if (effectOnS)
        {
            effectOnS = false;
            aud.Stop("slow");

            grv.veloSlowmotion = false;

            durationTime = 0;
            slowFilling.fillAmount = 0f;

            mov.movementSpeed = mov.movementSpeed / multiSpeed;

            slowmo.Stop();

            inp.powerup = false;
            inp.shield.enabled = false;
            inp.slow.enabled = false;
            inp.shieldImageactive = false;
            inp.slowImageactive = false;
            inp.downOnImage = false;
            inp.down.enabled = false;
            inp.noSwitch = false;
            inp.powerupActive = false;

            activePowerup = null;

            Time.timeScale = 1;
            Time.fixedDeltaTime = .02f;
        }
    }

    public void EndofSlow()
    {
        effectOnS = false;
        aud.Stop("slow");

        grv.veloSlowmotion = false;

        durationTime = 0;
        slowFilling.fillAmount = 0f;

        mov.movementSpeed = mov.movementSpeed / multiSpeed;

        slowmo.Stop();

        inp.powerup = false;
        inp.shield.enabled = false;
        inp.slow.enabled = false;
        inp.shieldImageactive = false;
        inp.slowImageactive = false;
        inp.downOnImage = false;
        inp.down.enabled = false;
        inp.noSwitch = false;
        inp.powerupActive = false;

        activePowerup = null;

        Time.timeScale = 1;
        Time.fixedDeltaTime = .02f;
    }

    public void EndofDown()
    {
        effectOnD = false;
        aud.Stop("shrinkDown");
        aud.Play("shrinkUp");

        player.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        inp.shrinkingPS.Stop();

        downFilling.fillAmount = 0f;

        inp.powerup = false;
        inp.shield.enabled = false;
        inp.slow.enabled = false;
        inp.shieldImageactive = false;
        inp.slowImageactive = false;
        inp.down.enabled = false;
        inp.downOnImage = false;
        inp.noSwitch = false;
        inp.powerupActive = false;

        activePowerup = null;
    }

    #endregion

    #region PowerupController
    public void Switch()
    {
        //Austausch Abfrage
        if(secondPowerup == slowSmall)
        {
            slowSmall.enabled = false;

            if(activePowerup == inp.slow)
            {
                slowSmall.enabled = true;
                secondPowerup = slowSmall;
            }
            else if(activePowerup == inp.shield)
            {
                shieldSmall.enabled = true;
                secondPowerup = shieldSmall;
            }
            else if(activePowerup == inp.down)
            {
                downSmall.enabled = true;
                secondPowerup = downSmall;
            }

            activePowerup = inp.slow;

        }

        else if (secondPowerup == shieldSmall)
        {
            shieldSmall.enabled = false;

            if (activePowerup == inp.slow)
            {
                slowSmall.enabled = true;
                secondPowerup = slowSmall;
            }
            else if (activePowerup == inp.shield)
            {
                shieldSmall.enabled = true;
                secondPowerup = shieldSmall;
            }
            else if (activePowerup == inp.down)
            {
                downSmall.enabled = true;
                secondPowerup = downSmall;
            }

            activePowerup = inp.shield;

        }

        else if (secondPowerup == downSmall)
        {
            downSmall.enabled = false;

            if (activePowerup == inp.slow)
            {
                slowSmall.enabled = true;
                secondPowerup = slowSmall;
            }
            else if (activePowerup == inp.shield)
            {
                shieldSmall.enabled = true;
                secondPowerup = shieldSmall;
            }
            else if (activePowerup == inp.down)
            {
                downSmall.enabled = true;
                secondPowerup = downSmall;
            }

            activePowerup = inp.down;

        }

        //Probleme mit Shrinking
    }

    void CheckActives()
    {
        if(activePowerup == inp.slow)
        {
            Slow();
        }

        else if(activePowerup == inp.shield)
        {
            Shield();
        }

        else if(activePowerup == inp.down)
        {
            Downsizing();
        }

        else if(activePowerup == null)
        {
            if (secondPowerup != null)
            {
                emptyBig = false;
                if (downSmall.enabled)
                {
                    activePowerup = inp.down;
                    secondPowerup = null;
                }

                else if (slowSmall.enabled)
                {
                    activePowerup = inp.slow;
                    secondPowerup = null;
                }

                else if (shieldSmall.enabled)
                {
                    activePowerup = inp.shield;
                    secondPowerup = null;
                }
            }
            else if(secondPowerup == null)
            {
                emptyBig = true;
                inp.timeTrue = false;
                inp.shieldOn = false;
                inp.downOn = false;

                inp.shieldImageactive = false;
                inp.downOnImage = false;
                inp.slowImageactive = false;
            }

        }

        if(firstPowerup == null)
        {
            emptyBig = true;
        }

        if(secondPowerup == null)
        {
            emptySmall = true;
            downSmall.enabled = false;
            slowSmall.enabled = false;
            shieldSmall.enabled = false;
        }

        if(activePowerup != null && !inp.powerupActive)
        {
            inp.powerup = true;
        }
    }

    void Slow()
    {
        inp.timeTrue = true;
        inp.shieldOn = false;
        inp.downOn = false;

        inp.shieldImageactive = false;
        inp.downOnImage = false;
        inp.slowImageactive = true;
    }

    void Shield()
    {
        inp.timeTrue = false;
        inp.shieldOn = true;
        inp.downOn = false;

        inp.shieldImageactive = true;
        inp.slowImageactive = false;
        inp.downOnImage = false;
    }

    void Downsizing()
    {
        inp.timeTrue = false;
        inp.shieldOn = false;
        inp.downOn = true;

        inp.shieldImageactive = false;
        inp.downOnImage = true;
        inp.slowImageactive = false;
    }
    #endregion

    #region FillImage
    public void FillBig()
    {
        if (down)
        {
            down = false;
            activePowerup = inp.down;
            firstPowerup = inp.down;
        }

        else if (slow)
        {
            slow = false;
            activePowerup = inp.slow;
            firstPowerup = inp.slow;
        }

        else if (shield)
        {
            shield = false;
            activePowerup = inp.shield;
            firstPowerup = inp.shield;
        }

        emptyBig = false;
    }
    public void FillSmall()
    {
        if (down)
        {
            down = false;
            secondPowerup = downSmall;
            downSmall.enabled = true;
        }

        else if (slow)
        {
            slow = false;
            secondPowerup = slowSmall;
            slowSmall.enabled = inp.slow;
        }

        else if (shield)
        {
            shield = false;
            secondPowerup = shieldSmall;
            shieldSmall.enabled = inp.shield;
        }

        emptySmall = false;
    }
    public void SwitchSmall()
    {
        downSmall.enabled = false;
        slowSmall.enabled = false;
        shieldSmall.enabled = false;

        if (down)
        {
            down = false;
            secondPowerup = downSmall;
            downSmall.enabled = true;
        }

        else if (slow)
        {
            slow = false;
            secondPowerup = slowSmall;
            slowSmall.enabled = inp.slow;
        }

        else if (shield)
        {
            shield = false;
            secondPowerup = shieldSmall;
            shieldSmall.enabled = inp.shield;
        }

        emptySmall = false;
    }
    #endregion
}
