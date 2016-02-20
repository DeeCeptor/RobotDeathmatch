using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotController : PlayerInput 
{
	Queue<string> player_input_queue = new Queue<string>();		// Input the player is adding in, input that has yet to be confirmed
	Queue<string> actions_queue = new Queue<string>();	// Actions that we are in the midst of performing

	string current_action;	// Action we are performing
	Rigidbody2D physics;
	float movement_time = 0.4f;	// Time in seconds it takes the robot to move per move action
	float movement_velocity = 2.0f;
	bool performing_action = false;
	int input_limit = 3;	// If over this limit, dequeue the inputs


	void Awake()
	{
		physics = this.GetComponent<Rigidbody2D>();
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
			Debug.Log("horizontal");
			if (horizontal_movement > 0)
				player_input_queue.Enqueue("MoveRight");
			else
				player_input_queue.Enqueue("MoveLeft");
		}
		if (prev_vertical_movement == 0 && vertical_movement != 0)
		{
			Debug.Log("vertical");
			if (vertical_movement > 0)
				player_input_queue.Enqueue("MoveUp");
			else
				player_input_queue.Enqueue("MoveDown");
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
		Debug.Log(physics.velocity + " " + direction);
		performing_action = true;
		physics.velocity = direction;

		yield return new WaitForSeconds(movement_time);

		// Done moving
		performing_action = false;
		physics.velocity = Vector2.zero;
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

			}
			else if (command.Contains("RocketLauncher"))
			{

			}
		}
	}
}
