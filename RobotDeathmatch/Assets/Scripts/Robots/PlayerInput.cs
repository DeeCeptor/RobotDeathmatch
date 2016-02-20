﻿using UnityEngine;
using System.Collections;

// Grabs whatever input method we are using for this player
public class PlayerInput : MonoBehaviour 
{
	public string player_name;	// Ex: P1
	public int team_number;		// 1-4 teams, can only hurt players on other teams
	public bool controller = true;	// Using a controller?
	public Color player_color;

	public float max_health = 1000;
	public float cur_health;

	public float horizontal_movement;	// Updated during update
	public float prev_horizontal_movement;
	public float vertical_movement;
	public float prev_vertical_movement;
	public Vector2 aiming_direction;
	public Vector2 prev_aiming_direction;

	public bool left_trigger_held_down = false;		// Currently held down
	public bool left_trigger_pressed = false;		// Trigger was pressed in this frame
	public bool right_trigger_held_down = false;		// Currently held down
	public bool right_trigger_pressed = false;		// Trigger was pressed in this frame
	public bool left_bumper_held_down = false;		// Currently held down
	public bool left_bumper_pressed = false;		// Trigger was pressed in this frame
	public bool right_bumper_held_down = false;		// Currently held down
	public bool right_bumper_pressed = false;		// Trigger was pressed in this frame


	public void UpdateInputs () 
	{
		// Grab inputs from our controller
		prev_horizontal_movement = horizontal_movement;
		horizontal_movement = Input.GetAxis(player_name + " LeftStick X");

		prev_vertical_movement = vertical_movement;
		vertical_movement = Input.GetAxis(player_name + " LeftStick Y");

		// Is the player aiming anywhere?
		if (controller)
		{
			prev_aiming_direction = aiming_direction;
			aiming_direction = new Vector2(Input.GetAxis(player_name + " RightStick X"),
										Input.GetAxis(player_name + " RightStick Y"));
		}
		else
		{
			// Keyboard controls looks towards the mouse
			prev_aiming_direction = aiming_direction;

			// Only record mouse looks when the mouse clicks
			if (Input.GetMouseButtonDown(0))
			{
				Vector2 mouse_pos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
				aiming_direction = ((Vector2) (mouse_pos - (Vector2) this.transform.position)).normalized;
			}
			else
			{
				aiming_direction = Vector2.zero;
			}
		}
	}



	public virtual void TakeHit(float damage)
	{
		cur_health -= damage;

		if (cur_health <= 0)
			Die();
	}
	public virtual void Die()
	{

	}
}
