using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour {

	public void ChangeLevel(string name) {
		LevelManager.instance.WaitForFadeAndLoadLevel ();
	}
}
