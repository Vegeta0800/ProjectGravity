using UnityEngine;

public class CollisionCheckpoint : MonoBehaviour {

    public Transform checkpoint;
    public ParticleSystem check;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            check.Play();
            PlayerPrefs.SetFloat("playerX", checkpoint.position.x);
            PlayerPrefs.SetFloat("playerY", checkpoint.position.y);
            PlayerPrefs.SetFloat("playerZ", checkpoint.position.z);
        }
    }
}
