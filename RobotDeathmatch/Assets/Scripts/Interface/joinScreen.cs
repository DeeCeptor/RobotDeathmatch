using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class joinScreen : MonoBehaviour {

	bool p1;
	bool p2;
	bool p3;
	bool p4;

	public GameObject p1Check;
	public GameObject p2Check;
	public GameObject p3Check;
	public GameObject p4Check;

	Player player1;
	Player player2;
	Player player3;
	Player player4;

	public Text p1text;
	public Text p2text;
	public Text p3text;
	public Text p4text;


	public Text startText;

	void Start () 
	{
		startText.enabled = false;
		p1Check.SetActive (false);
		p2Check.SetActive (false);
		p3Check.SetActive (false);
		p4Check.SetActive (false);
	}
	

	void Update () 
	{
		if (Input.GetButtonDown ("Cancel")) {
			SceneManager.LoadScene ("StartMenu");
		}

		if (Input.GetButtonDown ("P1 A")) 
		{
			if(!p1)
			{
				p1 = true;
				p1Check.SetActive (true);
				player1 = new Player(1, Color.white, false);
				PlayerInformation.player_info.players.Insert(0, player1);
				p1text.text = GetCharacterTypeText(player1.robot, 1);
			}
		}
		if (Input.GetButtonDown("P1 Y") && p1)
		{
			// Player has already joined, switch human/robot status
			player1.robot = !player1.robot;
			p1text.text = GetCharacterTypeText(player1.robot, 1);
		}

		if (Input.GetButtonDown ("P2 A")) 
		{
			if(!p2)
			{
				p2 = true;
				p2Check.SetActive (true);
				player2 = new Player(2, Color.red, true);
				PlayerInformation.player_info.players.Add(player2);
				p2text.text = GetCharacterTypeText(player2.robot, 2);
			}
		}
		if (Input.GetButtonDown("P2 Y") && p2)
		{
			// Player has already joined, switch human/robot status
			player2.robot = !player2.robot;
			p2text.text = GetCharacterTypeText(player2.robot, 2);
		}

		if (Input.GetButtonDown ("P3 A")) 
		{
			if(!p3)
			{
				p3 = true;
				p3Check.SetActive (true);
				player3 = new Player(3, Color.green, true);
				PlayerInformation.player_info.players.Add(player3);
				p3text.text = GetCharacterTypeText(player3.robot, 3);
			}
		}
		if (Input.GetButtonDown("P3 Y") && p3)
		{
			// Player has already joined, switch human/robot status
			player3.robot = !player3.robot;
			p3text.text = GetCharacterTypeText(player3.robot, 3);
		}

		if (Input.GetButtonDown ("P3 A")) 
		{
			if(!p4)
			{
				p4 = true;
				p4Check.SetActive (true);
				player4 = new Player(4, Color.blue, true);
				PlayerInformation.player_info.players.Add(player4);
				p4text.text = GetCharacterTypeText(player4.robot, 4);
			}
		}
		if (Input.GetButtonDown("P4 Y") && p3)
		{
			// Player has already joined, switch human/robot status
			player4.robot = !player4.robot;
			p4text.text = GetCharacterTypeText(player4.robot, 4);
		}

		if (p1 && (p2 || p3 || p4)) {
			startText.enabled = true;
		}

		if (startText.enabled && Input.GetButtonDown("Submit")) 
		{
			Debug.Log ("Submitted");
			SceneManager.LoadScene("LevelSelect");
		}
	}


	public string GetCharacterTypeText(bool is_robot, int player_num)
	{
		if (is_robot)
			return "Player " + player_num + "      Robot";
		else
			return "Player " + player_num + "      Human";
	}
}
