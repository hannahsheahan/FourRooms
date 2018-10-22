using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReminderStar : MonoBehaviour
{
	private Text wellDoneText;
	private Text timesUpText;
	private float startTime;
	private GameObject fps;
    private GameObject pas;
	private float endTime;
	private float entryTime;
	private float lag;
	private float timeLimit;
	private float directTime;
	private int n;


	void Start()
	{
		wellDoneText = GameObject.Find ("WellDoneText").GetComponent<Text>();
		timesUpText = GameObject.Find ("TimesUpText").GetComponent<Text>();
		wellDoneText.text = "";
		timesUpText.text = "";
		startTime = Time.time;
		endTime = -1f;
		entryTime = -1f;
        timeLimit = 45f;
        lag = 2f;

				fps = GameObject.Find ("FPSController");
        //pas = GameObject.Find("PlayAreaScripts");


		if (ReminderCount.Count == 5) {
			wellDoneText.text = "Section Completed!";
			fps.GetComponent<FirstPersonController>().enabled = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		entryTime = Time.time;
		endTime = Time.time + lag;
        GetComponent<MeshRenderer>().enabled = true;
    }

    void OnTriggerExit(Collider other)
	{
		entryTime = -1f;
		endTime = -1f;
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
	{
		if (entryTime > 0 & Time.time - entryTime >= lag)
		{
			wellDoneText.text = "You'll find money here!";
			fps.GetComponent<FirstPersonController>().enabled = false;
		}

		else if (entryTime < 0 & Time.time - startTime >= timeLimit)
		{
			timesUpText.text = "Times Up!";
			fps.GetComponent<FirstPersonController>().enabled = false;
		}

             if (endTime > 0 & Time.time - endTime >= 3f)
		{
            ReminderCount.Count++;

            using (StreamWriter writer =
                new StreamWriter(filepath.path + Participant.id + "reminder.txt", true))
            {
                writer.WriteLine(ReminderCount.Count);
                for (var i = 0; i < fps.GetComponent<TrackingScript>().getCoords().Count; i++)
                {
                    writer.WriteLine("{0}", fps.GetComponent<TrackingScript>().getCoords()[i]);
                }
                writer.WriteLine("Completed");
                writer.WriteLine("");
            }

            SceneManager.LoadScene("BlankReminder");
        }

		if (Time.time - startTime >= timeLimit + 3.5f)
		{
            using (StreamWriter writer =
                new StreamWriter(filepath.path + Participant.id + "reminder.txt", true))
            {
                writer.WriteLine(ReminderCount.Count);
                for (var i = 0; i < fps.GetComponent<TrackingScript>().getCoords().Count; i++)
                {
                    writer.WriteLine("{0}", fps.GetComponent<TrackingScript>().getCoords()[i]);
                }
                writer.WriteLine("Uncompleted");
                writer.WriteLine("");
            }

            SceneManager.LoadScene("BlankReminder");
		}
	}
}
