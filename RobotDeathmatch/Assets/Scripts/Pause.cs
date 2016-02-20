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
		if (Input.GetButtonDown ("Pause")) {
			TogglePause();
		}
	}

	public void TogglePause()
	{
		if (ispaused == false){
			Time.timeScale = 0f;
			ispaused = true;
			pausedscreen.enabled = true;
		} else if (ispaused == true) {
			Time.timeScale = 1f;
			ispaused = false;
			pausedscreen.enabled=false;
		}
	}
	public void Quit(){
		Application.Quit ();
	}

	}

