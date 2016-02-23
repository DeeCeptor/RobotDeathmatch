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

        if (PlayerInformation.player_info.cur_modes == PlayerInformation.Modes.Kill_Humans)
        {
            // If mode is kill all humans, randomly select which player is the human
            int human_num = Random.Range(0, PlayerInformation.player_info.players.Count);
            Debug.Log("Randomized human player: " + (human_num + 1));
            for (int x = 0; x < PlayerInformation.player_info.players.Count; x++)
            {
                Player plyr = PlayerInformation.player_info.players[x];

                // Check if this is the human player
                if (x == human_num)
                {
                    plyr.robot = false;
                }
                // Nope, is a robit
                else
                {
                    plyr.robot = true;
                }
            }
            // Make the human player in the first slot
            Player hooman = PlayerInformation.player_info.players[human_num];
            PlayerInformation.player_info.players.Remove(hooman);
            PlayerInformation.player_info.players.Insert(0, hooman);
        }


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

			// Set some information
			PlayerInput input = obj.GetComponent<PlayerInput>();
			if (input == null)
				input = obj.GetComponentInChildren<PlayerInput>();
			input.controller = true;
			input.player_name = plyr.player_string;
			input.player_color = plyr.player_colour;

			if (PlayerInformation.player_info.cur_modes == PlayerInformation.Modes.Kill_Humans)
			{
				Debug.Log("Kill humans mode");
				input.team_number = (plyr.robot ? 2 : 1);
			}
			else if (PlayerInformation.player_info.cur_modes == PlayerInformation.Modes.Protect_Human)
			{
				Debug.Log("Protect human mode");
				input.team_number = plyr.team_number;
				input.team_name = plyr.team_name;
				input.team_color = plyr.team_color;
				input.ColourizeTeam();
			}
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
