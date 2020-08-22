using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Listens for the OnClick events for the difficulty menu buttons
/// </summary>
public class DifficultyMenu : MonoBehaviour
{
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
		// add event component and add invoker to event manager
	}

	/// <summary>
	/// Handles the on click event from the easy button
	/// </summary>
	public void HandleEasyButtonOnClickEvent()
	{
		//ADD AI values to low
		GameObject.FindGameObjectWithTag("DifficultyUtils").GetComponent<DifficultyUtils>().setDifficulty(1);
		//end add here
		MenuManager.GoToMenu(MenuName.Game);
	}

	/// <summary>
	/// Handles the on click event from the medium button
	/// </summary>
	public void HandleMediumButtonOnClickEvent()
	{
		//ADD AI values to medium
		GameObject.FindGameObjectWithTag("DifficultyUtils").GetComponent<DifficultyUtils>().setDifficulty(2);
		//end add here
		MenuManager.GoToMenu(MenuName.Game);

	}

	/// <summary>
	/// Handles the on click event from the hard button
	/// </summary>
	public void HandleHardButtonOnClickEvent()
	{
		//ADD AI values to high
		GameObject.FindGameObjectWithTag("DifficultyUtils").GetComponent<DifficultyUtils>().setDifficulty(3);
		//end add here
		MenuManager.GoToMenu(MenuName.Game);
	}
}
