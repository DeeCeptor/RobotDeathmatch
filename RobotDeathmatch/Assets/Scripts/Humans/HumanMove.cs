﻿using UnityEngine;
using System.Collections;

public class HumanMove : PlayerInput {

	public GameObject shot;
	public Transform shotSpawn;
	public float FireRate;
	Vector3 mouse_pos;
	Vector3 human_pos;
	Transform target;
	float angle;

	private float nextFire;
	public float speed;
	Animator anim;

	// Use this for initialization
	void Start () {
		target = this.GetComponent<Transform> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateInputs ();

		float MoveHorizontal = this.horizontal_movement;
		float MoveVertical = vertical_movement;

		Vector2 movement = new Vector2 (MoveHorizontal, MoveVertical);
		GetComponent<Rigidbody2D>().velocity = movement * speed;

		bool walking = MoveHorizontal != 0 || MoveVertical != 0;
		anim.SetBool ("walking", walking);

		mouse_pos = Input.mousePosition;
		human_pos = Camera.main.WorldToScreenPoint (target.position);
		mouse_pos.x = mouse_pos.x - human_pos.x;
		mouse_pos.y = mouse_pos.y - human_pos.y;
		angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.Euler (0, 0, angle);


		FireBullet ();
	}

	void FireBullet(){
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + FireRate;
			GameObject bullet = (GameObject) Instantiate ( (GameObject) shot, shotSpawn.position, shotSpawn.rotation);
			bullet.GetComponent<Bullet> ().Initialize_Bullet(1, 10, aiming_direction, 10, 3) ;
			anim.SetTrigger ("shoot");
		}
	}

	public override void TakeHit (float damage)
	{
		base.TakeHit (damage);
	}
	public override void Die ()
	{
		base.Die ();
	}
}
