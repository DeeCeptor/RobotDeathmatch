using UnityEngine;
using System.Collections;

public class MoveShot : MonoBehaviour {
	public float speed;
	public int damage;


	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().velocity = transform.right * speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
