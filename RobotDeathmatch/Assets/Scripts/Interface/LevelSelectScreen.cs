using UnityEngine;
using System.Collections;

public class LevelSelectScreen : MonoBehaviour {
	public GameObject slot0;
	public GameObject slot1;
	public GameObject slot2;
	public GameObject slot3;
	public GameObject slot4;
	public GameObject slot5;

	int current = 0;
	public float delay;
	float moveTime;
	// Use this for initialization
	void Start () {
		moveTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > moveTime + delay) {
			float vertical_movement = Input.GetAxis ("P1 LeftStick Y");
		}
	}
}
