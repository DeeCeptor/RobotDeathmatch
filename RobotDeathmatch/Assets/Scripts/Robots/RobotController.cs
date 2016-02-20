using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RobotController : PlayerInput 
{
	Queue<string> player_input_queue = new Queue<string>();		// Input the player is adding in, input that has yet to be confirmed
	Queue<string> actions_queue = new Queue<string>();	// Actions that we are in the midst of performing
	InputTimer timer;
	Image[] action_icons;
	Transform UI_parent; 
	Animator top_anim;

	Transform sprite_parent;
	Quaternion desired_rotation;
	float rotation_speed = 5.0f;
	public Transform barrel_tip;

	// SOUNDS
	AudioSource audio;
	public AudioClip death_noise;
	public AudioClip machinegun_noise;
	public AudioClip moving_noise;

	// ACTIONS
	string current_action;	// Action we are performing
	Rigidbody2D physics;
	float movement_time = 0.4f;	// Time in seconds it takes the robot to move per move action
	float movement_velocity = 2.0f;
	bool performing_action = false;
	int input_limit = 5;	// If over this limit, dequeue the inputs

	// MACHINE GUN
	float machine_gun_duration = .2f;
	int num_machine_gun_bullets = 15;
	float machine_gun_speed = 20.0f;
	float machine_gun_damage = 50;

	public Sprite open_action_slot;
	public Sprite filled_action_slot;

	public GameObject robosparks;

	void Awake()
	{
		audio = this.GetComponent<AudioSource>();
		sprite_parent = this.transform.FindChild("Sprites");
		physics = this.GetComponent<Rigidbody2D>();
		cur_health = max_health;
		top_anim = this.GetComponentInChildren<Animator>();

		// Find the timer and register the robot
		timer = GameObject.FindWithTag("Timer").GetComponent<InputTimer>();
		timer.AddActiveRobot(this);

		// Spawn UI icons
		UI_parent = this.transform.GetComponentInChildren<GridLayoutGroup>().transform;
		action_icons = new Image[input_limit];
		for (int x = 0; x < input_limit; x++)
		{
			// Spawn 1 UI action icon per input limit
			GameObject icon = (GameObject) Instantiate(Resources.Load("ActionSlot") as GameObject);
			icon.transform.SetParent(UI_parent);
			icon.transform.localScale = Vector3.one;
			action_icons[x] = icon.GetComponent<Image>();
		
		}
		base.init ();
	}


	void Update () 
	{
		// We have all of the player input
		UpdateInputs();

		// If not maxed out on player inputs, allow more action inputs
		if (player_input_queue.Count < input_limit)
		{
			// Check for player input to be added the to the player input queue
			if (prev_horizontal_movement == 0 && horizontal_movement != 0
				&& Mathf.Abs(horizontal_movement) > Mathf.Abs(vertical_movement))
			{
				if (horizontal_movement > 0)
					QueueNewInput("MoveRight");
				else
					QueueNewInput("MoveLeft");
			}
			if (prev_vertical_movement == 0 && vertical_movement != 0
				&& Mathf.Abs(vertical_movement) > Mathf.Abs(horizontal_movement))
			{
				if (vertical_movement > 0)
					QueueNewInput("MoveUp");
				else
					QueueNewInput("MoveDown");
			}
			// Aiming and firing
			if (prev_flicked_aiming_direction == Vector2.zero && flicked_aiming_direction != Vector2.zero)
			{
				QueueNewInput("MachineGun " + flicked_aiming_direction.x + " " + flicked_aiming_direction.y);
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

		// Set correct rotation
		sprite_parent.rotation = Quaternion.Slerp(sprite_parent.rotation, desired_rotation, Time.deltaTime * rotation_speed);
	}


	public void QueueNewInput(string command)
	{
		player_input_queue.Enqueue(command);

		action_icons[player_input_queue.Count - 1].sprite = filled_action_slot;
		//action_icons[player_input_queue.Count - 1].GetBool("active");
		//actions_queue[player_input_queue.Count - 1].
	}


	IEnumerator Move_Action(Vector2 direction)
	{
		performing_action = true;
		physics.velocity = direction;
		top_anim.SetBool ("walking", true);
		audio.clip = moving_noise;
		audio.Play ();

		// Rotate towards direction we're walking
		// Look towards where we're shooting
		float angle_ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		desired_rotation = Quaternion.AngleAxis(angle_ - 90, Vector3.forward);

		yield return new WaitForSeconds(movement_time);

		// Done moving
		audio.Stop();
		top_anim.SetBool ("walking", false);
		performing_action = false;
		physics.velocity = Vector2.zero;
	}
	IEnumerator MachineGun_Action(Vector2 direction)
	{
		performing_action = true;
		// Look towards where we're shooting
		float angle_ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		desired_rotation = Quaternion.AngleAxis(angle_ - 90, Vector3.forward);

		yield return new WaitForSeconds(0.2f);
		top_anim.SetTrigger ("shoot");
		audio.clip = machinegun_noise;
		audio.loop = true;
		audio.Play ();

		int bullets_left = (int) num_machine_gun_bullets;
		while (bullets_left > 0)
		{
			bullets_left--;
			top_anim.SetTrigger ("shoot");

			// Spawn a bullet, with a random variation on the aiming vector
			Vector3 bullet_dir = new Vector3(direction.x + (Random.value - 0.5f),
				direction.y + (Random.value - 0.5f), 0);

			// Set rotation, speed
			var angle = Mathf.Atan2(bullet_dir.y, bullet_dir.x) * Mathf.Rad2Deg;

			GameObject bullet = (GameObject) Instantiate(Resources.Load("Bullet") as GameObject, 
				this.barrel_tip.position, 
				Quaternion.AngleAxis(angle, Vector3.forward));
			bullet.GetComponent<Bullet>().Initialize_Bullet(this.player_name, this.team_number, machine_gun_damage, bullet_dir, machine_gun_speed, 3.0f);

			// Shoot out a bullet casing perpendicular to the firing line
			GameObject casing = (GameObject) Instantiate(Resources.Load("Casing") as GameObject, 
				this.barrel_tip.position, 
				Quaternion.AngleAxis(angle + 90, Vector3.forward));
			Quaternion quat = Quaternion.AngleAxis(90 + Random.value * 10 - 5f, Vector3.forward);
			Vector3 vect= (Vector3) direction;
			vect = quat * vect;
			casing.GetComponent<Rigidbody2D>().AddForce((Vector2) vect * 15);

			float wait_duration = machine_gun_duration / (float) num_machine_gun_bullets;
			yield return new WaitForSeconds(wait_duration);
		}
		audio.loop = false;
		audio.Stop();
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

		foreach (Image anim in action_icons)
		{
			anim.sprite = open_action_slot;
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


	public override void TakeHit(float damage, Vector3 collision_position, string attacker_name)
	{
		base.TakeHit(damage, collision_position, attacker_name);
		GameObject spobj = Instantiate (robosparks, collision_position, this.transform.rotation) as GameObject;
		Destroy (spobj, 1);
	}
	public override void Die(string attacker_name)
	{
		base.Die(attacker_name);
		audio.clip = death_noise;
		audio.Play ();
		top_anim.SetTrigger ("die");
		Destroy (this.gameObject, 3);
	}
}
