using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    private DataController dataController;

    public AudioClip buttonClickSound;
    private AudioSource source;

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

    public void StartGameOnClick()
    {
        //dataController.SetParticipantID(ID);  // Send participant data to the DataController
        source.PlayOneShot(buttonClickSound,1F);
        GameController.control.StartGame();   // Launch first trial
    }

}
