using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listens for the OnClick events for the main menu buttons
/// </summary>
public class MainMenu : MonoBehaviour
{
	/// <summary>
	/// Handles the on click event from the play button
	/// </summary>
	public void HandlePlayButtonOnClickEvent()
	{
		MenuManager.GoToMenu(MenuName.Difficulty);
	}

	/// <summary>
	/// Handles the on click event from the high score button
	/// </summary>
	public void HandleHelpButtonOnClickEvent()
	{
		MenuManager.GoToMenu(MenuName.Help);
	}

	/// <summary>
	/// Handles the on click event from the quit button
	/// </summary>
	public void HandleQuitButtonOnClickEvent()
	{
		Application.Quit();
	}
}
