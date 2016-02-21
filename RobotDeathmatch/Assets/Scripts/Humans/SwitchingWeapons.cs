using UnityEngine;
using System.Collections;

public class SwitchingWeapons : MonoBehaviour {
	CircleCollider2D guncollider;
	SpriteRenderer gunrender;
	public Canvas GunText;
	float GunTextTime;
	float GunTextTDelay=5;
	bool GunTextVis;
	float Delaytime =60;
	float TimeGrab;
	bool grabbed;

	public HumanMove human;
	// Use this for initialization
	void Start () {
		gunrender = GetComponent<SpriteRenderer>();
		guncollider = GetComponent<CircleCollider2D>();
		GunText.enabled = false;
		GunTextVis = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (grabbed && Time.time > TimeGrab + Delaytime) {
			guncollider.enabled = true;
			gunrender.enabled = true;
			grabbed = false;
			}
		if (GunTextVis && Time.time > GunTextTime+GunTextTDelay){
			GunText.enabled = false;
			GunTextVis = false;
		}
}
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("Hi");
		if (other.tag == "Human") 
		{
			grabbed = true;
			TimeGrab = Time.time;
			guncollider.enabled = false;
			gunrender.enabled = !gunrender.enabled;
			human = other.gameObject.GetComponent<HumanMove>();
			human.ammo = 30;
			human.HasRifle=true;
			human.GetRifle();

			GunText.enabled = true;
			GunTextVis = true;
			GunTextTime = Time.time;

		}

	}
}