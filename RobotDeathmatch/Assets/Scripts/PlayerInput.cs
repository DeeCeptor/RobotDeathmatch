using UnityEngine;
using System.Collections;

// Grabs whatever input method we are using for this player
public class PlayerInput : MonoBehaviour 
{
	public string player_name;	// Ex: P1
	public int team_number;		// 1-4 teams, can only hurt players on other teams
	public bool controller = true;	// Using a controller?
	public Color player_color = Color.white;
	public string team_name;
	public Color team_color;

	public float max_health = 1000;
	public float cur_health;

	public float horizontal_movement;	// Updated during update
	public float prev_horizontal_movement;
	public float vertical_movement;
	public float prev_vertical_movement;
	public Vector2 aiming_direction;
	public Vector2 prev_aiming_direction;
	public Vector2 flicked_aiming_direction;	// Used for robots flicking in the direction to shoot
	public Vector2 prev_flicked_aiming_direction;

	public bool left_trigger_held_down = false;		// Currently held down
	public bool left_trigger_pressed = false;		// Trigger was pressed in this frame
	public bool right_trigger_held_down = false;		// Currently held down
	public bool right_trigger_pressed = false;		// Trigger was pressed in this frame
	public bool left_bumper_held_down = false;		// Currently held down
	public bool left_bumper_pressed = false;		// Trigger was pressed in this frame
	public bool right_bumper_held_down = false;		// Currently held down
	public bool right_bumper_pressed = false;		// Trigger was pressed in this frame

	HealthBar healthbar;
	public void init() 
	{
		healthbar = GetComponent<HealthBar> ();
		if (healthbar) {
			healthbar.maxHealth = max_health;
			healthbar.setHealth (max_health);
		}
	}


	void Start()
	{
		// Register this player with the scores
		if (!AllScores.all_scores.scores.ContainsKey(player_name))
			AllScores.all_scores.scores.Add(player_name, new IndividualScore());
	}


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
			prev_flicked_aiming_direction = flicked_aiming_direction;
			aiming_direction = new Vector2(Input.GetAxis(player_name + " RightStick X"),
										Input.GetAxis(player_name + " RightStick Y"));

			if (prev_aiming_direction == Vector2.zero && aiming_direction != Vector2.zero)
			{
				flicked_aiming_direction = aiming_direction.normalized;
			}
			else
				flicked_aiming_direction = Vector2.zero;
		}
		else
		{
			// Keyboard controls looks towards the mouse
			prev_aiming_direction = aiming_direction;
			prev_flicked_aiming_direction = flicked_aiming_direction;

			Vector2 mouse_pos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
			aiming_direction = ((Vector2) (mouse_pos - (Vector2) this.transform.position)).normalized;

			// Only record mouse looks when the mouse clicks
			if (Input.GetMouseButtonDown(0))
			{
				flicked_aiming_direction = aiming_direction;
			}
			else
			{
				flicked_aiming_direction = Vector2.zero;
			}
		}
	}



	public virtual void TakeHit(float damage, Vector3 collision_position, string attacker_name)
	{
		cur_health = Mathf.Clamp(cur_health - damage, 0, max_health);

		if (healthbar) 
		{
			healthbar.setHealth (cur_health);
		}

		if (cur_health <= 0)
			Die(attacker_name);
	}
	public virtual void Die(string attacker_name)
	{
		if (!string.IsNullOrEmpty(attacker_name))
		{
			// Record kill and death
			AllScores.all_scores.scores[player_name].deaths++;
			AllScores.all_scores.scores[attacker_name].kills++;

			Debug.Log(attacker_name + " killed " + this.player_name);
		}
	}


	public virtual void Colourize()
	{

	}
}
