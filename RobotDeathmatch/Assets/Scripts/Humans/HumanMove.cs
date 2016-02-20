using UnityEngine;
using System.Collections;

public class HumanMove : MonoBehaviour {

	public float speed;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		float MoveHorizontal = Input.GetAxis("Horizontal");
		float MoveVertical = Input.GetAxis("Vertical");

		Vector2 movement = new Vector2 (MoveHorizontal, MoveVertical);
		GetComponent<Rigidbody2D>().velocity = movement * speed;


	}
}
