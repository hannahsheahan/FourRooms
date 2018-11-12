using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueInstructionsButtonScript : MonoBehaviour
{
    private DataController dataController;

    public AudioClip buttonClickSound;
    private AudioSource source;

    public string enterIDmessage;

    // ********************************************************************** //

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // ********************************************************************** //

    private void Start()
    {
        dataController = FindObjectOfType<DataController>(); // Fetch our single DataController
    }

    // ********************************************************************** //

    public void ContinueOnClick()
    {
        if (dataController.participantIDSet)  // the player has entered a name (this will avoid multiple datafiles with no participant ID number)
        {
            source.PlayOneShot(buttonClickSound, 1F);
            GameController.control.ShowInstructions(); // Continue to the instructions page 
        }

    }

}
