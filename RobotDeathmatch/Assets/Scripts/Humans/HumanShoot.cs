using UnityEngine;
using System.Collections;

public class HumanShoot : MonoBehaviour {
	public GameObject shot;
	public Transform shotSpawn;
	public float FireRate;

	private float nextFire;

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + FireRate;
//			GameObject bullet = Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			//bullet.GetComponent<Bullet>().Initialize_Bullet(
			//anim.SetTrigger ("shoot");

		}
	}
}
