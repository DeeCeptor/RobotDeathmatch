using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour 
{
	public float time_to_live = 1.0f;
	
	void Update () 
	{
		time_to_live -= Time.deltaTime;
	}
}
