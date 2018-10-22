using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class StartScreenReminder : MonoBehaviour
{
    public string ID = "";
    private string levelstrng = "1";
    private int levl;
    void OnGUI()
    {
        GUI.Box(new Rect(80, 60, 300, 250), "Start Screen");

        GUI.Label(new Rect(90, 90, 200, 20), "Participant ID");

        ID = GUI.TextField(new Rect(90, 115, 200, 20), ID, 20);

        if (GUI.Button(new Rect(200, 250, 50, 25), "Start") || Input.GetKeyDown("return"))
        {
            Participant.id = ID;
            ReminderCount.Count = 1;

            using (StreamWriter writer =
                new StreamWriter(filepath.path + Participant.id + "reminder.txt", true))
            {
                writer.WriteLine("Participant: " + ID);
                writer.WriteLine("");
            }

            SceneManager.LoadScene("Reminder");
        }
    }
}
