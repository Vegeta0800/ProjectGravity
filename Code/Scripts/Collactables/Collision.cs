using UnityEngine;

public class Collision : MonoBehaviour {

    PowerupControllers pow;
    AudioManager aud;

    private void Start()
    {
        pow = FindObjectOfType<PowerupControllers>();
        aud = FindObjectOfType<AudioManager>();
    }

    int powerupNumber;
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            aud.Play("PowerupPick");

            Destroy(gameObject);

            if (this.CompareTag("Powerup"))
            {
                powerupNumber = Random.Range(1, 4);

                if (powerupNumber == 1)
                {
                    if (pow.emptyBig)
                    {
                        pow.shield = true;
                        pow.FillBig();
                    }
                    else if (pow.emptySmall && !pow.emptyBig)
                    {
                        pow.shield = true;
                        pow.FillSmall();
                    }
                    else if (!pow.emptyBig && !pow.emptySmall)
                    {
                        pow.shield = true;
                        pow.SwitchSmall();
                    }
                }

                else if (powerupNumber == 2)
                {
                    if (pow.emptyBig)
                    {
                        pow.slow = true;
                        pow.FillBig();
                    }
                    else if (pow.emptySmall && !pow.emptyBig)
                    {
                        pow.slow = true;
                        pow.FillSmall();
                    }
                    else if (!pow.emptyBig && !pow.emptySmall)
                    {
                        pow.slow = true;
                        pow.SwitchSmall();
                    }
                }
                else if (powerupNumber == 3)
                {
                    if (pow.emptyBig)
                    {
                        pow.down = true;
                        pow.FillBig();
                    }

                    else if (pow.emptySmall && !pow.emptyBig)
                    {
                        pow.down = true;
                        pow.FillSmall();
                    }

                    else if (!pow.emptyBig && !pow.emptySmall)
                    {
                        pow.down = true;
                        pow.SwitchSmall();
                    }
                }
            }
            else
            {

                if (this.CompareTag("Shield"))
                {
                    if (pow.emptyBig)
                    {
                        pow.shield = true;
                        pow.FillBig();
                    }

                    else if (pow.emptySmall && !pow.emptyBig)
                    {
                        pow.shield = true;
                        pow.FillSmall();
                    }

                    else if (!pow.emptyBig && !pow.emptySmall)
                    {
                        pow.shield = true;
                        pow.SwitchSmall();
                    }
                }

                if (this.CompareTag("Slow"))
                {
                    if (pow.emptyBig)
                    {
                        pow.slow = true;
                        pow.FillBig();

                    }

                    else if (pow.emptySmall && !pow.emptyBig)
                    {
                        pow.slow = true;
                        pow.FillSmall();
                    }

                    else if (!pow.emptyBig && !pow.emptySmall)
                    {
                        pow.slow = true;
                        pow.SwitchSmall();
                    }
                }

                if (this.CompareTag("Down"))
                {
                    if (pow.emptyBig)
                    {
                        pow.down = true;
                        pow.FillBig();
                    }

                    else if (pow.emptySmall && !pow.emptyBig)
                    {
                        pow.down = true;
                        pow.FillSmall();
                    }

                    else if (!pow.emptyBig && !pow.emptySmall)
                    {
                        pow.down = true;
                        pow.SwitchSmall();
                    }
                }
            }




        }
    }
}
