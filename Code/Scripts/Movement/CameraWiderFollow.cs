using UnityEngine;

public class CameraWiderFollow : MonoBehaviour {

    #region Declarations
    public CameraFollowPlayer Cam;

    public Transform player; //Spieler wird importiert

    public Transform cam;

    [SerializeField]
    private Vector3 offset; //Im Inspector veränderbarer Vector wird importiert.

    [SerializeField]
    private float smoothSpeed; //Abrundungsgeschwindigkeit von der Camera.

    public bool camSwitch = false; 
    #endregion

    //Update each Second
    void FixedUpdate()
    {
        if (camSwitch)
        {
            //Position der Kamera wird auf die Spieler Position plus den offset gesetzt.
            Vector3 actualPosition = player.position + offset;

            //Die Abgerundete Position ist die Kamera Position + die neue Position + Abrundungsgeschwindigkeit
            Vector3 smoothedPosition = Vector3.Lerp(cam.position, actualPosition, smoothSpeed);

            //Kamera Position ist jetzt die Abgerundete Position
            cam.position = smoothedPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camSwitch = true;
            Cam.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camSwitch = false;
            Cam.enabled = true;
        }
    }

}
