using UnityEngine;
using System.Collections;

public class SwitchingWeapons : MonoBehaviour {
	//Collider2D guncollider = GetComponent(;

	public HumanMove human;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
}
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("Hi");
		if (other.tag == "Human") 
		{
			human = other.gameObject.GetComponent<HumanMove>();
			human.ammo = 30;
			human.HasRifle=true;
			human.GetRifle();
		}
	}
}