using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashLogo : MonoBehaviour {

    public AudioClip splashClip;


	void Start () {
		Invoke ("NextScreen", 1.5f);
	}

	void NextScreen() {
        //SoundSystem._instance.PlaySFX(splashClip);
        LevelManager.instance.LoadLevel ("Main Menu");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
          NextScreen ();
		}

		if (Input.GetMouseButtonDown (0)) {
            NextScreen ();
		}
	}
}
