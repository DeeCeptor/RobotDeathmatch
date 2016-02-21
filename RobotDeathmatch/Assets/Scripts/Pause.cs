using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

	bool ispaused = false;
	public Canvas pausedscreen; 

	public GameObject slot0;
	public GameObject slot1;

	StartItem item0;
	StartItem item1;


	int current = 0;
	public float delay;
	float moveTime;

	// Use this for initialization
	void Start () {
		moveTime = 0;
		item0 = slot0.GetComponent<StartItem> ();
		item1 = slot1.GetComponent<StartItem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause")) {
			TogglePause();
		}

		if (ispaused) {
			float vertical_movement = Input.GetAxis ("P1 LeftStick Y");
			if (vertical_movement > 0.5) {
				moveDown ();
			} else if (vertical_movement < -0.5) {
				moveUp ();
			}

			if (Input.GetButtonDown ("P1 A")) {
				loadLevel ();
			}
				
			selectLevel (current);
		}
	}
		

	public void moveUp(){
		switch (current) {
		case 0:
			break;
		case 1:
			item1.Deselect ();
			current--;
			break;
		}
	}

	public void moveDown(){
		switch (current) {
		case 0:
			item0.Deselect ();
			current++;
			break;
		case 1:
			break;
		}
	}

	public void selectLevel(int levelnum)
	{
		switch (current) {
		case 0:
			item0.Select ();
			break;
		case 1:
			item1.Select ();
			break;
		}
	}

	public void loadLevel()
	{
		switch (current) 
		{
		case 0:
			TogglePause ();
			//SceneManager.LoadScene("Catacombs");
			break;
		case 1:
			Time.timeScale = 1f;
			SceneManager.LoadScene("Join");
			break;
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

