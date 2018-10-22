using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RefreshStar1 : MonoBehaviour
{
	public Text wellDoneText;
	public Text timesUpText;
	private float startTime;
	private float endTime;
	private float entryTime;
	public Transform fpsCamera;
	private int trigger;
	private float lag;
	private float timeLimit;
	private string rewardText;


	void Start()
	{
		wellDoneText.text = "";
		timesUpText.text = "";
		rewardText = "Well Done! This is where money is found.";
		startTime = Time.time;
		endTime = -1f;
		entryTime = -1f;
		trigger = 0;
		timeLimit = 60f;
		lag = 0f;
	}

	void OnTriggerEnter(Collider other)
	{		
		entryTime = Time.time;
		endTime = Time.time + lag;
		trigger = 1;
		GameObject.Find("RealStar").GetComponent<MeshRenderer>().enabled = true;
		GameObject.Find("RealStar").GetComponent<Transform>().position = fpsCamera.position + 5*fpsCamera.forward + Vector3.up;
		GameObject.Find("RealStar").GetComponent<Transform>().rotation = fpsCamera.rotation;
	}

	void OnTriggerExit(Collider other)
	{
		entryTime = -1f;
		endTime = -1f;
		trigger = 0;
		GameObject.Find("RealStar").GetComponent<MeshRenderer>().enabled = false;
	}

	void Update()
	{
		if (trigger == 1) 
		{
			GameObject.Find("RealStar").GetComponent<Transform>().position = fpsCamera.position + 5*fpsCamera.forward + Vector3.up;
			GameObject.Find("RealStar").GetComponent<Transform>().rotation = fpsCamera.rotation;
		}

		if (entryTime > 0 & Time.time - entryTime >= lag) 
		{
			wellDoneText.text = rewardText;
			GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
		}

		else if (Time.time - startTime >= timeLimit) 
		{
			timesUpText.text = "Times Up!";
			GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
		}

		if (endTime > 0 & Time.time - endTime >= 3f) 
		{
			using (StreamWriter writer =
				new StreamWriter(filepath.path + Participant.id +"test1.txt",true))
			{
				writer.WriteLine("Refresher");
				for (var i = 0; i < GameObject.Find("FPSController").GetComponent<TrackingScript>().getCoords().Count; i++)
				{
					writer.WriteLine("{0}", GameObject.Find("FPSController").GetComponent<TrackingScript>().getCoords()[i]);
				}
				writer.WriteLine("Completed");
				writer.WriteLine ("");
			}
			SceneManager.LoadScene ("hold01r");
		}

		if (Time.time - startTime >= timeLimit + 3.5f) 
		{
			using (StreamWriter writer =
				new StreamWriter(filepath.path + Participant.id +"test1.txt",true))
			{
				writer.WriteLine("Refresher");
				for (var i = 0; i < GameObject.Find("FPSController").GetComponent<TrackingScript>().getCoords().Count; i++)
				{
					writer.WriteLine("{0}", GameObject.Find("FPSController").GetComponent<TrackingScript>().getCoords()[i]);
				}
				writer.WriteLine("Uncompleted");
				writer.WriteLine ("");
			}
			SceneManager.LoadScene ("Refresh1");
		}

	}
}