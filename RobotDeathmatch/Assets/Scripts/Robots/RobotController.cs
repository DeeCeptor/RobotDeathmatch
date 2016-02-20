using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RobotController : PlayerInput 
{
	Queue<string> player_input_queue = new Queue<string>();		// Input the player is adding in, input that has yet to be confirmed
	Queue<string> actions_queue = new Queue<string>();	// Actions that we are in the midst of performing
	InputTimer timer;
	Animator[] action_icons;
	Transform UI_parent;

	// ACTIONS
	string current_action;	// Action we are performing
	Rigidbody2D physics;
	float movement_time = 0.4f;	// Time in seconds it takes the robot to move per move action
	float movement_velocity = 2.0f;
	bool performing_action = false;
	int input_limit = 3;	// If over this limit, dequeue the inputs

	// MACHINE GUN
	float machine_gun_duration = .2f;
	int num_machine_gun_bullets = 15;
	float machine_gun_speed = 20.0f;
	float machine_gun_damage = 50;

	void Awake()
	{
		physics = this.GetComponent<Rigidbody2D>();
		cur_health = max_health;

		// Find the timer and register the robot
		timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<InputTimer>();
		timer.AddActiveRobot(this);

		// Spawn UI icons
		UI_parent = this.transform.GetComponentInChildren<GridLayoutGroup>().transform;
		action_icons = new Animator[input_limit];
		for (int x = 0; x < input_limit; x++)
		{
			// Spawn 1 UI action icon per input limit
			GameObject icon = (GameObject) Instantiate(Resources.Load("ActionSlot") as GameObject);
			icon.transform.parent = UI_parent;
			icon.transform.localScale = Vector3.one;
			action_icons[x] = icon.GetComponent<Animator>();
		}
	}
	void Start()
	{

	}


	void Update () 
	{
		// We have all of the player input
		UpdateInputs();

		// If not maxed out on player inputs, allow more action inputs
		if (player_input_queue.Count < input_limit)
		{
			// Check for player input to be added the to the player input queue
			if (prev_horizontal_movement == 0 && horizontal_movement != 0)
			{
				if (horizontal_movement > 0)
					QueueNewInput("MoveRight");
				else
					QueueNewInput("MoveLeft");
			}
			if (prev_vertical_movement == 0 && vertical_movement != 0)
			{
				if (vertical_movement > 0)
					QueueNewInput("MoveUp");
				else
					QueueNewInput("MoveDown");
			}
			// Aiming and firing
			if (prev_flicked_aiming_direction == Vector2.zero && flicked_aiming_direction != Vector2.zero)
			{
				QueueNewInput("MachineGun " + aiming_direction.x + " " + aiming_direction.y);
			}
		}

		/*
		if (player_input_queue.Count >= input_limit)
		{
			DequeuePlayerInput();
		}*/


		// Currently performing actions?
		if (!performing_action)
		{
			ResolveNextAction();
		}
	}


	public void QueueNewInput(string command)
	{
		player_input_queue.Enqueue(command);

		action_icons[player_input_queue.Count - 1].SetBool("active", true);
		//action_icons[player_input_queue.Count - 1].GetBool("active");
		//actions_queue[player_input_queue.Count - 1].
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
			var angle = Mathf.Atan2(bullet_dir.y, bullet_dir.x) * Mathf.Rad2Deg;

			GameObject bullet = (GameObject) Instantiate(Resources.Load("Bullet") as GameObject, 
				this.transform.position, 
				Quaternion.AngleAxis(angle, Vector3.forward));
			bullet.GetComponent<Bullet>().Initialize_Bullet(this.team_number, machine_gun_damage, bullet_dir, machine_gun_speed, 3.0f);

			GameObject casing = (GameObject) Instantiate(Resources.Load("Casing") as GameObject, 
				this.transform.position, 
				Quaternion.AngleAxis(angle, Vector3.forward));
			casing.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0));

			float wait_duration = machine_gun_duration / (float) num_machine_gun_bullets;
			yield return new WaitForSeconds(wait_duration);
		}
		Debug.Log(timer.time_left_in_iteration);
		performing_action = false;
	}


	// Takes all input currently entered and adds it to the queue that is executing actions
	public void DequeuePlayerInput()
	{
		while (player_input_queue.Count > 0)
		{
			string command = player_input_queue.Dequeue();
			actions_queue.Enqueue(command);
		}

		foreach (Animator anim in action_icons)
		{
			anim.SetBool("active", false);
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


	public override void TakeHit(float damage)
	{
		base.TakeHit(damage);
	}
	public override void Die()
	{
		base.Die();

		Destroy(this.gameObject);
	}
}
