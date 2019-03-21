using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    #region Declarations
    public CharacterController player;

    public float movementSpeed;

    [HideInInspector]
    public float rotationY;
    [HideInInspector]
    public float rotationZ;
    public bool frontActive;
    public Vector3 move;
#endregion

    //Jeden Frame
    void FixedUpdate()
    {
        player.transform.eulerAngles = new Vector3(0, rotationY, 0);

        //Spieler in die Richtung und mit dem Speed, die im InputScript festgelegt werden, bewegt.
        player.Move(move * Time.deltaTime * movementSpeed);
    }
}
