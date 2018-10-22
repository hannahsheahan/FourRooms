using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ForagingStar : MonoBehaviour
{
	private Text wellDoneText;
	private Text timesUpText;
	private float entryTime;
	private float lag;
	private float angle;

	void Start()
	{
		wellDoneText = GameObject.Find ("WellDoneText").GetComponent<Text>();
		wellDoneText.text = "";
		entryTime = -1f;
		lag = 2.5f;
		angle = Random.Range (0f, 360f);
		transform.eulerAngles = new Vector3 (0f, angle, 0f);
	}

	void OnTriggerEnter(Collider other)
	{		
		entryTime = Time.time;
	}

	void OnTriggerExit(Collider other)
	{
		entryTime = -1f;
	}

	void Update()
	{
		if (entryTime > 0 & Time.time - entryTime >= lag) 
		{
			wellDoneText.text = "Reward!";
			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<Collider> ().enabled = false;
		}

		if (entryTime > 0 & Time.time - entryTime >= lag + 1f) 
		{
			wellDoneText.text = "";
			GetComponent<ForagingStar> ().enabled = false;
		}
			
	}
}