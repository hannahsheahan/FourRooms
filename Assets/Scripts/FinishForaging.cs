using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishForaging : MonoBehaviour
{
	private Text wellDoneText;
	private Text timesUpText;
	private float startTime;
	private GameObject fps;
    private GameObject playAreaScripts;
	private float lag;
	private float timeLimit;
	private float angle;

	void Start()
	{
		wellDoneText = GameObject.Find ("WellDoneText").GetComponent<Text>();
		timesUpText = GameObject.Find ("TimesUpText").GetComponent<Text>();
		timesUpText.text = "";
		startTime = Time.time;
		fps = GameObject.Find ("FPSController");
        timeLimit = 60f;
	}

	void Update()
	{
		if (Time.time - startTime >= timeLimit)
		{
			wellDoneText.text = "";
			timesUpText.text = "Times Up!";
			fps.GetComponent<FirstPersonController>().enabled = false;
		}

		if (Time.time - startTime >= timeLimit + 3.5f)
		{

			using (StreamWriter writer =
				new StreamWriter(filepath.path + Participant.id +"training.txt",true))
			{
				writer.WriteLine("Foraging");
				for (var i = 0; i < fps.GetComponent<TrackingScript>().getCoords().Count; i++)
				{
					writer.WriteLine("{0}", fps.GetComponent<TrackingScript>().getCoords()[i]);
				}
				writer.WriteLine ("");
			}

			TrainCount.ring = 1;

			SceneManager.LoadScene ("training");
		}
	}
}
