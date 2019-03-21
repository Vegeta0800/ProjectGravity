using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMechanic : MonoBehaviour {

    #region Declarations
    public CharacterController player;

    public bool superHeroLandingActiveDown;
    public bool superHeroLandingActiveUp;
    public bool superHeroLandingOff;

    public float gravity; //Anpassbarer Wert wie schnell man fallen oder hochsteigen soll.

    public bool gravityActive; //Bool ob Gravitation aktiviert oder nicht ist.

    public float velocity;

    public float expoVelocity;

    public bool velo;
    public bool veloSlowmotion = false;
    #endregion

    //Jede Sekunde
    void FixedUpdate()
    {

        Vector3 Gravity = new Vector3(0, 1, 0); //neuer Vektor mit dem Wert 1 aus Y.

        if (velo && !veloSlowmotion)  
        {
            velocity += expoVelocity;
        }
        else if (!velo || !velo && veloSlowmotion)
        {
            velocity = 1f;
        }
        if (!gravityActive) //Wenn Gravity an ist.
        {
            player.Move(Gravity * (-gravity * velocity)* Time.deltaTime); //wird der Spieler nach unten gezogen.

        }

        else
        {
            player.Move(Gravity * (gravity * velocity) * Time.deltaTime); //wird der Spieler nach oben gezogen.

        }
    }
}
