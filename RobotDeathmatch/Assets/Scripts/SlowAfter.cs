using UnityEngine;
using System.Collections;

public class SlowAfter : MonoBehaviour 
{
	public float slow_After = 1f;	// Time in seconds
	Rigidbody2D physics;

	void Start () {
		physics = this.GetComponent<Rigidbody2D>();
		slow_After = slow_After + Random.value * slow_After - slow_After / 2;	// A bit of random variation when to slow down
	}
	

	void Update () 
	{
		slow_After -= Time.deltaTime;

		if (slow_After <= 0)
			physics.velocity = physics.velocity * 0.9f;
	}
}
