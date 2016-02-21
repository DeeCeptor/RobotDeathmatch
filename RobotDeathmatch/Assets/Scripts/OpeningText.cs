using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class OpeningText : MonoBehaviour 
{
	public string[] statements = new string[] 
	{ "KILL", "ALL", "HUMANS" };
	float statement_duration = 0.5f;
	public Text statement_text;


	void Start () 
	{
		
	}


	void Update () 
	{
		int statement_num = (int) (Time.time / statement_duration);

		if (statement_num >= statements.Length)
			this.gameObject.SetActive(false);
		else
			statement_text.text = statements[statement_num];
	}
}
