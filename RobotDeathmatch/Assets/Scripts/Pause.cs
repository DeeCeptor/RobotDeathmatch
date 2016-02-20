using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	Font pausemenufont;
	bool ispaused = false;
	public Canvas pausedscreen; 

	// Use this for initialization
	void Start () {
		GUI.skin.box.font = pausemenufont;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause") && ispaused == false) {
			Time.timeScale = 0f;
			ispaused = true;
			pausedscreen.enabled = true;
		} else if (Input.GetButtonDown ("Pause") && ispaused == true) {
			Time.timeScale = 1f;
			ispaused = false;
			pausedscreen.enabled=false;
		}
	}

	}

