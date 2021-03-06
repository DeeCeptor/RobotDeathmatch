using UnityEngine;
using System.Collections;

public class HumanMove : PlayerInput {

	public GameObject shot;
	public Transform shotSpawn;
	// Transform Guntip;
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
	public bool HasRifle= false;
	public float ammo;
	public GameObject Guntip1;
	public GameObject Guntip2;

	public GameObject parent;

	Quaternion desired_rotation;
	float rotation_speed = 10f;
    Vector2 last_movement_direction;

	private float nextFire;
	public float speed;
	float bullet_speed = 15;
	float bullet_damage = 10;


	Animator anim;
	AudioSource audio;

	void Awake () 
	{
		parent = transform.parent.gameObject;
		target = this.GetComponent<Transform> ();
		anim = this.GetComponentInChildren<Animator> ();
		thisCollider = GetComponent<CircleCollider2D> ();
		audio = GetComponent<AudioSource> ();
		Timer.timer.RegisterHuman();
		this.cur_health = this.max_health;

		base.init ();
	}
	

	void Update ()
    {
		if (Time.timeScale > 0)
        {
			UpdateInputs ();

			if (IsDead == false)
            {
				float MoveHorizontal = this.horizontal_movement;
				float MoveVertical = vertical_movement;

				Vector2 movement = new Vector2 (MoveHorizontal, -MoveVertical);
				parent.GetComponent<Rigidbody2D> ().velocity = movement * speed;

                if (movement != Vector2.zero)
                    last_movement_direction = movement;

				bool walking = MoveHorizontal != 0 || MoveVertical != 0;
					
				anim.SetBool ("walking", walking);

				FireBullet ();

				// Rotate towards mouse
				if (!controller)
                {
					mouse_pos = Input.mousePosition;
					human_pos = Camera.main.WorldToScreenPoint (target.position);
					mouse_pos.x = mouse_pos.x - human_pos.x;
					mouse_pos.y = mouse_pos.y - human_pos.y;
					angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90;
					transform.rotation = Quaternion.Euler (0, 0, angle);
				}
                else
                {
					// Look towards where shooting or if we haven't shot in a while rotate towards movement direction
					// Check if we've shot recently
					if (Time.time > nextFire + 0.8f)
                    {
						// Rotate towards walking direction
						float angle_ = Mathf.Atan2 (last_movement_direction.y, last_movement_direction.x) * Mathf.Rad2Deg;
						desired_rotation = Quaternion.AngleAxis (angle_ - 90, Vector3.forward);
					}

					this.target.rotation = Quaternion.Slerp (this.target.rotation, desired_rotation, Time.deltaTime * rotation_speed);
				}
			}
            else if (IsDead == true)
            {
				thisCollider.enabled = false;

			}
		}
	}
	void FireBullet()
    {
		if (Time.time > nextFire
			&& ((controller && aiming_direction != Vector2.zero) || (!controller && flicked_aiming_direction != Vector2.zero)))
		{
			// Look towards where we're shooting
			GetRifle ();
			if (HasRifle==true){
				FireRate=0.1f;
				shotSpawn = Guntip2.transform;
				ammo-=1;
			}else if (HasRifle == false){
				FireRate = 0.25f;
				shotSpawn = Guntip1.transform;
			}if (ammo <= 0) {
				
				HasRifle = false;

			}
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

	public void GetRifle(){
		if (HasRifle == true) {
			anim.SetBool ("rifle", true);
		} else if (HasRifle == false) {
			anim.SetBool ("rifle", false);
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
		if (!IsDead)
		{
			base.Die (attacker_name);
			IsDead = true;
			audio.clip = DeathNoise;
			audio.Play ();
			Timer.timer.HumanDied();
			anim.SetTrigger ("die");
			parent.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			Destroy (this.gameObject, 3);
		}
	} 
}
