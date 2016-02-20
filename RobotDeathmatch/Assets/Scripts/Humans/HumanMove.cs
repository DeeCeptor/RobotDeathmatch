using UnityEngine;
using System.Collections;

public class HumanMove : PlayerInput {

	public GameObject shot;
	public Transform shotSpawn;
	public float FireRate;
	Vector3 mouse_pos;
	Vector3 human_pos;
	Transform target;
	float angle;
	bool IsDead;
	CircleCollider2D thisCollider;
	public AudioClip DeathNoise;
	public AudioClip Gunshot;


	public GameObject parent;

	private float nextFire;
	public float speed;
	float bullet_speed = 15;
	float bullet_damage = 10;


	Animator anim;
	AudioSource audio;

	// Use this for initialization
	public void Start () {
		parent = transform.parent.gameObject;
		target = this.GetComponent<Transform> ();
		anim = GetComponent<Animator> ();
		thisCollider = GetComponent<CircleCollider2D> ();
		audio = GetComponent<AudioSource> ();

		this.cur_health = this.max_health;

		base.init ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateInputs ();
		if (IsDead == false) {
			float MoveHorizontal = this.horizontal_movement;
			float MoveVertical = vertical_movement;

			Vector2 movement = new Vector2 (MoveHorizontal, -MoveVertical);
			parent.GetComponent<Rigidbody2D> ().velocity = movement * speed;

			bool walking = MoveHorizontal != 0 || MoveVertical != 0;
			anim.SetBool ("walking", walking);

			mouse_pos = Input.mousePosition;
			human_pos = Camera.main.WorldToScreenPoint (target.position);
			mouse_pos.x = mouse_pos.x - human_pos.x;
			mouse_pos.y = mouse_pos.y - human_pos.y;
			angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.Euler (0, 0, angle);


			FireBullet ();
		} else if (IsDead == true) {
			thisCollider.enabled = false;

		}
	}

	void FireBullet(){
		if (Time.time > nextFire
			&& ((controller && aiming_direction != Vector2.zero) || (!controller && flicked_aiming_direction != Vector2.zero)))
		{
			nextFire = Time.time + FireRate;
			GameObject bullet = (GameObject) Instantiate ( (GameObject) shot, shotSpawn.position, shotSpawn.rotation);
			bullet.GetComponent<Bullet> ().Initialize_Bullet(team_number, bullet_damage, aiming_direction, bullet_speed, 3) ;
			anim.SetTrigger ("shoot");
			audio.clip = Gunshot;
			audio.Play ();
		}
	}

	public override void TakeHit (float damage, Vector3 collision_position)
	{
		base.TakeHit (damage, collision_position);
	}
	public override void Die ()
	{

		base.Die ();
		IsDead = true;
		audio.clip = DeathNoise;
		audio.Play ();
		anim.SetTrigger ("die");
		Destroy (this.gameObject, 3);
	} 
}
