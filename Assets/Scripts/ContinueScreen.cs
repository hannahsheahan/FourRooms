using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class ContinueScreen : MonoBehaviour
{
	void OnGUI()
	{
		GUI.Box (new Rect(80, 60, 300, 250), "Ready To Continue?");

		if (GUI.Button (new Rect (200, 250, 50, 25), "Yes") || Input.GetKeyDown("return")) 
		{
            GameController.control.SceneContinue();  // Continue by playing the current scene again
        }
	}
}
