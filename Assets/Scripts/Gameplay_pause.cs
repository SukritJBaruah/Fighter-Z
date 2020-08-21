using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Gameplay_pause : MonoBehaviour
{
	void Start()
    {
	}

    void Update()
	{
		// check for pausing game
		if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
		{
			MenuManager.GoToMenu(MenuName.Pause);
		}
	}
}
