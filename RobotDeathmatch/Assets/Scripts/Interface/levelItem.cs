using UnityEngine;
using System.Collections;

public class levelItem : MonoBehaviour {

	public Sprite deselected;
	public Sprite selected;

	SpriteRenderer render;
	// Use this for initialization
	void Start () {
		render = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Select(){
		render.sprite = selected;
	}

	public void Deselect(){
		render.sprite = deselected;
	}
}
