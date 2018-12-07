using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentRevealScript : MonoBehaviour {

    public GameObject present;
    public Text OpenPresentMessage;
    public int presentIndex;

    // Define the indices for the different presents
    //private const int GREEN1  = 0;
    //private const int GREEN2  = 1;
    //private const int RED1    = 2;
    //private const int RED2    = 3;
    //private const int YELLOW1 = 4;
    //private const int YELLOW2 = 5;
    //private const int BLUE1   = 6;
    //private const int BLUE2   = 7;

    // ********************************************************************** //

    void Start()
    {
        // Load the present spawn location from the configured datafile
        transform.position = GameController.control.presentPositions[presentIndex];
    }

    // ********************************************************************** //

    private void OnTriggerEnter(Collider other)
    {
        OpenPresentMessage.text = "Press space-bar to open the present";
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameController.control.OpenBox();
            OpenPresentMessage.text = "";
            present.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OpenPresentMessage.text = "";
    }

}
