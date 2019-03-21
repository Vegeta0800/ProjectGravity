using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour {

    Inputs inp;
    GravityMechanic grv;
    Animations anim;
    AudioManager aud;

    int i = 0;
    private void Start()
    {
        inp = FindObjectOfType<Inputs>();
        grv = FindObjectOfType<GravityMechanic>();
        anim = FindObjectOfType<Animations>();
        aud = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            #region GravityEnter

            if (this.CompareTag("Gravity"))
            {
                inp.onWall = true;
                grv.velo = false;

                if (grv.velocity >= 2.8f)
                {
                    if (grv.gravityActive)
                    {
                        grv.superHeroLandingActiveUp = true;
                        grv.superHeroLandingActiveDown = false;
                    }

                    else if (!grv.gravityActive)
                    {
                        grv.superHeroLandingActiveUp = false;
                        grv.superHeroLandingActiveDown = true;
                    }
                }
            }

            #endregion

            #region SliderEnter

            if (this.CompareTag("Slider"))
            {
                other.transform.parent = transform;
                inp.onWall = true;
                grv.velo = false;
            }

            #endregion

            #region Obstacle

            if (this.CompareTag("Obstacle"))
            {
                grv.velo = false;

                if (!inp.shieldTrue || i == inp.shieldActivations)
                {
                    aud.Play("Laserhit");
                    other.GetComponent<CharacterController>().enabled = false;
                    anim.death = true;
                }

                else if (inp.shieldTrue)
                {
                    StartCoroutine(Shield());

                    inp.shieldHit = true;

                    i += 1;

                    if (i == inp.shieldActivations)
                    {
                        i = 0;
                        inp.shieldDestroyed = true;
                        inp.shieldTrue = false;
                    }
                }
            }

            else if (this.CompareTag("Water"))
            {
                aud.Play("waterHit");
                grv.velo = false;
                inp.WaterCollisonPS.Play();

                if (inp.shieldTrue)
                {
                    i = 0;
                    anim.death = true;
                }
                else
                {
                    anim.death = true;
                }

            }
            #endregion
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            #region GravityStay

            if (this.CompareTag("Gravity") || this.CompareTag("Slider"))
            {
                inp.onWall = true;
                grv.velo = false;
            }

            #endregion
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            #region GravityExit

            if (this.CompareTag("Gravity") || this.CompareTag("Slider"))
            {
                inp.onWall = false;
                grv.velo = true;
            }

            #endregion

            #region SliderExit

            if (this.CompareTag("Slider"))
            {
                other.transform.SetParent(null);
            }

            #endregion
        }
    }

    IEnumerator Shield()
    {
        BoxCollider[] box = GetComponents<BoxCollider>();

        box[0].enabled = false;
        box[1].enabled = false;

        yield return new WaitForSeconds(0.6f);

        box[0].enabled = true;
        box[1].enabled = true;
    }

}
