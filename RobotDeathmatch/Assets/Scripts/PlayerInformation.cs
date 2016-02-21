using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInformation : MonoBehaviour 
{
	public static PlayerInformation player_info;
	public List<Player> players = new List<Player>();

	void Awake ()
	{
		DontDestroyOnLoad(this.gameObject);
		player_info = this;
	}
}

public class Player
{
	public int player_number;
	public string player_string;
	public Color player_colour;
	public bool robot = false;

	public Player(int plyr_numbr, Color plyr_colour, bool is_robot)
	{
		player_number = plyr_numbr;
		player_string = "P" + plyr_numbr;
		player_colour = plyr_colour;
		robot = is_robot;
	}
}