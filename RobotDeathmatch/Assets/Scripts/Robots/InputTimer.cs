using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InputTimer : MonoBehaviour 
{
	public float time_left_in_iteration;
	public float time_per_iteration = 3.0f;	// How long the players have to key input

	private List<RobotController> active_robots = new List<RobotController>();
	bool iterating = false;

	Image countdown_image;
	public Sprite three;
	public Sprite two;
	public Sprite one;
	Text countdown_text;

	void Start () 
	{
		time_left_in_iteration = time_per_iteration;
		countdown_image = this.GetComponent<Image>();
		countdown_text = this.GetComponentInChildren<Text>();
	}
	

	void FixedUpdate () 
	{
		time_left_in_iteration -= Time.deltaTime;

		if (time_left_in_iteration > 2)
		{
			countdown_image.sprite = three;
			countdown_text.text = "3";
		}
		else if (time_left_in_iteration > 1)
		{
			countdown_image.sprite = two;
			countdown_text.text = "2";
		}
		else
		{
			countdown_image.sprite = one;
			countdown_text.text = "1";
		}

		if (time_left_in_iteration <= 0)
		{
			EnterInput();
		}
	}
	public void EnterInput()
	{
		iterating = true;

		// Done this iteration
		foreach (RobotController robot in active_robots)
		{
			// Finalize inputs done in this this iteration
			robot.DequeuePlayerInput();
		}
		time_left_in_iteration = time_per_iteration;

		iterating = false;
	}


	public void AddActiveRobot(RobotController robot)
	{
		StartCoroutine(AddRobot(robot));
	}
	IEnumerator AddRobot(RobotController robot)
	{
		while (iterating)
		{
			yield return new WaitForSeconds(0.1f);
		}
		if (!active_robots.Contains(robot))
			active_robots.Add(robot);
	}
	public void RemoveActiveRobot(RobotController robot)
	{
		StartCoroutine(RemoveRobot(robot));
	}
	IEnumerator RemoveRobot(RobotController robot)
	{
		while (iterating)
		{
			yield return new WaitForSeconds(0.1f);
		}
		active_robots.Remove(robot);
	}


	void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 1000, 1000), "" + time_left_in_iteration);
	}
}
