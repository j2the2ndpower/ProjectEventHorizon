using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
	public static LevelManager instance;

	public static string sceneToLoad;
	public static bool newChampPending = false;

	void Start() {
    if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void WaitForFadeAndLoadLevel() {
		SceneManager.LoadScene (LevelManager.sceneToLoad);
	}

	public void LoadLevel(string name){
		LevelManager.sceneToLoad = name;
		Fader fader = GameObject.FindObjectOfType<Fader> ();

		if (fader) {
			Animator ani = fader.GetComponent<Animator> ();
			ani.SetBool ("FadeOut", true);
		} else {
      Debug.Log("No Fader found on " + gameObject);
			LevelManager.instance.WaitForFadeAndLoadLevel ();
		}
	}

	public void QuitRequest(){
		Application.Quit ();
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		Fader fader = GameObject.FindObjectOfType<Fader> ();
		if (fader) {
			Animator ani = fader.GetComponent<Animator> ();
			ani.SetBool ("FadeOut", false);
		}
	}
}
