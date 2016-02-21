using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

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

		SortPlayerList();
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
		Player plyr = GetPlayer(1);
		plyr.robot = false;
		plyr.team_number = 1;
		plyr.team_color = Color.red;
		plyr.team_name = "Red";
		// Robot
		plyr = GetPlayer(2);
		plyr.robot = true;
		plyr.team_number = 1;
		plyr.team_color = Color.red;
		plyr.team_name = "Red";


		// Team 2
		// Human
		plyr = GetPlayer(3);
		plyr.robot = false;
		plyr.team_number = 2;
		plyr.team_color = Color.blue;
		plyr.team_name = "Blue";
		// Robot
		plyr = GetPlayer(4);
		plyr.robot = true;
		plyr.team_number = 2;
		plyr.team_color = Color.blue;
		plyr.team_name = "Blue";


		SceneManager.LoadScene("LevelSelect");
	}


	public void SortPlayerList()
	{
		List<Player> SortedList = PlayerInformation.player_info.players.OrderBy(o=>o.player_number).ToList();
		PlayerInformation.player_info.players = SortedList;
		Debug.Log("Player lists sorted");
	}

	public Player GetPlayer(int player_number)
	{
		foreach (Player plyr in PlayerInformation.player_info.players)
		{
			if (plyr.player_number == player_number)
				return plyr;
		}

		Debug.Log("Did not find player number " + player_number);
		return null;
	}


	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Cancel")) 
		{
			SceneManager.LoadScene("Join");
		}
	}
}
