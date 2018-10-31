using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;


public class HideStar : MonoBehaviour
{
	private Text wellDoneText;
	private Text timesUpText;
	private float startTime;
	private float endTime;
	private float entryTime;
	private GameObject fps;
    private GameObject pas;

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
    }

    void OnTriggerEnter(Collider other)
	{
		entryTime = Time.time;
		endTime = Time.time + 2f;
		GetComponent<MeshRenderer> ().enabled = true;
	}

	void OnTriggerExit(Collider other)
	{
		entryTime = -1f;
		endTime = -1f;
		GetComponent<MeshRenderer>().enabled = false;
	}

	void Update()
	{
		if (entryTime > 0 & Time.time - entryTime >= 2f)
		{
			wellDoneText.text = "Well Done!";
			fps.GetComponent<FirstPersonController>().enabled = false;
		}

		else if (entryTime < 0 & Time.time - startTime >= 45f)
		{
			timesUpText.text = "Times Up!";
			fps.GetComponent<FirstPersonController>().enabled = false;
        }

        if (endTime > 0 & Time.time - endTime >= 2f)
		{
			using (StreamWriter writer = new StreamWriter(filepath.path + Participant.id +"Testing.txt",true))
			{
				for (var i = 0; i < fps.GetComponent<TrackingScript>().getCoords().Count; i++)
				{
					writer.WriteLine("{0}", fps.GetComponent<TrackingScript>().getCoords()[i]);
				}
				writer.WriteLine("YES");
			}
		}

		else if (endTime < 0 & Time.time - startTime >= 49f)
		{
			using (StreamWriter writer = new StreamWriter (filepath.path + Participant.id + "Testing.txt", true)) 
            {
				for (var i = 0; i < fps.GetComponent<TrackingScript> ().getCoords ().Count; i++) 
                {
					writer.WriteLine ("{0}", fps.GetComponent<TrackingScript> ().getCoords () [i]);
				}
				writer.WriteLine ("NO");
			}
   		}
	}
}
