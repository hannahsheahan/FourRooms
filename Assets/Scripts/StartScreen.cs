using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    private DataController dataController;
   
    public string ID = "";
    public string mapString = "1";
    public int mapIndex;

    // ********************************************************************** //

    private void Start()
    {
        dataController = FindObjectOfType<DataController>(); // Fetch our single DataController
    }

    // ********************************************************************** //

    void OnGUI()
	{
		// Display some boxes and input areas
        GUI.Box (new Rect(80, 60, 300, 250), "Welcome");

		GUI.Label (new Rect (90, 90, 200, 20), "Participant ID");
		ID = GUI.TextField (new Rect (90, 115, 200, 20), ID, 20);

        GUI.Label(new Rect(90, 140, 200, 20), "Which maze?");
        mapString = GUI.TextField(new Rect(90, 165, 200, 20), mapString, 20);
        mapIndex = Convert.ToInt32(mapString) - 1;

        // Store input and start game when ready
        if (GUI.Button (new Rect (200, 250, 50, 25), "Start") || Input.GetKeyDown("return")) 
		{
            dataController.SetParticipantID(ID);  // Send participant data to the DataController
            GameController.control.NextScene("tartarus" + (mapIndex + 1)); // Launch scene
        }
	}
}
