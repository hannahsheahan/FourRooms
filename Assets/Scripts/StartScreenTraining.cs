using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class StartScreenTraining : MonoBehaviour
{
	public string ID = "";
    private string levelstrng = "1";
    private int levl;
	void OnGUI()
	{
		GUI.Box (new Rect(80, 60, 300, 250), "Start Screen");

		GUI.Label (new Rect (90, 90, 200, 20), "Participant ID");

		ID = GUI.TextField (new Rect (90, 115, 200, 20), ID, 20);

        GUI.Label(new Rect(90, 140, 200, 20), "Level: (1-6)");

        levelstrng = GUI.TextField(new Rect(90, 165, 200, 20), levelstrng, 20);

        if (GUI.Button (new Rect (200, 250, 50, 25), "Start") || Input.GetKeyDown("return")) 
		{
            levl = Convert.ToInt32(levelstrng);

			Participant.id = ID;
			TrainCount.trainCount = 0;
			TrainCount.directCount = 0;
			TrainCount.ring = levl;

			using (StreamWriter writer =
				new StreamWriter(filepath.path + Participant.id +"training.txt",true))
			{
				writer.WriteLine("Participant: " + ID);
				writer.WriteLine ("");
			}
            if (levl == 1)
            {
                SceneManager.LoadScene("Foraging");
            } else
            {
                SceneManager.LoadScene("training");
            }
		}

	}

}
