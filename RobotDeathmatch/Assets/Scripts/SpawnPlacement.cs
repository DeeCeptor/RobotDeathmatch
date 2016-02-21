using UnityEngine;
using System.Collections;

public class SpawnPlacement : MonoBehaviour 
{
	public Transform[] spawn_points = new Transform[4];


	void Start () 
	{
		if (PlayerInformation.player_info != null)
		{
			SpawnFromPlayerInformation();
		}
		else
		{
			DebugSpawnPositions();
		}
	}


	public void SpawnFromPlayerInformation()
	{
		Debug.Log("Loaded player information for spawning");

		for (int x = 0; x < PlayerInformation.player_info.players.Count; x++)
		{
			Player plyr = PlayerInformation.player_info.players[x];
			string spawn_string = "Human";
			if (plyr.robot)
				spawn_string = "RobotPlayer";

			// Spawn the player
			GameObject obj = (GameObject) Instantiate(Resources.Load(spawn_string) as GameObject, 
				spawn_points[x].position, 
				Quaternion.identity);
			Debug.Log(plyr.player_string + " " + plyr.robot);
			// Set some information
			PlayerInput input = obj.GetComponent<PlayerInput>();
			if (input == null)
				input = obj.GetComponentInChildren<PlayerInput>();
			input.player_name = plyr.player_string;
			input.player_color = plyr.player_colour;
			input.team_number = (plyr.robot ? 2 : 1);
			input.Colourize();
		}
	}


	public void DebugSpawnPositions()
	{
		Debug.Log("Debug spawn positions");

		// Spawn a human
		GameObject obj = (GameObject) Instantiate(Resources.Load("Human") as GameObject, 
			spawn_points[0].position, 
			Quaternion.identity);
		PlayerInput input = obj.GetComponentInChildren<PlayerInput>();
		input.player_name = "P1";
		input.player_color = Color.white;
		input.team_number = 1;
		input.Colourize();

		// Spawn a robit
		obj = (GameObject) Instantiate(Resources.Load("RobotPlayer") as GameObject, 
			spawn_points[1].position, 
			Quaternion.identity);
		input = obj.GetComponent<PlayerInput>();
		input.player_name = "P2";
		input.player_color = Color.red;
		input.team_number = 2;
		input.Colourize();

		// Spawn a robit
		obj = (GameObject) Instantiate(Resources.Load("RobotPlayer") as GameObject, 
			spawn_points[2].position, 
			Quaternion.identity);
		input = obj.GetComponent<PlayerInput>();
		input.player_name = "P3";
		input.player_color = Color.green;
		input.team_number = 2;
		input.Colourize();

		// Spawn a robit
		obj = (GameObject) Instantiate(Resources.Load("RobotPlayer") as GameObject, 
			spawn_points[3].position, 
			Quaternion.identity);
		input = obj.GetComponent<PlayerInput>();
		input.player_name = "P4";
		input.player_color = Color.blue;
		input.team_number = 2;
		input.Colourize();
	}
}
