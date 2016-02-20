using UnityEngine;
using System.Collections;

public class DestructibleObstacle : MonoBehaviour 
{
	public float max_health = 500;
	public float cur_health;


	void Awake () 
	{
		cur_health = max_health;
	}

	public virtual void TakeHit(float damage)
	{
		cur_health -= damage;

		if (cur_health <= 0)
			Die();
	}
	public virtual void Die()
	{
		Destroy(this.gameObject);
	}
}
