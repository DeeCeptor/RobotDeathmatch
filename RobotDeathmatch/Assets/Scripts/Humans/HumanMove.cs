using UnityEngine;
using System.Collections;

public class HumanMove : PlayerInput {

	public GameObject shot;
	public Transform shotSpawn;
	public float FireRate;
	Vector3 mouse_pos;
	Vector3 human_pos;
	public Transform target;
	float angle;
	bool IsDead;
	CircleCollider2D thisCollider;
	public AudioClip DeathNoise;
	public AudioClip Gunshot;
	public GameObject bloodspray;
	public GameObject bloodpool1;
	public GameObject bloodpool2;
	public GameObject bloodpool3;

	public GameObject parent;

	Quaternion desired_rotation;
	float rotation_speed = 10f;

	private float nextFire;
	public float speed;
	float bullet_speed = 15;
	public float bullet_damage = 50;


	Animator anim;
	AudioSource audio;

	void Awake () 
	{
		parent = this.gameObject;
		anim = this.GetComponentInChildren<Animator> ();
		thisCollider = GetComponent<CircleCollider2D> ();
		audio = GetComponent<AudioSource> ();

		this.cur_health = this.max_health;

		// Register a live human
		GameObject.FindGameObjectWithTag("Scores").GetComponent<Timer>().RegisterHuman();

		base.init ();
	}
	

	void Update () {
		if (Time.timeScale > 0) 
		{
			UpdateInputs ();

			if (IsDead == false) 
			{
				float MoveHorizontal = this.horizontal_movement;
				float MoveVertical = vertical_movement;

				Vector2 movement = new Vector2 (MoveHorizontal, -MoveVertical);
				parent.GetComponent<Rigidbody2D> ().velocity = movement * speed;

				bool walking = MoveHorizontal != 0 || MoveVertical != 0;
				anim.SetBool ("walking", walking);


				FireBullet ();

				// Rotate towards desired rotation
				if (!controller)
				{
					mouse_pos = Input.mousePosition;
					human_pos = Camera.main.WorldToScreenPoint (target.position);
					mouse_pos.x = mouse_pos.x - human_pos.x;
					mouse_pos.y = mouse_pos.y - human_pos.y;
					angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90;
					target.rotation = Quaternion.Euler (0, 0, angle);
				}
				else
				{
					// Look towards where shooting or if we haven't shot in a while rotate towards movement direction
					// Check if we've shot recently
					if (Time.time > nextFire + 0.8f && movement != Vector2.zero)
					{
						// Rotate towards walking direction
						float angle_ = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
						desired_rotation = Quaternion.AngleAxis(angle_ - 90, Vector3.forward);
					}

					this.target.rotation = Quaternion.Slerp(this.target.rotation, desired_rotation, Time.deltaTime * rotation_speed);
				}
			} 
			else if (IsDead == true) 
			{
				thisCollider.enabled = false;

			}
		}
	}

	void FireBullet(){
		if (Time.time > nextFire
			&& ((controller && aiming_direction != Vector2.zero) || (!controller && flicked_aiming_direction != Vector2.zero)))
		{
			// Look towards where we're shooting
			float angle_ = Mathf.Atan2(aiming_direction.y, aiming_direction.x) * Mathf.Rad2Deg;
			desired_rotation = Quaternion.AngleAxis(angle_ - 90, Vector3.forward);

			nextFire = Time.time + FireRate;
			GameObject bullet = (GameObject) Instantiate ( (GameObject) shot, shotSpawn.position, shotSpawn.rotation);
			bullet.GetComponent<Bullet> ().Initialize_Bullet(this.player_name, team_number, bullet_damage, aiming_direction, bullet_speed, 3) ;
			anim.SetTrigger ("shoot");
			audio.clip = Gunshot;
			audio.Play ();
		}
	}

	public override void TakeHit (float damage, Vector3 collision_position, string attacker_name)
	{
		base.TakeHit (damage, collision_position, attacker_name);
		GameObject spobj = Instantiate (bloodspray, collision_position, this.transform.rotation) as GameObject;
		switch (Random.Range (0, 3)) {
			case 0:
			Instantiate (bloodpool1, collision_position, this.transform.rotation);
				break;
			case 1:
			Instantiate (bloodpool2, collision_position, this.transform.rotation);
				break;
			case 2:
			Instantiate (bloodpool3, collision_position, this.transform.rotation);
				break;
		}

		Destroy (spobj, 1);
	}
	public override void Die (string attacker_name)
	{
		base.Die (attacker_name);
		IsDead = true;
		audio.clip = DeathNoise;
		audio.Play ();
		anim.SetTrigger ("die");
		GameObject.FindGameObjectWithTag("Scores").GetComponent<Timer>().HumanDied();
		Destroy (this.gameObject, 3);
	} 
}
