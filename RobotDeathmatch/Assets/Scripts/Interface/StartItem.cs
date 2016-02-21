using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartItem : MonoBehaviour {

	public Sprite deselected;
	public Sprite selected;

	Image render;
	// Use this for initialization
	void Start () {
		render = GetComponent<Image> ();
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
