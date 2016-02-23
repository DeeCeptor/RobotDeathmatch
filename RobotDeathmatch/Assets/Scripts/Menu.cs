using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

	public Canvas QuitMenu;
	public Button PlayButton;
	public Button ExitButton;

	void Start()
	{
		QuitMenu = QuitMenu.GetComponent<Canvas> ();
		PlayButton = PlayButton.GetComponent<Button> ();
		ExitButton = ExitButton.GetComponent<Button> ();
		QuitMenu.enabled = false;
	}

	public void ExitPress()
	{
		QuitMenu.enabled = true;
		PlayButton.enabled = false;
		ExitButton.enabled = false;
        ExitGame();
	}

	public void NoPress()
	{
		QuitMenu.enabled = false;
		PlayButton.enabled = true;
		ExitButton.enabled = true;
	}

	public void StartLevel()
	{
		Application.LoadLevel (1);
	}

	public void ExitGame()
	{
		Application.Quit ();
	}

}
