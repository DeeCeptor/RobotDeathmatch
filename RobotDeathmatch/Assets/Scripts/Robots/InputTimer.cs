using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputTimer : MonoBehaviour 
{
	public float time_left_in_iteration;
	public float time_per_iteration = 3.0f;	// How long the players have to key input

	private List<RobotController> active_robots = new List<RobotController>();
	bool iterating = false;

	void Start () 
	{
		time_left_in_iteration = time_per_iteration;
	}
	

	void FixedUpdate () 
	{
		time_left_in_iteration -= Time.deltaTime;

		if (time_left_in_iteration <= 0)
		{
			EnterInput();
		}
	}
	public void EnterInput()
	{
		Debug.Log("Entering input");
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
