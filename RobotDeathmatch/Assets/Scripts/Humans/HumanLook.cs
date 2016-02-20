using UnityEngine;
using System.Collections;

public class HumanLook : MonoBehaviour {

	Vector3 mouse_pos;
	Vector3 human_pos;
	Transform target;
	float angle;

	// Use this for initialization
	void Start () {
		target = this.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		mouse_pos = Input.mousePosition;
		human_pos = Camera.main.WorldToScreenPoint (target.position);
		mouse_pos.x = mouse_pos.x - human_pos.x;
		mouse_pos.y = mouse_pos.y - human_pos.y;
		angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0, 0, angle);
		}
}
