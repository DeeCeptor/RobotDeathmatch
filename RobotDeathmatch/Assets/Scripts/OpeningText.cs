using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class OpeningText : MonoBehaviour 
{
	public string[] statements = new string[] 
	{ "KILL", "ALL", "HUMANS" };
	float statement_duration = 1f;
	public Text statement_text;
	float start_time;

	void Start () 
	{
		StartText();
	}


	public void StartText()
	{
		if (PlayerInformation.player_info != null 
			&& PlayerInformation.player_info.cur_modes == PlayerInformation.Modes.Protect_Human)
			statements = new string[] { "PROTECT", "YOUR", "HUMAN" };

		start_time = Time.time;
		statement_text.gameObject.SetActive(true);
	}
	public void StartText(string[] new_statements)
	{
		statements = new_statements;
		statement_text.text = new_statements[0];
		StartText();
	}


	void Update () 
	{
		int statement_num = (int) ((Time.time - start_time)/ statement_duration);

		if (statement_num >= statements.Length)
			statement_text.gameObject.SetActive(false);
		else
			statement_text.text = statements[statement_num];
	}
}
