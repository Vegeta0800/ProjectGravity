using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LOGOGUCCIGANG : MonoBehaviour {

	public MovieTexture mov;

	// Use this for initialization
	void Start () {
		GetComponent<RawImage> ().texture = mov;
		mov.Play ();
	}

	void Update(){
		if (!mov.isPlaying) {
			SceneManager.LoadScene ("MainMenu");
		}
	}

}
