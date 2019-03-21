using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

    #region Declarations
    public Transform player; //Spieler wird importiert

    [SerializeField]
    private Vector3 offset; //Im Inspector veränderbarer Vector wird importiert.

    [SerializeField]
    private float smoothSpeed; //Abrundungsgeschwindigkeit von der Camera.
#endregion

    //Update each Second
    void FixedUpdate () {

         //Position der Kamera wird auf die Spieler Position plus den offset gesetzt.
        Vector3 actualPosition = player.position + offset;

        //Die Abgerundete Position ist die Kamera Position + die neue Position + Abrundungsgeschwindigkeit
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, actualPosition, smoothSpeed);

        //Kamera Position ist jetzt die Abgerundete Position
        transform.position = smoothedPosition;

	}
}
