using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Retrieves and displays high score and listens for
/// the OnClick events for the high score menu button
/// </summary>
public class HelpMenu : MonoBehaviour
{

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
		// pause the game when added to the scene
		Time.timeScale = 0;
	}

	/// <summary>
	/// Handles the on click event from the quit button
	/// </summary>
	public void HandleQuitButtonOnClickEvent()
	{

		// unpause game and go to main menu
		Time.timeScale = 1;
		MenuManager.GoToMenu(MenuName.Main);
	}
}
