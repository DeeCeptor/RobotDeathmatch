using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class joinScreen : MonoBehaviour {

	bool p1;
	bool p2;
	bool p3;
	bool p4;

	public GameObject p1Check;
	public GameObject p2Check;
	public GameObject p3Check;
	public GameObject p4Check;

	public Text startText;
	// Use this for initialization
	void Start () {
		startText.enabled = false;
		p1Check.SetActive (false);
		p2Check.SetActive (false);
		p3Check.SetActive (false);
		p4Check.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("P1 A")) {
			p1 = true;
			p1Check.SetActive (true);
			PlayerInformation.player_info.players.Add(new Player(1, Color.white, false));
			Debug.Log("P1");
		}

		if (Input.GetButtonDown ("P2 A")) {
			p2 = true;
			p2Check.SetActive (true);
			PlayerInformation.player_info.players.Add(new Player(2, Color.red, true));
			Debug.Log("P2");
		}

		if (Input.GetButtonDown ("P3 A")) {
			p3 = true;
			p3Check.SetActive (true);
			PlayerInformation.player_info.players.Add(new Player(3, Color.green, true));
		}

		if (Input.GetButtonDown ("P4 A")) {
			p4 = true;
			p4Check.SetActive (true);
			PlayerInformation.player_info.players.Add(new Player(4, Color.blue, true));
		}

		if (p1 && (p2 || p3 || p4)) {
			startText.enabled = true;
		}

		if (startText.enabled && Input.GetButtonDown("Submit")) 
		{
			Debug.Log ("Submitted");
			SceneManager.LoadScene("LevelSelect");
		}
	}
}
