using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
	public float fadeInTime;
	public float startAlpha = 1;
	public float stopAlpha = 0;

	private Image fadePanel;
	private Color currentColor;
	private float StartTime;

	// Use this for initialization
	void Start () {
		fadePanel = GetComponent<Image> ();
		currentColor = fadePanel.color;
		currentColor.a = startAlpha;
		fadePanel.color = currentColor;
		StartTime = Time.time;
	}

	void Update () {
		float timeSinceStart = Time.time - StartTime;
		if (timeSinceStart < fadeInTime) {
			float fadeDelta = Time.deltaTime / fadeInTime * (stopAlpha - startAlpha);
			currentColor.a += fadeDelta;
			fadePanel.color = currentColor;
		} else {
			//Give some time before blowing up
			gameObject.SetActive (false);
			Invoke ("BlowUp", 0.5f);
		}
	}

	void BlowUp() {
		Destroy (gameObject);
	}
}
