using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllScores : MonoBehaviour 
{
	public static AllScores all_scores;

	// Accessed by player_name string
	public Dictionary<string, IndividualScore> scores = new Dictionary<string, IndividualScore>();

	void Awake () 
	{
		all_scores = this;
	}
	

	void Update () 
	{
	
	}
}
