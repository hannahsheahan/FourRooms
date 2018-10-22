using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
	public string ID = "";
    private string lvlstring = "1";
    private int lvl;

	void OnGUI()
	{
		GUI.Box (new Rect(80, 60, 300, 250), "Start Screen");

		GUI.Label (new Rect (90, 90, 200, 20), "Participant ID");

		ID = GUI.TextField (new Rect (90, 115, 200, 20), ID, 20);

        GUI.Label(new Rect(90, 140, 200, 20), "Level");

        lvlstring = GUI.TextField(new Rect(90, 165, 200, 20), lvlstring, 20);

        if (GUI.Button (new Rect (200, 250, 50, 25), "Start") || Input.GetKeyDown("return")) 
		{
			Participant.id = ID;
			using (StreamWriter writer =
				new StreamWriter(filepath.path + Participant.id +"Testing.txt",true))
			{
				writer.WriteLine("Participant: " + ID);
				writer.WriteLine ("");
			}

            lvl = Convert.ToInt32(lvlstring)-1;

			LevelManager.trial = 0;
			LevelManager.level = lvl;

			SceneManager.LoadScene ("tartarus" + (LevelManager.level + 1));
		}

	}

}
