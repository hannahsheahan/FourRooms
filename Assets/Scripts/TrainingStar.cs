using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrainingStar : MonoBehaviour
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
	public Material gold1;
	public Material gold2;
	public Material gold3;
	public Material gold4;
	private Material rend;

	void Start()
	{
		wellDoneText = GameObject.Find ("WellDoneText").GetComponent<Text>();
		timesUpText = GameObject.Find ("TimesUpText").GetComponent<Text>();
		wellDoneText.text = "";
		timesUpText.text = "";
		startTime = Time.time;
		endTime = -1f;
		entryTime = -1f;
		fps = GameObject.Find ("FPSController");
    //    pas = GameObject.Find("PlayAreaScripts");


        if (TrainCount.ring == 1) {
			rend = gold1;
			lag = 1f;
			directTime = 10f;
			timeLimit = 45f;
			GetComponent<Renderer> ().material = rend;
		} else if (TrainCount.ring == 2) {
			rend = gold2;
			lag = 1f;
			directTime = 10f;
			timeLimit = 45f;
			GetComponent<Renderer> ().material = rend;
		} else if (TrainCount.ring == 3) {
			lag = 1f;
			rend = gold3;
			directTime = 15f;
			timeLimit = 30f;
			GetComponent<Renderer> ().material = rend;
		} else if (TrainCount.ring == 4) {
			lag = 2f;
			rend = gold3;
			directTime = 20f;
			timeLimit = 30f;
			GetComponent<Renderer> ().material = rend;
		} else if (TrainCount.ring == 5) {
			lag = 2f;
            rend = gold1;
            GetComponent<MeshRenderer>().enabled = false;
			directTime = 45f;
			timeLimit = 45f;
			GetComponent<Renderer> ().material = rend;
		} else if (TrainCount.ring == 6) {
			lag = 2f;
            rend = gold1;
            GetComponent<MeshRenderer>().enabled = false;
            directTime = 45f;
			timeLimit = 45f;
			GetComponent<Renderer> ().material = rend;
		}

		n = TrainCount.trainCount - 1;
		if (TrainCount.ring >= 5) {
			if ((TrainCount.terminate [n] + TrainCount.terminate [n - 1] + TrainCount.terminate [n - 2] + TrainCount.terminate [n - 3] + TrainCount.terminate [n - 4]) / 5 >= 0.8f) {
				wellDoneText.text = "Section Completed!";
				fps.GetComponent<FirstPersonController>().enabled = false;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		entryTime = Time.time;
		endTime = Time.time + lag;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Renderer>().material = gold1;
    }

    void OnTriggerExit(Collider other)
	{
		entryTime = -1f;
		endTime = -1f;
        GetComponent<Renderer>().material = rend;
        if (TrainCount.ring >= 5)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
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

		if (endTime > 0 & Time.time - endTime >= 3f & entryTime - startTime < directTime)
		{
			if (TrainCount.ring >= 5) {
				TrainCount.terminate.Add (1f);
			} else {
				TrainCount.terminate.Add (0f);
			}

			TrainCount.directCount++;
			TrainCount.trainCount++;

			using (StreamWriter writer =
				new StreamWriter(filepath.path + Participant.id +"training.txt",true))
			{
				writer.WriteLine(TrainCount.trainCount + " " + TrainCount.ring);
				for (var i = 0; i < fps.GetComponent<TrackingScript>().getCoords().Count; i++)
				{
					writer.WriteLine("{0}", fps.GetComponent<TrackingScript>().getCoords()[i]);
				}
				writer.WriteLine("Completed and Direct");
				writer.WriteLine ("");
			}

			if (TrainCount.directCount >= 2 && TrainCount.ring < 6)
			{
				TrainCount.ring++;
				TrainCount.directCount = 0;
			}

            if ((TrainCount.trainCount + 1) % 10 == 0)
            {
                SceneManager.LoadScene("ContinueScreenTraining");
            }
            else
            {
                SceneManager.LoadScene("Blank");
            }
        }
		else if (endTime > 0 & Time.time - endTime >= 3f)
		{
			TrainCount.terminate.Add (0f);
			TrainCount.directCount = 0;
			TrainCount.trainCount++;

			using (StreamWriter writer =
				new StreamWriter(filepath.path + Participant.id +"training.txt",true))
			{
				writer.WriteLine(TrainCount.trainCount + " " + TrainCount.ring);
				for (var i = 0; i < fps.GetComponent<TrackingScript>().getCoords().Count; i++)
				{
					writer.WriteLine("{0}", fps.GetComponent<TrackingScript>().getCoords()[i]);
				}
				writer.WriteLine("Completed");
				writer.WriteLine ("");
			}

			if ((TrainCount.trainCount+1) % 10 == 0){
				SceneManager.LoadScene ("ContinueScreenTraining");
			} else {
				SceneManager.LoadScene ("Blank");
			}
		}

		if (Time.time - startTime >= timeLimit + 3.5f)
		{
			TrainCount.terminate.Add (0f);
			TrainCount.directCount = 0;
			TrainCount.trainCount++;

			using (StreamWriter writer =
				new StreamWriter(filepath.path + Participant.id +"training.txt",true))
			{
				writer.WriteLine(TrainCount.trainCount + " " + TrainCount.ring);
				for (var i = 0; i < fps.GetComponent<TrackingScript>().getCoords().Count; i++)
				{
					writer.WriteLine("{0}", fps.GetComponent<TrackingScript>().getCoords()[i]);
				}
				writer.WriteLine("Uncompleted");
				writer.WriteLine ("");
			}

			if (TrainCount.ring > 1)
			{
				TrainCount.ring--;
			}

			if ((TrainCount.trainCount+1) % 10 == 0){
				SceneManager.LoadScene ("ContinueScreenTraining");
			} else {
				SceneManager.LoadScene ("Blank");
			}
		}
	}
}
