﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Timer : MonoBehaviour 
{
	public Text timer_text;
	public float remaining_time;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		remaining_time -= Time.deltaTime;
		timer_text.text = getFormattedTime(remaining_time);

		if (remaining_time <= 0)
		{
			Debug.Log("Time is up");
			GameOver();
		}
	}


	public void GameOver()
	{
		Debug.Log("Game over");
	}


	public string getFormattedTime(float in_time)
	{
		int minutes = (int) ((in_time) / 60.0f);
		int seconds = (int) (in_time % 60);
		int milliseconds = (int) ((in_time - (minutes * 60) - seconds) * 100);
		return ("" + minutes).PadLeft(2, '0') + ":" + ("" + seconds).PadLeft(2, '0') + "." + ("" + milliseconds).PadLeft(2, '0');
	}
}