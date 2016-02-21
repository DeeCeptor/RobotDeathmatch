using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public float damage;	// Grab this damage from the player controller
	public int shooter_team_number;
	float time_left;	// When this hits 0, destroy bullet
	public GameObject sparks;
	string owners_name;

	void Awake ()
	{

	}
	void Start () {
	
	}


	void Update()
	{
		time_left -= Time.deltaTime;

		if (time_left <= 0)
			Destroy(this.gameObject);
	}

	// NEEDS TO BE CALLED
	public void Initialize_Bullet(string shooters_name, int team_number, float bullet_damage, Vector2 direction, float speed, float duration)
	{
		owners_name = shooters_name;
		shooter_team_number = team_number;
		damage = bullet_damage;
		time_left = duration;

		// Rotation
		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		this.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);

		// Velocity
		this.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
	}


	// Returns true if this bullet will hurt a player belonging to that team
	public bool Should_Bullet_Damage_You(int victim_team_number)
	{
		return shooter_team_number != victim_team_number;
	}

	// Returns the damage done by this bullet
	public void Bullet_Impacted()
	{
		// Any graphical stuff of the bullet hitting something
		GameObject spobj = Instantiate (sparks, this.transform.position, this.transform.rotation) as GameObject;
		Destroy (spobj, 1);

		Transform trail = this.GetComponentInChildren<TrailRenderer>().transform;
		trail = null;
		Destroy(trail, 3.0f);

		Destroy(this.gameObject);
	}



	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Robot" || other.tag == "Human") 
		{
			PlayerInput player = other.GetComponent<PlayerInput> ();
			if (player == null)
				player = other.GetComponentInChildren<PlayerInput>();

			// Should we be hurt by this bullet?
			if (!this.Should_Bullet_Damage_You (player.team_number))
				return;

			// Take a hit
			player.TakeHit (this.damage, this.transform.position, owners_name);
			this.Bullet_Impacted();
		} 
		else if (other.tag == "Obstacle") {
			// Hit a piece of cover, destroy this bullet
			DestructibleObstacle obs = other.GetComponent<DestructibleObstacle> ();

			obs.TakeHit (this.damage);

			this.Bullet_Impacted ();
		} 
		else if (other.tag != "Bullet")
		{
			this.Bullet_Impacted ();
		}
	}
}
