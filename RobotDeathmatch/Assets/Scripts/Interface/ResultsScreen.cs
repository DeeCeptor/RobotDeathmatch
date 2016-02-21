using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ResultsScreen : MonoBehaviour {

	public Text p1kills;
	public Text p2kills;
	public Text p3kills;
	public Text p4kills;

	public Text p1deaths;
	public Text p2deaths;
	public Text p3deaths;
	public Text p4deaths;
	// Use this for initialization
	void Start () 
	{
		//getData
		if (AllScores.all_scores.scores.ContainsKey("P1"))
		{
			setPlayerKills("P1",AllScores.all_scores.scores["P1"].kills);
			setPlayerDeaths("P1",AllScores.all_scores.scores["P1"].deaths);
		}
		if (AllScores.all_scores.scores.ContainsKey("P2"))
		{
			setPlayerKills("P2",AllScores.all_scores.scores["P2"].kills);
			setPlayerDeaths("P2",AllScores.all_scores.scores["P2"].deaths);
		}
		if (AllScores.all_scores.scores.ContainsKey("P3"))
		{
			setPlayerKills("P3",AllScores.all_scores.scores["P3"].kills);
			setPlayerDeaths("P3",AllScores.all_scores.scores["P3"].deaths);
		}
		if (AllScores.all_scores.scores.ContainsKey("P4"))
		{
			setPlayerKills("P4",AllScores.all_scores.scores["P4"].kills);
			setPlayerDeaths("P4",AllScores.all_scores.scores["P4"].deaths);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Submit")) 
		{
			Debug.Log ("Submitted");

			if (AllScores.all_scores != null)
				Destroy(AllScores.all_scores.gameObject);
			
			SceneManager.LoadScene("LevelSelect");
		}
	}

	public void setPlayerKills(string player, int number)
	{
		switch (player) {
		case "P1":
			p1kills.text = ""+number;
			break;
		case "P2":
			p2kills.text = ""+number;
			break;
		case "P3":
			p3kills.text = ""+number;
			break;
		case "P4":
			p4kills.text = ""+number;
			break;
		}
	}

	public void setPlayerDeaths(string player, int number)
	{
		switch (player) {
		case "P1":
			p1deaths.text = ""+number;
			break;
		case "P2":
			p2deaths.text = ""+number;
			break;
		case "P3":
			p3deaths.text = ""+number;
			break;
		case "P4":
			p4deaths.text = ""+number;
			break;
		}
	}
}
