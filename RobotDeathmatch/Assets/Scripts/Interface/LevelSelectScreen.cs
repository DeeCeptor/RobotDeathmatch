using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelSelectScreen : MonoBehaviour {
	public GameObject slot0;
	public GameObject slot1;
	public GameObject slot2;
	public GameObject slot3;
	public GameObject slot4;
	public GameObject slot5;

	levelItem item0;
	levelItem item1;
	levelItem item2;
	levelItem item3;
	levelItem item4;
	levelItem item5;

	int current = 0;
	public float delay;
	float moveTime;
	// Use this for initialization
	void Start () {
		moveTime = 0;
		item0 = slot0.GetComponent<levelItem> ();
		item1 = slot1.GetComponent<levelItem> ();
		item2 = slot2.GetComponent<levelItem> ();
		item3 = slot3.GetComponent<levelItem> ();
		item4 = slot4.GetComponent<levelItem> ();
		item5 = slot5.GetComponent<levelItem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > moveTime + delay) {
			moveTime = Time.time;
			float vertical_movement = Input.GetAxis ("P1 LeftStick Y");
			if (vertical_movement > 0.5) {
				moveDown ();
			} else if (vertical_movement < -0.5) {
				moveUp ();
			}
		}

		if (Input.GetButtonDown ("P1 A")) {
			loadLevel ();
		}

		if (Input.GetButtonDown ("Cancel")) {
			SceneManager.LoadScene ("Join");
		}
		selectLevel (current);
	}

	public void moveUp(){
		switch (current) {
		case 0:
			item0.Deselect ();
			current = 5;
			break;
		case 1:
			item1.Deselect ();
			current--;
			break;
		case 2:
			item2.Deselect ();
			current--;
			break;
		case 3:
			item3.Deselect ();
			current--;
			break;
		case 4:
			item4.Deselect ();
			current--;
			break;
		case 5:
			item5.Deselect ();
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
			item1.Deselect ();
			current++;
			break;
		case 2:
			item2.Deselect ();
			current++;
			break;
		case 3:
			item3.Deselect ();
			current++;
			break;
		case 4:
			item4.Deselect ();
			current++;
			break;
		case 5:
			item5.Deselect ();
			current = 0;
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
		case 2:
			item2.Select ();
			break;
		case 3:
			item3.Select ();
			break;
		case 4:
			item4.Select ();
			break;
		case 5:
			item5.Select ();
			break;
		}
	}

	public void loadLevel()
	{
		MenuMusic.stopMusic ();
		switch (current) 
		{
			case 0:
				Debug.Log("loadLevel");
				SceneManager.LoadScene("Catacombs");
				break;
			case 1:
				Debug.Log("loadLevel");
				SceneManager.LoadScene("Yige_level");
				break;
			case 2:
				Debug.Log("loadLevel");
				SceneManager.LoadScene("johns_level");
				break;
			case 3:
				Debug.Log("loadLevel");
				SceneManager.LoadScene("Yige_level_2");
				break;
			case 4:
				Debug.Log("loadLevel");
				SceneManager.LoadScene("Yige_level_4");
				break;
			case 5:
				Debug.Log("loadLevel");
			SceneManager.LoadScene("Columns");
				break;
		}
	}
}
