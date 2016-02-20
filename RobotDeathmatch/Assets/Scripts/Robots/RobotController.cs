using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotController : PlayerInput 
{
	Queue<string> player_input_queue = new Queue<string>();		// Input the player is adding in, input that has yet to be confirmed
	Queue<string> actions_queue = new Queue<string>();	// Actions that we are in the midst of performing

	public float max_health = 1000;
	public float cur_health;

	string current_action;	// Action we are performing
	Rigidbody2D physics;
	float movement_time = 0.4f;	// Time in seconds it takes the robot to move per move action
	float movement_velocity = 2.0f;
	bool performing_action = false;
	int input_limit = 3;	// If over this limit, dequeue the inputs

	float machine_gun_duration = .4f;
	int num_machine_gun_bullets = 20;
	float machine_gun_speed = 9.0f;

	void Awake()
	{
		physics = this.GetComponent<Rigidbody2D>();
		cur_health = max_health;
	}
	void Start()
	{

	}


	void Update () 
	{
		// We have all of the player input
		UpdateInputs();

		// Check for player input to be added the to the player input queue
		if (prev_horizontal_movement == 0 && horizontal_movement != 0)
		{
			if (horizontal_movement > 0)
				player_input_queue.Enqueue("MoveRight");
			else
				player_input_queue.Enqueue("MoveLeft");
		}
		if (prev_vertical_movement == 0 && vertical_movement != 0)
		{
			if (vertical_movement > 0)
				player_input_queue.Enqueue("MoveUp");
			else
				player_input_queue.Enqueue("MoveDown");
		}
		// Aiming and firing
		if (prev_aiming_direction == Vector2.zero && aiming_direction != Vector2.zero)
		{
			player_input_queue.Enqueue("MachineGun " + aiming_direction.x + " " + aiming_direction.y);
		}


		if (player_input_queue.Count >= input_limit)
		{
			DequeuePlayerInput();
		}


		// Currently performing actions?
		if (!performing_action)
		{
			ResolveNextAction();
		}
	}


	IEnumerator Move_Action(Vector2 direction)
	{
		performing_action = true;
		physics.velocity = direction;

		yield return new WaitForSeconds(movement_time);

		// Done moving
		performing_action = false;
		physics.velocity = Vector2.zero;
	}
	IEnumerator MachineGun_Action(Vector2 direction)
	{
		performing_action = true;

		int bullets_left = (int) num_machine_gun_bullets;
		while (bullets_left > 0)
		{
			bullets_left--;

			// Spawn a bullet, with a random variation on the aiming vector
			Vector3 bullet_dir = new Vector3(direction.x + (Random.value - 0.5f),
				direction.y + (Random.value - 0.5f), 0);

			// Set rotation, speed
			GameObject bullet = (GameObject) Instantiate(Resources.Load("Bullet") as GameObject, this.transform.position, Quaternion.Euler(bullet_dir));
			bullet.GetComponent<Rigidbody2D>().velocity = bullet_dir.normalized * machine_gun_speed;

			yield return new WaitForSeconds(machine_gun_duration / num_machine_gun_bullets);
		}

		performing_action = false;
	}

	public void DequeuePlayerInput()
	{
		while (player_input_queue.Count > 0)
		{
			string command = player_input_queue.Dequeue();
			actions_queue.Enqueue(command);
		}
	}

	public void ResolveNextAction()
	{
		if (actions_queue.Count > 0)
		{
			string command = actions_queue.Dequeue();
			if (command == "MoveLeft")
			{
				StartCoroutine(Move_Action(new Vector2(-movement_velocity, 0)));
			}
			else if (command == "MoveRight")
			{
				StartCoroutine(Move_Action(new Vector2(movement_velocity, 0)));
			}
			else if (command == "MoveUp")
			{
				StartCoroutine(Move_Action(new Vector2(0, -movement_velocity)));
			}
			else if (command == "MoveDown")
			{
				StartCoroutine(Move_Action(new Vector2(0, movement_velocity)));
			}
			else if (command.Contains("MachineGun"))
			{
				string[] splits = command.Split(' ');
				float x = float.Parse(splits[1]);
				float y = float.Parse(splits[2]);
				StartCoroutine(MachineGun_Action(new Vector2(x, y)));
			}
			else if (command.Contains("RocketLauncher"))
			{

			}
		}
	}
}
