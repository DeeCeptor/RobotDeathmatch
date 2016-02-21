using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour 
{
	public static Timer timer;

	public Text timer_text;
	public float remaining_time;
	public int alive_humans = 0;
	public OpeningText big_text;
	bool game_over = false;
	AudioSource audio;

	void Awake () 
	{
		timer = this;
		audio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		remaining_time -= Time.deltaTime;

		if (!game_over)
			timer_text.text = getFormattedTime(remaining_time);
		else
			timer_text.text = "";
		
		if (remaining_time <= 0 && !game_over)
		{
			Debug.Log("Time is up, humans win!");
			big_text.StartText(new string[] { "HUMAN(S)", "SURVIVED" });
			GameOver();
		}
	}


	public void HumanDied()
	{
		alive_humans--;

		if (PlayerInformation.player_info.cur_modes == PlayerInformation.Modes.Kill_Humans)
		{
			if (alive_humans <= 0)
			{
				Debug.Log("All humans are dead");
				big_text.StartText(new string[] { "HUMANS", "ARE", "DEAD" });
				GameOver();
			}
		}
		else if (PlayerInformation.player_info.cur_modes == PlayerInformation.Modes.Protect_Human)
		{
			Debug.Log("A human has died");
			big_text.StartText(new string[] { "HUMAN", "IS", "DEAD" });
			GameOver();
		}
	}
	public void RegisterHuman()
	{
		alive_humans++;
	}

	public void GameOver()
	{
		Debug.Log("Game over");
		game_over = true;
		Invoke("ToScoreScreen", 2.0f);
		audio.Play();
	}


	public void ToScoreScreen()
	{
		Debug.Log("Loading score screen");
		SceneManager.LoadScene("Results");
	}


	public string getFormattedTime(float in_time)
	{
		int minutes = (int) ((in_time) / 60.0f);
		int seconds = (int) (in_time % 60);
		int milliseconds = (int) ((in_time - (minutes * 60) - seconds) * 100);
		return ("" + minutes).PadLeft(2, '0') + ":" + ("" + seconds).PadLeft(2, '0') + "." + ("" + milliseconds).PadLeft(2, '0');
	}
}
