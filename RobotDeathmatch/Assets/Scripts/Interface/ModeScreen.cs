using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeScreen : MonoBehaviour 
{
	public Button protect_humans;
	void Start () 
	{
		if (PlayerInformation.player_info != null &&
			PlayerInformation.player_info.players.Count < 4)
		{
			Debug.Log("Not enough players for protecting humans");
			protect_humans.interactable = false;
			PlayerInformation.player_info.cur_modes = PlayerInformation.Modes.Kill_Humans;
		}
	}


	public void KillAllHumans()
	{
		Debug.Log("KILL ALL HUMANS");
		PlayerInformation.player_info.cur_modes = PlayerInformation.Modes.Kill_Humans;
		SceneManager.LoadScene("LevelSelect");
	}
	public void ProtectHumans()
	{
		Debug.Log("PROTECT YOUR HUMAN");
		PlayerInformation.player_info.cur_modes = PlayerInformation.Modes.Protect_Human;

		// Set player information

		// Team 1
		// Human
		Player plyr = PlayerInformation.player_info.players[0];
		plyr.robot = false;
		plyr.team_number = 1;
		plyr.team_color = Color.red;
		plyr.team_name = "Red";
		// Robot
		plyr = PlayerInformation.player_info.players[1];
		plyr.robot = true;
		plyr.team_number = 1;
		plyr.team_color = Color.red;
		plyr.team_name = "Red";


		// Team 2
		// Human
		plyr = PlayerInformation.player_info.players[2];
		plyr.robot = false;
		plyr.team_number = 2;
		plyr.team_color = Color.blue;
		plyr.team_name = "Blue";
		// Robot
		plyr = PlayerInformation.player_info.players[3];
		plyr.robot = true;
		plyr.team_number = 2;
		plyr.team_color = Color.blue;
		plyr.team_name = "Blue";


		SceneManager.LoadScene("LevelSelectScreen");
	}


	// Update is called once per frame
	void Update () {
	
	}
}
