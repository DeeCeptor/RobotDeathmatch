using UnityEngine;
using System.Collections;

// Grabs whatever input method we are using for this player
public class PlayerInput : MonoBehaviour 
{
	public string player_name;	// Ex: P1
	public bool controller = true;	// Using a controller?

	public float horizontal_movement;	// Updated during update
	public float prev_horizontal_movement;
	public float vertical_movement;
	public float prev_vertical_movement;
	public Vector2 aiming_direction;

	
	public void UpdateInputs () 
	{
		// Grab inputs from our controller
		prev_horizontal_movement = horizontal_movement;
		horizontal_movement = Input.GetAxis(player_name + " LeftStick X");

		prev_vertical_movement = vertical_movement;
		vertical_movement = Input.GetAxis(player_name + " LeftStick Y");

		// Is the player aiming anywhere?
		aiming_direction = new Vector2(Input.GetAxis(player_name + " RightStick X"),
										Input.GetAxis(player_name + " RightStick Y"));
	}
}
