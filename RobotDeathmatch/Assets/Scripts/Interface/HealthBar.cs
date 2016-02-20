using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public GameObject anchor;
	public GameObject healthBar;
	public GameObject healthContainer;
	public float maxHealth;
	float health;

	public float fadeDelay;
	float showTime;

	SpriteRenderer barRender;
	SpriteRenderer containerRender;

	// Use this for initialization
	void Start () {
		barRender = healthBar.GetComponent<SpriteRenderer> ();
		containerRender = healthContainer.GetComponent<SpriteRenderer> ();
		showTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > showTime + fadeDelay) {
			Color barColor = barRender.color;
			barColor.a -= 5;
			barRender.color = barColor;
			Color containerColor = containerRender.color;
			containerColor.a -= 5;
			containerRender.color = containerColor;
		}
	}

	public void setHealth(float health)
	{
		this.health = health;
		updateBar ();
	}

	public void doDamage(float damage)
	{
		health -= damage;
		if (health < 0)
			health = 0;
		updateBar();
	}

	void updateBar()
	{
		Vector3 scale = anchor.transform.localScale;
		scale.x= health / maxHealth;
		anchor.transform.localScale = scale;
		showBar ();
	}

	void showBar()
	{
		showTime = Time.time;
		Color barColor = barRender.color;
		barColor.a = 255;
		barRender.color = barColor;
		Color containerColor = containerRender.color;
		containerColor.a = 255;
		containerRender.color = containerColor;
	}
}
