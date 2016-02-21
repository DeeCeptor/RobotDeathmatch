using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class StartScreen : MonoBehaviour {
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
		if (Time.time > moveTime + delay) {
			moveTime = Time.time;
			float horizontal_movement = Input.GetAxis ("P1 LeftStick X");
			if (horizontal_movement > 0.5) {
				moveRight ();
			} else if (horizontal_movement < -0.5) {
				moveLeft ();
			}
		}

		if (Input.GetButtonDown ("P1 A")) {
			loadLevel ();
		}
		selectLevel (current);
	}

	public void moveLeft(){
		switch (current) {
		case 0:
			item0.Deselect ();
			current = 1;
			break;
		case 1:
			item1.Deselect ();
			current--;
			break;
		}
	}

	public void moveRight(){
		switch (current) {
		case 0:
			item0.Deselect ();
			current++;
			break;
		case 1:
			item1.Deselect ();
			current=0;
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
			SceneManager.LoadScene("Join");
			break;
		case 1:
			Application.Quit ();
			break;

		}
	}
}
