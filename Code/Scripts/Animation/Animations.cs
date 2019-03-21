using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour {
    public GravityMechanic grv;
    public Movement mov;
    public Inputs inp;
    public AudioManager aud;
    public Starting str;

    public Animator ani;
    public bool death;
    public bool spawn;

    [Header("Screen Shake")]
    [SerializeField] private float power;
    [SerializeField] private float duration;
    [SerializeField] private Transform cam;


    Vector3 startPosition;
    bool shakeItOff;
    float initialDuration;


    private void Start()
    {
        Time.timeScale = 1f;

        initialDuration = duration;
        aud.Play("spawn");
    }

    void Update()
    {
        if (death)
        {
            death = false;
            ani.Rebind();
            StartCoroutine(Death());
        }
        else if (spawn)
        {
            spawn = false;
            ani.Rebind();
            StartCoroutine(Spawning());
        }
        else if (inp.gravityAniTop)
        {
            inp.gravityAniTop = false;
            ani.Rebind();
            ani.SetBool("Gravity", true);

        }
        else if (inp.gravityAniBot)
        {
            inp.gravityAniBot = false;
            ani.Rebind();
            ani.SetBool("Gravity", false);
            StartCoroutine(Gravity());
        }
        else if (grv.superHeroLandingActiveDown)
        {
            ani.Rebind();
            grv.superHeroLandingActiveDown = false;

            if (!inp.superheroOnGB.activeInHierarchy)
            {
                inp.superheroOnGB.SetActive(true);
            }

            StartCoroutine(SuperheroLandingDown());
        }
        else if (grv.superHeroLandingActiveUp)
        {
            ani.Rebind();
            grv.superHeroLandingActiveUp = false;

            if (!inp.superheroUpOnGB.activeInHierarchy)
            {
                inp.superheroUpOnGB.SetActive(true);
            }

            StartCoroutine(SuperheroLandingUp());
        }
        else
        {
            ani.SetFloat("Speed", inp.speed);
        }


        #region ScreenShake

        if (shakeItOff)
        {
            if(duration > 0f)
            {
                cam.localPosition += Random.insideUnitSphere * power;
                duration -= Time.deltaTime;
            }
            else
            {
                shakeItOff = false;
                duration = initialDuration;
            }
        }

        #endregion

    }

    IEnumerator Death()
    {
        inp.WaterCollisonPS.Stop();

        if (inp.shieldTrue)
        {
            aud.Stop("Shield");
            aud.Play("ShieldHit");

            inp.noSwitch = false;
            inp.powerupActive = false;

            inp.pow.activePowerup = null;
            inp.shieldDestroyed = false;
            inp.shieldTrue = false;
            inp.shieldImageactive = false;
            inp.slowImageactive = false;
            inp.downOn = false;
            inp.downOnImage = false;

            inp.shield.enabled = false;
            inp.slow.enabled = false;
            inp.down.enabled = false;

            inp.powerupFilling.fillAmount = 0f;


            inp.shieldOnPS.Stop();
        }

        this.transform.parent = null;

        ani.Play("Lose");

        mov.enabled = false;

        inp.enabled = false;

        yield return new WaitForSeconds(0.3f);

        inp.enabled = true;
        spawn = true;
        //check if there is saved data from a checkpointS

    }

    IEnumerator Spawning()
    {

        str.StartofGame();
        //Deaktivere die beiden Scripts

        mov.enabled = false;

        inp.deathOn = true;
        grv.enabled = false;
        ani.SetBool("Spawning", true); //Aktiviere die Spawn Animation
        yield return new WaitForSeconds(1f); //Warte 1 Sekunde

        ani.SetBool("Spawning", false); //Deaktivere die Animation

        yield return new WaitForSeconds(1f); //Warte 1 Sekunde

        //Aktivere die beiden Scripts
        inp.deathOn = false;
        mov.enabled = true;
        grv.enabled = true;
        grv.gravityActive = false;
    }

    IEnumerator Gravity()
    {
        ani.SetBool("GravityOff", true);
        yield return new WaitForSeconds(0.2f);
        ani.SetBool("GravityOff", false);
    }

    IEnumerator SuperheroLandingDown()
    {
        aud.Play("Landing");
        ani.SetBool("SuperheroDown", true);
        inp.superheroLandingPS.Play();
        mov.enabled = false;
        grv.enabled = false;
        inp.enabled = false;
        shakeItOff = true;
        yield return new WaitForSeconds(0.5f);
        inp.superheroLandingPS.Stop();
        ani.SetBool("Gravity", false);
        ani.SetBool("SuperheroDown", false);
        yield return new WaitForSeconds(0.5f);
        mov.enabled = true;
        grv.enabled = true;
        inp.enabled = true;
    }

    IEnumerator SuperheroLandingUp()
    {
        aud.Play("Landing");
        mov.enabled = false;
        grv.enabled = false;
        inp.enabled = false;
        shakeItOff = true;
        ani.SetBool("SuperheroUp", true);
        inp.superheroLandingUpPS.Play();
        yield return new WaitForSeconds(0.5f);
        inp.superheroLandingUpPS.Stop();
        ani.SetBool("Gravity", true);
        ani.SetBool("SuperheroUp", false);
        yield return new WaitForSeconds(0.5f);
        mov.enabled = true;
        grv.enabled = true;
        inp.enabled = true;
    }
}
